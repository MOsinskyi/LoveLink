using Plugin.Maui.SwipeCardView.Core;
using LoveLink.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace LoveLink.ViewModels;

public class MainPageViewModel : BasePageViewModel
{
    private ObservableCollection<Profile> _profiles = [];

    private uint _threshold;

    public MainPageViewModel()
    {
        InitializeProfiles();

        Threshold = (uint)(DeviceDisplay.MainDisplayInfo.Width / DeviceDisplay.MainDisplayInfo.Density / 3);

        SwipedCommand = new Command<SwipedCardEventArgs>(OnSwipedCommand);
        DraggingCommand = new Command<DraggingCardEventArgs>(OnDraggingCommand);

        ClearItemsCommand = new Command(OnClearItemsCommand);
        AddItemsCommand = new Command(OnAddItemsCommand);
    }

    public ObservableCollection<Profile> Profiles
    {
        get => _profiles;
        set
        {
            _profiles = value;
            RaisePropertyChanged();
        }
    }

    public uint Threshold
    {
        get => _threshold;
        set
        {
            _threshold = value;
            RaisePropertyChanged();
        }
    }

    public ICommand SwipedCommand { get; }

    public ICommand DraggingCommand { get; }

    public ICommand ClearItemsCommand { get; }

    public ICommand AddItemsCommand { get; }

    private void OnSwipedCommand(SwipedCardEventArgs eventArgs)
    {
    }

    private void OnDraggingCommand(DraggingCardEventArgs eventArgs)
    {
        switch (eventArgs.Position)
        {
            case DraggingCardPosition.Start:
                return;

            case DraggingCardPosition.UnderThreshold:
                break;

            case DraggingCardPosition.OverThreshold:
                break;

            case DraggingCardPosition.FinishedUnderThreshold:
                return;

            case DraggingCardPosition.FinishedOverThreshold:
                break;

            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void OnClearItemsCommand()
    {
        Profiles.Clear();
    }

    private void OnAddItemsCommand()
    {
    }

    private void InitializeProfiles()
    {
        Profiles.Add(new Profile { ProfileId = 1, Name = "Laura", Age = 24, Country = "Ukraine", City = "Lviv", Gender = 'w', Photo = "p705193.jpg" });
        Profiles.Add(new Profile { ProfileId = 2, Name = "Sophia", Age = 21, Country = "Ukraine", City = "Kyiv", Gender = 'w', Photo = "p597956.jpg" });
        Profiles.Add(new Profile { ProfileId = 3, Name = "Anne", Age = 19, Country = "Ukraine", City = "Kyiv", Gender = 'w', Photo = "p497489.jpg" });
        Profiles.Add(new Profile { ProfileId = 4, Name = "Yvonne ", Age = 27, Country = "Ukraine", City = "Kharkiv", Gender = 'w', Photo = "p467499.jpg" });
        Profiles.Add(new Profile { ProfileId = 5, Name = "Abby", Age = 25, Country = "Ukraine", City = "Kharkiv", Gender = 'w', Photo = "p589739.jpg" });
        Profiles.Add(new Profile { ProfileId = 6, Name = "Andressa", Age = 28, Country = "Ukraine", City = "Odesa", Gender = 'w', Photo = "p453095.jpg" });
        Profiles.Add(new Profile { ProfileId = 7, Name = "June", Age = 29, Country = "Ukraine", City = "Odesa", Gender = 'w', Photo = "p503001.jpg" });
        Profiles.Add(new Profile { ProfileId = 8, Name = "Kim", Age = 22, Country = "Ukraine", City = "Lviv", Gender = 'w', Photo = "p627958.jpg" });
        Profiles.Add(new Profile { ProfileId = 9, Name = "Denesha", Age = 26, Country = "Ukraine", City = "Lviv", Gender = 'w', Photo = "p474893.jpg" });
        Profiles.Add(new Profile { ProfileId = 10, Name = "Sasha", Age = 23, Country = "Ukraine", City = "Kyiv", Gender = 'w', Photo = "p458914.jpg" });

        Profiles.Add(new Profile { ProfileId = 11, Name = "Austin", Age = 28, Country = "Ukraine", City = "Lviv", Gender = 'm', Photo = "p378674.jpg" });
        Profiles.Add(new Profile { ProfileId = 12, Name = "James", Age = 32, Country = "Ukraine", City = "Lviv", Gender = 'm', Photo = "p398931.jpg" });
        Profiles.Add(new Profile { ProfileId = 13, Name = "Chris", Age = 27, Country = "Ukraine", City = "Kyiv", Gender = 'm', Photo = "p401107.jpg" });
        Profiles.Add(new Profile { ProfileId = 14, Name = "Alexander", Age = 30, Country = "Ukraine", City = "Kyiv", Gender = 'm', Photo = "p731150.jpg" });
        Profiles.Add(new Profile { ProfileId = 15, Name = "Steve", Age = 31, Country = "Ukraine", City = "Odesa", Gender = 'm', Photo = "p327144.jpg" });
    }
}