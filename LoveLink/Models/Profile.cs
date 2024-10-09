namespace LoveLink.Models;

public class Profile
{
    public int ProfileId { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Age { get; set; }
    public string Photo { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public char Gender { get; set; }
}