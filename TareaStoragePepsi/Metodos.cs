using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TareaStoragePepsi
{
    class Metodos
    {
        public static List<CLCHKIMG> lista = new List<CLCHKIMG>();
        public static int contador = 0;
        public static void Respuesta()
        {
            while (contador <= 5)
            {
                if (Verificador.Verificadorinternet())
                {
                    EnvioStorage.mistorage();
                    break;
                }
                else
                {
                    contador = contador + 1;
                    Thread.Sleep(3000);
                };
            }
        }
    }
}
