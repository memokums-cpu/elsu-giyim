import type { Product, SortOption } from "@/types/product";
import type { FilterState } from "@/components/FilterSidebar";

// NOT: Bu fonksiyon şu an istemci tarafında (demo veri üzerinde) çalışıyor.
// Aşama 3'te backend hazır olduğunda bu mantık API tarafına (GET /api/products
// query parametreleri) taşınacak; bu dosya o zaman kaldırılacak.
export function filterProducts(products: Product[], filters: FilterState): Product[] {
  return products.filter((product) => {
    const price = product.salePrice ?? product.price;
    if (price > filters.maxPrice) return false;

    if (filters.inStockOnly) {
      const hasStock = product.variants.some((v) => v.stock > 0);
      if (!hasStock) return false;
    }

    if (filters.sizes.length > 0) {
      const hasSize = product.variants.some((v) => filters.sizes.includes(v.size));
      if (!hasSize) return false;
    }

    if (filters.colors.length > 0) {
      const hasColor = product.variants.some((v) => filters.colors.includes(v.color));
      if (!hasColor) return false;
    }

    if (filters.fits.length > 0) {
      if (!product.fit || !filters.fits.includes(product.fit)) return false;
    }

    return true;
  });
}

export function sortProducts(products: Product[], sort: SortOption): Product[] {
  const copy = [...products];
  switch (sort) {
    case "fiyat-artan":
      return copy.sort(
        (a, b) => (a.salePrice ?? a.price) - (b.salePrice ?? b.price)
      );
    case "fiyat-azalan":
      return copy.sort(
        (a, b) => (b.salePrice ?? b.price) - (a.salePrice ?? a.price)
      );
    case "cok-satan":
      return copy.sort((a, b) => Number(b.isBestSeller) - Number(a.isBestSeller));
    case "en-yeni":
      return copy.sort((a, b) => Number(b.isNew) - Number(a.isNew));
    case "onerilen":
    default:
      return copy;
  }
}
