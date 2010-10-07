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
    public partial class frmPagosFactura : Form
    {
        frmPrincipal Principal;
        frmCheque formCheque;
        frmContado formContado;
        clsConexion Conn;

        DataSet dsCheques;
        DataSet dsContado;
        DataSet dsPagos;

        String idfactura;

        public frmPagosFactura(frmPrincipal prc, clsConexion C, String nrofactura)
        {
            InitializeComponent();
            Principal = prc;
            Principal.Enabled = false;
            Conn = C;
            dsContado = new DataSet();
            dsCheques = new DataSet();
            dsPagos = new DataSet();
            idfactura = nrofactura;
            CargarPagos();
        }


        public void CargarPagos()
        {
            Conn.CargarDatos(dgvCheques, dsCheques, "dsCheques", "select nro_cheque as 'Nº Cheque',banco as Banco, monto as Importe,nro_recibo as 'Nº Recibo', fecha as Fecha,idcheque from pago_cheque where idfactura = " + idfactura);
            Conn.CargarDatos(dgvContado, dsContado, "dsContado", "select nro_recibo as 'Nº Recibo', fecha as Fecha, monto as Importe, idcontado from pago_contado where idfactura = " + idfactura);
            Conn.CargarDatos(dsPagos, "dsPagos", "select pagado,ibb,ganancias,suss,saldo from pago_factura where idfactura = " + idfactura);

            dgvCheques.Columns[5].Visible = false; //hago no visible el idcheque
            dgvContado.Columns[3].Visible = false; //hago no visible el idcontado
            /*dgvCheques.Columns[2].DefaultCellStyle.Format = "N2";           
            dgvContado.Columns[2].DefaultCellStyle.Format = "N2";*/

            txtIBB.Text = dsPagos.Tables[0].Rows[0][1].ToString();
            txtGanancias.Text = dsPagos.Tables[0].Rows[0][2].ToString();
            txtSUSS.Text = dsPagos.Tables[0].Rows[0][3].ToString();
            txtSaldo.Text = dsPagos.Tables[0].Rows[0][4].ToString(); 

            CalcularSaldo();
        }

        private void Salir()
        {
            Principal.Enabled = true;
            Principal.ActualizarFacturas();
            this.Dispose();
        }

        private void frmPagosFactura_FormClosing(object sender, FormClosingEventArgs e)
        {
            btnGrabar_Click(null, null);
        }

        private void CalcularSaldo()
        {
            DataSet dsAux = new DataSet();
            float pagado = 0;
            float ibb,ganancias,suss;
            int i;
            try
            {
                Conn.CargarDatos(dsAux, "dsAux", "select total from factura where idfactura = " + idfactura);
                txtSaldo.Text = dsAux.Tables[0].Rows[0][0].ToString();
                for (i = 0; i < dgvCheques.Rows.Count; i++)
                {
                    pagado = pagado + Utils.parseAndRound(dgvCheques.Rows[i].Cells[2].Value.ToString());
                }
                for (i = 0; i < dgvContado.Rows.Count; i++)
                {
                    pagado = pagado + Utils.parseAndRound(dgvContado.Rows[i].Cells[2].Value.ToString());
                }

                try
                {
                    ibb = Utils.parseAndRound(txtIBB.Text);
                }
                catch
                {
                    txtIBB.Text = "0";
                    ibb = 0;
                }
                try
                {
                    ganancias = Utils.parseAndRound(txtGanancias.Text);
                }
                catch
                {
                    txtGanancias.Text = "0";
                    ganancias = 0;
                }
                try
                {
                    suss = Utils.parseAndRound(txtSUSS.Text);
                }
                catch
                {
                    txtSUSS.Text = "0";
                    suss = 0;
                }

                txtSaldo.Text = Math.Round((Utils.parseAndRound(txtSaldo.Text) - pagado - (ibb + ganancias + suss)),2) + "";
            }
            catch { }
        }

        private void txtIBB_Leave(object sender, EventArgs e)
        {
            CalcularSaldo();
        }

        private void txtGanancias_Leave(object sender, EventArgs e)
        {
            CalcularSaldo();
        }

        private void txtSUSS_Leave(object sender, EventArgs e)
        {
            CalcularSaldo();
        }

        private void btnGrabar_Click(object sender, EventArgs e)
        {
           // int i;

            /*Conn.InsertOrUpdate("delete from pago_cheque where idfactura = " + idfactura);
            Conn.InsertOrUpdate("delete from pago_contado where idfactura = " + idfactura);  */         

            if (txtSaldo.Text == "0")
            {
                Conn.InsertOrUpdate("update pago_factura set ibb = " + txtIBB.Text + 
                                    ",ganancias = " + txtGanancias.Text + 
                                    ",suss = " + txtSUSS.Text + 
                                    ",saldo = " + txtSaldo.Text +
                                    ",pagado = true where idfactura = " + idfactura);
            }
            else
            {
                Conn.InsertOrUpdate("update pago_factura set ibb = " + txtIBB.Text +
                                    ",ganancias = " + txtGanancias.Text +
                                    ",suss = " + txtSUSS.Text +
                                    ",saldo = " + txtSaldo.Text +
                                    ",pagado = false where idfactura = " + idfactura);
            }
            Salir();
        }

        private void btnAgregarCheque_Click(object sender, EventArgs e)
        {
            formCheque = new frmCheque(this, Conn, idfactura);
            formCheque.Show();
        }

        private void btnAgregarContado_Click(object sender, EventArgs e)
        {
            formContado = new frmContado(this, Conn, idfactura);
            formContado.Show();
        }

        private void btnEliminarCheque_Click(object sender, EventArgs e)
        {
            try
            {
                Conn.InsertOrUpdate("delete from pago_cheque where idcheque = " + dgvCheques.SelectedRows[0].Cells[5].Value.ToString() + " and idfactura = " + idfactura);
                CargarPagos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("error en la eliminacion de cheque:" + ex.Message);
            }
        }

        private void btnEliminarContado_Click(object sender, EventArgs e)
        {
            try
            {
                Conn.InsertOrUpdate("delete from pago_contado where idcontado = " + dgvContado.SelectedRows[0].Cells[3].Value.ToString() + " and idfactura = " + idfactura);
                CargarPagos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("error en la eliminacion de contado:" + ex.Message);
            }
        }
    }
}
