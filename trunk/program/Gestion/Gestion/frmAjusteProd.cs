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
    public partial class frmAjusteProd : Form
    {
        clsConexion Conn;
        frmPrincipal principal;
        float ajuste;
        string codigo;

        public frmAjusteProd(clsConexion C,frmPrincipal prc,String cod)
        {
            InitializeComponent();
            Conn = C;
            principal = prc;
            codigo = cod;
        }

        public void refreshCodigo(String cod)
        {
            codigo = cod;
        }

        private void txtAjuste_Leave(object sender, EventArgs e)
        {
            txtAjuste.Text = txtAjuste.Text.Replace(",", ".");

            if (!float.TryParse(txtAjuste.Text, out ajuste))
            {
                MessageBox.Show("Por favor ingrese un número", "Gestión - Ajustar Producto", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtAjuste.Text = "0";
            }
            
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            principal.Enabled = true;
            this.Hide();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            Conn.AjustarProducto(codigo, txtAjuste.Text);
            principal.cargarProductos();
            principal.Enabled = true;
            txtAjuste.Text = "";
            this.Hide();

        }

        private void frmAjusteProd_Load(object sender, EventArgs e)
        {
            principal.Enabled = false;
        }

        private void frmAjusteProd_FormClosing(object sender, FormClosingEventArgs e)
        {
            principal.Enabled = true;
        }

       
    }
}
