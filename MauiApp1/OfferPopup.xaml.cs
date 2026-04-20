using CommunityToolkit.Maui.Views;
using MauiApp1.Models;
using MauiApp1.ViewModels;

namespace MauiApp1;

public partial class OfferPopup : Popup<OfferPopupResult>
{
    public OfferPopup(OfferPopupViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
        vm.CloseRequested += async result => await CloseAsync(result);
    }
}