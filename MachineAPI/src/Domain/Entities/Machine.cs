namespace MachineAPI.Domain.Entities
{
    public class Machine
    {   
        public required int Id { get; set; }
        public int SerialNumber { get; set; } = 0; 
        public required string Name { get; set; }
        public string Model { get; set; } = string.Empty; 
        public DateTime ManufactureDate { get; set; } = DateTime.MinValue; 

        // Category entity n:1 relation
        public int CategoryId { get; set; } = 0; 
        public Category? Category { get; set; }


        // Location entity n:1 relation
        public int LocationId { get; set; } = 0; 
        public Location? Location { get; set; }


    }
}