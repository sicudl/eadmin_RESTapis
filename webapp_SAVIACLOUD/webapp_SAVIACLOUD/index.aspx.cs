using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UdLSaviaCloud;

namespace webapp_SAVIACLOUD
    {
    
    public partial class index : System.Web.UI.Page
        {
        
        private UdLSaviaCloud.Client Connector = new UdLSaviaCloud.Client();

        protected void Page_Load(object sender, EventArgs e)
            {
            this.lbTITLE.Text = "SAVIA Cloud UdL v.0.7 -debug tool- REST API Oauth2";
            this.Title=this.lbTITLE.Text;
            
            Connector.Iniciar();
            }

        protected void btGetDadesLab_Click(object sender, EventArgs e)
            {
            if (Connector.GetDades_Laboral(txCodiEntidad.Text, txCodieEmple.Text,txVersio.Text,ckDarrera.Checked))
                this.txResult.Text = Connector.Dades_JSON;
            }

        protected void btGetDadesPers_Click(object sender, EventArgs e)
            {

                    if (Connector.GetDades_Empleado(txCodiEntidad.Text, txCodieEmple.Text))
                        this.txResult.Text = Connector.Dades_JSON;

            }

        protected void txVersio1_TextChanged(object sender, EventArgs e)
            {

            }

        protected void btGetDptos_Click(object sender, EventArgs e)
            {
            if (Connector.GetDades_Departament(this.txCodiEntidad.Text,this.txDpot1.Text, this.txDpot2.Text))
                this.txResult.Text = Connector.Dades_JSON;
            }
        }
    }