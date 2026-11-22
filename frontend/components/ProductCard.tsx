import Link from "next/link";
import type { Product } from "@/types/product";
import { formatPrice } from "@/lib/demo-products";

interface ProductCardProps {
  product: Product;
  className?: string;
}

export default function ProductCard({ product, className = "" }: ProductCardProps) {
  const totalStock = product.variants.reduce((sum, v) => sum + v.stock, 0);
  const isOutOfStock = totalStock === 0;

  return (
    <Link href={`/urun/${product.id}`} className={`group block ${className}`}>
      <div className="aspect-[3/4] bg-neutral-200 relative overflow-hidden">
        {isOutOfStock && (
          <span className="absolute top-3 left-3 bg-background px-2.5 py-1 text-xs">
            Stokta Yok
          </span>
        )}
        {product.salePrice && !isOutOfStock && (
          <span className="absolute top-3 left-3 bg-accent text-background px-2.5 py-1 text-xs">
            İndirim
          </span>
        )}
      </div>
      <h3 className="mt-3 text-sm font-medium">{product.name}</h3>
      <div className="mt-1 flex items-center gap-2 text-sm">
        {product.salePrice ? (
          <>
            <span className="text-accent">{formatPrice(product.salePrice)}</span>
            <span className="text-neutral-400 line-through">
              {formatPrice(product.price)}
            </span>
          </>
        ) : (
          <span className="text-neutral-500">{formatPrice(product.price)}</span>
        )}
      </div>
    </Link>
  );
}
