using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Shared.Models;
using Shared.Services;

namespace MauiAddressBook.ViewModels;

public partial class ContactEditViewModel : ObservableObject, IQueryAttributable
{
    private readonly ContactService _contactService;

    public ContactEditViewModel(ContactService contactService)
    {
        _contactService = contactService;
    }

    [ObservableProperty]
    private AddressBookContact contact = new();

    /// <summary>
    /// Updates the contact information and redirects to ContactListPage
    /// </summary>
    [RelayCommand]
    private async Task UpdateContact()
    {
        _contactService.Update(contact);
        Contact = new();

        await Shell.Current.GoToAsync("//ContactListPage");
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        Contact = (query["Contact"] as AddressBookContact)!;
    }
}
