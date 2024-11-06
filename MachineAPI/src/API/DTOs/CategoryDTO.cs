namespace MachineAPI.API.DTOs
{
    public class CategoryDTO
    {

        public required int Id { get; set; }
        public required string Label { get; set; } 
        public List<Machine> Machines { get; set; } = new List<MachineDTO>();
    }
}