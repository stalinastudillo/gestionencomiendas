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
    public partial class frmCheque : Form
    {
        frmPagosFactura Principal;
        clsConexion Conn;
        String idfactura;

        public frmCheque(frmPagosFactura prc,clsConexion C,String nrofactura)
        {
            InitializeComponent();

            Principal = prc;
            Principal.Enabled = false;

            Conn = C;
            idfactura = nrofactura;
        }

        private void btnGrabar_Click(object sender, EventArgs e)
        {
            try
            {
                Conn.InsertOrUpdate("insert into pago_cheque (nro_cheque,banco,monto,nro_recibo,fecha,idfactura) values (" + txtNroCheque.Text + ",'" + cmbBancos.SelectedItem.ToString() + "'," + txtImporte.Text + "," + txtNroRecibo.Text + ",'" + datePagoCheque.Value.ToString("yyyy-MM-dd") + "'," + idfactura + ")");
                Principal.CargarPagos();
                Salir();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error insercion cheque: " + ex.Message);
            }
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

        private void frmCheque_FormClosing(object sender, FormClosingEventArgs e)
        {
            Salir();
        }

        private void txtNroCheque_Leave(object sender, EventArgs e)
        {
            try
            {
                int.Parse(txtNroCheque.Text);
            }
            catch
            {
                MessageBox.Show("Ingrese un número de cheque válido.", "Gestión - Pago con Cheque", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNroCheque.Text = "";
            }
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
                MessageBox.Show("Ingrese un número de recibo válido.", "Gestión - Pago con Cheque", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNroRecibo.Text = "";
            }
        }
    }
}
