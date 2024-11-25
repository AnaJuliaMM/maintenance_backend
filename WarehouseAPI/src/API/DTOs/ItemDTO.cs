namespace WarehouseAPI.API.DTOs
{
    public class ItemDTO
    {
        public int Id { get; set; }
        public required string Name { get; set; }

        public string? Description { get; set; }
        public string? Type { get; set; }

        public DateTime? acquisitionDate { get; set; }
        public string? Supplier { get; set; }

        public required int Quantity { get; set; }

        public required string Status { get; set; }
    }
}
