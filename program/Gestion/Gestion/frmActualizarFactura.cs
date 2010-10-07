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
    public partial class frmActualizarFactura : Form
    {
        frmPrincipal prc;
        clsConexion C;
        String oldNumFact;
        public frmActualizarFactura(clsConexion C, frmPrincipal prc, List<String> datos)
        {
            InitializeComponent();
            this.C = C;
            this.prc = prc;
            this.oldNumFact = datos.ElementAt(0);
            textBoxNumFactura.Text = oldNumFact;
        }

        private void btnActualizarFacturaAceptar_Click(object sender, EventArgs e)
        {
            String numFact = textBoxNumFactura.Text.ToString();
            try{
            if (!prc.facturaValida(numFact))
                {
                    MessageBox.Show("El número de factura ya existe o es incorrecto");
                }
                else
                {
                    C.InsertOrUpdate("update factura set idfactura=" + numFact+ " where idfactura="+ oldNumFact);
                    C.InsertOrUpdate("update pago_factura set idfactura=" + numFact + " where idfactura=" + oldNumFact);
                    C.InsertOrUpdate("update pago_cheque set idfactura=" + numFact + " where idfactura=" + oldNumFact);
                    C.InsertOrUpdate("update pago_contado set idfactura=" + numFact + " where idfactura=" + oldNumFact);
                    C.InsertOrUpdate("update remito set idfactura=" + numFact + " where idfactura=" + oldNumFact);
                    prc.ActualizarFacturas();
                    this.Dispose();

                }
            }
            catch
            {
            }

        }

        private void frmActualizarFactura_Load(object sender, EventArgs e)
        {

        }
    }
}
