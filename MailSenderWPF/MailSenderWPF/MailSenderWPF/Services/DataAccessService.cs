using System.Collections.ObjectModel;


namespace MailSenderWPF.Services
{
    public interface IDataAccessService
    {
        ObservableCollection<Email> GetEmails();
        int CreateEmail(Email email);
    }
    /// <summary>
    /// Класс реализующий доступ к базе данных
    /// </summary>
    public class DataAccessService : IDataAccessService
    {
        EmailsDataContext context;

        public DataAccessService()
        {
            context = new EmailsDataContext();
        }

        /// <summary>
        /// Получить колекцию электронных адресов с базы данных
        /// </summary>
        public ObservableCollection<Email> GetEmails()
        {
            ObservableCollection<Email> Emails = new ObservableCollection<Email>();
            foreach (var item in context.Email)
            {            
                Emails.Add(item);
            }
            return Emails;
        }

     /// <summary>
     /// Добавить электронную почту в базу данных
     /// </summary>
     /// <param name="email">Email</param>
     /// <returns>Id</returns>
        public int CreateEmail(Email email)
        {
                context.Email.InsertOnSubmit(email);
                context.SubmitChanges();
                return email.Id;
        }
    }
}