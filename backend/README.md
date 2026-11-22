# Elsu Giyim — Backend (Aşama 3)

ASP.NET Core 10 Web API, katmanlı mimari (Domain / Application / Infrastructure / API)
ve PostgreSQL ile ürün kataloğu backend'i.

Bu aşamada **sadece ürün ve kategori** API'leri var. Sipariş, sepet, kullanıcı
girişi, ödeme ve yönetim paneli API'leri sonraki aşamalarda eklenecek.

## Önkoşullar

- .NET 10 SDK (`dotnet --version` → 10.x göstermeli)
- PostgreSQL 18 (`psql --version` → 18.x göstermeli)
- `dotnet-ef` aracı (aşağıda kurulum adımı var)

## 1. PostgreSQL'de veritabanını oluştur

pgAdmin 4'ü aç, PostgreSQL sunucusuna (kurulumda belirlediğin şifreyle) bağlan,
sol taraftan "Databases" üzerine sağ tıkla → Create → Database. İsim olarak:

```
elsugiyim
```

yaz ve kaydet.

## 2. Bağlantı dizesini User Secrets'a ekle

**Bağlantı dizesi (ve içindeki şifre) hiçbir zaman kod içine veya git'e
yazılmayacak.** Bunun yerine .NET'in "User Secrets" özelliğini kullanıyoruz —
bu, şifreni sadece kendi bilgisayarında, proje klasörünün dışında bir yerde
saklar.

Terminalde `ElsuGiyim.API` klasörüne gidip şunları çalıştır (POSTGRES_SIFREN
yerine kurulumda belirlediğin gerçek şifreyi yaz):

```bash
cd backend/ElsuGiyim.API
dotnet user-secrets init
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Host=localhost;Port=5432;Database=elsugiyim;Username=postgres;Password=POSTGRES_SIFREN"
```

## 3. EF Core araçlarını kur (bir kere, global)

```bash
dotnet tool install --global dotnet-ef
```

Zaten kuruluysa "already installed" gibi bir mesaj görürsün, sorun değil.

## 4. Migration oluştur ve veritabanını güncelle

Çözüm klasöründe (`backend/` içinde, `ElsuGiyim.sln`'in yanında):

```bash
dotnet ef migrations add InitialCreate --project ElsuGiyim.Infrastructure --startup-project ElsuGiyim.API
```

Bu komut `ElsuGiyim.Infrastructure/Migrations` klasörünü oluşturacak.

**Not:** API'yi çalıştırdığında (`dotnet run`), geliştirme ortamında migration'lar
otomatik uygulanacak şekilde ayarladım (`Program.cs` içinde `db.Database.Migrate()`),
yani ayrıca `dotnet ef database update` çalıştırmana gerek yok — API ilk
çalıştığında veritabanı tablolarını ve demo verileri kendisi oluşturacak.

## 5. API'yi çalıştır

```bash
cd ElsuGiyim.API
dotnet run
```

Terminalde `Now listening on: http://localhost:5080` gibi bir satır görünce,
tarayıcıda şu adresleri dene:

- `http://localhost:5080/api/categories` → 4 demo kategori dönmeli
- `http://localhost:5080/api/products/category/erkek` → erkek kategorisindeki demo ürünler
- `http://localhost:5080/api/products/keten-gomlek` → tek ürün detayı
- `http://localhost:5080/openapi/v1.json` → otomatik oluşturulan API şeması

## Önemli Notlar

- Veritabanındaki demo veriler (3 ürün, 4 kategori) kod içinde (`DemoDataSeeder.cs`)
  tanımlı — gerçek ürünler Aşama 4'teki yönetim paneliyle eklenecek.
- Şu an filtreleme (beden, renk, fiyat) hâlâ frontend'de istemci tarafında
  çalışıyor. Bu endpoint'ler ileride query parametreleriyle genişletilip
  filtreleme backend'e taşınacak.
- CORS, şu an sadece `http://localhost:3000`'e (Next.js dev sunucusu) izin
  veriyor. Canlıya geçerken `appsettings.json` içindeki `AllowedOrigins`
  gerçek domain ile değiştirilecek.
- Kimlik doğrulama (giriş, roller) henüz yok — bu Aşama 4/5'te eklenecek.
  Şu an tüm endpoint'ler herkese açık (sadece okuma/GET).
