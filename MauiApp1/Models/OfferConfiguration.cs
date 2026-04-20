namespace MauiApp1.Models;

public sealed record OfferConfiguration(
    string Title,
    OfferTheme Theme,
    bool ShowIcon,
    OfferType Type
);
