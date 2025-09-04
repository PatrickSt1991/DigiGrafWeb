using DigiGrafWeb.Data;
using DigiGrafWeb.DTOs;
using DigiGrafWeb.Mappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ClosedXML.Excel;


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

        // POST: api/invoice
        [HttpPost]
        public async Task<ActionResult<InvoiceDto>> SaveInvoice([FromBody] InvoiceDto invoiceDto)
        {
            var invoice = InvoiceMapper.ToEntity(invoiceDto);

            var existingInvoice = await _context.Invoices
                .Include(i => i.PriceComponents)
                .FirstOrDefaultAsync(i => i.Id == invoice.Id);

            if (existingInvoice != null)
            {
                // Update existing invoice
                existingInvoice.SelectedVerzekeraar = invoice.SelectedVerzekeraar;
                existingInvoice.DiscountAmount = invoice.DiscountAmount;
                existingInvoice.Subtotal = invoice.Subtotal;
                existingInvoice.Total = invoice.Total;

                // Replace price components
                _context.PriceComponents.RemoveRange(existingInvoice.PriceComponents);
                existingInvoice.PriceComponents = invoice.PriceComponents;

                await _context.SaveChangesAsync();
                return Ok(InvoiceMapper.ToDto(existingInvoice));
            }
            else
            {
                // Create new invoice
                _context.Invoices.Add(invoice);
                await _context.SaveChangesAsync();
                return Ok(InvoiceMapper.ToDto(invoice));
            }
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
    }
}
