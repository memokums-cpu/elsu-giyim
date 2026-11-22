"use client";

import { useMemo, useState } from "react";
import type { Product, SortOption, FitType } from "@/types/product";
import FilterSidebar, { type FilterState } from "@/components/FilterSidebar";
import SortDropdown from "@/components/SortDropdown";
import ProductCard from "@/components/ProductCard";
import { filterProducts, sortProducts } from "@/lib/filter-products";

interface CategoryPageClientProps {
  title: string;
  products: Product[];
}

export default function CategoryPageClient({
  title,
  products,
}: CategoryPageClientProps) {
  const priceCeiling = Math.max(...products.map((p) => p.salePrice ?? p.price), 1000);

  const availableSizes = Array.from(
    new Set(products.flatMap((p) => p.variants.map((v) => v.size)))
  );
  const availableColors = Array.from(
    new Map(
      products.flatMap((p) => p.variants.map((v) => [v.color, { name: v.color, hex: v.colorHex }] as const))
    ).values()
  );
  const availableFits = Array.from(
    new Set(products.map((p) => p.fit).filter((f): f is FitType => Boolean(f)))
  );

  const [filters, setFilters] = useState<FilterState>({
    sizes: [],
    colors: [],
    fits: [],
    maxPrice: priceCeiling,
    inStockOnly: false,
  });
  const [sort, setSort] = useState<SortOption>("onerilen");

  const visibleProducts = useMemo(
    () => sortProducts(filterProducts(products, filters), sort),
    [products, filters, sort]
  );

  return (
    <div className="max-w-7xl mx-auto px-8 py-12">
      <div className="flex items-center justify-between mb-8">
        <h1 className="font-display text-3xl">{title}</h1>
        <SortDropdown value={sort} onChange={setSort} />
      </div>

      <div className="flex flex-col md:flex-row gap-10">
        <FilterSidebar
          availableSizes={availableSizes}
          availableColors={availableColors}
          availableFits={availableFits}
          priceCeiling={priceCeiling}
          onChange={setFilters}
        />

        <div className="flex-1">
          {visibleProducts.length === 0 ? (
            <p className="text-neutral-500 text-sm">
              Bu filtrelere uyan ürün bulunamadı.
            </p>
          ) : (
            <div className="grid grid-cols-2 md:grid-cols-3 gap-x-6 gap-y-10">
              {visibleProducts.map((product) => (
                <ProductCard key={product.id} product={product} />
              ))}
            </div>
          )}
        </div>
      </div>
    </div>
  );
}
