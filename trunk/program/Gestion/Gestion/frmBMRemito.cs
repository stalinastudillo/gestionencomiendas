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


    public partial class frmBMRemito : Form
    {

        frmPrincipal principal;
        clsConexion Conn;
        clsFunciones Funciones = new clsFunciones();
        clsConexion C_city;
        clsRemito Remito = new clsRemito();

        String idRemito;
        String idCliente;
        String oldCondVta;
        String iduser;
        String oldRemito;
        int cantProd = 0;

        public frmBMRemito(clsConexion C,clsConexion C2, frmPrincipal prc, String idR,String idU)
        {
            InitializeComponent();
            Conn = C;
            C_city = C2;
            principal = prc;
            idRemito = idR;
            iduser = idU;

            principal.Enabled = false;

            CargarRemito();

        }

        private void btnAddProdRemito_Click(object sender, EventArgs e)
        {          
            int indice;

            if (dataGridProductosRemito.Rows.Count != 0)
            {
                cantProd++;
                indice = (YaInsertado(dataGridProductosRemito.SelectedRows[0].Cells[2].Value.ToString(), dataGridProdVtaRemito));
                if ((indice) == -1)
                {
                    dataGridProdVtaRemito.Rows.Add();
                    dataGridProdVtaRemito.Rows[dataGridProdVtaRemito.Rows.Count - 1].Cells[0].Value = "1";
                    dataGridProdVtaRemito.Rows[dataGridProdVtaRemito.Rows.Count - 1].Cells[1].Value = dataGridProductosRemito.SelectedRows[0].Cells[0].Value;
                    dataGridProdVtaRemito.Rows[dataGridProdVtaRemito.Rows.Count - 1].Cells[2].Value = "0";
                    dataGridProdVtaRemito.Rows[dataGridProdVtaRemito.Rows.Count - 1].Cells[3].Value = dataGridProductosRemito.SelectedRows[0].Cells[1].Value;
                    dataGridProdVtaRemito.Rows[dataGridProdVtaRemito.Rows.Count - 1].Cells[4].Value = dataGridProductosRemito.SelectedRows[0].Cells[2].Value;
                    ActualizarMontos();
                }
                else
                {
                    dataGridProdVtaRemito.Rows[indice].Cells[0].Value = (int.Parse(dataGridProdVtaRemito.Rows[indice].Cells[0].Value.ToString()) + 1) + "";
                    ActualizarMontos();
                }
            }
        }

        private void btnDelProdRemito_Click(object sender, EventArgs e)
        {
            if (dataGridProdVtaRemito.Rows.Count != 0)
            {
                cantProd = cantProd - int.Parse(dataGridProdVtaRemito.SelectedRows[0].Cells[0].Value.ToString());
                dataGridProdVtaRemito.Rows.Remove(dataGridProdVtaRemito.Rows[dataGridProdVtaRemito.SelectedCells[0].RowIndex]);
            }
            ActualizarMontos();
        }

        private void ActualizarMontos()
        {
            try
            {
                int count = 0;
                float flete = 0;
                float seguro = 0;
                DataSet dsC = new DataSet();

                Conn.CargarDatos(dsC, "dsC", "select porcentaje_vd,porcentaje_cr from clientes where idcliente = " + idCliente);
                while (count < dataGridProdVtaRemito.Rows.Count)
                {
                    flete = flete + (Utils.parseAndRound(dataGridProdVtaRemito.Rows[count].Cells[0].Value.ToString()) * Utils.parseAndRound(dataGridProdVtaRemito.Rows[count].Cells[3].Value.ToString()));
                    count++;
                }
                seguro = (Utils.parseAndRound(txtVD.Text) * Utils.parseAndRound(dsC.Tables[0].Rows[0][0].ToString())) / 100 + ((Utils.parseAndRound(txtCR.Text) * Utils.parseAndRound(dsC.Tables[0].Rows[0][1].ToString())) / 100);

                if (cmbCondVtaRemito.SelectedItem.ToString() == "Contado")
                {
                    txtFleteC.Text = flete + "";
                    txtSeguroC.Text = seguro + "";
                }
                else
                {
                    txtFleteAC.Text = flete + "";
                    txtSeguroAC.Text = seguro + "";
                }
            }
            catch (Exception e) { MessageBox.Show(e.Message); }
        }

        private int YaInsertado(String codigo, DataGridView grid)
        {
            int index, total;
            Boolean encontre = false;

            index = 0;
            total = grid.Rows.Count;

            while ((index < total) && (!encontre))
            {
                encontre = (grid.Rows[index].Cells[4].Value.ToString()) == codigo;
                index++;
            }
            if (encontre)
            {
                return --index;
            }
            else return -1;

        }

        private void txtCUITd_Leave(object sender, EventArgs e)
        {
            if ((!(txtCUITd.Text == "  -        -")) && (!Funciones.CuiltValido(txtCUITd.Text)))
            {
                MessageBox.Show("Por favor ingrese un CUIT/L válido.", "Gestión - Remito", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCUITd.Text = "";
            }
        }

        private void txtCUITr_Leave(object sender, EventArgs e)
        {
            if ((!(txtCUITr.Text == "  -        -")) && (!Funciones.CuiltValido(txtCUITr.Text)))
            {
                MessageBox.Show("Por favor ingrese un CUIT/L válido.", "Gestión - Remito", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCUITr.Text = "";
            }
        }

        private void txtVD_Leave(object sender, EventArgs e)
        {
            try
            {
                Funciones.ValidarNumero(txtVD, "Por favor ingrese un número.", "Gestión - Remito");
                ActualizarMontos();
            }
            catch { }
        }

        private void txtCR_Leave(object sender, EventArgs e)
        {
            try
            {
                Funciones.ValidarNumero(txtCR, "Por favor ingrese un número.", "Gestión - Remito");
                ActualizarMontos();
            }
            catch { }
        }

        private void dataGridProdVtaRemito_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //AbrirFormulario(formCantidadPrecio, "cantidad_precio", null);
        }
        
        private void CargarProductosRemito()
        {
            DataSet dsProductos = new DataSet();
            int cant = 0;
            try
            {
                Conn.CargarDatos(dsProductos, "dsProductos", "select nroprod_vendido,cantidad,descripcion,kg,precio from productos_vendidos where idremito = " + idRemito);
                while (cant < dsProductos.Tables[0].Rows.Count)
                {
                    dataGridProdVtaRemito.Rows.Add();
                    dataGridProdVtaRemito.Rows[cant].Cells[0].Value = dsProductos.Tables[0].Rows[cant][1].ToString();
                    dataGridProdVtaRemito.Rows[cant].Cells[1].Value = dsProductos.Tables[0].Rows[cant][2].ToString();
                    dataGridProdVtaRemito.Rows[cant].Cells[2].Value = dsProductos.Tables[0].Rows[cant][3].ToString();
                    dataGridProdVtaRemito.Rows[cant].Cells[3].Value = dsProductos.Tables[0].Rows[cant][4].ToString();
                    dataGridProdVtaRemito.Rows[cant].Cells[4].Value = dsProductos.Tables[0].Rows[cant][0].ToString();

                    cant++;
                }
                cantProd = cant;
                ActualizarMontos();
            }
            catch { }
        }

        private void ObtenerCliente()
        {
            DataSet dsCliente = new DataSet();

            try
            {
                Conn.CargarDatos(dsCliente, "dsCliente", "select idcliente from remito where idremito = " + idRemito);
                idCliente = dsCliente.Tables[0].Rows[0][0].ToString();
            }
            catch { }
        }

        private void CargarEncabezado()        
        {
            DataSet dsEncabezado = new DataSet();

            Conn.CargarDatos(dsEncabezado, "dsEncabezado", "select nro_remito,remitente,domicilior,localidadr,cuilr,destinatario,domiciliod,localidadd,cuild,fecha,observacion,cond_vta,iva,valor_declarado,contrareembolso from remito where idremito = " + idRemito);
            txtNumeroR.Text = dsEncabezado.Tables[0].Rows[0][0].ToString().Replace("'","");
            txtRemitente.Text = dsEncabezado.Tables[0].Rows[0][1].ToString().Replace("'", "");
            txtDomicilior.Text = dsEncabezado.Tables[0].Rows[0][2].ToString().Replace("'", "");            
            cmbLocalidadr.SelectedItem = dsEncabezado.Tables[0].Rows[0][3].ToString().Replace("'","");
            txtCUITr.Text = dsEncabezado.Tables[0].Rows[0][4].ToString().Replace("'", "");
            txtDestinatario.Text = dsEncabezado.Tables[0].Rows[0][5].ToString().Replace("'", "");
            txtDomiciliod.Text = dsEncabezado.Tables[0].Rows[0][6].ToString().Replace("'", "");
            cmbLocalidadd.SelectedItem = dsEncabezado.Tables[0].Rows[0][7].ToString().Replace("'", "");
            txtCUITd.Text = dsEncabezado.Tables[0].Rows[0][8].ToString().Replace("'", "");
            dateFechaRemito.Text = dsEncabezado.Tables[0].Rows[0][9].ToString().Replace("'", "");
            txtObservacionR.Text = dsEncabezado.Tables[0].Rows[0][10].ToString().Replace("'", "");
            cmbCondVtaRemito.SelectedItem = dsEncabezado.Tables[0].Rows[0][11].ToString().Replace("'", "");
            cmbIvaRemito.SelectedItem = dsEncabezado.Tables[0].Rows[0][12].ToString().Replace("'", "");
            txtVD.Text = dsEncabezado.Tables[0].Rows[0][13].ToString().Replace("'", "");
            txtCR.Text = dsEncabezado.Tables[0].Rows[0][14].ToString().Replace("'", "");

            oldCondVta = cmbCondVtaRemito.SelectedItem.ToString();
            oldRemito = txtNumeroR.Text;
        }

        private void CargarProductosCliente()
        {
            DataSet dsProdRemito = new DataSet();

            Conn.CargarDatos(dataGridProductosRemito, dsProdRemito, "dsProdRemito", "select nombre as Producto,precio as Precio,codigo from producto where idcliente = " + idCliente);
            //oculto el numero de producto. Lo necesito para no insertar prod repetidos.
            dataGridProductosRemito.Columns[2].Visible = false;
        }

        private void CargarCiudades()
        {
            DataSet dsCiudad = new DataSet();
            
            C_city.CargarDatos(dsCiudad, "dsCiudad", "select ciudad from city order by idcity");
            Funciones.CargarDatos(cmbLocalidadr, dsCiudad);
            Funciones.CargarDatos(cmbLocalidadd, dsCiudad);
        }

        private void cmbCondVtaRemito_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (oldCondVta != cmbCondVtaRemito.SelectedItem.ToString())
            {
                if (cmbCondVtaRemito.SelectedItem.ToString() == "Contado")
                {
                    txtFleteC.Text = txtFleteAC.Text;
                    txtSeguroC.Text = txtSeguroAC.Text;
                    txtFleteAC.Text = "0";
                    txtSeguroAC.Text = "0";
                }
                else
                {
                    txtFleteAC.Text = txtFleteC.Text;
                    txtSeguroAC.Text = txtSeguroC.Text;
                    txtFleteC.Text = "0";
                    txtSeguroC.Text = "0";
                }
                oldCondVta = cmbCondVtaRemito.SelectedItem.ToString();
            }
        }

        private void CargarRemito()
        {
            CargarCiudades();
            ObtenerCliente();
            CargarEncabezado();
            CargarProductosRemito();
            CargarProductosCliente();
        }

        private void btnGrabarRemito_Click(object sender, EventArgs e)
        {
            Boolean remitoRepetido;
            DataSet auxRem = new DataSet();
            if (oldRemito != txtNumeroR.Text)
            {
                Conn.CargarDatos(auxRem, "auxRem", "select nro_remito from remito where nro_remito=" + "'" + txtNumeroR.Text + "'");
                if (auxRem.Tables[0].Rows.Count != 0)
                    remitoRepetido = true;
                else
                    remitoRepetido = false;

                if (!remitoRepetido)
                {
                    EliminarRemito();
                    guardarRemito();
                }
                else
                    MessageBox.Show("El remito ingresado ya existe en el sistema", "Gestion - Remito", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                EliminarRemito();
                guardarRemito();
            }
        }

        private void guardarRemito()
        {
            List<String> datos = new List<string>(21);

            if (cantProd != 0)
            {
                datos.Clear();
                datos.Add(dateFechaRemito.Value.ToString("yyyy-MM-dd"));
                datos.Add(txtRemitente.Text);
                datos.Add(txtDomicilior.Text);
                datos.Add(cmbLocalidadr.SelectedItem.ToString());
                datos.Add(txtCUITr.Text);
                datos.Add(txtDestinatario.Text);
                datos.Add(txtDomiciliod.Text);
                datos.Add(cmbLocalidadd.SelectedItem.ToString());
                datos.Add(txtCUITd.Text);
                datos.Add(cmbCondVtaRemito.SelectedItem.ToString());
                datos.Add(cmbIvaRemito.SelectedItem.ToString());
                datos.Add(txtVD.Text.Replace(",", "."));
                datos.Add(txtCR.Text.Replace(",", "."));

                if (cmbCondVtaRemito.SelectedItem.ToString() == "Contado")
                {
                    datos.Add(txtFleteC.Text.Replace(",", "."));
                    datos.Add(txtSeguroC.Text.Replace(",", "."));
                }
                else
                {
                    datos.Add(txtFleteAC.Text.Replace(",", "."));
                    datos.Add(txtSeguroAC.Text.Replace(",", "."));
                }
                datos.Add(txtObservacionR.Text);
                datos.Add(idCliente);
                datos.Add(txtNumeroR.Text);
                datos.Add(iduser);
                Remito.Insertar(Conn, datos, dataGridProdVtaRemito);

                Funciones.limpiarCampos(this);
                cantProd = 0;
 
                    MessageBox.Show("Remito modificado exitosamente", "Gestion - Remito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Salir();
            }
            else
                MessageBox.Show("No hay productos seleccionados", "Gestion - Remito", MessageBoxButtons.OK, MessageBoxIcon.Warning);            
        }

        private void EliminarRemito()
        {
            Conn.InsertOrUpdate("delete from productos_vendidos where idremito = " + idRemito);
            Conn.InsertOrUpdate("delete from remito where idremito = " + idRemito);
        }

        private void frmBMRemito_FormClosing(object sender, FormClosingEventArgs e)
        {
            Salir();
        }

        public void CargarDatosCantidadPrecio(String cantidad, String precio)
        {
            dataGridProdVtaRemito.SelectedRows[0].Cells[0].Value = cantidad;
            dataGridProdVtaRemito.SelectedRows[0].Cells[3].Value = precio;
            ActualizarMontos();
        }
        
        private void Salir()
        {
            principal.ActualizarInfoRemitos();
            principal.Enabled = true;
            this.Dispose();
        }

        private void dataGridProdVtaRemito_CellDoubleClick_1(object sender, DataGridViewCellEventArgs e)
        {
            frmCantidadPrecio formulario = new frmCantidadPrecio(this,"bmremito");
            formulario.Show();
            formulario.ActualizarDatos(dataGridProdVtaRemito.SelectedRows[0].Cells[0].Value.ToString(), dataGridProdVtaRemito.SelectedRows[0].Cells[3].Value.ToString());            
        }

        private void btnCancelarRemito_Click(object sender, EventArgs e)
        {
            Salir();
        }


        
    }
}




