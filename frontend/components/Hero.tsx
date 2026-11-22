import Link from "next/link";

export default function Hero() {
  return (
    <section className="grid grid-cols-1 md:grid-cols-[1.1fr_1fr] items-center gap-10 max-w-7xl mx-auto px-8 py-16 md:py-24">
      <div>
        <h1 className="font-display font-light text-5xl md:text-7xl leading-[1.02]">
          Sade çizgiler,
          <br />
          kalıcı giyim.
        </h1>
        <p className="mt-6 max-w-md text-base leading-relaxed text-neutral-600">
          Elsu, mevsim fark etmeksizin dolabında kalacak parçalar üzerine
          kurulu bir giyim markası. Yeni sezon koleksiyonu şimdi mağazada.
        </p>
        <Link
          href="/kategori/yeni-sezon"
          className="inline-block mt-8 px-7 py-3.5 bg-ink text-background text-sm"
        >
          Koleksiyonu Keşfet
        </Link>
      </div>

      {/* Demo görsel alanı — gerçek ürün fotoğrafı Aşama sonrası eklenecek */}
      <div className="aspect-[3/4] bg-gradient-to-br from-soft to-accent/60 relative">
        <span className="absolute bottom-4 left-4 bg-white px-3 py-2 text-xs">
          Yeni Sezon — Demo Görsel
        </span>
      </div>
    </section>
  );
}
