# Elsu Giyim — Proje Bağlamı

Bu dosya, Claude Code (veya bu projede çalışan herhangi bir AI asistanı) için
proje hakkında hızlı bağlam sağlar. Detaylı mimari kararlar için
`docs/ARCHITECTURE.md`, ilerleme durumu için `docs/ROADMAP.md`'ye bakın.

## Proje Özeti

Elsu Giyim, kadın/erkek giyim, ayakkabı ve aksesuar satan bir e-ticaret
platformu. Teknoloji yığını:

- **Frontend:** Next.js 16 (App Router), TypeScript, Tailwind CSS, `frontend/` klasöründe
- **Backend:** ASP.NET Core 10 Web API, katmanlı mimari (Domain/Application/Infrastructure/API), `backend/` klasöründe
- **Veritabanı:** PostgreSQL 18, Entity Framework Core + Npgsql

## Çalışma Kuralları

1. Her şeyi tek seferde değiştirmeye çalışma — küçük, test edilebilir adımlarla ilerle.
2. Değişiklik yapmadan önce ilgili dosyaları oku, varsayımda bulunma.
3. Backend'de DTO + servis katmanı + validasyon kullan, controller'lara iş mantığı yazma.
4. Frontend'de TypeScript strict mode'a uy, `any` kullanmaktan kaçın.
5. Gerçek API anahtarları, şifreler asla kod içine veya appsettings.json'a yazılmaz —
   backend için `dotnet user-secrets`, frontend için `.env.local` (git'e eklenmez) kullanılır.
6. Demo/örnek veriler açıkça yorumlarla belirtilir (`// DEMO VERİDİR` gibi).
7. Her değişiklikten sonra ilgili tarafı (frontend: `npm run dev`, backend: `dotnet run`) test et.

## Hızlı Başlangıç

**Backend:**
```bash
cd backend/ElsuGiyim.API
dotnet run
```
API: `http://localhost:5080`

**Frontend:**
```bash
cd frontend
npm run dev
```
Site: `http://localhost:3000`

Backend'in çalışıyor olması gerekir, aksi halde frontend sayfaları boş/veri
olmadan gelir (ürünler backend'den çekiliyor).

## Mevcut Durum

Aşama 3 (Backend — ürün kataloğu) tamamlandı. Sıradaki aşama: Aşama 4
(Yönetim Paneli). Detaylar için `docs/ROADMAP.md`.
