using System.ComponentModel;
using Firebase.Auth;
using LoveLink.Views;
using Newtonsoft.Json;

namespace LoveLink.ViewModels;

public class LoginPageViewModel : INotifyPropertyChanged
{
    private const string Key = "FreshFirebaseToken";
    private readonly INavigation _navigation;

    private string _userEmail = null!;
    private string _userPassword = null!;

    public event PropertyChangedEventHandler? PropertyChanged;

    public Command CreateAccountButton { get; set; }
    public Command LoginButton { get; set; }

    public string UserEmail
    {
        get => _userEmail;
        set
        {
            _userEmail = value;
            RaisePropertyChanged("UserEmail");
        }
    }

    public string UserPassword
    {
        get => _userPassword;
        set
        {
            _userPassword = value;
            RaisePropertyChanged("UserPassword");
        }
    }

    public LoginPageViewModel(INavigation navigation)
    {
        _navigation = navigation;
        SkipLoginPage();
        CreateAccountButton = new Command(CreateAccount);
        LoginButton = new Command(LoginUser);
    }

    private async void SkipLoginPage()
    {
        if (!Preferences.ContainsKey(Key)) return;
        var serializedContent = Preferences.Get(Key, string.Empty);
        var savedAuth = JsonConvert.DeserializeObject<FirebaseAuthLink>(serializedContent);

        if (savedAuth != null && !savedAuth.IsExpired())
        {
            await _navigation.PushAsync(new MainPage());
        }
    }

    private async void LoginUser(object obj)
    {
        var authProvider = new FirebaseAuthProvider(new FirebaseConfig(WebApi.Key));
        try
        {
            var auth = await authProvider.SignInWithEmailAndPasswordAsync(UserEmail, UserPassword);
            var content = await auth.GetFreshAuthAsync();
            var serializedContent = JsonConvert.SerializeObject(content);
            Preferences.Set("FreshFirebaseToken", serializedContent);
            await _navigation.PushAsync(new MainPage());
        }
        catch (Exception e)
        {
            await Application.Current?.MainPage?.DisplayAlert("Alert", e.Message, "Ok")!;
        }
    }

    private async void CreateAccount(object obj)
    {
        await _navigation.PushAsync(new RegistrationPage());
    }

    private void RaisePropertyChanged(string v)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(v));
    }
}