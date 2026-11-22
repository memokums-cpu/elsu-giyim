import { notFound } from "next/navigation";
import Header from "@/components/Header";
import Footer from "@/components/Footer";
import ProductGallery from "@/components/ProductGallery";
import VariantSelector from "@/components/VariantSelector";
import SizeGuideModal from "@/components/SizeGuideModal";
import ProductCard from "@/components/ProductCard";
import { getProductBySlug } from "@/lib/api";
import { formatPrice } from "@/lib/demo-products";

export const dynamic = "force-dynamic";

interface ProductPageProps {
  params: Promise<{ slug: string }>;
}

export default async function ProductPage({ params }: ProductPageProps) {
  const { slug } = await params;
  const product = await getProductBySlug(slug);

  if (!product) {
    notFound();
  }

  const outfitProducts = product.outfitProducts ?? [];
  const similarProducts = product.similarProducts ?? [];

  return (
    <>
      <Header />

      <div className="max-w-7xl mx-auto px-8 py-12 grid grid-cols-1 md:grid-cols-2 gap-14">
        <ProductGallery productName={product.name} />

        <div>
          <p className="text-xs text-neutral-400 mb-2">
            {product.category.toUpperCase()} / {product.subCategory}
          </p>
          <h1 className="font-display text-3xl">{product.name}</h1>

          <div className="mt-3 flex items-center gap-3 text-lg">
            {product.salePrice ? (
              <>
                <span className="text-accent">{formatPrice(product.salePrice)}</span>
                <span className="text-neutral-400 line-through text-base">
                  {formatPrice(product.price)}
                </span>
              </>
            ) : (
              <span>{formatPrice(product.price)}</span>
            )}
          </div>

          {product.fit && (
            <p className="mt-2 text-sm text-neutral-500">Kalıp: {product.fit}</p>
          )}

          <div className="mt-6">
            <VariantSelector variants={product.variants} />
          </div>

          {product.sizeChart && product.sizeChart.length > 0 && (
            <div className="mt-4">
              <SizeGuideModal sizeChart={product.sizeChart} />
            </div>
          )}

          <div className="mt-8 border-t border-soft pt-6 space-y-4 text-sm">
            <p className="text-neutral-700">{product.description}</p>
            <p>
              <span className="font-medium">Kumaş:</span> {product.fabric}
            </p>
            <p>
              <span className="font-medium">Yıkama Talimatı:</span>{" "}
              {product.careInstructions}
            </p>
            {product.modelInfo && (
              <p>
                <span className="font-medium">Manken Bilgisi:</span>{" "}
                {product.modelInfo}
              </p>
            )}
            <p className="text-neutral-400 text-xs">
              SKU örnekleri:{" "}
              {product.variants.map((v) => v.sku).join(", ")}
            </p>
          </div>

          <div className="mt-6 border-t border-soft pt-6 text-sm text-neutral-500">
            <p>
              Standart kargo 2-4 iş günü içinde teslim edilir. 30 gün
              içinde ücretsiz iade hakkınız bulunmaktadır.
            </p>
          </div>
        </div>
      </div>

      {outfitProducts.length > 0 && (
        <section className="max-w-7xl mx-auto px-8 pb-16">
          <h2 className="font-display text-2xl mb-6">Ürünü Tamamla</h2>
          <div className="grid grid-cols-2 md:grid-cols-4 gap-6">
            {outfitProducts.map((p) => (
              <ProductCard key={p.id} product={p} />
            ))}
          </div>
        </section>
      )}

      {similarProducts.length > 0 && (
        <section className="max-w-7xl mx-auto px-8 pb-16">
          <h2 className="font-display text-2xl mb-6">Benzer Ürünler</h2>
          <div className="grid grid-cols-2 md:grid-cols-4 gap-6">
            {similarProducts.map((p) => (
              <ProductCard key={p.id} product={p} />
            ))}
          </div>
        </section>
      )}

      <section className="max-w-7xl mx-auto px-8 pb-20 border-t border-soft pt-12">
        <h2 className="font-display text-2xl mb-4">Müşteri Yorumları</h2>
        <p className="text-sm text-neutral-400">
          Henüz yorum bulunmuyor. Yorum ve puanlama sistemi Aşama 7&apos;de
          etkinleştirilecek.
        </p>
      </section>

      <Footer />
    </>
  );
}
