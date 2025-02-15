# 🕒 QuartzTimer: Zamanlanmış Görevler ve E-posta Bildirimleri 🚀
QuartzTimer, Quartz.NET kütüphanesi kullanılarak geliştirilmiş, zamanlanmış görevleri yöneten ve otomatik e-posta bildirimleri gönderen bir arka plan servisi uygulamasıdır.
Bu proje, özellikle belirli bir belgeye sahip olmayan öğrencileri tespit ederek, onlara otomatik e-posta göndermek için tasarlanmıştır.
Proje, Entity Framework Core ile veritabanı entegrasyonu ve MailKit ile e-posta gönderimi gibi modern teknolojileri kullanır.

# 🌟 Öne Çıkan Özellikler
## - Zamanlanmış Görevler: Quartz.NET ile belirli zaman aralıklarında otomatik olarak çalışan görevler.
## - E-posta Bildirimleri: Belgesi olmayan öğrencilere otomatik e-posta gönderimi.
## - Veritabanı Entegrasyonu: Entity Framework Core ile veritabanı işlemleri.

# 🛠️ Kurulum ve Çalıştırma
## Gereksinimler
- Visual Studio 2022
- SQL Server
# 🚀 Nasıl Çalışır?
Proje, belirli zaman aralıklarında (cron ifadesi ile belirlenir) otomatik olarak çalışır.
Her çalıştığında, veritabanındaki öğrencilerin belge durumunu kontrol eder.
Eğer bir öğrencinin belgesi yoksa, otomatik olarak bir e-posta gönderir ve öğrencinin durumunu günceller.

# Örnek Senaryo
## Veritabanında Öğrenciler:
- Öğrenci A: Belge yok.
- Öğrenci B: Belge var.
## Zamanlanmış Görev Çalışır:
- Öğrenci A'ya e-posta gönderilir.
- Öğrenci A'nın durumu "Mail Gönderildi" olarak güncellenir.
## Sonuç:
- Öğrenci A artık e-posta aldı ve durumu güncellendi.
- Öğrenci B'nin durumu değişmez.

# ⚙️ Teknik Detaylar
## Kullanılan Teknolojiler
- Quartz.NET: Zamanlanmış görevler için kullanılan kütüphane.
- Entity Framework Core: Veritabanı işlemleri için ORM aracı.
- MailKit: E-posta gönderimi için kullanılan kütüphane.
- ASP.NET Core Dependency Injection: Servislerin yönetimi için kullanılan mekanizma.

## Kod Yapısı
- EmailReminderJob.cs: Zamanlanmış görevin tanımlandığı sınıf.
- JobSchedule.cs: Görev zamanlamalarının yönetildiği sınıf.
- QuartzHostedService.cs: Quartz.NET servislerinin yönetildiği sınıf.
- SingletonJobFactory.cs: Dependency Injection ile job'ların yönetildiği sınıf.
