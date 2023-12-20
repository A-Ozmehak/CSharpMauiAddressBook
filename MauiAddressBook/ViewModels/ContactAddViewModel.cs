using CommunityToolkit.Mvvm.ComponentModel;
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

            foreach (AddressBookContact contact in _contactService.GetContacts())
            {
                ContactList.Add(contact);
            }
        }

        [ObservableProperty]
        private AddressBookContact _addContactForm = new();

        [ObservableProperty]
        private ObservableCollection<AddressBookContact> _contactList = [];

        /// <summary>
        /// Asynchronously adds a new contact to the contact list if the contact form is valid.
        /// </summary>
        [RelayCommand]
        private async Task AddContact()
        {
            if (AddContactForm != null && !string.IsNullOrWhiteSpace(AddContactForm.FirstName) && !string.IsNullOrWhiteSpace(AddContactForm.LastName))
            {
                var result = _contactService.AddContactToList(AddContactForm);
                if (result)
                {
                    AddContactForm = new();
                    await Shell.Current.GoToAsync("..");
                }
            }
        }
    }
}
