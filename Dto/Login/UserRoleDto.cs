namespace Practiced_E_commerce.Dto.Login
{
    public class UserRoleDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string RoleName { get; set; }
    }
}
