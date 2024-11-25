namespace MachineAPI.API.DTOs
{
    public class MachineDTO
    {
        public int Id { get; set; }
        public int? SerialNumber { get; set; }
        public required string Name { get; set; }
        public string? Model { get; set; }
        public DateTime? ManufactureDate { get; set; }
        public CategoryDTO? Category { get; set; }
        public LocationDTO? Location { get; set; }
    }
}
