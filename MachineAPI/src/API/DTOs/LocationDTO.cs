namespace MachineAPI.API.DTOs
{
    public class LocationDTO
    {

        public required int Id { get; set; }
        public required string Name { get; set; }
        public string Description { get; set; } = string.Empty;
        public List<Machine> Machines { get; set; } = new List<MachineDTO>();
    }
}