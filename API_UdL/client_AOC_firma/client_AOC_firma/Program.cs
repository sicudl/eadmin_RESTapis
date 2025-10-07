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
            for(int i=0;i<128;i++)
                signadorPDFAOC.Init("","");
        }
    }
}
