"use client";

import { useState } from "react";
import { X } from "lucide-react";
import type { SizeChartRow } from "@/types/product";

interface SizeGuideModalProps {
  sizeChart: SizeChartRow[];
}

export default function SizeGuideModal({ sizeChart }: SizeGuideModalProps) {
  const [open, setOpen] = useState(false);

  return (
    <>
      <button
        onClick={() => setOpen(true)}
        className="text-sm underline text-accent"
      >
        Beden Rehberi
      </button>

      {open && (
        <div
          className="fixed inset-0 bg-black/40 flex items-center justify-center z-50 p-4"
          onClick={() => setOpen(false)}
        >
          <div
            className="bg-background max-w-md w-full p-6"
            onClick={(e) => e.stopPropagation()}
          >
            <div className="flex items-center justify-between mb-4">
              <h3 className="font-display text-xl">Beden Rehberi</h3>
              <button onClick={() => setOpen(false)} aria-label="Kapat">
                <X size={18} />
              </button>
            </div>

            <table className="w-full text-sm">
              <thead>
                <tr className="border-b border-soft text-left">
                  <th className="py-2">Beden</th>
                  {sizeChart[0]?.chest && <th className="py-2">Göğüs</th>}
                  {sizeChart[0]?.waist && <th className="py-2">Bel</th>}
                  {sizeChart[0]?.length && <th className="py-2">Boy</th>}
                </tr>
              </thead>
              <tbody>
                {sizeChart.map((row) => (
                  <tr key={row.size} className="border-b border-soft/50">
                    <td className="py-2">{row.size}</td>
                    {row.chest && <td className="py-2">{row.chest}</td>}
                    {row.waist && <td className="py-2">{row.waist}</td>}
                    {row.length && <td className="py-2">{row.length}</td>}
                  </tr>
                ))}
              </tbody>
            </table>

            <p className="mt-4 text-xs text-neutral-500">
              Ölçüler yaklaşık değerlerdir ve kumaş esnekliğine göre ±1-2 cm
              fark gösterebilir.
            </p>
          </div>
        </div>
      )}
    </>
  );
}
