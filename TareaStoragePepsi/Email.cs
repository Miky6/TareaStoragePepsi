using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Drawing;
using System.Reflection;

namespace TareaStoragePepsi
{
    class Email
    {
        //correo,asunto,cuerpo htm, null,true
        public static bool SendEmail(string _strTo, string _strSubject, string _strBody, string _strAttachment, bool _bIsBodyHTML)
        {
            try
            {
                var objFromAddress = new MailAddress("noreply@metricamovil.com", "Métrica Móvil");
                const string strFromPassword = "MyGeotab#01";

                var objSMTP = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(objFromAddress.Address, strFromPassword)
                };

                using (var objMessage = new MailMessage(objFromAddress.Address, _strTo)
                {
                    Subject = _strSubject,
                    Body = _strBody,
                    IsBodyHtml = _bIsBodyHTML,
                })
                {
                    objMessage.Bcc.Add(new MailAddress("miguel.sanchez@metricamovil.com"));
                    objMessage.Bcc.Add(new MailAddress("ivan.gaytan@metricamovil.com"));
                    //objMessage.Bcc.Add(new MailAddress("csaborit@itn.com.mx"));
                    //objMessage.Bcc.Add(new MailAddress("carmen.vega@metricamovil.com"));
                    //objMessage.Bcc.Add(new MailAddress("oscar.cisneros@metricamovil.com"));
                    //objMessage.Bcc.Add(new MailAddress("gerardo.ortiz@metricamovil.com"));
                    //objMessage.Bcc.Add(new MailAddress("ladame@metricamovil.com"));
                    //objMessage.To.Add(new MailAddress(_strTo, ""));
                    objSMTP.Send(objMessage);
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("[" + _strTo + "] -> EMAIL (" + System.Reflection.MethodBase.GetCurrentMethod().Name + ") -> " + ex.Message);
            }
        }
    }
}
