namespace DigiGrafWeb.Interface
{
    public interface IGeneralEntity
    {
        Guid? Id { get; set; }
        string Code { get; set; }
        string Label { get; set; }
        string? Description { get; set; }
        bool IsActive { get; set; }
    }
}
