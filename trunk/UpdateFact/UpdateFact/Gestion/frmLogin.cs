using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace Gestion
{
    public partial class frmLogin : Form
    {
        Principal principal;
        String cadena_conexionDelivery,cadena_conexionAntares;


        public frmLogin()
        {
            InitializeComponent();

            String pathCommand = System.IO.Path.GetDirectoryName(Application.ExecutablePath).ToString();

            //Se obtiene del archivo conf.dat la ip de la maquina host
            //---------------------------------------------------------
            StreamReader objReader = new StreamReader(pathCommand + @"\" + "conf.dat");
            string sLine = objReader.ReadLine();
            //Obtengo la ip que aparece despues de la cadena "ipHost= "
            string dirIp = sLine.Substring(8);
            //---------------------------------------------------------



            //Cadena conexion a delivery
            cadena_conexionDelivery = "Driver={MySQL ODBC 5.1 Driver};Server=" + dirIp + ";Database=gestionDelivery; User=postal;Password=postal;Option=3";
            //Cadena conexion a antares
            cadena_conexionAntares = "Driver={MySQL ODBC 5.1 Driver};Server=" + dirIp + ";Database=gestionAntares; User=postal;Password=postal;Option=3";


        }
 
        private void btnConectar_Click(object sender, EventArgs e)
        {
            
            if (option1.Checked)
            {
                principal = new Principal(cadena_conexionDelivery);
            }
            else
            {
               
                principal = new Principal(cadena_conexionAntares);
            }
            principal.updateFact();
            this.Dispose();
        }

 



    }
}
