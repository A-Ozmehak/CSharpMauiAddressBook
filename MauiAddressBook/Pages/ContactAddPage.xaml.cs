using MauiAddressBook.ViewModels;

namespace MauiAddressBook.Pages;

public partial class ContactAddPage : ContentPage
{
	public ContactAddPage(ContactAddViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}