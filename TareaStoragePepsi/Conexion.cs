using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TareaStoragePepsi
{
    class Conexion
    {
        static SqlConnection conexion;
        public static SqlConnection SQLActiva()
        {
            conexion = new SqlConnection(@"Data Source=35.184.6.120;Initial Catalog=MM1_PAL_COM;Persist Security Info=True;User ID=usrprod;Password=qwerty");
            conexion.Open();
            return conexion;
        }
        public static SqlConnection SqlPasivo()
        {
            conexion.Close();
            return conexion;
        }
    }
}
