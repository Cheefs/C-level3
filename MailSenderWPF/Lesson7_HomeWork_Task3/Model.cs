using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Lesson7_HomeWork_Task3
{
    class Model
    {
        IViev viev;
        public Model(IViev Viev)
        {
            this.viev = Viev;
        }

       public ObservableCollection <Order> orders = new ObservableCollection<Order>();
       public List<string> movies = new List<string>()
       {
               "boolWekend",
               "charX",
               "string3"
       };
        public void AddOrder()
        {
            orders.Add(new Order
            {
                Movie = viev.VievMovie,
                BiletsCount = viev.VievBiletsCount,
                Time = DateTime.Now.ToString()
            });
        }     
    }
}
