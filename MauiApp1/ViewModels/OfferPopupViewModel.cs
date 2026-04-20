using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiApp1.Constants;
using MauiApp1.Models;

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
        Symbol = config.Type == OfferType.Percentage ? TypeConstant.PercentSymbol : TypeConstant.FixedSymbol;
        OfferValue = config.Value.ToString("F2");

        ConfigureTheme(config.Theme);
    }

    private void ConfigureTheme(OfferTheme theme)
    {
        if (theme == OfferTheme.Dark)
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
    [RelayCommand]
    private void Cancel() => CloseRequested?.Invoke(OfferPopupResult.Dismissed);
}
