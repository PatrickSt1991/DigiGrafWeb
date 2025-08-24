using System;

namespace DigiGrafWeb.DTOs
{
    public class DeceasedDto
    {
        public Guid? Id { get; set; }
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public string Salutation { get; set; } = "";
        public DateTime? DOB { get; set; }
        public string PlaceOfBirth { get; set; } = "";
        public string PostalCode { get; set; } = "";
        public string Street { get; set; } = "";
        public string HouseNumber { get; set; } = "";
        public string? HouseNumberAddition { get; set; }
        public string City { get; set; } = "";
        public string County { get; set; } = "";
        public bool HomeDeceased { get; set; }
    }
}
