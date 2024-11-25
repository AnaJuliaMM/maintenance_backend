namespace MachineAPI.Domain.Entities
{
    public class Machine
    {
        public required int Id { get; set; }
        public int? SerialNumber { get; set; }
        public required string Name { get; set; }
        public string? Model { get; set; }
        public DateTime? ManufactureDate { get; set; }

        // Category entity n:1 relation
        public int? CategoryId { get; set; }
        public Category? Category { get; set; }

        // Location entity n:1 relation
        public int? LocationId { get; set; }
        public Location? Location { get; set; }
    }
}
