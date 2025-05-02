# Acer Pro Test Proje Dokümantasyonu

## Genel Bakýþ
Bu depo, bir Razor Pages projesi (`AP.UI`) ve bir API projesi (`AP.API`) içermektedir. 
Aþaðýda Git iþ akýþý, kurulum talimatlarý ve proje iþleyiþi hakkýnda detaylar bulabilirsiniz.

---


### Katmanlar
- **Veritabaný**: `AP.Generic` - Veritabaný baðlantý ayarlarý ve yapýlandýrmalarý.
- **API**: `AP.API` - API baþlangýç ve yönetim katmaný. Standalone olarak çalýþmaktadýr.
- **UI**: `AP.UI` - Uygulamanýn User Interface'i.
- **AP.Data **: `AP.Data` - Veritabaný ile ilgili tüm kodlar bu katmanda koþmaktadýr.
- **AP.Presentation**: `AP.Presentation` - API Controller katmaný .
- **AP.Utils**: `AP.Utils` - API Helper sýnýflarý- JWT yönetimi .
- 
 
---

## Kurulum Talimatlarý

### API Projesi (`AP.API`)
1. `api/AP.UI` dizinine gidin.
2. `appsettings.json` Main ConnectionString deðerini deðiþtirin.
3. Veritabanýný güncelleyin: `update-database`.


 
---

## Genel Yapý Hakkýnda

Test projesi;
MVC Web App ve .Net Core Web API projelerini içermektedir. 
Proje, kullanýcý arayüzü ve API katmanlarý arasýnda veri alýþveriþini saðlamak için `ApiConnectionService`   servis sýnýfýný kullanmaktadýr. 
Bu yapý, projenin modülerliðini artýrýr ve bakýmýný kolaylaþtýrýr.
API projesi, `AP.API` dizininde bulunurken, Web App projesi `AP.UI` dizinindedir.

Entityframework Core kullanýlarak veritabaný iþlemleri gerçekleþtirilir. CodeFirst yaklaþýmý ile veritabaný tablolarý oluþturulmuþtur.
Veritabaný iþlemleri için Entity Framework Core kullanýlmaktadýr.  
Repository ve UnitOfWork desenleri kullanýlarak veri eriþimi saðlanmaktadýr.
JWT (JSON Web Token) ile kimlik doðrulama ve yetkilendirme iþlemleri `AP.Utils\Auth` içindeki yardýmcý sýnýflar ile  yapýlmaktadýr.
JWT ayarlarý `appsettings.json` dosyasýnda yapýlandýrýlmýþtýr.
Hatalar her exception durumunda `AP.API` dizinindeki `ErrorHandler` sýnýfý tarafýndan yakalanýr ve yönetilir.

## Yaþanýlan zorluklar
- Proje baþlangýcýnda, Razor Pages ve API projeleri arasýndaki veri alýþveriþini saðlamak için uygun bir yapý oluþturmak zor oldu.
- Veritabaný baðlantý ayarlarýnýn doðru yapýlandýrýlmasý ve Entity Framework Core ile veritabaný iþlemlerinin düzgün çalýþmasý için zaman harcandý.
- API ve Razor Pages projeleri arasýnda veri alýþveriþini saðlamak için `ApiConnectionService` sýnýfýnýn doðru bir þekilde yapýlandýrýlmasý gerekti.
- Veritabaný iþlemleri sýrasýnda karþýlaþýlan hatalarýn yönetimi için `AP.Utils` dizinindeki `ErrorHandler` sýnýfýnýn doðru bir þekilde yapýlandýrýlmasý gerekti.
