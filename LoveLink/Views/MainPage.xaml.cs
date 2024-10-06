using LoveLink.ViewModels;
using Plugin.Maui.SwipeCardView.Core;

namespace LoveLink.Views;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
        BindingContext = new MainPageViewModel();
        SwipeCardView.Dragging += OnDragging;
    }

private void OnDislikeClicked(object sender, EventArgs e)
    {
        SwipeCardView.InvokeSwipe(SwipeCardDirection.Left);
    }

    private void OnSuperLikeClicked(object sender, EventArgs e)
    {
        SwipeCardView.InvokeSwipe(SwipeCardDirection.Up);
    }

    private void OnLikeClicked(object sender, EventArgs e)
    {
        SwipeCardView.InvokeSwipe(SwipeCardDirection.Right);
    }

    private void OnDragging(object sender, DraggingCardEventArgs e)
    {
        var view = (View)sender;
        var nopeFrame = view.FindByName<Frame>("NopeFrame");
        var likeFrame = view.FindByName<Frame>("LikeFrame");
        var threshold = (BindingContext as MainPageViewModel).Threshold;

        var draggedXPercent = e.DistanceDraggedX / threshold;

        var draggedYPercent = e.DistanceDraggedY / threshold;

        switch (e.Position)
        {
            case DraggingCardPosition.Start:
                nopeFrame.Opacity = 0;
                likeFrame.Opacity = 0;
                NopeButton.Scale = 1;
                LikeButton.Scale = 1;
                MoreButton.Scale = 1;
                break;

            case DraggingCardPosition.UnderThreshold:
                if (e.Direction == SwipeCardDirection.Left)
                {
                    nopeFrame.Opacity = (-1) * draggedXPercent;
                    NopeButton.Scale = 1 + draggedXPercent / 2;
                    MoreButton.Scale = 1;
                }
                else if (e.Direction == SwipeCardDirection.Right)
                {
                    likeFrame.Opacity = draggedXPercent;
                    LikeButton.Scale = 1 - draggedXPercent / 2;
                    MoreButton.Scale = 1;
                }

                break;

            case DraggingCardPosition.OverThreshold:
                if (e.Direction == SwipeCardDirection.Left)
                {
                    nopeFrame.Opacity = 1;
                }
                else if (e.Direction == SwipeCardDirection.Right)
                {
                    likeFrame.Opacity = 1;
                }

                break;

            case DraggingCardPosition.FinishedUnderThreshold:
                nopeFrame.Opacity = 0;
                likeFrame.Opacity = 0;
                NopeButton.Scale = 1;
                LikeButton.Scale = 1;
                MoreButton.Scale = 1;
                break;

            case DraggingCardPosition.FinishedOverThreshold:
                nopeFrame.Opacity = 0;
                likeFrame.Opacity = 0;
                NopeButton.Scale = 1;
                LikeButton.Scale = 1;
                MoreButton.Scale = 1;
                break;

            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}