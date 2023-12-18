using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Shared.Models;
using Shared.Services;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace MauiAddressBook.ViewModels;

public partial class ContactListViewModel : ObservableObject
{
    private readonly ContactService _contactService;
    [ObservableProperty]
    private ObservableCollection<AddressBookContact> _contactList = [];

    public ContactListViewModel(ContactService contactService)
    {
        _contactService = contactService;
        UpdateContactList();
    }

    public void UpdateContactList()
    {
        ContactList = new ObservableCollection<AddressBookContact>(_contactService.Contacts.Select(contact => contact).ToList());
    }


    //protected override void OnAppearing()
    //{
    //    base.OnAppearing();
    //    LoadContacts();
    //}

    //private void LoadContacts()
    //{
    //    try
    //    {
    //        var contacts = _contactService.GetContacts();
    //        ContactsCollectionView.ItemsSource = contacts;
    //    }
    //    catch (Exception ex)
    //    {
    //        Debug.WriteLine(ex.Message);
    //        // Show an error message to the user
    //    }
    //}

    //public string GetSingleContactFromList()
    //{

    //}

    //[RelayCommand]
    //private async Task NavigateToAddContact()
    //{
    //    await Shell.Current.GoToAsync("ContactAddPage");
    //}
}
