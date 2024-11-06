namespace MachineAPI.Domain.Entities
{
    public class Machine
    {   
        public required int Id { get; set; }
        public int SerialNumber { get; set; } = 0; 
        public required string Name { get; set; }
        public string Model { get; set; } = string.Empty; 
        public DateTime ManufactureDate { get; set; } = DateTime.MinValue; 
        public int TypeId { get; set; } = 0; 
    }
}