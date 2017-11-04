using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace TareaStoragePepsi
{
    class Storage
    {
        public static string IdFolder;
        public static string IdUrl;
        public static string IdBucket;
        public static List<CLCHKIMG>Consulta()
        {
            List<CLCHKIMG> lista = new List<CLCHKIMG>();
            SqlCommand general = new SqlCommand(String.Format("select * from CLCHKIMG"),Conexion.SQLActiva());
            SqlDataReader reader = general.ExecuteReader();
            while (reader.Read())
            {
                CLCHKIMG objeto_general = new CLCHKIMG();
                objeto_general.CMCIAID = reader.GetDecimal(0);
                objeto_general.CLCHKID = reader.GetDecimal(1);
                objeto_general.CLCHKPROID = reader.GetDecimal(2);
                objeto_general.CLCHKACTID = reader.GetDecimal(3);
                objeto_general.CLCHKIMGLIN = reader.GetInt32(4);
                objeto_general.CLCHKIMGBLOB = (byte[])reader[5]; 
                lista.Add(objeto_general);
            }
            return lista;
        }
        public void insertaStorage()
        {
            List<CLCHKIMG> listaimagenes = new List<CLCHKIMG>();
            listaimagenes = Consulta();
            int elementos =  listaimagenes.Count();
            
        }
        public static void EmpresaStorage(int idcompania)
        {
            String Query = "";
            Query += "SELECT" + (Char)13;
            Query += "      RIGHT('000' + CONVERT(VARCHAR, B.CMCIAID), 3) + '-' + B.CMCIACODE + '/' + --GPO," + (Char)13;
            Query += "    RIGHT('000' + CONVERT(VARCHAR, A.CMCIAID), 3) + '-' + A.CMCIACODE + '/'[FOLDER]," + (Char)13;
            Query += "    B.CMCIASTGURL [URL], B.CMCIASTGBUCKET [BUCKET]" + (Char)13;
            Query += "FROM CMCIA A" + (Char)13;
            Query += "INNER JOIN CMCIA B" + (Char)13;
            Query += "            ON  B.CMCIAID = A.CMCIAGPOG" + (Char)13;
            Query += "WHERE A.CMCIAID =  " + idcompania + "  " + (Char)13;

            SqlCommand ComandoConsu = new SqlCommand(Query, Conexion.SQLActiva());
            SqlDataReader reader = ComandoConsu.ExecuteReader();
            if (reader.Read())
            {
                IdFolder = reader.GetString(0);
                IdUrl = reader.GetString(1);
                IdBucket = reader.GetString(2);
            }
            Conexion.SqlPasivo();
        }
    }
}
