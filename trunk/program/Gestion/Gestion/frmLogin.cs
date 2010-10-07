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
        DataSet dsUser;
        clsConexion C;


        List<String> cadena_conexion;

        public frmLogin()
        {
            InitializeComponent();
            dsUser = new DataSet();
            C = new clsConexion();



            String pathCommand = System.IO.Path.GetDirectoryName(Application.ExecutablePath).ToString();

            //Se obtiene del archivo conf.dat la ip de la maquina host
            //---------------------------------------------------------
            StreamReader objReader = new StreamReader(pathCommand + @"\" + "conf.dat");
            string sLine = objReader.ReadLine();
            //Obtengo la ip que aparece despues de la cadena "ipHost= "
            string dirIp = sLine.Substring(8);
            //---------------------------------------------------------

         
            //C.Conectar("Driver={MySQL ODBC 5.1 Driver};Server=" + dirIp + ";Database=gestionE; User=userRed;Password=clave;Option=3");



            C.Conectar("Driver={MySQL ODBC 5.1 Driver};Server="+dirIp+";Database=users; User=usuarios;Password=usuarios;Option=3");
            groupBox2.Visible = false;
            cadena_conexion = new List<string>(3);



            //Cadena conexion a delivery
            cadena_conexion.Add("Driver={MySQL ODBC 5.1 Driver};Server=" + dirIp +";Database=gestionDelivery; User=postal;Password=postal;Option=3");
            //Cadena conexion a antares
            cadena_conexion.Add("Driver={MySQL ODBC 5.1 Driver};Server="+dirIp+";Database=gestionAntares; User=postal;Password=postal;Option=3");
            //Cadena conexion a ciudades
            cadena_conexion.Add("Driver={MySQL ODBC 5.1 Driver};Server=" + dirIp + ";Database=ciudades; User=postal;Password=postal;Option=3");
            //Cadena conexion a user
            cadena_conexion.Add("Driver={MySQL ODBC 5.1 Driver};Server=" + dirIp + ";Database=users; User=usuarios;Password=usuarios;Option=3");
            //Cadena conexion a "NUEVA EMPRESA"
            cadena_conexion.Add("Driver={MySQL ODBC 5.1 Driver};Server=" + dirIp + ";Database=gestionNuevo; User=postal;Password=postal;Option=3");
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            C.CargarDatos(dsUser, "dsUser", "select iduser from usuario where nombre = '" + txtUsuario.Text + "' and password = '" + txtPassword.Text + "'");

            if (dsUser.Tables[0].Rows.Count != 0)
            {
                groupBox1.Visible = false;
                groupBox2.Visible = true;                                
            }
            else
            {
                MessageBox.Show("Nombre de Usuario o Contraseña incorrecto.", "Gestión - Login", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                txtUsuario.Text = "";
                txtPassword.Text = "";
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnConectar_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmPrincipal principal = new frmPrincipal();
            if (option1.Checked)
            {
                principal.ActualizarCadenaConexion(cadena_conexion[0],cadena_conexion[2],cadena_conexion[3]);
            }
            else
            {
                if (option2.Checked)
                {
                    principal.ActualizarCadenaConexion(cadena_conexion[1], cadena_conexion[2], cadena_conexion[3]);
                }
                else
                {
                    principal.ActualizarCadenaConexion(cadena_conexion[4], cadena_conexion[2], cadena_conexion[3]);
                }
            }
            principal.ActualizarUsuarioActual(dsUser.Tables[0].Rows[0][0].ToString());
            
            C.CerrarConexion();
            principal.Show();            
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnAceptar_Click(null, null);
            }
        }



    }
}
