namespace ElsuGiyim.Application.DTOs;

public record ProductVariantDto(
    string Sku,
    string Size,
    string Color,
    string ColorHex,
    int Stock
);
