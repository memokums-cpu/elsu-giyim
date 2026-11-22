"use client";

import { useState } from "react";

interface ProductGalleryProps {
  productName: string;
  imageCount?: number;
}

export default function ProductGallery({
  productName,
  imageCount = 4,
}: ProductGalleryProps) {
  const [active, setActive] = useState(0);
  const images = Array.from({ length: imageCount });

  return (
    <div>
      <div className="aspect-[3/4] bg-neutral-200 flex items-end p-4">
        <span className="text-xs bg-background px-2 py-1">
          {productName} — Demo Görsel {active + 1}/{imageCount}
        </span>
      </div>
      <div className="flex gap-2 mt-3">
        {images.map((_, index) => (
          <button
            key={index}
            onClick={() => setActive(index)}
            className={`w-16 h-20 bg-neutral-200 border ${
              active === index ? "border-accent" : "border-transparent"
            }`}
          />
        ))}
      </div>
    </div>
  );
}
