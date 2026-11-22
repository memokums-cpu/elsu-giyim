import Link from "next/link";

const FOOTER_COLUMNS = [
  {
    title: "Mağaza",
    links: [
      { label: "Kadın", href: "/kategori/kadin" },
      { label: "Erkek", href: "/kategori/erkek" },
      { label: "Ayakkabı", href: "/kategori/ayakkabi" },
      { label: "Aksesuar", href: "/kategori/aksesuar" },
    ],
  },
  {
    title: "Yardım",
    links: [
      { label: "Kargo & İade", href: "/kargo-iade" },
      { label: "Beden Rehberi", href: "/beden-rehberi" },
      { label: "İletişim", href: "/iletisim" },
    ],
  },
  {
    title: "Kurumsal",
    links: [
      { label: "Hakkımızda", href: "/hakkimizda" },
      { label: "Gizlilik Politikası", href: "/gizlilik-politikasi" },
      { label: "KVKK", href: "/kvkk" },
    ],
  },
];

export default function Footer() {
  return (
    <footer className="border-t border-soft px-8 pt-16 pb-10">
      <div className="max-w-7xl mx-auto grid grid-cols-2 md:grid-cols-4 gap-8">
        <div>
          <div className="font-display text-2xl mb-3">ELSU</div>
          <p className="text-sm text-neutral-500 max-w-xs">
            Zamansız parçalarla kurulmuş bir gardırop felsefesi.
          </p>
        </div>

        {FOOTER_COLUMNS.map((column) => (
          <div key={column.title}>
            <h4 className="text-sm mb-3">{column.title}</h4>
            {column.links.map((link) => (
              <Link
                key={link.href}
                href={link.href}
                className="block text-sm text-neutral-500 mb-2"
              >
                {link.label}
              </Link>
            ))}
          </div>
        ))}
      </div>

      <p className="max-w-7xl mx-auto mt-10 text-xs text-neutral-400">
        © 2026 Elsu Giyim — Demo veriler ve yer tutucu metinlerle
        hazırlanmıştır.
      </p>
    </footer>
  );
}
