using System.ComponentModel;
using Firebase.Auth;
using LoveLink.Views;
using Newtonsoft.Json;

namespace LoveLink.ViewModels;

public class LoginPageViewModel : INotifyPropertyChanged
{
    private readonly INavigation _navigation;

    private string _userEmail;
    private string _userPassword;

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
        CreateAccountButton = new Command(CreateAccount);
        LoginButton = new Command(LoginUser);
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
            await App.Current.MainPage.DisplayAlert("Alert", e.Message, "Ok");
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