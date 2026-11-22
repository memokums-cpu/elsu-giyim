"use client";

import { useMemo, useState } from "react";
import { Heart, ShoppingBag } from "lucide-react";
import type { ProductVariant } from "@/types/product";

interface VariantSelectorProps {
  variants: ProductVariant[];
}

export default function VariantSelector({ variants }: VariantSelectorProps) {
  const colors = useMemo(
    () => Array.from(new Map(variants.map((v) => [v.color, v])).values()),
    [variants]
  );

  const [selectedColor, setSelectedColor] = useState(colors[0]?.color ?? "");
  const [selectedSize, setSelectedSize] = useState<string | null>(null);

  const sizesForColor = variants.filter((v) => v.color === selectedColor);
  const activeVariant = variants.find(
    (v) => v.color === selectedColor && v.size === selectedSize
  );

  const canAddToCart = Boolean(activeVariant && activeVariant.stock > 0);

  return (
    <div className="space-y-6">
      <div>
        <h3 className="text-sm font-medium mb-2">
          Renk: <span className="font-normal">{selectedColor}</span>
        </h3>
        <div className="flex gap-2">
          {colors.map((variant) => (
            <button
              key={variant.color}
              onClick={() => {
                setSelectedColor(variant.color);
                setSelectedSize(null);
              }}
              title={variant.color}
              className={`w-8 h-8 rounded-full border-2 ${
                selectedColor === variant.color
                  ? "border-accent"
                  : "border-transparent"
              }`}
              style={{ backgroundColor: variant.colorHex }}
            />
          ))}
        </div>
      </div>

      <div>
        <h3 className="text-sm font-medium mb-2">Beden</h3>
        <div className="flex flex-wrap gap-2">
          {sizesForColor.map((variant) => {
            const outOfStock = variant.stock === 0;
            return (
              <button
                key={variant.sku}
                disabled={outOfStock}
                onClick={() => setSelectedSize(variant.size)}
                className={`px-4 py-2 border text-sm relative ${
                  outOfStock
                    ? "border-soft text-neutral-400 cursor-not-allowed line-through"
                    : selectedSize === variant.size
                    ? "border-ink bg-ink text-background"
                    : "border-soft"
                }`}
              >
                {variant.size}
              </button>
            );
          })}
        </div>
        {selectedSize && !activeVariant?.stock && (
          <p className="mt-2 text-xs text-accent">
            Bu beden stokta yok. Stok bildirimi talep edebilirsiniz.
          </p>
        )}
      </div>

      <div className="flex gap-3 pt-2">
        <button
          disabled={!canAddToCart}
          className={`flex-1 flex items-center justify-center gap-2 py-3.5 text-sm ${
            canAddToCart
              ? "bg-ink text-background"
              : "bg-soft text-neutral-500 cursor-not-allowed"
          }`}
        >
          <ShoppingBag size={16} />
          {selectedSize
            ? canAddToCart
              ? "Sepete Ekle"
              : "Stokta Yok"
            : "Önce Beden Seçin"}
        </button>
        <button
          aria-label="Favorilere ekle"
          className="w-12 flex items-center justify-center border border-soft"
        >
          <Heart size={16} />
        </button>
      </div>

      <p className="text-xs text-neutral-400">
        Not: Sepet ve favori işlevleri Aşama 5&apos;te etkinleştirilecek —
        şu an yalnızca arayüz hazırlanmıştır.
      </p>
    </div>
  );
}
