import type { Product } from "@/types/product";

// DEMO VERİDİR. Gerçek ürünler ve fotoğraflar Aşama 3'te backend/veritabanı
// üzerinden eklenecek. Fiyatlar ve stok sayıları örnektir.

export const DEMO_PRODUCTS: Product[] = [
  {
    id: "keten-gomlek",
    name: "Keten Gömlek",
    category: "erkek",
    subCategory: "Gömlek",
    price: 1490,
    description:
      "Nefes alabilen %100 keten kumaştan üretilmiş, yaz aylarına uygun rahat kesim gömlek.",
    fabric: "%100 Keten",
    careInstructions: "30°C'de yıkayın, düşük ısıda ütüleyin, çamaşır suyu kullanmayın.",
    fit: "Regular Fit",
    modelInfo: "Manken 186 cm, 76 kg, M beden giyiyor.",
    sizeChart: [
      { size: "S", chest: "96-100 cm", length: "72 cm" },
      { size: "M", chest: "100-104 cm", length: "74 cm" },
      { size: "L", chest: "104-108 cm", length: "76 cm" },
      { size: "XL", chest: "108-112 cm", length: "78 cm" },
    ],
    variants: [
      { sku: "KG-BEJ-S", size: "S", color: "Bej", colorHex: "#D8D3CA", stock: 6 },
      { sku: "KG-BEJ-M", size: "M", color: "Bej", colorHex: "#D8D3CA", stock: 0 },
      { sku: "KG-BEJ-L", size: "L", color: "Bej", colorHex: "#D8D3CA", stock: 9 },
      { sku: "KG-BEYAZ-S", size: "S", color: "Beyaz", colorHex: "#F5F5F2", stock: 4 },
      { sku: "KG-BEYAZ-M", size: "M", color: "Beyaz", colorHex: "#F5F5F2", stock: 12 },
    ],
    outfitProductIds: ["klasik-kot-pantolon", "deri-canta"],
    similarProductIds: ["oversize-blazer"],
    isNew: true,
  },
  {
    id: "oversize-blazer",
    name: "Oversize Blazer",
    category: "kadin",
    subCategory: "Ceket",
    price: 2390,
    salePrice: 1990,
    description:
      "Yapılandırılmamış omuzlara sahip, günlük ve ofis kombinlerine uyum sağlayan oversize blazer.",
    fabric: "%70 Yün, %30 Polyester",
    careInstructions: "Kuru temizlemeye verin.",
    fit: "Oversize",
    modelInfo: "Manken 178 cm, 58 kg, S beden giyiyor.",
    sizeChart: [
      { size: "XS", chest: "88-92 cm" },
      { size: "S", chest: "92-96 cm" },
      { size: "M", chest: "96-100 cm" },
      { size: "L", chest: "100-104 cm" },
    ],
    variants: [
      { sku: "OB-SIYAH-XS", size: "XS", color: "Siyah", colorHex: "#202020", stock: 3 },
      { sku: "OB-SIYAH-S", size: "S", color: "Siyah", colorHex: "#202020", stock: 7 },
      { sku: "OB-SIYAH-M", size: "M", color: "Siyah", colorHex: "#202020", stock: 2 },
      { sku: "OB-KREM-S", size: "S", color: "Krem", colorHex: "#EDE8DD", stock: 0 },
    ],
    outfitProductIds: ["klasik-kot-pantolon", "sneaker"],
    similarProductIds: ["trenckot"],
    isNew: true,
    isBestSeller: true,
  },
  {
    id: "pamuklu-tisort",
    name: "Pamuklu Tişört",
    category: "erkek",
    subCategory: "Tişört",
    price: 690,
    description: "Günlük kullanıma uygun, %100 organik pamuktan temel tişört.",
    fabric: "%100 Organik Pamuk",
    careInstructions: "40°C'de yıkayın.",
    fit: "Regular Fit",
    modelInfo: "Manken 184 cm, 74 kg, M beden giyiyor.",
    sizeChart: [
      { size: "S", chest: "94-98 cm" },
      { size: "M", chest: "98-102 cm" },
      { size: "L", chest: "102-106 cm" },
      { size: "XL", chest: "106-110 cm" },
    ],
    variants: [
      { sku: "PT-SIYAH-S", size: "S", color: "Siyah", colorHex: "#202020", stock: 15 },
      { sku: "PT-SIYAH-M", size: "M", color: "Siyah", colorHex: "#202020", stock: 20 },
      { sku: "PT-BEYAZ-M", size: "M", color: "Beyaz", colorHex: "#F5F5F2", stock: 18 },
      { sku: "PT-BEYAZ-L", size: "L", color: "Beyaz", colorHex: "#F5F5F2", stock: 5 },
    ],
    isNew: true,
  },
  {
    id: "deri-canta",
    name: "Deri Çanta",
    category: "aksesuar",
    subCategory: "Çanta",
    price: 3190,
    description: "El yapımı hakiki deri, günlük kullanıma uygun omuz çantası.",
    fabric: "Hakiki Deri",
    careInstructions: "Nemden koruyun, deri bakım kremi ile temizleyin.",
    variants: [
      { sku: "DC-KAHVE-TEK", size: "Tek Ebat", color: "Kahverengi", colorHex: "#6B4A32", stock: 8 },
      { sku: "DC-SIYAH-TEK", size: "Tek Ebat", color: "Siyah", colorHex: "#202020", stock: 4 },
    ],
    isNew: true,
  },
  {
    id: "yun-kazak",
    name: "Yün Kazak",
    category: "kadin",
    subCategory: "Sweatshirt",
    price: 1890,
    description: "Yumuşak merinos yünden örülmüş, bol kesim kazak.",
    fabric: "%100 Merinos Yün",
    careInstructions: "Elde, soğuk suda yıkayın.",
    fit: "Geniş Kalıp",
    modelInfo: "Manken 175 cm, 55 kg, S beden giyiyor.",
    sizeChart: [
      { size: "S", chest: "90-96 cm" },
      { size: "M", chest: "96-102 cm" },
      { size: "L", chest: "102-108 cm" },
    ],
    variants: [
      { sku: "YK-GRI-S", size: "S", color: "Gri", colorHex: "#9C9A94", stock: 10 },
      { sku: "YK-GRI-M", size: "M", color: "Gri", colorHex: "#9C9A94", stock: 6 },
      { sku: "YK-BORDO-M", size: "M", color: "Bordo", colorHex: "#6E2A32", stock: 0 },
    ],
  },
  {
    id: "klasik-kot-pantolon",
    name: "Klasik Kot Pantolon",
    category: "erkek",
    subCategory: "Pantolon",
    price: 1190,
    description: "Zamansız kesim, orta bel, düz paça kot pantolon.",
    fabric: "%98 Pamuk, %2 Elastan",
    careInstructions: "30°C'de ters çevirerek yıkayın.",
    fit: "Regular Fit",
    modelInfo: "Manken 186 cm, 76 kg, 32 beden giyiyor.",
    sizeChart: [
      { size: "30", waist: "76-80 cm", length: "102 cm" },
      { size: "32", waist: "81-85 cm", length: "104 cm" },
      { size: "34", waist: "86-90 cm", length: "106 cm" },
      { size: "36", waist: "91-95 cm", length: "108 cm" },
    ],
    variants: [
      { sku: "KKP-INDIGO-30", size: "30", color: "İndigo", colorHex: "#38506B", stock: 5 },
      { sku: "KKP-INDIGO-32", size: "32", color: "İndigo", colorHex: "#38506B", stock: 11 },
      { sku: "KKP-INDIGO-34", size: "34", color: "İndigo", colorHex: "#38506B", stock: 0 },
    ],
    isBestSeller: true,
  },
  {
    id: "trenckot",
    name: "Trençkot",
    category: "kadin",
    subCategory: "Mont ve Ceket",
    price: 3490,
    description: "Su iticiliği olan kumaştan üretilmiş, kemerli klasik trençkot.",
    fabric: "%65 Pamuk, %35 Polyester",
    careInstructions: "Kuru temizlemeye verin.",
    fit: "Regular Fit",
    modelInfo: "Manken 178 cm, 58 kg, S beden giyiyor.",
    sizeChart: [
      { size: "S", chest: "88-92 cm" },
      { size: "M", chest: "92-96 cm" },
      { size: "L", chest: "96-100 cm" },
    ],
    variants: [
      { sku: "TK-BEJ-S", size: "S", color: "Bej", colorHex: "#D8D3CA", stock: 4 },
      { sku: "TK-BEJ-M", size: "M", color: "Bej", colorHex: "#D8D3CA", stock: 6 },
    ],
    isBestSeller: true,
  },
  {
    id: "sneaker",
    name: "Sneaker",
    category: "ayakkabi",
    subCategory: "Ayakkabı",
    price: 2290,
    description: "Günlük kullanıma uygun, hafif tabanlı deri sneaker.",
    fabric: "Hakiki Deri, Kauçuk Taban",
    careInstructions: "Nemli bezle silerek temizleyin.",
    variants: [
      { sku: "SN-BEYAZ-40", size: "40", color: "Beyaz", colorHex: "#F5F5F2", stock: 3 },
      { sku: "SN-BEYAZ-41", size: "41", color: "Beyaz", colorHex: "#F5F5F2", stock: 7 },
      { sku: "SN-BEYAZ-42", size: "42", color: "Beyaz", colorHex: "#F5F5F2", stock: 9 },
      { sku: "SN-BEYAZ-43", size: "43", color: "Beyaz", colorHex: "#F5F5F2", stock: 0 },
    ],
    isBestSeller: true,
  },
  {
    id: "mini-elbise",
    name: "Mini Elbise",
    category: "kadin",
    subCategory: "Elbise",
    price: 1690,
    description: "Vücudu saran kesim, günlük ve özel günlere uygun mini elbise.",
    fabric: "%95 Viskon, %5 Elastan",
    careInstructions: "30°C'de hassas yıkayın.",
    fit: "Slim Fit",
    modelInfo: "Manken 178 cm, 58 kg, S beden giyiyor.",
    sizeChart: [
      { size: "XS", chest: "82-86 cm" },
      { size: "S", chest: "86-90 cm" },
      { size: "M", chest: "90-94 cm" },
    ],
    variants: [
      { sku: "ME-SIYAH-XS", size: "XS", color: "Siyah", colorHex: "#202020", stock: 2 },
      { sku: "ME-SIYAH-S", size: "S", color: "Siyah", colorHex: "#202020", stock: 5 },
    ],
    isBestSeller: true,
  },
];

export function getProductById(id: string): Product | undefined {
  return DEMO_PRODUCTS.find((p) => p.id === id);
}

export function getProductsByCategory(category: string): Product[] {
  return DEMO_PRODUCTS.filter((p) => p.category === category);
}

export function getProductsByIds(ids: string[]): Product[] {
  return ids
    .map((id) => getProductById(id))
    .filter((p): p is Product => Boolean(p));
}

export function formatPrice(value: number): string {
  return `${value.toLocaleString("tr-TR")} TL`;
}
