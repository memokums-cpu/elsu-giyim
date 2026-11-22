import Link from "next/link";
import type { Product } from "@/types/product";
import ProductCard from "@/components/ProductCard";

interface ProductRailProps {
  title: string;
  viewAllHref: string;
  products: Product[];
}

export default function ProductRail({
  title,
  viewAllHref,
  products,
}: ProductRailProps) {
  return (
    <section className="max-w-7xl mx-auto px-8 pb-20">
      <div className="flex items-baseline justify-between pb-6">
        <h2 className="font-display text-2xl">{title}</h2>
        <Link href={viewAllHref} className="text-sm text-accent">
          Tümünü Gör
        </Link>
      </div>

      <div className="flex gap-5 overflow-x-auto no-scrollbar">
        {products.map((product) => (
          <ProductCard key={product.id} product={product} className="flex-none w-60" />
        ))}
      </div>
    </section>
  );
}
