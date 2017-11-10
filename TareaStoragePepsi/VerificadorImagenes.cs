using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Drawing;
using System.Data.SqlClient;

namespace TareaStoragePepsi
{
    class VerificadorImagenes
    {
        public static List<TablaImages> listachk = new List<TablaImages>();
        public static List<int> errores = new List<int>();
        public static List<TablaImages> validadorImage()
        {
            listachk = Obtiene.obtener();
            if (listachk.Count() != 0)
            {
                using (SqlConnection con = new SqlConnection(Obtiene.connString))
                {
                    con.Open();
                    using (DataClasses1DataContext dc = new TareaStoragePepsi.DataClasses1DataContext(Obtiene.connString))
                    {
                        for (int i = 0; i <= (listachk.Count() - 1); i++)
                        {
                            //ACTUALIZAR CLCHKTAR
                            //CLCHKIMG ima = dc.CLCHKIMG.FirstOrDefault(empl => empl.CLCHKIMGNAME.Equals(listachk[i].CLCHKIMGNAME)
                            CLCHKIMG ima = dc.CLCHKIMG.FirstOrDefault(actu => actu.CMCIAID.Equals(listachk[i].CMCIAID) && actu.CLCHKID.Equals(listachk[i].CLCHKID) && actu.CLCHKPROID.Equals(listachk[i].CLCHKPROID)&& actu.CLCHKACTID.Equals(listachk[i].CLCHKACTID) && actu.CLCHKIMGLIN.Equals(listachk[i].CLCHKIMGLIN));
                            try
                            {
                                using (var ms = new MemoryStream(listachk[i].CLCHKIMGBLOB, 0, listachk[i].CLCHKIMGBLOB.Length))
                                {
                                    Image image = Image.FromStream(ms, true);
                                }
                                ima.CLCHKIMGSTS = 1;
                                dc.SubmitChanges();
                            }
                            catch (Exception)
                            {
                                ima.CLCHKIMGSTS = 2;
                                dc.SubmitChanges();
                                errores.Add(i);
                                Console.WriteLine("Error en el registro:  " + listachk[i].CMCIAID +"-"+listachk[i].CLCHKID +"-"+listachk[i].CLCHKPROID+"-"+listachk[i].CLCHKACTID+"-"+listachk[i].CLCHKIMGLIN + " Se encontró una anomalía en la imagen ");
                            }
                            finally
                            {

                            }
                        }
                    }
                }
                for (int i = 0; i <= (errores.Count() - 1); i++)
                {
                    listachk.RemoveAt(errores[i]);
                }
            }
            else
            {
                    
            }
            return listachk;
        }
    }
}
