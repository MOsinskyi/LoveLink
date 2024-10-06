using System.ComponentModel;
using Firebase.Auth;
using LoveLink.Views;
using PropertyChangingEventArgs = Microsoft.Maui.Controls.PropertyChangingEventArgs;
using PropertyChangingEventHandler = Microsoft.Maui.Controls.PropertyChangingEventHandler;

namespace LoveLink.ViewModels;

public class RegistrationPageViewModel : INotifyPropertyChanged
{
    private readonly INavigation _navigation;

    private string? _email;
    private string? _password;

    public Command RegisterButton { get; set; }

    public event PropertyChangedEventHandler? PropertyChanged;

    public string? Email
    {
        get => _email;
        set
        {
            _email = value;
            RaisePropertyChanged("Email");
        }
    }

    public string? Password
    {
        get => _password;
        set
        {
            _password = value;
            RaisePropertyChanged("Password");
        }
    }


    public RegistrationPageViewModel(INavigation navigation)
    {
        _navigation = navigation;
        RegisterButton = new Command(RegisterUser);
    }

    private async void RegisterUser(object obj)
    {
        try
        {
            var authProvider = new FirebaseAuthProvider(new FirebaseConfig(WebApi.Key));
            var auth = await authProvider.CreateUserWithEmailAndPasswordAsync(Email, Password);
            var token = auth.FirebaseToken;
            if (token != null)
            {
                await App.Current.MainPage.DisplayAlert("Alert", "User Registered Successfully", "Ok");
            }

            await _navigation.PopAsync();
        }
        catch (Exception e)
        {
            await App.Current.MainPage.DisplayAlert("Alert", e.Message, "Ok");
            throw;
        }
    }

    private void RaisePropertyChanged(string v)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(v));
    }
}