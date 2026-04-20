using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiApp1.Models;
using Microsoft.Maui.Graphics.Text;

namespace MauiApp1.ViewModels;

public partial class OfferPopupViewModel : ObservableObject
{
    [ObservableProperty] private string title = string.Empty;
    [ObservableProperty] private string symbol = string.Empty;
    [ObservableProperty] private string offerValue = string.Empty;
    [ObservableProperty] private bool showIcon;
    [ObservableProperty] private Color backgroundColor = Colors.White;
    [ObservableProperty] private Color textColor = Colors.Black;

    public event Action<OfferPopupResult>? CloseRequested;

    public void Configure(OfferConfiguration config)
    {
        Title = config.Title;
        ShowIcon = config.ShowIcon;
        Symbol = config.Type == OfferType.Percentage ? "%" : "$";
        OfferValue = config.Value.ToString("F2");

        if (config.Theme == OfferTheme.Dark)
        {
            BackgroundColor = Colors.Black;
            TextColor = Colors.White;
        }
        else
        {
            BackgroundColor = Colors.White;
            TextColor = Colors.Black;
        }
    }

    [RelayCommand]
    private void Claim() => CloseRequested?.Invoke(OfferPopupResult.Accepted);
}
