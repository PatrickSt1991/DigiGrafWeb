using System;

namespace DigiGrafWeb.DTOs
{
    public class DeathInfoDto
    {
        public Guid Id { get; set; }
        public Guid DossierId { get; set; }
        public DateTime? DateOfDeath { get; set; }
        public TimeSpan? TimeOfDeath { get; set; }
        public string LocationOfDeath { get; set; } = "";
        public string PostalCodeOfDeath { get; set; } = "";
        public string HouseNumberOfDeath { get; set; } = "";
        public string HouseNumberAdditionOfDeath { get; set; } = "";
        public string StreetOfDeath { get; set; } = "";
        public string CityOfDeath { get; set; } = "";
        public string CountyOfDeath { get; set; } = "";
        public string BodyFinding { get; set; } = "";
        public string Origin { get; set; } = "";
    }
}
