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
    public partial class frmCantidadPrecio : Form
    {
        clsFunciones Funciones;
        Form principal;
        String destino;
        

        public frmCantidadPrecio(Form  prc,String dest)
        {
            InitializeComponent();
            Funciones = new clsFunciones();
            principal = prc;
            destino = dest;
        }

        public void ActualizarDatos(String cantidad, String precio)
        {
            txtCantidad.Text = cantidad;
            txtPrecio.Text = precio;
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            Funciones.ValidarNumero(txtCantidad, "Por favor ingrese un número.", "Gestión - Remito");
            Funciones.ValidarNumero(txtPrecio, "Por favor ingrese un número.", "Gestión - Remito");
            if (txtCantidad.Text == "0")
            {
                txtCantidad.Text = "1";
            }
            if (destino == "principal")
            {
                ((frmPrincipal)principal).CargarDatosCantidadPrecio(txtCantidad.Text, txtPrecio.Text);
            }
            else
            {
                ((frmBMRemito)principal).CargarDatosCantidadPrecio(txtCantidad.Text, txtPrecio.Text);
            }
            principal.Enabled = true;
            this.Visible = false;
        }

        private void frmCantidadPrecio_Load(object sender, EventArgs e)
        {
            principal.Enabled = false;
        }

        private void frmCantidadPrecio_FormClosing(object sender, FormClosingEventArgs e)
        {
            principal.Enabled = true;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            principal.Enabled = true;
            this.Visible = false;
        }





    }
}
