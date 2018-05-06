using System.Linq;

namespace HomeWork
{
    /// <summary>
    /// Класс базы данных
    /// </summary>
    class DBClass
    {
        private EmailsDataContext emails = new EmailsDataContext();
        /// <summary>
        /// Получение адресов с беы данных
        /// </summary>
        public IQueryable<Email> Emails
        {
            get => from c in emails.Email select c;
        }   
    }
}