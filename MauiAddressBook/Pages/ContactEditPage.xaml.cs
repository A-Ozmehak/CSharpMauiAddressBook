using MauiAddressBook.ViewModels;

namespace MauiAddressBook.Pages;

public partial class ContactEditPage : ContentPage
{
	public ContactEditPage(ContactEditViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}