using System.ComponentModel;
using Firebase.Auth;
using LoveLink.Views;

namespace LoveLink.ViewModels;

public class RegistrationPageViewModel : INotifyPropertyChanged
{
    private readonly INavigation _navigation;

    private string? _email;
    private string? _password;

    private bool _buttonEnabled;
    private bool _requiredEmailLabel = true;
    private bool _requiredPasswordLabel = true;

    public Command RegisterButton { get; set; }

    public event PropertyChangedEventHandler? PropertyChanged;

    public string? Email
    {
        get => _email;
        set
        {
            _email = value;
            RaisePropertyChanged(nameof(Email));
        }
    }

    public string? Password
    {
        get => _password;
        set
        {
            _password = value;
            RaisePropertyChanged(nameof(Password));
            
        }
    }

    public bool ButtonEnabled
    {
        get => _buttonEnabled;
        set
        {
            _buttonEnabled = value;
            RaisePropertyChanged(nameof(ButtonEnabled));
        }
    }

    public bool RequiredEmailLabel
    {
        get => _requiredEmailLabel;
        set
        {
            _requiredEmailLabel = value;
            RaisePropertyChanged(nameof(RequiredEmailLabel));
        }
    }

    public bool RequiredPasswordLabel
    {
        get => _requiredPasswordLabel;
        set
        {
            _requiredPasswordLabel = value; 
            RaisePropertyChanged(nameof(RequiredPasswordLabel));
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
                await App.Current.MainPage.DisplayAlert("Done!", "User Registered Successfully", "Ok");
                await _navigation.PushAsync(new LoginPage());
            }

            await _navigation.PopAsync();
        }
        catch (Exception e)
        {
            await App.Current.MainPage.DisplayAlert("Alert", e.Message, "Ok");
        }
    }

    private void RaisePropertyChanged(string v)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(v));
    }
}