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
        public static Timer t = new Timer();
        static void Main(string[] args)
        {
            t.Interval = 3000;
            t.Start();
            t.Elapsed += ChecarTiempo;
            // Wait for the user to hit <Enter>;
            Console.ReadKey();
        }

        private static void ChecarTiempo(object sender, ElapsedEventArgs e)
        {
            //    if ((string.Format("{0:HH:mm}", DateTime.Now) == "13:35"))
            //    {
            t.Stop();
            Metodos.Respuesta();
            LiberarMemoria ob = new LiberarMemoria();
            ob.Dispose();
            t.Start();
            //}
        }
    }
}
