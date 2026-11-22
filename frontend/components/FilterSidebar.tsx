"use client";

import { useState } from "react";
import type { FitType } from "@/types/product";

export interface FilterState {
  sizes: string[];
  colors: string[];
  maxPrice: number;
  fits: FitType[];
  inStockOnly: boolean;
}

interface FilterSidebarProps {
  availableSizes: string[];
  availableColors: { name: string; hex: string }[];
  availableFits: FitType[];
  priceCeiling: number;
  onChange: (filters: FilterState) => void;
}

export default function FilterSidebar({
  availableSizes,
  availableColors,
  availableFits,
  priceCeiling,
  onChange,
}: FilterSidebarProps) {
  const [sizes, setSizes] = useState<string[]>([]);
  const [colors, setColors] = useState<string[]>([]);
  const [fits, setFits] = useState<FitType[]>([]);
  const [maxPrice, setMaxPrice] = useState(priceCeiling);
  const [inStockOnly, setInStockOnly] = useState(false);

  function emit(next: Partial<FilterState>) {
    const merged: FilterState = {
      sizes,
      colors,
      fits,
      maxPrice,
      inStockOnly,
      ...next,
    };
    onChange(merged);
  }

  function toggle<T>(list: T[], value: T): T[] {
    return list.includes(value) ? list.filter((v) => v !== value) : [...list, value];
  }

  return (
    <aside className="w-full md:w-56 flex-none space-y-8 text-sm">
      <div>
        <h3 className="font-medium mb-3">Beden</h3>
        <div className="flex flex-wrap gap-2">
          {availableSizes.map((size) => {
            const active = sizes.includes(size);
            return (
              <button
                key={size}
                onClick={() => {
                  const next = toggle(sizes, size);
                  setSizes(next);
                  emit({ sizes: next });
                }}
                className={`px-3 py-1.5 border text-xs ${
                  active
                    ? "border-ink bg-ink text-background"
                    : "border-soft text-ink"
                }`}
              >
                {size}
              </button>
            );
          })}
        </div>
      </div>

      <div>
        <h3 className="font-medium mb-3">Renk</h3>
        <div className="flex flex-wrap gap-2">
          {availableColors.map((color) => {
            const active = colors.includes(color.name);
            return (
              <button
                key={color.name}
                onClick={() => {
                  const next = toggle(colors, color.name);
                  setColors(next);
                  emit({ colors: next });
                }}
                title={color.name}
                className={`w-7 h-7 rounded-full border-2 ${
                  active ? "border-accent" : "border-transparent"
                }`}
                style={{ backgroundColor: color.hex }}
              />
            );
          })}
        </div>
      </div>

      {availableFits.length > 0 && (
        <div>
          <h3 className="font-medium mb-3">Kalıp</h3>
          <div className="space-y-2">
            {availableFits.map((fit) => (
              <label key={fit} className="flex items-center gap-2 cursor-pointer">
                <input
                  type="checkbox"
                  checked={fits.includes(fit)}
                  onChange={() => {
                    const next = toggle(fits, fit);
                    setFits(next);
                    emit({ fits: next });
                  }}
                />
                {fit}
              </label>
            ))}
          </div>
        </div>
      )}

      <div>
        <h3 className="font-medium mb-3">Fiyat (en çok {maxPrice.toLocaleString("tr-TR")} TL)</h3>
        <input
          type="range"
          min={0}
          max={priceCeiling}
          step={100}
          value={maxPrice}
          onChange={(e) => {
            const value = Number(e.target.value);
            setMaxPrice(value);
            emit({ maxPrice: value });
          }}
          className="w-full accent-accent"
        />
      </div>

      <label className="flex items-center gap-2 cursor-pointer">
        <input
          type="checkbox"
          checked={inStockOnly}
          onChange={() => {
            const next = !inStockOnly;
            setInStockOnly(next);
            emit({ inStockOnly: next });
          }}
        />
        Sadece stokta olanlar
      </label>
    </aside>
  );
}
