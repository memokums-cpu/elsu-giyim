// Bu tipler Aşama 3'te backend DTO'larıyla birebir eşleştirilecek.

export type FitType =
  | "Slim Fit"
  | "Regular Fit"
  | "Oversize"
  | "Dar Kalıp"
  | "Geniş Kalıp";

export interface ProductVariant {
  sku: string;
  size: string; // "S", "M", "42" gibi — ürün tipine göre değişir
  color: string;
  colorHex: string;
  stock: number; // 0 ise "stokta yok"
}

export interface SizeChartRow {
  size: string;
  chest?: string; // göğüs ölçüsü
  waist?: string; // bel ölçüsü
  length?: string; // boy
}

export interface Product {
  id: string; // slug olarak kullanılıyor
  name: string;
  category: "kadin" | "erkek" | "ayakkabi" | "aksesuar";
  subCategory: string; // "Tişört", "Pantolon", "Elbise" vb.
  price: number;
  salePrice?: number;
  description: string;
  fabric: string;
  careInstructions: string;
  fit?: FitType; // aksesuar gibi ürünlerde olmayabilir
  modelInfo?: string; // "Manken 180 cm, 68 kg, M beden giyiyor"
  sizeChart?: SizeChartRow[];
  variants: ProductVariant[];
  outfitProducts?: Product[]; // kombin önerileri (backend'den tam nesne olarak gelir)
  similarProducts?: Product[];
  isNew?: boolean;
  isBestSeller?: boolean;
}

// Basit filtre/sıralama tipleri — Aşama 3'te query parametrelerine dönüşecek
export interface ProductFilters {
  sizes: string[];
  colors: string[];
  minPrice?: number;
  maxPrice?: number;
  fabrics: string[];
  fits: FitType[];
  inStockOnly: boolean;
}

export type SortOption =
  | "onerilen"
  | "en-yeni"
  | "fiyat-artan"
  | "fiyat-azalan"
  | "cok-satan";
