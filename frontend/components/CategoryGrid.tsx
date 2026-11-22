import Link from "next/link";

// Demo veridir; Aşama 3'te yönetim panelinden/kategori API'sinden gelecek.
const CATEGORIES = [
  { label: "Kadın", href: "/kategori/kadin" },
  { label: "Erkek", href: "/kategori/erkek" },
  { label: "Ayakkabı", href: "/kategori/ayakkabi" },
  { label: "Aksesuar", href: "/kategori/aksesuar" },
];

export default function CategoryGrid() {
  return (
    <section className="max-w-7xl mx-auto px-8 pb-20 grid grid-cols-2 md:grid-cols-4 gap-4">
      {CATEGORIES.map((category) => (
        <Link
          key={category.href}
          href={category.href}
          className="aspect-[3/4] bg-soft flex items-end p-4 hover:opacity-90 transition-opacity"
        >
          <span className="font-display text-lg">{category.label}</span>
        </Link>
      ))}
    </section>
  );
}
