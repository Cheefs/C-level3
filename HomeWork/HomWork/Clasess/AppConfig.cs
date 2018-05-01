using System.Collections.Generic;

namespace HomeWork
{
    /// <summary>
    /// Класс настройки рассыльшика
    /// </summary>
   public static class AppConfig
   {
        /// <summary>
        /// Список доступных настроек Smpt сервера
        /// </summary>
        public static List<string> config = new List<string> { "mail.ru", "yandex.ru", "gmail.ru" };
        /// <summary>
        /// Порт Smpt сервера
        /// </summary>
        public static int Port = 25;
   
        public static string yandexSmtp = "smtp.yandex.ru";
        //public static int yandexPort = 25;
        public static string mailRuSmpt = "smtp.mail.ru";
        public static int mailRuPort = 465;
        public static string gmailSmpt = "smtp.gmail.com";
        //public static int gmailPort = 25;
    }
}
