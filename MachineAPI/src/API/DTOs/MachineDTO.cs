namespace MachineAPI.API.DTOs
{
    public class MachineDTO
    {

        public required int Id { get; set; }
        public int? SerialNumber { get; set; } 
        public required string Name { get; set; }
        public string? Model { get; set; }
        public DateTime ManufactureDate { get; set; }
        public int? CategoryId { get; set; }
        public int? LocationId { get; set; }

    }
}