using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Storage.V1;
using System.IO;
using System.Data.SqlClient;

namespace TareaStoragePepsi
{
    class EnvioStorage
    {
        public static List<TablaImages> listachk = new List<TablaImages>();
        public static List<StorageInfo> listadato = new List<StorageInfo>();
        public static string ligadealmacenamiento = null;
        public static void mistorage()
        {
            StorageClient storageClient = StorageClient.Create();
            while (true)
            {
                listachk = VerificadorImagenes.validadorImage();
                if (listachk.Count() != 0)
                {
                    int compa = Convert.ToInt32(listachk[0].CMCIAID);
                    listadato = Obtiene.InfoStora(compa);
                    //string img64, int idActividad, int idRespuesta
                    using (SqlConnection con = new SqlConnection(Obtiene.connString))
                    {
                        con.Open();
                        using (DataClasses1DataContext dc = new TareaStoragePepsi.DataClasses1DataContext(con))
                        {
                            dc.SubmitChanges();
                            for (int i = 0; i <= (listachk.Count() - 1); i++)
                            {
                                Console.WriteLine(listachk[i].CLCHKID);
                                Random rnd = new Random();
                                int tmp = rnd.Next(1000);
                                DateTimeOffset now = DateTimeOffset.Now;
                                string route = Convert.ToString(listadato[0].IdFolder) + now.ToString("yyyy-MM") + "/";
                                route += string.Format("{0:00000}", Convert.ToString(listachk[i].CLCHKID)) + "/";
                                string fileName = route + string.Format("{0:000}", Convert.ToString(listachk[i].CMCIAID)) + "_" + string.Format("{0:000}", Convert.ToString(listachk[i].CLCHKID)) + "_" + string.Format("{0:000}", Convert.ToString(listachk[i].CLCHKPROID)) + "_" + string.Format("{0:000}", Convert.ToString(listachk[i].CLCHKACTID)) + "_" + string.Format("{0:000}", Convert.ToString(listachk[i].CLCHKIMGLIN)) + "_" + string.Format("{0:000}", tmp) + ".jpeg";
                                try
                                {
                                    var options = new UploadObjectOptions { PredefinedAcl = PredefinedObjectAcl.PublicRead };
                                    var obj1 = storageClient.UploadObject(Convert.ToString(listadato[0].IdBucket), fileName, "image/jpeg", new MemoryStream(listachk[i].CLCHKIMGBLOB), options);
                                    CLCHKIMG ima = dc.CLCHKIMG.FirstOrDefault(actu => actu.CMCIAID.Equals(listachk[i].CMCIAID) && actu.CLCHKID.Equals(listachk[i].CLCHKID) && actu.CLCHKPROID.Equals(listachk[i].CLCHKPROID) && actu.CLCHKACTID.Equals(listachk[i].CLCHKACTID) && actu.CLCHKIMGLIN.Equals(listachk[i].CLCHKIMGLIN));
                                    ima.CLCHKIMGSTORAGE = 1;
                                    try
                                    {
                                        CLCHKTAR tarea = dc.CLCHKTAR.FirstOrDefault(tar => tar.CMCIAID.Equals(listachk[i].CMCIAID) && tar.CLCHKID.Equals(listachk[i].CLCHKID) && tar.CLCHKPROID.Equals(listachk[i].CLCHKPROID) && tar.CLCHKACTID.Equals(listachk[i].CLCHKACTID) && tar.CLCHKTARLIN.Equals(listachk[i].CLCHKIMGLIN));
                                        tarea.CLCHKTARTXC = listadato[0].IdUrl + listadato[0].IdBucket + '/' + obj1.Name.ToString();
                                        dc.SubmitChanges();
                                    }
                                    catch (Exception)
                                    {
                                        Console.WriteLine("Los datos no coinciden en la tabla CLCHKTAR", listachk[i].CMCIAID+"-"+ listachk[i].CLCHKID +"-"+ listachk[i].CLCHKPROID+"-"+ listachk[i].CLCHKACTID+"-"+ listachk[i].CLCHKIMGLIN);
                                    }
                                    finally
                                    {

                                    }
                                }
                                catch (Google.GoogleApiException e)
                                {
                                    // The bucket already exists.  That's fine.
                                    // throw e;
                                }
                            }
                        }
                    }
                }
                else
                {
                    //liberar memoria
                    break;
                }
            } 
        }
    }
}