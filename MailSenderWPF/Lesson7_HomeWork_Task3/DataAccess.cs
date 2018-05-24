using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Collections.ObjectModel;

namespace Lesson7_HomeWork_Task3
{
        public interface IDataAccessService
        {
            ObservableCollection<Orders> GetOrder();
            int NewOrder(Orders orders);
        }
        /// <summary>
        /// Класс реализующий доступ к базе данных
        /// </summary>
        public class DataAccessService : IDataAccessService
        {
            DataBaseDataContext context;

            public DataAccessService()
            {
                context = new DataBaseDataContext();
            }

            /// <summary>
            /// Получить колекцию электронных адресов с базы данных
            /// </summary>
            public ObservableCollection<Orders> GetOrder()
            {
                ObservableCollection<Orders> orders = new ObservableCollection<Orders>();
                foreach (var item in context.Orders)
                {
                    orders.Add(item);
                }
                return orders;
            }


            public int NewOrder(Orders order)
            {
                context.Orders.InsertOnSubmit(order);
                context.SubmitChanges();
                return order.Id;
            }
        }
    
}
