namespace ElsuGiyim.Application.Categories.Admin;

public record CreateCategoryRequest(
    string Name,
    string Slug,
    int DisplayOrder,
    Guid? ParentCategoryId
);

public record UpdateCategoryRequest(
    string Name,
    string Slug,
    int DisplayOrder,
    bool IsActive,
    Guid? ParentCategoryId
);
