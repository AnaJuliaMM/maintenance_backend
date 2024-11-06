namespace MachineAPI.API.DTOs
{
    public class MachineLocationDTO
    {

        public required int Id { get; set; }
        public required string Name { get; set; }
        public string Description { get; set; } = string.Empty;
    }
}