using ClosedXML.Excel;
using DigiGrafWeb.Data;
using DigiGrafWeb.DTOs;
using DigiGrafWeb.Mappers;
using DigiGrafWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace DigiGrafWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        private readonly AppDbContext _context;

        public InvoiceController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/invoice/{overledeneId}
        [HttpGet("{overledeneId:guid}")]
        public async Task<ActionResult<InvoiceDto>> GetInvoice(Guid overledeneId)
        {
            var invoice = await _context.Invoices
                .Include(i => i.PriceComponents)
                .FirstOrDefaultAsync(i => i.DeceasedId == overledeneId);

            if (invoice == null)
                return NotFound();

            return Ok(InvoiceMapper.ToDto(invoice));
        }

        [HttpPost]
        public async Task<ActionResult<InvoiceDto>> SaveInvoice([FromBody] InvoiceDto invoiceDto)
        {
            var invoice = InvoiceMapper.ToEntity(invoiceDto);

            var existingInvoice = await _context.Invoices
                .Include(i => i.PriceComponents)
                .FirstOrDefaultAsync(i => i.DeceasedId == invoice.DeceasedId);

            if (existingInvoice != null)
            {
                // Update core fields
                existingInvoice.SelectedVerzekeraar = invoice.SelectedVerzekeraar;
                existingInvoice.SelectedVerzekeraarId = invoice.SelectedVerzekeraarId;
                existingInvoice.DiscountAmount = invoice.DiscountAmount;

                // Apply insurer agreements
                if (invoice.SelectedVerzekeraarId != Guid.Empty)
                {
                    await ApplyInsurancePriceComponentsAsync(
                        existingInvoice,
                        invoice.SelectedVerzekeraarId
                    );
                }

                await _context.SaveChangesAsync();
                return Ok(InvoiceMapper.ToDto(existingInvoice));
            }
            else
            {
                // Create invoice
                invoice.Id = Guid.NewGuid();
                _context.Invoices.Add(invoice);
                await _context.SaveChangesAsync();

                // Apply insurer agreements AFTER invoice exists
                if (invoice.SelectedVerzekeraarId != Guid.Empty)
                {
                    await ApplyInsurancePriceComponentsAsync(
                        invoice,
                        invoice.SelectedVerzekeraarId
                    );

                    await _context.SaveChangesAsync();
                }

                return Ok(InvoiceMapper.ToDto(invoice));
            }
        }
        // GET: api/invoice/templates?insurancePartyId=guid
        [HttpGet("templates")]
        public async Task<ActionResult<List<PriceComponentDto>>> GetInsuranceTemplate(
            [FromQuery] Guid insurancePartyId)
        {
            if (insurancePartyId == Guid.Empty)
                return BadRequest("InsurancePartyId is required.");

            var agreements = await _context.InsurancePriceComponents
                .Where(x =>
                    x.InsurancePartyId == insurancePartyId &&
                    x.IsActive
                )
                .OrderBy(x => x.SortOrder)
                .ToListAsync();

            var result = agreements.Select(a => new PriceComponentDto
            {
                Id = Guid.Empty, // new invoice line
                Omschrijving = a.Omschrijving,
                Aantal = a.Aantal,
                Bedrag = a.Bedrag
            }).ToList();

            return Ok(result);
        }


        // POST: api/invoice/generate-excel
        [HttpPost("generate-excel")]
        public IActionResult GenerateExcel([FromBody] InvoiceDto invoiceDto)
        {
            var invoice = InvoiceMapper.ToEntity(invoiceDto);

            using var workbook = new XLWorkbook();
            var ws = workbook.Worksheets.Add("Invoice");

            // Headers
            ws.Cell(1, 1).Value = "Omschrijving";
            ws.Cell(1, 2).Value = "Aantal";
            ws.Cell(1, 3).Value = "Bedrag";
            ws.Row(1).Style.Font.Bold = true;

            // Data
            int row = 2;
            foreach (var item in invoice.PriceComponents)
            {
                ws.Cell(row, 1).Value = item.Omschrijving;
                ws.Cell(row, 2).Value = item.Aantal;
                ws.Cell(row, 3).Value = item.Bedrag;
                row++;
            }

            // Totals
            ws.Cell(row, 2).Value = "Subtotaal";
            ws.Cell(row, 3).Value = invoice.Subtotal;
            ws.Range(row, 2, row, 3).Style.Font.Bold = true;
            row++;

            ws.Cell(row, 2).Value = "Korting";
            ws.Cell(row, 3).Value = invoice.DiscountAmount;
            ws.Range(row, 2, row, 3).Style.Font.Bold = true;
            row++;

            ws.Cell(row, 2).Value = "Totaal";
            ws.Cell(row, 3).Value = invoice.Total;
            ws.Range(row, 2, row, 3).Style.Font.Bold = true;

            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            stream.Position = 0;

            var fileName = $"Invoice_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx";
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
        }
        private async Task ApplyInsurancePriceComponentsAsync(
            Invoice invoice,
            Guid insurancePartyId
        )
        {
            // Get insurer price agreements
            var agreements = await _context.InsurancePriceComponents
                .Where(x =>
                    x.InsurancePartyId == insurancePartyId &&
                    x.IsActive
                )
                .OrderBy(x => x.SortOrder)
                .ToListAsync();

            // Remove existing auto-generated invoice lines
            var existingLines = await _context.PriceComponents
                .Where(pc => pc.InvoiceId == invoice.Id)
                .ToListAsync();

            _context.PriceComponents.RemoveRange(existingLines);

            // Copy agreements → invoice lines
            var newLines = agreements.Select(a => new PriceComponent
            {
                Id = Guid.NewGuid(),
                InvoiceId = invoice.Id,
                Omschrijving = a.Omschrijving,
                Aantal = a.Aantal,
                Bedrag = a.Bedrag
            }).ToList();

            _context.PriceComponents.AddRange(newLines);

            // Recalculate totals
            invoice.Subtotal = newLines.Sum(x => x.Aantal * x.Bedrag);
            invoice.Total = invoice.Subtotal - invoice.DiscountAmount;
        }

    }
}
