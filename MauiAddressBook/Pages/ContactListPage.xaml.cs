using MauiAddressBook.ViewModels;

namespace MauiAddressBook.Pages;

public partial class ContactListPage : ContentPage
{
	public ContactListPage(ContactListViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}