namespace MenuSaz.Identity.Application.Feature.User.Dtos;
public class UserDto
{
    public string Username { get; set; }
    public string Firstname { get; set; }
    public string Lastname { get; set; }
    public long PhoneNumber { get; set; }
    public bool IsActive { get; set; }
}
