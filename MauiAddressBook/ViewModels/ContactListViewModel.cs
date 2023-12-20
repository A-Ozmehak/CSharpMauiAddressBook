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
        //UpdateContactList();
    }

    public void UpdateContactList()
    {
        ContactList = new ObservableCollection<AddressBookContact>(_contactService.Contacts.Select(contact => contact).ToList());
    }

    [RelayCommand]
    private void RemoveContact(AddressBookContact contact)
    {
        _contactService.RemoveCustomerFromList(contact.Email);

    }

    [RelayCommand]
    private async Task NavigateToEdit(AddressBookContact contact)
    {
        var parameters = new ShellNavigationQueryParameters
        {
            {"Contact", contact }
        };

        await Shell.Current.GoToAsync("ContactEditPage", parameters);
    }

    [RelayCommand]
    private async Task NavigateToAdd(AddressBookContact contact)
    {
        await Shell.Current.GoToAsync("ContactAddPage");
    }

    //[RelayCommand]
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
