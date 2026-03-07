using DigiGrafWeb.Interface;

namespace DigiGrafWeb.Models
{
    public class Asbestemming : IGeneralEntity
    {
        public Guid? Id { get; set; }
        public string Code { get; set; }
        public string Label { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
    }
}
