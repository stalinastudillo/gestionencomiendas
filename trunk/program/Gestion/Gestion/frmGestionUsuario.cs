using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Gestion
{
    public partial class frmGestionUsuario : Form
    {
        frmAMUsuario formAMUsuario;
        frmPrincipal principal;

        clsConexion C;
        clsUsuario Usuario;

        DataSet dsUsuarios;

        public frmGestionUsuario(frmPrincipal prc)
        {
            InitializeComponent();
            C = new clsConexion();


            String pathCommand = System.IO.Path.GetDirectoryName(Application.ExecutablePath).ToString();

            //Se obtiene del archivo conf.dat la ip de la maquina host
            //---------------------------------------------------------
            StreamReader objReader = new StreamReader(pathCommand + @"\" + "conf.dat");
            string sLine = objReader.ReadLine();
            //Obtengo la ip que aparece despues de la cadena "ipHost= "
            string dirIp = sLine.Substring(8);
            //---------------------------------------------------------


            C.Conectar("Driver={MySQL ODBC 5.1 Driver};Server="+dirIp+";Database=users; User=usuarios;Password=usuarios;Option=3");
            Usuario = new clsUsuario();
            dsUsuarios = new DataSet();
            principal = prc;
            principal.Enabled = false;
            CargarUsuarios();
        }

        private void btnQuitar_Click(object sender, EventArgs e)
        {
            try
            {
                if (lstUsuarios.Items.Count != 0)
                {
                    DialogResult result = MessageBox.Show("¿Esta seguro que desea eliminar este usuario?", "Gestión - Eliminar Usuario", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        Usuario.Eliminar(C, lstUsuarios.SelectedItem.ToString());
                        CargarUsuarios();
                    }
                }
            }
            catch { }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            try
            {
                if (lstUsuarios.Items.Count != 0)
                {
                    formAMUsuario = new frmAMUsuario(C, this, "modificar", lstUsuarios.SelectedItem.ToString());
                    formAMUsuario.Show();
                }
            }
            catch { }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            formAMUsuario = new frmAMUsuario(C, this, "crear", null);
            formAMUsuario.Show();
        }

        private void frmGestionUsuario_FormClosing(object sender, FormClosingEventArgs e)
        {
            principal.Enabled = true;
            C.CerrarConexion();
            this.Dispose();
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            principal.Enabled = true;
            C.CerrarConexion();
            this.Dispose();
        }

        public void CargarUsuarios()
        {
            C.CargarDatos(lstUsuarios, dsUsuarios, "dsUsuarios", "select nombre from usuario where iduser > 1");
        }
    }
}
