using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using UdLSaviaCloud;
/*
 * 
 Access Token URL
https://login.microsoftonline.com/e44593f3-8e64-45a9-983a-37ed656a0a2a/oauth2/v2.0/token
client ID 
e13c498b-0f87-4619-a8e8-85d553c80678
secret value 
gG27Q~yxmzk2kGZ_pi1V-zwWiOU7tnQPXn6j2
Scope 
api://c651b39c-e9bb-4607-94d4-0b29f45522b8/.default
 * 
 **/
namespace clien_UdL_tSaviaCloud
    {  
    class Program
        {
        static void Main(string[] args)
            {
            UdLSaviaCloud.Client SCloud = new UdLSaviaCloud.Client();            

            if (args.Length > 0)
                SCloud.TokenBearer = args[0];
            else
                SCloud.Iniciar();

            bool dades=SCloud.GetDades_Empleado("1", "");
            if (dades)
                Console.WriteLine(SCloud.Dades_JSON);
            else
                Console.WriteLine("error");
            }
        }
    }
