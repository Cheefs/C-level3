
using System.Data.Entity;
using System.Collections.ObjectModel;

namespace Lesson7_HomeWork_Task4
{
    public interface IDataAccessService
    {
        ObservableCollection<Task4> GetInfo();
        int Add(Task4 info);
        int Update(Task4 info);
        //int DeleteEmail(Email email);
    }

    public class DataAccessService : IDataAccessService
    {
        Task4ModelContainer context;
      
        public DataAccessService()
        {
            
            context = new Task4ModelContainer();
        }
        public ObservableCollection<Task4> GetInfo()
        {
            ObservableCollection<Task4> Info = new ObservableCollection<Task4>();
            foreach (var item in context.Task4Set)
            {
                Info.Add(item);
            }
            return Info;
        }
        public int Add(Task4 info)
        {
            context.Task4Set.Add(info);
            context.SaveChanges();
            return info.Id;
        }
        public int Update(Task4 info)
        {

            context.Task4Set.Attach(info);
            context.Entry(info).State = EntityState.Modified;
            context.SaveChanges();
            return info.Id;
        }
        //public int DeleteEmail(Email email)
        //{
        //    if (context.Entry(email).State == EntityState.Detached)
        //    {
        //        context.Emails.Attach(email);
        //    }
        //    context.Emails.Remove(email);

        //    context.SaveChanges();
        //    return email.Id;
        //}
    }
}
