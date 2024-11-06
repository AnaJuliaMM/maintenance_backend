namespace MachineAPI.Domain.Entities
{
    public class MachineLocation
    {
        public required int MachineId { get; set; }
        public required Machine Machine { get; set; }
        public required int LocationId { get; set; }
        public required Location Location { get; set; }

    }
}