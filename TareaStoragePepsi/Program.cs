using System;
using System.Timers;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TareaStoragePepsi
{
    class Program
    {
        static void Main(string[] args)
        {
            Timer t = new Timer();
            t.Interval = 6000;
            t.Start();
            t.Elapsed += ChecarTiempo;
            // Wait for the user to hit <Enter>;
            Console.ReadKey();
        }

        private static void ChecarTiempo(object sender, ElapsedEventArgs e)
        {
            Console.WriteLine(String.Format("{0:HH:mm}", DateTime.Now));
            if ((string.Format("{0:HH:mm}", DateTime.Now) == "15:33"))
            {
                Storage storage = new Storage();
                storage.insertaStorage();
            }
        }
    }
}
