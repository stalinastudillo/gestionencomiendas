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
    public partial class frmContado : Form
    {
        frmPagosFactura Principal;
        clsConexion Conn;
        String idfactura;

        public frmContado(frmPagosFactura prc, clsConexion C, String nrofactura)
        {
            InitializeComponent();

            Principal = prc;
            Principal.Enabled = false;

            Conn = C;
            idfactura = nrofactura;
        }

        private void Salir()
        {
            Principal.Enabled = true;
            this.Dispose();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Salir();
        }

        private void btnGrabar_Click(object sender, EventArgs e)
        {
            try
            {
                Conn.InsertOrUpdate("insert into pago_contado (fecha,nro_recibo,monto,idfactura) values ('" + datePagoContado.Value.ToString("yyyy-MM-dd") + "'," + txtNroRecibo.Text + "," + txtImporte.Text + "," + idfactura + ")");
                Principal.CargarPagos();
                Salir();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error insercion contado: " + ex.Message);
            }
        }

        private void frmContado_FormClosing(object sender, FormClosingEventArgs e)
        {
            Salir();
        }

        private void txtImporte_Leave(object sender, EventArgs e)
        {
            try
            {
                Utils.parseAndRound(txtImporte.Text);
            }
            catch
            {
                txtImporte.Text = "0";
            }
        }

        private void txtNroRecibo_Leave(object sender, EventArgs e)
        {
            try
            {
                int.Parse(txtNroRecibo.Text);
            }
            catch
            {
                MessageBox.Show("Ingrese un número de recibo válido.", "Gestión - Pago Contado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNroRecibo.Text = "";
            }
        }

       
    }
}
