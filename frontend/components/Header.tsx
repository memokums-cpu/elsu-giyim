import { Search, User, Heart, ShoppingBag } from "lucide-react";
import Link from "next/link";

const NAV_LINKS = [
  { label: "Kadın", href: "/kategori/kadin" },
  { label: "Erkek", href: "/kategori/erkek" },
  { label: "Ayakkabı", href: "/kategori/ayakkabi" },
  { label: "Aksesuar", href: "/kategori/aksesuar" },
];

export default function Header() {
  return (
    <header className="flex items-center justify-between px-8 py-5 border-b border-soft max-w-7xl mx-auto">
      <Link href="/" className="font-display text-2xl tracking-wide">
        ELSU
      </Link>

      <nav className="hidden md:flex gap-7">
        {NAV_LINKS.map((link) => (
          <Link
            key={link.href}
            href={link.href}
            className="text-sm text-ink hover:text-accent transition-colors"
          >
            {link.label}
          </Link>
        ))}
      </nav>

      <div className="flex items-center gap-5">
        <button aria-label="Ara" className="text-ink hover:text-accent">
          <Search size={18} />
        </button>
        <Link href="/hesabim" aria-label="Hesabım" className="text-ink hover:text-accent">
          <User size={18} />
        </Link>
        <Link href="/favoriler" aria-label="Favoriler" className="text-ink hover:text-accent">
          <Heart size={18} />
        </Link>
        <Link href="/sepet" aria-label="Sepet" className="text-ink hover:text-accent">
          <ShoppingBag size={18} />
        </Link>
      </div>
    </header>
  );
}
