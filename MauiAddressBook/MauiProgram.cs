using MauiAddressBook.Pages;
using MauiAddressBook.ViewModels;
using Shared.Services;

namespace MauiAddressBook
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            builder.Services.AddSingleton<ContactAddPage>();
            builder.Services.AddSingleton<ContactAddViewModel>();

            builder.Services.AddSingleton<ContactListPage>();
            builder.Services.AddSingleton<ContactListViewModel>();

            builder.Services.AddSingleton<ContactEditPage>();
            builder.Services.AddSingleton<ContactEditViewModel>();

            builder.Services.AddSingleton<ContactService>();
            builder.Services.AddSingleton<FileService>();

            return builder.Build();
        }
    }
}
