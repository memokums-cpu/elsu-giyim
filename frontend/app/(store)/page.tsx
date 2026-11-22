import Header from "@/components/Header";
import Hero from "@/components/Hero";
import CategoryGrid from "@/components/CategoryGrid";
import ProductRail from "@/components/ProductRail";
import Banner from "@/components/Banner";
import Footer from "@/components/Footer";
import { getNewArrivals, getBestSellers } from "@/lib/api";

// Next.js'in sayfayı derleme zamanında statik üretmesini değil, her
// istekte backend'den taze veri çekmesini istiyoruz.
export const dynamic = "force-dynamic";

export default async function HomePage() {
  const [newArrivals, bestSellers] = await Promise.all([
    getNewArrivals(),
    getBestSellers(),
  ]);

  return (
    <>
      <Header />
      <Hero />
      <CategoryGrid />
      <ProductRail
        title="Yeni Gelenler"
        viewAllHref="/kategori/yeni-gelenler"
        products={newArrivals}
      />
      <Banner
        title="Kombin Önerileri"
        description="Her ürün sayfasında, mankenin giydiği diğer parçaları tek tıkla sepetine ekle."
      />
      <ProductRail
        title="Çok Satanlar"
        viewAllHref="/kategori/cok-satanlar"
        products={bestSellers}
      />
      <Footer />
    </>
  );
}
