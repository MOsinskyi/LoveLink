using LoveLink.ViewModels;

namespace LoveLink.Views;

public partial class RegistrationPage : ContentPage
{
    private bool _emailEntered;
    
    public RegistrationPage()
    {
        InitializeComponent();
        BindingContext = new RegistrationPageViewModel(Navigation);
    }

    private void OnEntryTextChanged(object? sender, TextChangedEventArgs e)
    {
        var currentEntry = sender as Entry;
        var model = (RegistrationPageViewModel)BindingContext;

        switch (currentEntry?.Placeholder)
        {
            case "Email" when string.IsNullOrEmpty(currentEntry.Text):
                model.RequiredEmailLabel = true;
                break;
            case "Email":
                model.RequiredEmailLabel = false;
                break;
            case "Password" when currentEntry.Text.Length < 6:
                model.RequiredPasswordLabel = true;
                break;
            case "Password":
                model.RequiredPasswordLabel = false;
                break;
        }

        model.ButtonEnabled = model is { RequiredEmailLabel: false, RequiredPasswordLabel: false };
    }
}