using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Shared.Models;
using Shared.Services;
using System.Collections.ObjectModel;

namespace MauiAddressBook.ViewModels;

public partial class ContactListViewModel : ObservableObject
{
    private readonly ContactService _contactService;

    [ObservableProperty]
    private ObservableCollection<AddressBookContact> contactList = [];

    public ContactListViewModel(ContactService contactService)
    {
        _contactService = contactService;
        _contactService.ContactUpdated += (sender, e) =>
        {
            ContactList = new ObservableCollection<AddressBookContact>(_contactService.GetContacts());
        };
    }

    /// <summary>
    /// Removes a contact from the list 
    /// </summary>
    /// <param name="contact">The contact to remove</param>
    [RelayCommand]
    public void RemoveContactFromList(AddressBookContact contact)
    {
        if (contact != null)
        {
            _contactService.RemoveCustomerFromList(contact.Email);
        }
    }

    /// <summary>
    /// Navigates to ContactEditPage
    /// </summary>
    /// <param name="contact">The contact that was edited</param>
    [RelayCommand]
    private async Task NavigateToEdit(AddressBookContact contact)
    {
        var parameters = new ShellNavigationQueryParameters
        {
            {"Contact", contact }
        };

        await Shell.Current.GoToAsync("ContactEditPage", parameters);
    }

    /// <summary>
    /// Navigates to ContactAddPage
    /// </summary>
    [RelayCommand]
    private async Task NavigateToAdd()
    {
        await Shell.Current.GoToAsync("ContactAddPage");
    }

    /// <summary>
    /// Load all contacts
    /// </summary>
    [RelayCommand]
    public async Task LoadContacts()
    {
        var contacts = _contactService.GetContacts();
        ContactList.Clear();
        foreach (var contact in contacts)
        {
            ContactList.Add(contact);
        }
        await Task.CompletedTask;
    }
}
