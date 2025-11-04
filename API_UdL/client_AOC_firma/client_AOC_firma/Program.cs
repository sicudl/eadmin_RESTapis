using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using client_REST_AOCscc;

namespace client_AOC_firma
{
    class Program
    {
        static void Main(string[] args)
        {
            client_REST_AOCscc.Client signadorPDFAOC = new Client();
            //for(int i=0;i<256;i++)
            string pp = client_REST_AOCscc.mProgram.Res;
            client_REST_AOCscc.mProgram.Crida("f");
            pp= client_REST_AOCscc.mProgram.Res;

            if (signadorPDFAOC.ErrorPeticio)
                Console.WriteLine("Error");
            else
                Console.WriteLine(signadorPDFAOC.PeticioRetorn);
            while (Console.KeyAvailable) { Console.ReadKey(true); }
        }
    }
}
