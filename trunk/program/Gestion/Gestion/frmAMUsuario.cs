using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Gestion
{
    public partial class frmAMUsuario : Form
    {
        frmGestionUsuario gest;
        
        clsUsuario Usuario;
        clsConexion Conn;

        String c_n;
        String operacion;

        public frmAMUsuario(clsConexion C,frmGestionUsuario gu, String op,String current_name)
        {
            InitializeComponent();
            operacion = op;
            c_n = current_name;
            Conn = C;
            Usuario = new clsUsuario();
            gest = gu;
            gest.Enabled = false; //deshabilito el formulario padre

            if (operacion == "modificar")
            {
                txtNombre.Text = c_n;
            }
        }

        private void btnGrabar_Click(object sender, EventArgs e)
        {
            if ((txtNombre.Text != "") && (txtNombre.Text.ToUpper() != "ADMIN"))
            {
                if (operacion == "crear")
                {
                    Usuario.Insertar(Conn, txtNombre.Text, txtPassword.Text);
                }
                else
                {
                    Usuario.Actualizar(Conn, c_n, txtNombre.Text, txtPassword.Text);
                }
                
                gest.CargarUsuarios();
                gest.Enabled = true;
                this.Dispose();

            }
            else
            {
                MessageBox.Show("El nombre de usuario debe tener al menos un carácter y no puede ser ADMIN", "Gestión - Administración de Usuarios", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            gest.Enabled = true;
            this.Dispose();
        }

        private void frmAMUsuario_FormClosing(object sender, FormClosingEventArgs e)
        {
            gest.Enabled = true;
            this.Dispose();
        }

    }
}
