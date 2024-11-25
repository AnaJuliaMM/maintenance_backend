namespace UserAPI.API.DTOs
{
    public class UserTokenPayloadDTO
    {
        public required string Name { get; set; }
        public required string Role { get; set; }
    }
}