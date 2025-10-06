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
            request.ClientCertificates = new X509CertificateCollection() { certificate };

            request.Method = "POST";
            request.ContentType = "application/json";                //"application/xml";
            request.Accept = "application/json";
            request.PreAuthenticate = true;
            request.ServerCertificateValidationCallback = ValidateServerCertficate;
            //ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
            request.ContentLength = data.Length;
            try
            { 
            using (var stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }
            var response = (HttpWebResponse)request.GetResponse();
            }
            catch(System.Exception err)
            {
                logger.WriteEntry(err.Message);
                

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
