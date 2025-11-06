using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using client_REST_AOCscc;
using MultiTools;

namespace client_AOC_firma
{
    class Program
    {
        static void Main(string[] args)
        {
            MultiTools.General.AppLog logger = new MultiTools.General.AppLog("");


        string NomDocument = "";
            string NomFinalDocument = "";// v_NomFinalDocument;
            string PDF_base64 = "";// v_PDF_base64;
            MultiTools.General.BASE64er b64 = new MultiTools.General.BASE64er();

            if (args.Length < 1)
                NomDocument = @"HOLA.pdf";
            else
                NomDocument = args[0];

            PDF_base64 = b64.EnCodeFile(NomDocument);
            NomFinalDocument = "provaSignat_" + System.DateTime.Now.Ticks.ToString() + NomDocument;
                        
            string pp = client_REST_AOCscc.ccSignadorAOC.Res;
            client_REST_AOCscc.ccSignadorAOC.Crida(
                b64.EnCode(NomDocument)+";"+
                b64.EnCode(NomFinalDocument) + ";" +
                PDF_base64+";;;;;;;;;;;");
            pp= client_REST_AOCscc.ccSignadorAOC.Res;

            if (client_REST_AOCscc.ccSignadorAOC.ErrorPeticio)
                Console.WriteLine("finalitza amb Error : " + client_REST_AOCscc.ccSignadorAOC.ErrorPeticioMissatge);

            if (client_REST_AOCscc.ccSignadorAOC.ErrorPeticio)
                logger.WriteEntry(client_REST_AOCscc.ccSignadorAOC.ErrorPeticioMissatge);
            
            b64.DeCodeAsFile(
                client_REST_AOCscc.ccSignadorAOC.RespostaSignatura.SignaturaResposta.bytesB64,
                NomFinalDocument);

            Console.WriteLine("finalitza amb signat = " + NomFinalDocument);
            Console.WriteLine("CSV del document = " + client_REST_AOCscc.ccSignadorAOC.RespostaSignatura.SignaturaResposta.codi);
           // Console.ReadKey();
        }
    }
}
