using LoveLink.ViewModels;

namespace LoveLink.Views;

public partial class LoginPage : ContentPage
{
    public LoginPage()
    {
        InitializeComponent();

        BindingContext = new LoginPageViewModel(Navigation);
    }
}