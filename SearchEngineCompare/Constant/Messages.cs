using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchEngineCompare.Constant
{
    static class Messages
    {
        public static string Unsuccessful = "Başarısız";
        public static string Successful = "Başarılı";
        public static string Inserted = "Kayıt eklendi.";
        public static string InsertedFail = "Veritabanına kayıt eklenirken hata oluştu.";
        public static string NotInserted = "Veritabanına kayıt eklenmedi.";
        public static string Modified = "Kayıt güncellendi.";
        public static string ModifiedFail = "Veritabanında kayıt güncellenirken hata oluştu.";
        public static string NotModified = "Veritabanında kayıt güncellenmedi.";
        public static string InvoiceNotFound = "Fatura bulunamadı.Önce faturanın oluşması gerekiyor.";
        public static string DataAlreadyExistsInTheSystem = "Kayıt zaten sistemde mevcuttur.";
        public static string DatabaseQueryError = "veritabanı sorgulama hatası";
        public static string NotFound = "Kayıt bulunamadı";
        public static string HttpRequestError = "Http istek hatası";
        public static string EmailSendError = "E-posta gönderme hatası";
        public static string FileCreateError = "Dosya oluşturma hatası";
        public static string FileDeleteError = "Dosya silme hatası";

    }
}
