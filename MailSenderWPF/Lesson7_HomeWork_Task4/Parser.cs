using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Lesson7_HomeWork_Task4
{
    class Parser
    {
        private const string path = "Test.csv";
       StreamReader sr = new StreamReader(path,Encoding.Default);
        Task4 task = new Task4();
        DataAccessService data = new DataAccessService();
        List<string> parser = new List<string>();
       
        string[] tmp;
        public void Read()
        {
            while (!(sr.EndOfStream))
            {
                string temp = sr.ReadLine();
                parser.Add(temp);
            }
         
            //text=File.ReadAllLines(path,Encoding.Unicode);

            foreach (var el in parser)
            {
               tmp= el.Split(';');
                task.FIO = tmp[0];
                task.Email = tmp[1];
                task.Phone = tmp[2];
                data.Add(task);
              
            }  
        }
    }
}
