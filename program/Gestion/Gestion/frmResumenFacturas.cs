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
    public partial class frmResumenFacturas : Form
    {
        clsConexion Conn;
        frmPrincipal principal;
        clsImpresion Impresion = new clsImpresion();
        String clienteSeleccionado = "1";
        String filtro = "Todas";

        public frmResumenFacturas(clsConexion C, frmPrincipal prc)
        {
            Conn = C;
            principal = prc;
            InitializeComponent();
        }

        private void frmResumenFacturas_Load(object sender, EventArgs e)
        {
            int i;
            DataSet dsClientes = new DataSet();

            rbtnTodasLasFacturas.Checked = true;
            cmbClientesFacturas.Enabled = false;
            rbtnTodas.Checked = true;

            Conn.CargarDatos(dsClientes, "dsClientes", "select idcliente,nombre,apellido from clientes");

            for (i = 0; i < dsClientes.Tables[0].Rows.Count; i++)
            {
                cmbClientesFacturas.Items.Add(dsClientes.Tables[0].Rows[i][0].ToString() + " - " + dsClientes.Tables[0].Rows[i][1].ToString() + dsClientes.Tables[0].Rows[i][2].ToString());
            }
            cmbClientesFacturas.SelectedIndex = 0;
        }

        private void btnOKResumenFacturas_Click(object sender, EventArgs e)
        {
            DataSet dsResumenFacturas = new DataSet();
            if (rbtnTodasLasFacturas.Checked)
            {
                if (printDialogResumenFacturas.ShowDialog() == DialogResult.OK)
                {
                    printResumenFacturas.Print();
                }
            }
            else
            {
                if (printDialogFacturasUnCliente.ShowDialog() == DialogResult.OK)
                {
                    printFacturasUnCliente.Print();
                }
            }

            this.Hide();
        }

        private void rbtnFactDeUnCliente_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnFactDeUnCliente.Checked == false)
                cmbClientesFacturas.Enabled = false;
            else
                cmbClientesFacturas.Enabled = true;
        }

        private void printResumenFacturas_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            int index = 0;
            String fechaInicial, fechaFinal;
            if(checkBoxPeriodo.Checked)
            {
                fechaInicial = dateInicioResumenFacturas.Value.ToString("yyyy-MM-dd");
                fechaFinal = dateFinResumenFacturas.Value.ToString("yyyy-MM-dd");
            }
            else
            {
                fechaInicial = "2000-01-01";
                fechaFinal = "2100-01-01";
            }

            index = Impresion.ResumenGeneralFacturas(Conn, filtro, fechaInicial, fechaFinal, e, 0);
            while (e.HasMorePages)
            {
                index = Impresion.ResumenGeneralFacturas(Conn, filtro, fechaInicial, fechaFinal, e, 0);
            }
        }

        private void printFacturasUnCliente_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            int index = 0;
            String fechaInicial, fechaFinal;
            if (checkBoxPeriodo.Checked)
            {
                fechaInicial = dateInicioResumenFacturas.Value.ToString("yyyy-MM-dd");
                fechaFinal = dateFinResumenFacturas.Value.ToString("yyyy-MM-dd");
            }
            else
            {
                fechaInicial = "";
                fechaFinal = "";
            }
            index = Impresion.FacturasDeUnCliente(Conn, clienteSeleccionado, filtro, fechaInicial, fechaFinal,e,0);
            while (e.HasMorePages)
            {
                index = Impresion.FacturasDeUnCliente(Conn, clienteSeleccionado, filtro, fechaInicial, fechaFinal, e, index);
            }
        }

        private int FinNumero(String cadena)
        {
            Boolean encontre = false;
            int i = 0;
            int aux = 0;
            while ((i < cadena.Length) && (!encontre))
            {
                encontre = (cadena[i] == ' ');
                if (encontre)
                    aux = i;
                else i++;
            }
            return aux;
        }

        private void cmbClientesFacturas_SelectedIndexChanged(object sender, EventArgs e)
        {
            String CadenaActual = cmbClientesFacturas.Items[cmbClientesFacturas.SelectedIndex].ToString();

            clienteSeleccionado = CadenaActual.Remove(FinNumero(CadenaActual));
        }

        private void rbtnTodas_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnTodas.Checked)
                filtro = "Todas";
        }

        private void rbtnPagas_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnPagas.Checked)
                filtro = "Pagas";
        }

        private void rbtnImpagas_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnImpagas.Checked)
                filtro = "Impagas";
        }

    }
}
