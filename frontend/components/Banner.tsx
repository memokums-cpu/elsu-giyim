interface BannerProps {
  title: string;
  description: string;
}

export default function Banner({ title, description }: BannerProps) {
  return (
    <section className="bg-accent text-background text-center px-8 py-16">
      <h2 className="font-display font-light text-3xl md:text-4xl">{title}</h2>
      <p className="mt-3 opacity-85">{description}</p>
    </section>
  );
}
