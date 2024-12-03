namespace UserAPI.API.DTOs
{
    public class CreateUpdateUserDTO
    {
        public required int Id { get; set; }
        public required string Name { get; set; }
        public required string Email { get; set; }
        public required string Username { get; set; }
        public string? Password { get; set; }

        public int? RoleId { get; set; }
    }
}
