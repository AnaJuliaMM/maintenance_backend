namespace MachineAPI.Domain.Entities
{
    public class Category
    {
        public required int Id { get; set; }
        public required string Name { get; set; }

        // A Categry have a list of machines
        public List<Machine> Machines { get; set; } = new List<Machine>();
    }
}