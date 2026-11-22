"use client";

import type { SortOption } from "@/types/product";

const OPTIONS: { value: SortOption; label: string }[] = [
  { value: "onerilen", label: "Önerilenler" },
  { value: "en-yeni", label: "En Yeniler" },
  { value: "fiyat-artan", label: "Fiyat (Artan)" },
  { value: "fiyat-azalan", label: "Fiyat (Azalan)" },
  { value: "cok-satan", label: "En Çok Satanlar" },
];

interface SortDropdownProps {
  value: SortOption;
  onChange: (value: SortOption) => void;
}

export default function SortDropdown({ value, onChange }: SortDropdownProps) {
  return (
    <select
      value={value}
      onChange={(e) => onChange(e.target.value as SortOption)}
      className="border border-soft bg-background px-3 py-2 text-sm"
    >
      {OPTIONS.map((option) => (
        <option key={option.value} value={option.value}>
          {option.label}
        </option>
      ))}
    </select>
  );
}
