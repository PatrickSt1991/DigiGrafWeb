namespace DigiGrafWeb.Models
{
    public class DocumentTemplate
    {
        public Guid Id { get; set; }
        public Guid OverledeneId { get; set; }
        public string Title { get; set; }
        public string FieldsJson { get; set; }
    }
}
