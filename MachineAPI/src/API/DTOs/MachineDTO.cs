namespace MachineAPI.API.DTOs
{
    public class MachineDTO
    {

        public required int Id { get; set; }
        public int SerialNumber { get; set; } = 0; 
        public required string Name { get; set; }
        public string Model { get; set; }; 
        public DateTime ManufactureDate { get; set; }; 

        public Category? Category { get; set; }

        public Location? Location { get; set; }

    }
}