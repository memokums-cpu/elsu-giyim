# Elsu Giyim

Kadın, erkek, ayakkabı ve aksesuar satan bir e-ticaret platformu.

- **Frontend:** Next.js 16, TypeScript, Tailwind CSS — bkz. `frontend/README.md`
- **Backend:** ASP.NET Core 10, PostgreSQL 18, EF Core — bkz. `backend/README.md`
- **Mimari kararlar:** `docs/ARCHITECTURE.md`
- **İlerleme durumu:** `docs/ROADMAP.md`
- **AI asistanları için bağlam:** `CLAUDE.md`

## Hızlı Başlangıç

İkisini de aynı anda çalıştırman gerekiyor (frontend, verisini backend'den çeker):

```bash
# Terminal 1
cd backend/ElsuGiyim.API
dotnet run

# Terminal 2
cd frontend
npm run dev
```

- Site: http://localhost:3000
- API: http://localhost:5080

Detaylı kurulum adımları için `backend/README.md` ve `frontend/README.md`
dosyalarına bakın.
