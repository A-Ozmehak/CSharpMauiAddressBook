using MauiAddressBook.ViewModels;

namespace MauiAddressBook.Pages;

public partial class ContactListPage : ContentPage
{
    private readonly ContactListViewModel _viewModel;
    public ContactListPage(ContactListViewModel viewModel)
	{
		InitializeComponent();
        _viewModel = viewModel;
        BindingContext = _viewModel;
	}

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        await _viewModel.LoadContacts();
    }

    private void SearchButton_Clicked(object sender, EventArgs e)
    {
        string firstName = FirstNameEntry.Text;
        var matchingContacts = _viewModel.FindContactsByFirstName(firstName);

        ContactListView.ItemsSource = matchingContacts;
    }
}