using System;
using System.Text.RegularExpressions;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography;
using System.Net.Security;

namespace System.Security.Authentication
{
    public static class SslProtocolsExtensions
    {
        // For .NET Framework 3.5
        public const SslProtocols Tls12 = (SslProtocols)3072;
        // For .NET Framework 4.6.2 and later
        public const SslProtocols Tls13 = (SslProtocols)12288;
    }
}

namespace client_REST_AOCscc
{
    class Tokensillo
    {
        public string token_type = "";
        public int expires_in = 0;
        public int ext_expires_in = 0;
        public string access_token = "";
    }
    class Empleado
    {
        public string empleadoCod = "";
        public int nombre = 0;
        public int apellido1 = 0;

        public int ext_expires_in = 0;
        public string access_token = "";
    }
    class Entidad
    {
        public string token_type = "";
        public int expires_in = 0;
        public int ext_expires_in = 0;
        public string access_token = "";
    }

    public class Client
    {              
        private MultiTools.General.AppLog logger = new MultiTools.General.AppLog("");
        public Hashtable TaulaDades = new Hashtable();
        private string base_URL = "https://cert.pci-cl-pre.aoc.cat/msc-ssc/api/signatura";

        string Peticio_signatura=
 "{'dipositari': '9821920002',"+
 "'rol': 'Segell proves UDL administracio electronica remot',"+
 "'pdf': {"+
 "'forma': 'PADES_T',"+
 "'document': {"+
 "'bytesB64': '@PDF_base_64_bytes',"+
 "'nom': '@PDF_nomFinal' }}} ";
        public void Init(string NomFinalDocument,string PDF_base64)
        {
            if (NomFinalDocument.Length < 1)
                NomFinalDocument = "prova_"+System.DateTime.Now.Ticks.ToString() + ".pdf";
            if (PDF_base64.Length<1)
            {
                MultiTools.General.BASE64er Filer = new MultiTools.General.BASE64er();
                PDF_base64=Filer.EnCodeFile(@"HOLA.pdf");
            }
            X509Certificate2 certificate = new X509Certificate2();
            byte[] rawCertificateData = File.ReadAllBytes(@"2ESfRXIbPRE.p12");
            certificate.Import(rawCertificateData, "2ESfRXIbPRE", X509KeyStorageFlags.PersistKeySet);
            
            Peticio_signatura = Peticio_signatura.Replace("@PDF_base_64_bytes", PDF_base64);
            Peticio_signatura = Peticio_signatura.Replace("@PDF_nomFinal", NomFinalDocument);
            var data = Encoding.ASCII.GetBytes(Peticio_signatura);

            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
            // PIN cert client : 2ESfRXIbPRE
            //TODO : carregar al client http que fa el post un .P12 amb el PIN que done AOC.
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(base_URL);

            //request.ClientCertificates =  new X509CertificateCollection() { certificate };

            request.Method = "POST";
            request.KeepAlive = true;
            request.UseDefaultCredentials = false;
            request.UnsafeAuthenticatedConnectionSharing = true;
            request.ContentType = "application/json;charset=utf-8";                //"application/xml";
            request.Accept = "application/json";
            request.Headers.Add("X-Amzn-Mtls-Clientcert", "-----BEGIN%20CERTIFICATE-----%0A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-----END%20CERTIFICATE-----%0A");
            request.PreAuthenticate = false;
            request.ServerCertificateValidationCallback = ValidateServerCertficate;
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
            request.ContentLength = data.Length;
            HttpWebResponse response = null;
            try
            { 
            using (var stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }
               response = (HttpWebResponse)request.GetResponse();
            }
            catch(System.Exception err)
            {                
                logger.WriteEntry(err.Message);
                logger.WriteEntry(Peticio_signatura);
               // logger.WriteEntry(response.StatusDescription);
                //logger.WriteEntry(response.Server);
            }
        }

        private static bool ValidateServerCertficate(
        object sender,
        X509Certificate cert,
        X509Chain chain,
        SslPolicyErrors sslPolicyErrors)
        {
            if (sslPolicyErrors == SslPolicyErrors.None)
            {
                // Good certificate.
                return true;
            }

        
            bool certMatch = false; // Assume failure
            byte[] certHash = cert.GetCertHash();
            if (certHash.Length == 0)
            {
                certMatch = true; // Now assume success.
                for (int idx = 0; idx < certHash.Length; idx++)
                {
                    if (certHash[idx] != 0)
                    {
                        certMatch = false; // No match
                        break;
                    }
                }
            }

            // Return true => allow unauthenticated server,
            //        false => disallow unauthenticated server.
            return certMatch;
        }
    }
}
