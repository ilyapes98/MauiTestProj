using CommunityToolkit.Maui;
using CommunityToolkit.Maui.Extensions;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiApp1.Constants;
using MauiApp1.Models;
using Microsoft.Maui.Storage;

namespace MauiApp1.ViewModels;

public partial class MainPageViewModel : ObservableObject
{
    private readonly IPopupService popupService;
    private readonly IServiceProvider _serviceProvider;
    public MainPageViewModel(IPopupService PopupService, IServiceProvider serviceProvider)
    {
        popupService = PopupService;
        _serviceProvider = serviceProvider;

        headerText   = Preferences.Get(PreferenceKeys.HeaderText, string.Empty);
        showIcon     = Preferences.Get(PreferenceKeys.ShowIcon, false);
        isPercentage = Preferences.Get(PreferenceKeys.IsPercentage, true);

        var themeName = Preferences.Get(PreferenceKeys.SelectedTheme, nameof(OfferTheme.Light));
        selectedTheme = Enum.TryParse<OfferTheme>(themeName, out var theme)
            ? theme
            : OfferTheme.Light;
    }

    [ObservableProperty] private string headerText = string.Empty;
    [ObservableProperty] private OfferTheme selectedTheme = OfferTheme.Light;
    [ObservableProperty] private bool showIcon;
    [ObservableProperty] private bool isPercentage = true;

    partial void OnHeaderTextChanged(string value) => Preferences.Set(PreferenceKeys.HeaderText, value);
    partial void OnShowIconChanged(bool value) => Preferences.Set(PreferenceKeys.ShowIcon, value);
    partial void OnIsPercentageChanged(bool value) => Preferences.Set(PreferenceKeys.IsPercentage, value);
    partial void OnSelectedThemeChanged(OfferTheme value) => Preferences.Set(PreferenceKeys.SelectedTheme, value.ToString());

    public OfferTheme[] Themes { get; } = Enum.GetValues<OfferTheme>();
    [RelayCommand]
    private async Task PreviewAsync()
    {
        try
        {
            var popupVm = _serviceProvider.GetRequiredService<OfferPopupViewModel>();

            var config = new OfferConfiguration(
                Title: HeaderText,
                Theme: SelectedTheme,
                ShowIcon: ShowIcon,
                Type: IsPercentage ? OfferType.Percentage : OfferType.Fixed);

            popupVm.Configure(config);

            var popup = new OfferPopup(popupVm);

            var result = await Shell.Current.ShowPopupAsync<OfferPopupResult>(popup);

            if (result.Result == OfferPopupResult.Accepted)
            {
                await Shell.Current.DisplayAlertAsync("Success", "User claimed the offer!", "OK");
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"PREVIEW FAILED: {ex}");
            await Shell.Current.DisplayAlertAsync(
                ex.GetType().Name,
                $"{ex.Message}\n\n{ex.InnerException?.Message}",
                "OK");
        }
    }
}
