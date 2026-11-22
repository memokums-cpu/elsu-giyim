# Elsu Giyim — Frontend (Aşama 1)

Bu klasör, Elsu Giyim e-ticaret platformunun frontend kısmının ilk aşamasıdır:
proje kurulumu + marka tasarımı + ana sayfa.

Backend, veritabanı, gerçek ödeme ve üyelik sistemi **henüz yok** — bunlar
sonraki aşamalarda eklenecek.

## Kurulum

Bu dosyaları bilgisayarındaki `elsugiyim-frontend` klasörüne koy, ardından
terminalde o klasöre gidip:

```bash
npm install
```

komutunu çalıştır. Bu, `package.json` içindeki Next.js, React, Tailwind ve
lucide-react paketlerini indirir.

## Geliştirme sunucusunu çalıştırma

```bash
npm run dev
```

Tarayıcıda `http://localhost:3000` adresini aç. Ana sayfayı göreceksin.

## Klasör yapısı

```
app/
  layout.tsx          → Kök düzen, fontlar (Fraunces + Inter)
  globals.css         → Global stiller ve Tailwind direktifleri
  (store)/
    page.tsx           → Ana sayfa
components/
  Header.tsx           → Üst navigasyon
  Hero.tsx             → Kampanya alanı
  CategoryGrid.tsx      → Kategori kartları
  ProductRail.tsx       → Yatay kaydırmalı ürün listesi
  Banner.tsx            → Kombin önerisi bandı
  Footer.tsx            → Alt bilgi
types/
  product.ts            → Geçici (demo) ürün tipi
```

## Önemli notlar

- Ürün verileri (`NEW_ARRIVALS`, `BEST_SELLERS`) şu an `app/(store)/page.tsx`
  içinde sabit kodlanmış **demo verilerdir**. Aşama 3'te backend API'sinden
  gelecek şekilde değiştirilecek.
- Ürün görselleri şu an düz renkli kutular (`bg-neutral-200` vb.). Gerçek
  fotoğraflar eklendiğinde `next/image` ile değiştirilecek.
- Tailwind renk paleti `tailwind.config.ts` içinde marka renkleriyle
  tanımlı: `background`, `ink`, `soft`, `accent`.

## Sıradaki adım

Aşama 2: ürün listeleme, kategori sayfaları, filtreleme ve ürün detay
sayfasının geliştirilmesi.
