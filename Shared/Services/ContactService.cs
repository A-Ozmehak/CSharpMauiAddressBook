using Newtonsoft.Json;
using Shared.Models;
using System.Diagnostics;
using System.Diagnostics.Contracts;

namespace Shared.Services;

public class ContactService
{
    private readonly FileService _fileService;

    public ContactService(FileService fileService)
    {
        _fileService = fileService;
    }

    public List<AddressBookContact> Contacts { get; private set; } = [];
    private readonly string _filePath = @"C:\Users\Anna\Documents\CSharpRepo\CSharpMauiAddressBook\addressBookContacts.json";

    public event EventHandler? ContactUpdated;

    public bool AddContactToList(AddressBookContact contact)
    {
        try
        {
            Contacts.Add(contact);

            string json = JsonConvert.SerializeObject(Contacts, new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Objects });
            _fileService.SaveContactToFile(_filePath, json);
            ContactUpdated?.Invoke(this, new EventArgs());

            return true;
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return false;
    }

    public IEnumerable<AddressBookContact> GetContacts()
    {
        try
        {
            var content = _fileService.GetContactsFromFile(_filePath);
            if (!string.IsNullOrEmpty(content))
            {
                Contacts = JsonConvert.DeserializeObject<List<AddressBookContact>>(content, new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All })!;
                return Contacts;
            }
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return null!;
    }

    public bool RemoveContactFromList(string email)
    {
        try
        {
            var contactToRemove = Contacts.FirstOrDefault(c => c.Email == email);
            if (contactToRemove != null)
            {
                Contacts.Remove(contactToRemove);

                string json = JsonConvert.SerializeObject(Contacts, new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Objects });
                _fileService.SaveContactToFile(_filePath, json);
                ContactUpdated?.Invoke(this, new EventArgs());

                return true;
            }
            else
            {
                Console.WriteLine("Contact not found.");
                return false;
            }
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return false;
    }

    public AddressBookContact GetSingleContact(string firstName)
    {
        try
        {
            GetContacts();

            var contact = Contacts.FirstOrDefault(x => x.FirstName == firstName);
            return contact ??= null!;
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return null!;
    }

    public void Update(AddressBookContact contact)
    {
        var addressBookItem = Contacts.FirstOrDefault(i => i.FullName == contact.FullName);
        if (addressBookItem != null)
        {
            addressBookItem = contact;

            string json = JsonConvert.SerializeObject(Contacts, new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Objects });
            _fileService.SaveContactToFile(_filePath, json);

            ContactUpdated?.Invoke(this, EventArgs.Empty);
        }
    }
}
