import type { FitType, Product, SizeChartRow } from "@/types/product";

const API_BASE_URL = process.env.API_BASE_URL ?? "http://localhost:5080";

// --- Backend'den gelen ham (wire format) tipler ---
// Bunlar backend'teki DTO'larla birebir eşleşir (alan adları dahil).

interface ApiVariant {
  sku: string;
  size: string;
  color: string;
  colorHex: string;
  stock: number;
}

interface ApiProductListItem {
  slug: string;
  name: string;
  category: string;
  subCategory: string;
  price: number;
  salePrice: number | null;
  fit: string | null;
  isNew: boolean;
  isBestSeller: boolean;
  variants: ApiVariant[];
}

interface ApiSizeChartRow {
  size: string;
  chest: string | null;
  waist: string | null;
  length: string | null;
}

interface ApiProductDetail extends ApiProductListItem {
  description: string;
  fabric: string;
  careInstructions: string;
  modelInfo: string | null;
  imageUrls: string[];
  sizeChart: ApiSizeChartRow[];
  outfitProducts: ApiProductListItem[];
  similarProducts: ApiProductListItem[];
}

export interface ApiCategory {
  id: string;
  name: string;
  slug: string;
  displayOrder: number;
}

// Backend'in enum adlarını ("RegularFit") frontend'in kullandığı
// okunabilir Türkçe etiketlere ("Regular Fit") çeviriyoruz.
const FIT_LABELS: Record<string, FitType> = {
  SlimFit: "Slim Fit",
  RegularFit: "Regular Fit",
  Oversize: "Oversize",
  DarKalip: "Dar Kalıp",
  GenisKalip: "Geniş Kalıp",
};

function mapFit(apiFit: string | null): FitType | undefined {
  if (!apiFit) return undefined;
  return FIT_LABELS[apiFit];
}

function mapListItem(item: ApiProductListItem): Product {
  return {
    id: item.slug,
    name: item.name,
    category: item.category as Product["category"],
    subCategory: item.subCategory,
    price: item.price,
    salePrice: item.salePrice ?? undefined,
    description: "",
    fabric: "",
    careInstructions: "",
    fit: mapFit(item.fit),
    variants: item.variants,
    isNew: item.isNew,
    isBestSeller: item.isBestSeller,
  };
}

function mapSizeChart(rows: ApiSizeChartRow[]): SizeChartRow[] {
  return rows.map((row) => ({
    size: row.size,
    chest: row.chest ?? undefined,
    waist: row.waist ?? undefined,
    length: row.length ?? undefined,
  }));
}

function mapDetail(item: ApiProductDetail): Product {
  return {
    ...mapListItem(item),
    description: item.description,
    fabric: item.fabric,
    careInstructions: item.careInstructions,
    modelInfo: item.modelInfo ?? undefined,
    sizeChart: item.sizeChart.length > 0 ? mapSizeChart(item.sizeChart) : undefined,
    outfitProducts: item.outfitProducts.map(mapListItem),
    similarProducts: item.similarProducts.map(mapListItem),
  };
}

// Next.js'in fetch'i isteğe bağlı olarak sonuçları önbelleğe alır.
// Geliştirme sırasında her zaman güncel veri görmek için önbelleği kapatıyoruz.
async function apiFetch<T>(path: string): Promise<T | null> {
  try {
    const res = await fetch(`${API_BASE_URL}${path}`, { cache: "no-store" });
    if (res.status === 404) return null;
    if (!res.ok) {
      throw new Error(`API hatası: ${res.status} ${res.statusText} (${path})`);
    }
    return (await res.json()) as T;
  } catch (error) {
    console.error(`Backend'e ulaşılamadı (${path}):`, error);
    // Backend çalışmıyorsa sayfanın tamamen çökmesindense boş veri
    // dönmesi tercih edildi — sayfa "ürün bulunamadı" gösterir.
    return null;
  }
}

export async function getCategories(): Promise<ApiCategory[]> {
  const data = await apiFetch<ApiCategory[]>("/api/categories");
  return data ?? [];
}

export async function getProductsByCategory(categorySlug: string): Promise<Product[]> {
  const data = await apiFetch<ApiProductListItem[]>(`/api/products/category/${categorySlug}`);
  return (data ?? []).map(mapListItem);
}

export async function getNewArrivals(): Promise<Product[]> {
  const data = await apiFetch<ApiProductListItem[]>("/api/products/new-arrivals");
  return (data ?? []).map(mapListItem);
}

export async function getBestSellers(): Promise<Product[]> {
  const data = await apiFetch<ApiProductListItem[]>("/api/products/best-sellers");
  return (data ?? []).map(mapListItem);
}

export async function getProductBySlug(slug: string): Promise<Product | null> {
  const data = await apiFetch<ApiProductDetail>(`/api/products/${slug}`);
  return data ? mapDetail(data) : null;
}
