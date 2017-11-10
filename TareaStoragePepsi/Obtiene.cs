using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TareaStoragePepsi
{
    class Obtiene
    {
        public static List<TablaImages> lista = new List<TablaImages>();
        public static List<StorageInfo> listainfo = new List<StorageInfo>();
        public static string Query;
        public static string connString = @"Data Source=35.184.6.120;Initial Catalog=MM1_PAL_COM;Persist Security Info=True;User ID=usrprod;Password=qwerty";
        public static List<TablaImages> obtener()
        {
            //DataClasses1DataContext db = new DataClasses1DataContext();
            //var query = from CLCHKIMG in db.CLCHKIMG select new CLCHKIMG() { CMCIAID = CLCHKIMG.CMCIAID, CLCHKID = CLCHKIMG.CLCHKID, CLCHKPROID = CLCHKIMG.CLCHKPROID, CLCHKACTID = CLCHKIMG.CLCHKACTID, CLCHKIMGLIN = CLCHKIMG.CLCHKIMGLIN, CLCHKIMGBLOB = CLCHKIMG.CLCHKIMGBLOB, CLCHKIMGSTORAGE = CLCHKIMG.CLCHKIMGSTORAGE } ;
            //return query.ToList();

            using (SqlConnection conn = new SqlConnection(connString))
            {
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    //select* from CLCHKIMG where CLCHKID = (select min(CLCHKID) from CLCHKIMG where CLCHKIMGSTORAGE = 0)
                    //select* from CLCHKIMG
                    //update CLCHKIMG set CLCHKIMGSTORAGE = 0
                    cmd.CommandText = "select CMCIAID,CLCHKID,CLCHKPROID,CLCHKACTID,CLCHKIMGLIN,CLCHKIMGBLOB,CLCHKIMGSTS,CLCHKIMGNAME from CLCHKIMG where CLCHKID = (select min(CLCHKID) from CLCHKIMG where CLCHKIMGSTORAGE = 0)";
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            TablaImages objeto_general = new TablaImages();
                            objeto_general.CMCIAID = reader.GetDecimal(reader.GetOrdinal("CMCIAID"));
                            objeto_general.CLCHKID = reader.GetDecimal(reader.GetOrdinal("CLCHKID"));
                            objeto_general.CLCHKPROID = reader.GetDecimal(reader.GetOrdinal("CLCHKPROID"));
                            objeto_general.CLCHKACTID = reader.GetDecimal(reader.GetOrdinal("CLCHKACTID"));
                            objeto_general.CLCHKIMGLIN = reader.GetInt32(reader.GetOrdinal("CLCHKIMGLIN"));
                            if(!reader.IsDBNull(reader.GetOrdinal("CLCHKIMGBLOB")))
                                objeto_general.CLCHKIMGBLOB = (byte[])reader[reader.GetOrdinal("CLCHKIMGBLOB")];
                            lista.Add(objeto_general);
                        }
                    }
                }
                conn.Close();
            }
            return lista;
        }
        public static List<StorageInfo> InfoStora(int idcompania)
        {
            Query = "";
            Query += "SELECT" + (Char)13;
            Query += "      RIGHT('000' + CONVERT(VARCHAR, B.CMCIAID), 3) + '-' + B.CMCIACODE + '/' + --GPO," + (Char)13;
            Query += "    RIGHT('000' + CONVERT(VARCHAR, A.CMCIAID), 3) + '-' + A.CMCIACODE + '/'[FOLDER]," + (Char)13;
            Query += "    B.CMCIASTGURL [URL], B.CMCIASTGBUCKET [BUCKET]" + (Char)13;
            Query += "FROM CMCIA A" + (Char)13;
            Query += "INNER JOIN CMCIA B" + (Char)13;
            Query += "            ON  B.CMCIAID = A.CMCIAGPOG" + (Char)13;
            Query += "WHERE A.CMCIAID =  " + idcompania + "  " + (Char)13;

            using (SqlConnection conn = new SqlConnection(connString))
            {
                using (SqlCommand ComandoConsu = new SqlCommand(Query,conn))
                {
                    conn.Open();
                    using (SqlDataReader reader = ComandoConsu.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            StorageInfo info = new StorageInfo();
                            info.IdFolder = reader.GetString(0);
                            info.IdUrl = reader.GetString(1);
                            info.IdBucket = reader.GetString(2);
                            listainfo.Add(info);
                        }
                    }
                }
            }
            return listainfo;
        }
    }
}
