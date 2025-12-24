namespace DigiGrafWeb.Models
{
    public enum CorrespondenceType
    {
        Address = 1,
        Mailbox = 2
    }

    public enum BillingType
    {
        Opdrachtgever = 1,
        OpdrachtgeverEnDerdePartij = 2,
        DerdePartij = 3
    }

    public class InsuranceParty
    {
        public Guid Id { get; set; }

        // Active / inactive
        public bool IsActive { get; set; } = true;

        // Identity
        public string Name { get; set; } = string.Empty;

        // Type flags
        public bool IsInsurance { get; set; }        // verzekeraar
        public bool IsAssociation { get; set; }      // vereniging
        public bool HasMembership { get; set; }
        public bool HasPackage { get; set; }
        public bool IsHerkomst { get; set; }

        // Correspondence
        public CorrespondenceType CorrespondenceType { get; set; } = CorrespondenceType.Address;

        public string Address { get; set; } = string.Empty;
        public string HouseNumber { get; set; } = string.Empty;
        public string HouseNumberSuffix { get; set; } = string.Empty;
        public string PostalCode { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;

        // Mailbox
        public string MailboxName { get; set; } = string.Empty;
        public string MailboxAddress { get; set; } = string.Empty;

        // Billing
        public BillingType BillingType { get; set; } = BillingType.Opdrachtgever;

        // Navigation
        public ICollection<InsurancePolicy> Policies { get; set; } = new List<InsurancePolicy>();
    }
}
