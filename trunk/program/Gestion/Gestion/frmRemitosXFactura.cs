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
    public partial class frmRemitosXFactura : Form
    {
        clsConexion Conn;
        clsRemito Remito;
        frmPrincipal principal;
        clsImpresion Impresion = new clsImpresion();
        int indexRemito;

        String idfactura;

        public frmRemitosXFactura(clsConexion C, frmPrincipal prc,String idfact)
        {
            InitializeComponent();
            Conn = C;
            principal = prc;
            principal.Enabled = false;
            idfactura = idfact;           
            Remito = new clsRemito();
            cargarRemitos();
            
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Salir();
        }

        private void frmRemitosXFactura_FormClosing(object sender, FormClosingEventArgs e)
        {
            Salir();
        }

        private void Salir()
        {
            principal.Enabled = true;
            this.Dispose();
        }

        private void cargarRemitos()
        {
            try
            {
                Remito.CargarRemitosSegunFactura(Conn, dataGridInfoRemitos, idfactura);
                textBoxTotalNetoInfoRemitos.Text = (Remito.calcTotalRemitos(dataGridInfoRemitos)).ToString();
            }
            catch { }
        }

        private void textBoxTotalNetoInfoRemitos_TextChanged(object sender, EventArgs e)
        {
            try
            {
                txtTotalIVAInfoRemito.Text = Math.Round((Utils.parseAndRound(textBoxTotalNetoInfoRemitos.Text) * 1.21), 2).ToString();
            }
            catch { }
        }

        private void dataGridInfoRemitos_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                textBoxTotalNetoInfoRemitos.Text = (Remito.calcTotalRemitos(dataGridInfoRemitos)).ToString();
            }
            catch { }
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            int i = 0;
            Boolean encontre = false;
            while ((i < dataGridInfoRemitos.RowCount) && (!encontre))
            {
                encontre = (dataGridInfoRemitos.Rows[i].Cells[0].Value.ToString() == "True");
                i++;
            }
            if (encontre)
            {
                if(printDialogRemitos.ShowDialog() == DialogResult.OK)
                {
                        printRemitos.PrinterSettings = printDialogRemitos.PrinterSettings;
                        indexRemito = 0;
                        printRemitos.DefaultPageSettings.Landscape = true;
                        printRemitos.Print();
                }
            }
            else
            {
                    MessageBox.Show("No hay ningun remito seleccionado", "Gestión - Remitos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void printRemitos_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            int nuevoindex;
            nuevoindex = Impresion.InfoRemitos(Conn, dataGridInfoRemitos, indexRemito, principal.verNroClienteFactura(), principal.verNombreClienteFactura() + " " + principal.verApellidoClienteFactura(), idfactura, principal.verPeriodoInicialFactura(), principal.verPeriodoFinalFactura(), e);
            indexRemito = nuevoindex;
        }
    }
}
