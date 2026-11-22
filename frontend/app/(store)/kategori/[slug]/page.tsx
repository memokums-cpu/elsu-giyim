import { notFound } from "next/navigation";
import Header from "@/components/Header";
import Footer from "@/components/Footer";
import CategoryPageClient from "@/components/CategoryPageClient";
import { getProductsByCategory } from "@/lib/api";

const CATEGORY_TITLES: Record<string, string> = {
  kadin: "Kadın",
  erkek: "Erkek",
  ayakkabi: "Ayakkabı",
  aksesuar: "Aksesuar",
};

export const dynamic = "force-dynamic";

interface CategoryPageProps {
  params: Promise<{ slug: string }>;
}

export default async function CategoryPage({ params }: CategoryPageProps) {
  const { slug } = await params;
  const title = CATEGORY_TITLES[slug];

  if (!title) {
    notFound();
  }

  const products = await getProductsByCategory(slug);

  return (
    <>
      <Header />
      <CategoryPageClient title={title} products={products} />
      <Footer />
    </>
  );
}
