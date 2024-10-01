using Plugin.Maui.SwipeCardView.Core;
using LoveLink.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace LoveLink.ViewModels;

public class MainPageModel : BasePageViewModel
{
    private Random random = new Random();

    private ObservableCollection<Profile> _profiles = new ObservableCollection<Profile>();

    private uint _threshold;

    private string[] _names = new string[15];

    private int[] _ages = new int[15];

    private string[] _photos =
    [
        "p705193", "p597956", "p497489", "p467499", "p589739", "p453095", "p503001", "p627958", "p474893",
        "p458914", "p378674", "p398931", "p401107", "p731150", "p327144"
    ];

    private void FillProfiles()
    {
        for(int i = 0; i < _names.Length; i++)
        {
            _names[i] = "Name" + i.ToString();
            _ages[i] = random.Next(18, 30);
        }

    }

    public MainPageModel()
    {
        FillProfiles();
        InitializeProfiles();

        Threshold = (uint)(DeviceDisplay.MainDisplayInfo.Width / DeviceDisplay.MainDisplayInfo.Density / 3);

        SwipedCommand = new Command<SwipedCardEventArgs>(OnSwipedCommand);
        DraggingCommand = new Command<DraggingCardEventArgs>(OnDraggingCommand);

        ClearItemsCommand = new Command(OnClearItemsCommand);
        AddItemsCommand = new Command(OnAddItemsCommand);
    }

    private void OnAddItemsCommand()
    {
    }

    public ICommand AddItemsCommand { get; }

    private void OnClearItemsCommand()
    {
        Profiles.Clear();
    }

    public ICommand ClearItemsCommand { get; }

    private void OnDraggingCommand(DraggingCardEventArgs obj)
    {
        switch (obj.Position)
        {
            case DraggingCardPosition.Start:
                return;
            case DraggingCardPosition.UnderThreshold:
                break;
            case DraggingCardPosition.OverThreshold:
                break;
            case DraggingCardPosition.FinishedUnderThreshold:
                break;
            case DraggingCardPosition.FinishedOverThreshold:
                break;
            
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    public ICommand DraggingCommand { get; }

    private void OnSwipedCommand(SwipedCardEventArgs obj)
    {
    }

    public ICommand SwipedCommand { get; }

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

    private void InitializeProfiles()
    {
        for (var i = 0; i < _names.Length; i++)
        {
            Profiles.Add(new Profile { ProfileId = i + 1, Name = _names[i], Age = _ages[i], Photo = _photos[i] + ".jpg" });
        }
        
    }
}