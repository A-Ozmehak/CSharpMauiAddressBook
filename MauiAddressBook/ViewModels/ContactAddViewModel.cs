﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Shared.Services;
using Shared.Models;
using System.Collections.ObjectModel;

namespace MauiAddressBook.ViewModels
{
    public partial class ContactAddViewModel : ObservableObject
    {
        private readonly ContactService _contactService;

        public ContactAddViewModel(ContactService contactService)
        {
            _contactService = contactService;
            UpdateContactList();
        }

        [ObservableProperty]
        private AddressBookContact _addContactForm = new();

        [ObservableProperty]
        private ObservableCollection<AddressBookContact> _contactList = [];

        [RelayCommand]
        private void AddContact()
        {
            if (AddContactForm != null && !string.IsNullOrWhiteSpace(AddContactForm.FirstName))
            {
                var result = _contactService.AddContactToList(AddContactForm);
                if (result)
                {
                    AddContactForm = new();
                }
            }
        }

        [RelayCommand]
        public void RemoveContactFromList(AddressBookContact contact)
        {
            if (contact != null)
            {
                var result = _contactService.RemoveCustomerFromList(contact.Email);
                if (result)
                {
                    UpdateContactList();
                }
            }
        }

        public void UpdateContactList()
        {
            ContactList = new ObservableCollection<AddressBookContact>(_contactService.Contacts.Select(contact => contact).ToList());
        }


        [RelayCommand]
        private async Task NavigateToList()
        {
            await Shell.Current.GoToAsync("ContactListPage");
        }
    }
}