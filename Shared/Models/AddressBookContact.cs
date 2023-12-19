namespace Shared.Models;

public class AddressBookContact
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public int PhoneNumber { get; set; }
    public string Address { get; set; } = null!;
    public string City { get; set; } = null!;
    public string ZipCode { get; set; } = null!;

    public string FullName
    {
        get
        {
            return $"{FirstName} {LastName}";
        }
    }

    public string FullAddress
    {
        get
        {
            return $"{Address} {ZipCode} {City}";
        }
    }
}
