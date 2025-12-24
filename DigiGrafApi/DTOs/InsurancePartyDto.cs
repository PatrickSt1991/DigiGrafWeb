namespace DigiGrafWeb.DTOs
{
    public class InsurancePartyDto
    {
        public Guid? Id { get; set; }

        // Core
        public string Name { get; set; } = "";
        public bool IsActive { get; set; } = true;

        // Type flags
        public bool IsInsurance { get; set; }
        public bool IsAssociation { get; set; }
        public bool HasMembership { get; set; }
        public bool HasPackage { get; set; }
        public bool IsHerkomst { get; set; }

        // Correspondence
        // "address" | "mailbox"
        public string CorrespondenceType { get; set; } = "address";

        public string Address { get; set; } = "";
        public string HouseNumber { get; set; } = "";
        public string HouseNumberSuffix { get; set; } = "";
        public string PostalCode { get; set; } = "";
        public string City { get; set; } = "";
        public string Country { get; set; } = "";
        public string Phone { get; set; } = "";

        // Mailbox
        public string MailboxName { get; set; } = "";
        public string MailboxAddress { get; set; } = "";

        // Billing
        // "Opdrachtgever" | "Opdrachtgever & Derde partij" | "Derde partij"
        public string BillingType { get; set; } = "Opdrachtgever";
    }
}
