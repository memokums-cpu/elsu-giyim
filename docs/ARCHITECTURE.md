# Mimari Kararlar

## Genel Yapı

Frontend ve backend tamamen ayrı projeler. Frontend hiçbir zaman doğrudan
veritabanına bağlanmaz, her şey backend API üzerinden geçer.

```
ELSUGIYIM/
├── frontend/     → Next.js 16 (App Router, TypeScript, Tailwind)
├── backend/      → ASP.NET Core 10, katmanlı mimari
└── docs/         → Bu dokümanlar
```

## Backend Katmanları

```
ElsuGiyim.Domain/         → Entity'ler, framework'ten bağımsız saf iş modelleri
ElsuGiyim.Application/    → DTO'lar, servis arayüzleri ve iş mantığı
ElsuGiyim.Infrastructure/ → EF Core, PostgreSQL, dış servis entegrasyonları
ElsuGiyim.API/            → Controller'lar, DI yapılandırması, middleware
```

Bağımlılık yönü: `API → Infrastructure → Application → Domain`.
Application katmanı, `IApplicationDbContext` arayüzü üzerinden veritabanına
erişir — EF Core'un kendisine doğrudan bağımlı değildir.

## Veritabanı Şeması (mevcut durum)

- **Category** — kategoriler, kendi kendine referans veren alt kategori desteğiyle
- **Product** — ürün; `SizeChartJson` alanında beden tablosu JSON olarak saklanır (basit tutmak için; ileride ayrı tabloya taşınabilir)
- **ProductVariant** — her biri kendi SKU'suna ve stoğuna sahip beden+renk kombinasyonu
- **ProductImage** — ürün görselleri (şu an boş, Aşama 4'te yönetim panelinden yüklenecek)
- **ProductRelation** — ürünler arası ilişki (Outfit = kombin önerisi, Similar = benzer ürün)
- **InventoryMovement** — stok hareketi geçmişi (şu an sadece şema var, kullanılmıyor — Aşama 4'te devreye girecek)

Henüz eklenmedi (ileride): Users, Roles, Orders, OrderItems, Payments,
Coupons, Favorites, Reviews, Returns, ShippingMethods, AuditLogs.

## Frontend Veri Akışı

Sayfalar (server component'ler) `lib/api.ts` üzerinden backend'e fetch
isteği atar. Backend'in DTO formatı ile frontend'in `types/product.ts`
içindeki görünüm modeli farklı şekillerde olabilir; `lib/api.ts` bu
dönüşümü (mapping) yapar. Böylece backend'in alan adları değişirse sadece
bu dosya güncellenir, component'lere dokunulmaz.

`export const dynamic = "force-dynamic"` her sayfada kullanılıyor, çünkü
geliştirme aşamasında veri sık değiştiği için statik önbellekleme
istemiyoruz. Canlıya geçerken bu, performans ihtiyacına göre
(`revalidate` süresi vb.) yeniden değerlendirilecek.

## Kimlik Doğrulama ve Yetkilendirme

Henüz yok. Aşama 4/5'te eklenecek: JWT tabanlı kimlik doğrulama, rol
tabanlı yetkilendirme (Admin, Ürün Yöneticisi, Sipariş Yöneticisi).

## Ödeme

Henüz yok. Aşama 6'da eklenecek — adaptör deseniyle (ödeme sağlayıcısına
özel kod doğrudan sipariş akışına gömülmeyecek), sandbox/test ortamıyla
başlanacak.
