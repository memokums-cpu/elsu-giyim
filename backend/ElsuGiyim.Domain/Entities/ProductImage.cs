using ElsuGiyim.Domain.Common;

namespace ElsuGiyim.Domain.Entities;

public class ProductImage : BaseEntity
{
    public string Url { get; set; } = string.Empty;
    public int DisplayOrder { get; set; }

    public Guid ProductId { get; set; }
    public Product Product { get; set; } = null!;
}
