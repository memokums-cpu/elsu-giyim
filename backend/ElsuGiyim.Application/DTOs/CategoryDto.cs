namespace ElsuGiyim.Application.DTOs;

public record CategoryDto(
    Guid Id,
    string Name,
    string Slug,
    int DisplayOrder
);
