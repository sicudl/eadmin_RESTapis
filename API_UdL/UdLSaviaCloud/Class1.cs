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
namespace UdLSaviaCloud
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
        private string DATA = "client_id=eab71ead-33ef-4a69-bd53-839385caa392" +
             "&scope=api://6e6a93e3-d455-456b-9dc4-eea60e473a6b/.default" +
             "&client_secret=UzD7Q~UK0uievYG3kf9tvSPuWH5aV7TJgqXU-&grant_type=client_credentials";
        private string URL = "https://login.microsoftonline.com/e44593f3-8e64-45a9-983a-37ed656a0a2a/oauth2/v2.0/token";
        /*private string DATA = "client_id=e13c498b-0f87-4619-a8e8-85d553c80678" +
             "&scope=api://c651b39c-e9bb-4607-94d4-0b29f45522b8/.default" +
             "&client_secret=gG27Q~yxmzk2kGZ_pi1V-zwWiOU7tnQPXn6j2&grant_type=client_credentials";
        private string URL = "https://login.microsoftonline.com/e44593f3-8e64-45a9-983a-37ed656a0a2a/oauth2/v2.0/token";
         * */
        public string TokenBearer = "";
        string URLREST_empleado = "https://api-pre.saviacloud.net/ginpix7/api/v1/Entidades/{entidadCod}/Empleados/{empleadoCod}";
        string URLREST_entidad = "https://api-pre.saviacloud.net/ginpix7/api/v1/Entidades/{entidadCod}";
        string URLREST_laborales ="https://api-pre.saviacloud.net/ginpix7/api/v1/Entidades/{entidadCod}/Empleados/{empleadoCod}/Laborales/{empleadoVers}";
        string URLREST_Dptos = "https://api-pre.saviacloud.net/ginpix7/api/v1/Entidades/{codiEntidad}/Departamentos?@codNivel1&@codNivel2";
        public string Dades_JSON="";

        public bool Iniciar()
            {
            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;

            if (TokenBearer.Length > 1) return true;

            bool Resultat = false;
            string varTOKEN = "";
            //:"AADSTS900561: The endpoint only accepts POST, OPTIONS requests. Received a GET request."
            try
                {
                var data = Encoding.ASCII.GetBytes(DATA);
                
                var request = WebRequest.Create(URL);
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = data.Length;

                using (var stream = request.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }
                var response = (HttpWebResponse)request.GetResponse();

                varTOKEN = new StreamReader(response.GetResponseStream()).ReadToEnd();
                Tokensillo myclass = Newtonsoft.Json.JsonConvert.DeserializeObject<Tokensillo>(varTOKEN);
                varTOKEN = myclass.access_token;
                if (varTOKEN.Length > 1)
                    {
                        logger.WriteSimpleTextLine("TOKEN ok =");
                        logger.WriteSimpleTextLine(varTOKEN);
                        Resultat = true;
                        TokenBearer = varTOKEN;
                    }
                else
                    logger.WriteSimpleTextLine("error no token");
                }
            catch (Exception Ex)
                {
                    logger.WriteSimpleTextLine("error no token + "+Ex.Message);
                Console.WriteLine(Ex.Message);
                }
            return Resultat;

            }
        private string GetdadesREST(string URL)
            {
            try
            {
            //var data = Encoding.ASCII.GetBytes(DATA);

            string Resposta = "";
            //URL="https://api-pre.saviacloud.net/ginpix7/api/v1/Entidades/1";

            logger.WriteSimpleTextLine("Peticio REST GET a " + URL);
            logger.WriteSimpleTextLine("TokenBearer" + TokenBearer);

            /*HttpWebRequest myHttpWebRequest = (HttpWebRequest)WebRequest.Create(URL);
            myHttpWebRequest.Method = "GET";
            myHttpWebRequest.Accept = "application/json";
            //myHttpWebRequest.SendChunked = true;
            //myHttpWebRequest.TransferEncoding="gzip, deflate, br";
            myHttpWebRequest.ContentType = "application/json";
            //myHttpWebRequest.Connection = "keep-alive";
            myHttpWebRequest.UserAgent = "PostmanRuntime/7.29.0";
            myHttpWebRequest.Headers.Add("Authorization", "Bearer "+TokenBearer);
             * 
            HttpWebResponse webResponse = (HttpWebResponse)myHttpWebRequest.GetResponse();
                */

            WebRequest webRequest = WebRequest.Create(URL);
            webRequest.Credentials = CredentialCache.DefaultCredentials;
            webRequest.Method = "GET";
            
            webRequest.Headers.Add("Authorization", "bearer "+TokenBearer);
            HttpWebResponse webResponse = (HttpWebResponse)webRequest.GetResponse();
                //"Bearer " + TokenBearer);
                
            //webRequest.Headers.Add("User-Agent", "PostmanRuntime/7.29.0");
            //webRequest.get.accepHeaders.Add("Accept", "application/json");
            //webRequest.Headers.Add("Accept-Encoding", "gzip, deflate, br");
            //webRequest.Headers.Add("Connetion", "keep-alive");    

            

            Stream memoryStream = webResponse.GetResponseStream();
            StreamReader streamReader = new StreamReader(memoryStream);
            Resposta = streamReader.ReadToEnd();
            //myHttpWebRequest.Abort();

            //System.Net.Cache.HttpRequestCachePolicy noCachePolicy = new System.Net.Cache.HttpRequestCachePolicy(System.Net.Cache.HttpRequestCacheLevel.NoCacheNoStore);
            //webRequest.CachePolicy = noCachePolicy;
            return Resposta;
            }
            catch (Exception Ex)
            {
                    logger.WriteSimpleTextLine("Error " + Ex.Message);
                    Console.WriteLine(Ex.Message);
                    throw Ex;
            }
            
            }
        public bool GetDades_Laboral(string entidadCod, string empleadoCod, string EmpleadoVers, bool GetUltima)
            {
            try
                {
                TaulaDades = new Hashtable();

                

                //string URLREST_empleado = "https://api-pre.saviacloud.net/ginpix7/api/v1/Entidades/{codiEntidad}/Empleados/{codiEmpleado}";
                if (entidadCod.Length > 0) URLREST_laborales = URLREST_laborales.Replace("{entidadCod}", entidadCod);
                if (empleadoCod.Length > 0) URLREST_laborales = URLREST_laborales.Replace("{empleadoCod}", empleadoCod);

                if (GetUltima)
                    {
                    string dades = GetdadesREST(URLREST_laborales.Replace("/{empleadoVers}", ""));
                    int darreraVErsionNum =1;
                    if (dades.Contains("\"version\":"))
                        darreraVErsionNum = Regex.Matches(dades, "\"version\":").Count;
                    /*List<string> TotesLesVersions= JsonConvert.DeserializeObject<List<string>>(dades);
                    int darreraVErsionNum = TotesLesVersions.Count;*/
                    URLREST_laborales = URLREST_laborales.Replace("{empleadoVers}", darreraVErsionNum.ToString());
                    }
                else
                    {
                    if (EmpleadoVers.Length > 0) URLREST_laborales = URLREST_laborales.Replace("{empleadoVers}", EmpleadoVers);
                    else
                        URLREST_laborales = URLREST_laborales.Replace("/{empleadoVers}", "");
                    }

                string RESPOSTA_SAVIA = GetdadesREST(URLREST_laborales);

                RESPOSTA_SAVIA = RESPOSTA_SAVIA.Replace(",", ",\r\n");
                RESPOSTA_SAVIA = RESPOSTA_SAVIA.Replace("},", "},\r\n");
                Dades_JSON = "";
                Dades_JSON = RESPOSTA_SAVIA;
                RESPOSTA_SAVIA = "";
                return true;
                }
            catch (Exception Ex)
                {
                Console.WriteLine(Ex.Message);
                return false;
                }
            //1 registre
            /*if (code.Length > 0)
                {
                RESPOSTA_SAVIA = RESPOSTA_SAVIA.Replace("{", "");
                RESPOSTA_SAVIA = RESPOSTA_SAVIA.Replace("}", "");
                RESPOSTA_SAVIA.Split(',');
                }*/
            }

        //string URLREST_Dptos = "https://api-pre.saviacloud.net/ginpix7/api/v1/Entidades/{codiEntidad}/Departamentos?@codNivel1&@codNivel2";
        public bool GetDades_Departament(string entidadCod, string codNivel1, string codNivel2)
            {
            try
                {
            TaulaDades = new Hashtable();

            var data = Encoding.ASCII.GetBytes(DATA);
            
            if (entidadCod.Length > 0) URLREST_Dptos = URLREST_Dptos.Replace("{codiEntidad}", entidadCod);

            if (codNivel1.Length > 0) URLREST_Dptos = URLREST_Dptos.Replace("@codNivel1", codNivel1);
            else
                URLREST_Dptos = URLREST_Dptos.Replace("?@codNivel1&@codNivel2", "");

            if (codNivel2.Length > 0) URLREST_Dptos = URLREST_Dptos.Replace("@codNivel2", codNivel2);
            else
                URLREST_Dptos = URLREST_Dptos.Replace("&@codNivel2", "");

            string RESPOSTA_SAVIA = GetdadesREST(URLREST_Dptos);

            RESPOSTA_SAVIA = RESPOSTA_SAVIA.Replace(",", ",\r\n");
            RESPOSTA_SAVIA = RESPOSTA_SAVIA.Replace("},", "},\r\n");
            Dades_JSON = "";
            Dades_JSON = RESPOSTA_SAVIA;
            RESPOSTA_SAVIA = "";
            return true;
                     }
            catch (Exception Ex)
                {
                Console.WriteLine(Ex.Message);
                return false;
                }
            //1 registre
            /*if (code.Length > 0)
                {
                RESPOSTA_SAVIA = RESPOSTA_SAVIA.Replace("{", "");
                RESPOSTA_SAVIA = RESPOSTA_SAVIA.Replace("}", "");
                RESPOSTA_SAVIA.Split(',');
                }*/
            }


        public bool GetDades_Empleado(string entidadCod, string empleadoCod)
            {
            try
                {
            TaulaDades = new Hashtable();

            var data = Encoding.ASCII.GetBytes(DATA);

            //string URLREST_empleado = "https://api-pre.saviacloud.net/ginpix7/api/v1/Entidades/{codiEntidad}/Empleados/{codiEmpleado}";
            if (entidadCod.Length > 0) URLREST_empleado = URLREST_empleado.Replace("{entidadCod}", entidadCod);
            if (empleadoCod.Length > 0) URLREST_empleado = URLREST_empleado.Replace("{empleadoCod}", empleadoCod);
            else
                URLREST_empleado = URLREST_empleado.Replace("/{empleadoCod}","");

            string RESPOSTA_SAVIA = GetdadesREST(URLREST_empleado);

            RESPOSTA_SAVIA = RESPOSTA_SAVIA.Replace(",", ",\r\n");
            RESPOSTA_SAVIA = RESPOSTA_SAVIA.Replace("},", "},\r\n");
            Dades_JSON = "";
            Dades_JSON = RESPOSTA_SAVIA;
            RESPOSTA_SAVIA = "";
            return true;
                     }
            catch (Exception Ex)
                {
                Console.WriteLine(Ex.Message);
                return false;
                }
            //1 registre
            /*if (code.Length > 0)
                {
                RESPOSTA_SAVIA = RESPOSTA_SAVIA.Replace("{", "");
                RESPOSTA_SAVIA = RESPOSTA_SAVIA.Replace("}", "");
                RESPOSTA_SAVIA.Split(',');
                }*/
            }

    }
}
