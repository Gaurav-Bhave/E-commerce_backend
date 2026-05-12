namespace Practiced_E_commerce.Dto.AllUsers
{
    public class AllUsersResponceDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string RoleName { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public int totalUsers { get; set; }
    }
}
