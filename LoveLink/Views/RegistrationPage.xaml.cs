using LoveLink.ViewModels;

namespace LoveLink.Views;

public partial class RegistrationPage : ContentPage
{
    public RegistrationPage()
    {
        InitializeComponent();
        BindingContext = new RegistrationPageViewModel(Navigation);
    }
}