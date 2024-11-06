namespace MachineAPI.Domain.Entities
{
    public class Location
    {
        public required int Id { get; set; }
        public required string Name { get; set; }
        public string Description { get; set; } = string.Empty;

        // A location have a list of machines
        public List<Machine> Machines { get; set; } = new List<Machine>();

    }
}