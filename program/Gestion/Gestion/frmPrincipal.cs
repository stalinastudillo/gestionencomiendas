using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.Odbc;
using System.Globalization;
using System.Drawing.Printing;
using System.Collections;

namespace Gestion
{
    public partial class frmPrincipal : Form
    {

        //DataSet////////
        DataSet dsC = new DataSet();
        DataSet dsProdRemito = new DataSet();
        DataSet dsProd = new DataSet();
        DataSet dsCiudad = new DataSet();
        ////////////

        //Variables globales de uso gral////
        String idclienteR = "";  //se usa para el remito
        String idclienteL = ""; //se usa para la liquidacion
        String idclienteInfoRemitos = "";//se usa para la info de remitos
        String idclienteInfoFactura = "";//se usa para la tab de factura
        List<String> destino = new List<string>(1);
        String cadena_conexion;
        String cadena_conexion_ciudad;
        String cadena_conexion_user;
        String iduser;
        String FiltroRemitos = "Todos";
        String FiltroFactura = "Todos";
        clsImpresion Impresion = new clsImpresion();
        int index_cliente = 0;
        int indexInfoRemitos;
        int cantProd = 0;
        String oldCondVta;


        ////////////////////////////////////

        //Clases////////
        clsFunciones Funciones = new clsFunciones();
        clsConexion C = new clsConexion();
        clsConexion C_city = new clsConexion();
        clsConexion C_user = new clsConexion();
        clsCliente Cliente = new clsCliente();
        clsProducto Producto = new clsProducto();
        clsRemito Remito = new clsRemito();
        ////////////////


        //String de consultas////////////////
        String all_clientes = "select idcliente as NroCliente,cuil as Cuil,nombre as Empresa,apellido as Apellido,direccion as Dirección,ciudad as Ciudad,codigo_postal as CP,telefono as Teléfono,celular as Celular,email as Email,porcentaje_vd as VD, porcentaje_cr as CR,credito as Crédito from clientes where idcliente>1";
        String all_productos = "select producto.codigo as Código, producto.nombre as Nombre, producto.precio as Precio, clientes.nombre as Nombre_Cliente,clientes.Apellido as Apellido_Cliente, descripcion as Descripción from producto join clientes where clientes.idcliente = producto.idcliente";
        /////////////////////////////////////

 

        //Declaración de los formularios/////
        frmAddCliente formAddCliente;
        frmAddProducto formAddProducto;
        frmAjusteProd formAjusteProd;
        frmBusquedaCliente formBusquedaCliente;
        frmUpdateCliente formUpdateCliente;
        frmPrintFactura formPrintFactura;
        frmUpdateProducto formUpdateProducto;
        frmCantidadPrecio formCantidadPrecio;
        frmActualizarFactura formActualizarFactura;
        frmEmpresa formEmpresa;
        frmGestionUsuario formGestionUsuarios;
        frmCiudad formCiudad;
        frmRemitoContado formRemitoContado;
        frmRemitosXFactura formRemitosXFactura;
        frmResumenFacturas formResumenFacturas;
        
        /////////////////////////////////////

        private void setupTooltips()
        {
            ToolTip toolTip1 = new ToolTip();

            // Set up the delays for the ToolTip.
            toolTip1.AutoPopDelay = 5000;
            toolTip1.InitialDelay = 750;
            toolTip1.ReshowDelay = 500;
            // Force the ToolTip text to be displayed whether or not the form is active.
            toolTip1.ShowAlways = true;

            // Set up the ToolTip text for the Button and Checkbox.
            toolTip1.SetToolTip(this.btnAgregarCliente, "Agregar Cliente");
            toolTip1.SetToolTip(this.btnListaClientes, "Imprimir precios por cliente");
            toolTip1.SetToolTip(this.btnAjustarProd, "Ajustar porcentaje");
            toolTip1.SetToolTip(this.btnAddProducto, "Agregar Producto");
            toolTip1.SetToolTip(this.btnEliminarProd, "Eliminar Producto");
            toolTip1.SetToolTip(this.btnEliminarCliente, "Eliminar Cliente");
            toolTip1.SetToolTip(this.btnRegistrarPagosFactura, "Registrar factura paga");
            toolTip1.SetToolTip(this.btnResumenFacturas, "Imprimir resúmen de facturas");
            toolTip1.SetToolTip(this.btnCambiarNumFactura, "Cambiar número de factura");
            toolTip1.SetToolTip(this.btnBorrarFactura, "Borrar factura");
        }
        
        public frmPrincipal()
        {
            InitializeComponent();
            setupTooltips();
            destino.Add("");  ///???
            
        }

        

        public void ActualizarFechaBackup(String newDate)
        {
            C.InsertOrUpdate("truncate ultimaFecha");
            C.InsertOrUpdate("insert into ultimaFecha(fecha) values" + "('" + newDate + "')");
        }

        public String getIdUser(){
            return(iduser);
        }


        public bool existeFactura(String numFact)
        {
            bool res = false;
            DataSet dataSet = null;
            try
            {
                dataSet = new DataSet();
                C.CargarDatos(dataSet, "dataSet", "select idfactura from factura where idfactura=" + numFact);
                res = dataSet.Tables[0].Rows.Count != 0;

            }
            catch
            {
            }
            return res;

        }

        private bool existePago(String numFactura)
        {
            DataSet dataSet = new DataSet();
            bool res=false;
            String query;
            try
            {
                query = "select * from pago_factura where idfactura="+numFactura;

                C.CargarDatos(dataSet, "DataSet",query);
                res = dataSet.Tables[0].Rows.Count != 0;
            }
            catch
            {
               
            }
            return res;
        }

        private void updateFact() {
            String nonQuery = "";
            DataSet dataSet = new DataSet();

            try
            {
                C.CargarDatos(dataSet, "DataSet", "select idfactura,total from factura");
                for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
                {
                    String numFact = dataSet.Tables[0].Rows[i][0].ToString();
                    String total = dataSet.Tables[0].Rows[i][1].ToString();
                    if (!existePago(numFact))
                    {
                        nonQuery = "insert into pago_factura(idfactura,pagado,ibb,ganancias,suss,saldo) values("+numFact+",false,null,null,null,"+total+")";

                        C.InsertOrUpdate(nonQuery);
                    }
                }
            }
            catch
            {

            }
        }

        private void btnAgregarCliente_Click(object sender, EventArgs e)
        {
            AbrirFormulario(formAddCliente, "addcliente", null);
        }

        private void frmPrincipal2_Load(object sender, EventArgs e)
        {

            
            cmbBuscarpor_Cliente.SelectedIndex = 1;
            cmbBuscarpor_Producto.SelectedIndex = 0;
            rbtnTodos.Checked = true;
            rbtnTodosFactura.Checked = true;

            C.Conectar(cadena_conexion);
            C_city.Conectar(cadena_conexion_ciudad);
            C_user.Conectar(cadena_conexion_user);

            //Cargo los datos en cada uno de los grid//////////
            cargarClientes();
            cargarProductos();
            cargarCiudades();
            //////////

            //del tab remito//////////////////
            cmbCondVtaRemito.SelectedIndex = 1;
            cmbIvaRemito.SelectedIndex = 0;
            cmbLocalidadr.SelectedIndex = 0;
            cmbLocalidadd.SelectedIndex = 0;
            //////////////////////////////////


            checkBackup();
            oldCondVta = cmbCondVtaRemito.SelectedItem.ToString();

            MostrarUser();
            //updateFact();
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            if (txtBuscar.Text != "")
            {
                String consulta;
                txtBuscar.Text = txtBuscar.Text.Replace("'", "");
                
                switch(cmbBuscarpor_Cliente.Items[cmbBuscarpor_Cliente.SelectedIndex].ToString()){
                    case "Empresa" : consulta = "nombre";
                                     break;
                    case "Numero Cliente": consulta="idcliente";
                                            break;
                    default: consulta = cmbBuscarpor_Cliente.Items[cmbBuscarpor_Cliente.SelectedIndex].ToString();
                                            break;
                }

                
                consulta.ToLower();
                
                C.CargarDatos(dataGridClientes, dsC, "dsC", all_clientes + " and " + consulta + " LIKE '" + txtBuscar.Text + "%'");
            }
            else
            {
                cargarClientes();
            }
        }

        private void txtBuscarProd_TextChanged(object sender, EventArgs e)
        {
            if (txtBuscarProd.Text != "")
            {
                txtBuscarProd.Text = txtBuscarProd.Text.Replace("'", "");
                C.CargarDatos(dataGridProducto, dsProd, "dsProd", all_productos + " and producto." + cmbBuscarpor_Producto.Items[cmbBuscarpor_Producto.SelectedIndex].ToString() + " LIKE '" + txtBuscarProd.Text + "%'");
            }
            else
            {
                cargarProductos();
            }
        }

        private void btnEliminarCliente_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult resultado;

                DataSet prodCliente = new DataSet();
                C.CargarDatos(prodCliente, "prodCliente", "select codigo from producto where idcliente=" + dataGridClientes.SelectedRows[0].Cells[0].Value.ToString());
                if (prodCliente.Tables[0].Rows.Count == 0)
                {
                    if ((dataGridClientes.Rows.Count - 1 != 0) && (dataGridClientes.SelectedRows[0].Index != dataGridClientes.RowCount - 1))
                    {
                        resultado = MessageBox.Show("Esta seguro que desea borrar el cliente " + dataGridClientes.SelectedRows[0].Cells[3].Value.ToString() + " " + dataGridClientes.SelectedRows[0].Cells[2].Value.ToString() + " ?", "Gestion - Borrar Cliente", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                        if (resultado == DialogResult.Yes)
                        {
                            Cliente.Eliminar(C, dataGridClientes.SelectedRows[0].Cells[0].Value.ToString());
                            cargarClientes();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("No se puede eliminar el cliente porque tiene productos asignados", "Gestion - Clientes", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch { }
        }

        private void btnAddProducto_Click(object sender, EventArgs e)
        {
            AbrirFormulario(formAddProducto, "addproducto", null);       
        }

        private void btnEliminarProd_Click(object sender, EventArgs e)
        {
            String nombre = "";
            DialogResult resultado;

            if ((dataGridProducto.Rows.Count - 1 != 0) && (dataGridProducto.SelectedRows[0].Index != dataGridProducto.RowCount - 1))
            {
                nombre = dataGridProducto.SelectedRows[0].Cells[1].Value.ToString();
                resultado = MessageBox.Show("Esta seguro que desea borrar el producto " + nombre + " ?", "Gestion - Borrar Producto", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (resultado == DialogResult.Yes)
                {
                    Producto.Eliminar(C, dataGridProducto.SelectedRows[0].Cells[0].Value.ToString());
                    cargarProductos();
                }
            }
        }

        private void checkBackup()
        {
            //string newDate = C.needBackup();
            //if (newDate != null)
            //{
            //    String pathCommand = System.IO.Path.GetDirectoryName(Application.ExecutablePath).ToString();
            //    pathCommand = pathCommand + @"\createBackup.bat";

            //    ActualizarFechaBackup(newDate);
            //    try
            //    {
            //        System.Diagnostics.Process p = new System.Diagnostics.Process();
            //        p.StartInfo.FileName = pathCommand;
            //        p.StartInfo.Arguments = "\"" + "backup.sql" + "\"";

            //        p.StartInfo.UseShellExecute = false;
            //        p.StartInfo.RedirectStandardInput = true;
            //        p.StartInfo.RedirectStandardOutput = true;
            //        p.StartInfo.RedirectStandardError = true;
            //        p.StartInfo.CreateNoWindow = true;
            //        p.Start();
            //        p.WaitForExit();

            //        p.Close();

            //    }
            //    catch (Exception ex)
            //    {
            //        MessageBox.Show(ex.Message);
            //    }
            //}
        }

        private void btnAjustarProd_Click(object sender, EventArgs e)
        {
            if ((dataGridProducto.Rows.Count - 1 != 0) && (dataGridProducto.SelectedRows[0].Index != dataGridProducto.RowCount - 1))
            {
                AbrirFormulario(formAjusteProd, "ajusteproducto", null);
            }
        }

        private void dataGridClientes_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            List<String> datos = new List<String>(13);
            if (dataGridClientes.SelectedRows[0].Index != dataGridClientes.RowCount - 1)
            {
                datos.Add(dataGridClientes.SelectedRows[0].Cells[0].Value.ToString());
                datos.Add(dataGridClientes.SelectedRows[0].Cells[1].Value.ToString());
                datos.Add(dataGridClientes.SelectedRows[0].Cells[2].Value.ToString());
                datos.Add(dataGridClientes.SelectedRows[0].Cells[3].Value.ToString());
                datos.Add(dataGridClientes.SelectedRows[0].Cells[4].Value.ToString());
                datos.Add(dataGridClientes.SelectedRows[0].Cells[5].Value.ToString());
                datos.Add(dataGridClientes.SelectedRows[0].Cells[6].Value.ToString());
                datos.Add(dataGridClientes.SelectedRows[0].Cells[7].Value.ToString());
                datos.Add(dataGridClientes.SelectedRows[0].Cells[8].Value.ToString());
                datos.Add(dataGridClientes.SelectedRows[0].Cells[9].Value.ToString());
                datos.Add(dataGridClientes.SelectedRows[0].Cells[10].Value.ToString());
                datos.Add(dataGridClientes.SelectedRows[0].Cells[11].Value.ToString());
                datos.Add(dataGridClientes.SelectedRows[0].Cells[12].Value.ToString());

                AbrirFormulario(formUpdateCliente, "updatecliente", datos);
            }
            else
            {
                AbrirFormulario(formAddCliente, "addcliente", null);
            }
        }

        private void dataGridProducto_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            List<String> datos = new List<String>(6);

            if (dataGridProducto.SelectedRows[0].Index != dataGridProducto.RowCount - 1)
            {
                datos.Add(dataGridProducto.SelectedRows[0].Cells[0].Value.ToString());
                datos.Add(dataGridProducto.SelectedRows[0].Cells[1].Value.ToString());
                datos.Add(dataGridProducto.SelectedRows[0].Cells[2].Value.ToString().Replace(",", "."));
                datos.Add(dataGridProducto.SelectedRows[0].Cells[3].Value.ToString());
                datos.Add(dataGridProducto.SelectedRows[0].Cells[4].Value.ToString());
                datos.Add(dataGridProducto.SelectedRows[0].Cells[5].Value.ToString());

                AbrirFormulario(formUpdateProducto, "updateproducto", datos);
            }
            else
            {
                AbrirFormulario(formAddProducto, "addproducto", null);
            }
        }

        private void ActualizarCantidadProd(DataSet data, String codigo, String cantidad)
        {
            data.Tables[0].Select("código = " + codigo).ElementAt(0).SetField("cantidad", cantidad);
        }

        private void AbrirFormulario(Form formulario, String tipo, List<String> datos)
        {
            if ((formulario == null) || (formulario.IsDisposed))
            {
                switch (tipo)
                {
                    case "addcliente":

                        formulario = new frmAddCliente(C,C_city, this);
                        formulario.Show();
                        break;

                    case "addproducto":

                        formulario = new frmAddProducto(C, this);
                        formulario.Show();
                        break;

                    case "ajusteproducto":

                        formulario = new frmAjusteProd(C, this, dataGridProducto.SelectedRows[0].Cells[0].Value.ToString());
                        formulario.Show();
                        break;

                    case "busquedacliente":

                        formulario = new frmBusquedaCliente(C, this, datos[0]);
                        formulario.Show();
                        break;

                    case "updatecliente":

                        formulario = new frmUpdateCliente(C,C_city, this, datos);
                        formulario.Show();
                        break;

                    case "updateproducto":

                        formulario = new frmUpdateProducto(C, this, datos);
                        formulario.Show();
                        break;


                    case "printfactura":

                        formulario = new frmPrintFactura(C, this, datos,dataGridRemitos,Remito);
                        formulario.Show();
                        break;
        
                    case "cantidad_precio":

                        formulario = new frmCantidadPrecio(this,"principal");
                        formulario.Show();
                        ((frmCantidadPrecio)formulario).ActualizarDatos(dataGridProdVtaRemito.SelectedRows[0].Cells[0].Value.ToString(), dataGridProdVtaRemito.SelectedRows[0].Cells[3].Value.ToString());
                        break;

                    case "actualizar_factura":

                        formulario = new frmActualizarFactura(C,this,datos);
                        formulario.Show();
                        break;

                    case "resumen_facturas":
                        formulario = new frmResumenFacturas(C, this);
                        formulario.Show();
                        break;

                }
            }
            else
            {
                this.Enabled = false;
                switch (tipo)
                {
                    case "addcliente":

                        formulario.Show();
                        break;

                    case "addproducto":

                        ((frmAddProducto)formulario).ActualizarDatos();
                        formulario.Show();
                        break;

                    case "ajusteproducto":

                        ((frmAjusteProd)formulario).refreshCodigo(dataGridProducto.SelectedRows[0].Cells[0].Value.ToString());
                        formulario.Show();
                        break;

                    case "busquedacliente":

                        ((frmBusquedaCliente)(formulario)).ActualizarDestino(destino[0]);
                        formulario.Show();
                        break;

                    case "updatecliente":

                        ((frmUpdateCliente)formulario).cargarDatos(datos);
                        formulario.Show();
                        break;

                    case "updateproducto":
                        
                        ((frmUpdateProducto)formulario).cargarDatos(datos);
                        formulario.Show();
                        break;

                    case "factview":

                        formulario.Show();
                        break;


                    case "cantidad_precio":

                        formulario.Show();
                        ((frmCantidadPrecio)formulario).ActualizarDatos(dataGridProdVtaRemito.SelectedRows[0].Cells[0].Value.ToString(), dataGridProdVtaRemito.SelectedRows[0].Cells[3].Value.ToString());
                        break;

                    case "actualizar_factura":
                        formulario.Show();
                        break;

                    case "resumen_facturas":
                        formulario.Show();
                        break;

                }
            }
        }

        public void cargarClientes()
        {
            C.CargarDatos(dataGridClientes, dsC, "dsC", all_clientes);
        }

        public void cargarProductos()
        {
            C.CargarDatos(dataGridProducto, dsProd, "dsProd", all_productos);
        }

        public void cargarCiudades()
        {
            C_city.CargarDatos(dsCiudad, "dsCiudad", "select ciudad from city order by idcity");
            Funciones.CargarDatos(cmbLocalidadr,dsCiudad);
            Funciones.CargarDatos(cmbLocalidadd, dsCiudad);
        }

        private void btnBuscarClienteR_Click(object sender, EventArgs e)
        {
            cantProd=0;
            Funciones.limpiarCampos(tabPage3);
            destino[0] = "remito";
            AbrirFormulario(formBusquedaCliente, "busquedacliente", destino);
        }

        public void CargarClienteRemito(String Nombre, String Apellido, String Domicilio,String Localidad,String Cuil, String idc)
        {           
            //////
            txtRemitente.Text = Nombre + " " + Apellido;
            txtDomicilior.Text = Domicilio;
            cmbLocalidadr.SelectedItem = Localidad;
            txtCUITr.Text = Cuil;
            /////
            txtNombreR.Text = Nombre;
            txtApellidoR.Text = Apellido;
            txtNroCliente.Text = idc;
            idclienteR = idc;
            txtVD.Text = "0";
            txtCR.Text = "0";
            txtFleteAC.Text = "0";
            txtFleteC.Text = "0";
            txtSeguroAC.Text = "0";
            txtSeguroC.Text = "0";
            C.CargarDatos(dataGridProductosRemito, dsProdRemito, "dsProdRemito", "select nombre as Producto,precio as Precio,codigo from producto where idcliente = " + idclienteR);
            //oculto el numero de producto. Lo necesito para no insertar prod repetidos.
            dataGridProductosRemito.Columns[2].Visible = false;
        }

        public void CargarClienteLiquidacion(String Nombre, String Apellido, String idc)
        {
            txtNombreL.Text = Nombre;
            txtApellidoL.Text = Apellido;
            txtNroClienteL.Text = idc;
            idclienteL = idc;

           Remito.CargarRemitos(C, dataGridRemitos, idclienteL, dateFechaDesde.Value.ToString("yyyy-MM-dd"), dateFechaHasta.Value.ToString("yyyy-MM-dd"));
           textBoxTotal.Text=(Remito.calcTotalRemitos(dataGridRemitos).ToString());
        }

        public void limpiarRemitos(){
            Funciones.limpiarCampos(tabPage4);
        }

        private void guardarRemito(){
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

                datos.Add(idclienteR);

                datos.Add(txtNumeroR.Text);


                Funciones.CargarDatos(cmbLocalidadr, dsCiudad);
                Funciones.CargarDatos(cmbLocalidadd, dsCiudad);

                datos.Add(iduser);

                //Command.CommandText = "call vender('" + Cuil + "','" + Nombre + "','" + Apellido + "','" + Iva + "','" + CondVenta + "')";

                if (Remito.Insertar(C, datos, dataGridProdVtaRemito))
                {
                    MessageBox.Show("Remito ingresado exitosamente", "Gestion - Remito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Funciones.limpiarCampos(tabPage3);
                    cantProd = 0;
                    if (cmbCondVtaRemito.SelectedItem.ToString() == "Contado")
                    {
                        List<String> datFact = new List<string>(2);
                        datFact.Add(dateFechaRemito.Value.ToString("yyyy-MM-dd"));
                        datFact.Add(dateFechaRemito.Value.ToString("yyyy-MM-dd"));
                        formRemitoContado = new frmRemitoContado(C, this, datFact);
                        formRemitoContado.Show();
                    }

                }
                else
                    MessageBox.Show("El remito no se pudo insertar.", "Gestion - Remito", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                
            }        
       }

        public bool facturaValida(String numFact)
        {
          int auxInt;
           bool res=false;

           bool suecces;
            try{
 
                suecces = int.TryParse(numFact, out auxInt);
                System.Console.WriteLine("succes "+suecces.ToString());
                System.Console.WriteLine("auxInt " + auxInt+"");
                System.Console.WriteLine("existeFactura " + existeFactura(numFact) + "");
                res = !(existeFactura(numFact) || numFact == "" || !suecces || auxInt < 0);
            }
            catch{
                }
            return res;
        }

        private void btnGrabarRemito_Click(object sender, EventArgs e)
        {

            Boolean remitoRepetido;
            DataSet auxRem = new DataSet();
            C.CargarDatos(auxRem, "auxRem", "select nro_remito from remito where nro_remito=" + "'"+txtNumeroR.Text+"'");
            if (auxRem.Tables[0].Rows.Count != 0)
                remitoRepetido = true;
            else
                remitoRepetido = false;

            if (!remitoRepetido) 
                    guardarRemito();
            else
                MessageBox.Show("El remito ingresado ya existe en el sistema", "Gestion - Remito", MessageBoxButtons.OK, MessageBoxIcon.Warning);             
            
        }

        private void btnBuscarClienteL_Click(object sender, EventArgs e)
        {
            destino[0] = "liquidacion";
            AbrirFormulario(formBusquedaCliente, "busquedacliente", destino);
        }

        private void dateFechaDesde_ValueChanged(object sender, EventArgs e)
        {
            ActualizarRemitosLiquidar();
            textBoxTotal.Text = (Remito.calcTotalRemitos(dataGridRemitos).ToString());
        }

        private void ActualizarRemitosLiquidar()
        {
            String desde = dateFechaDesde.Value.ToString("yyyy-MM-dd");
            String hasta = dateFechaHasta.Value.ToString("yyyy-MM-dd");

            if ((idclienteL != ""))
            {
                Remito.CargarRemitos(C, dataGridRemitos, idclienteL, desde, hasta);    
            }
            else
            {
                //se podria agregar un cartel por si una la fecha desde es mayor a la fecha hasta
                dataGridRemitos.Rows.Clear();
            }
        }

        public void ActualizarRemitosLiquidar(String desde, String hasta)
        {
            if ((idclienteL != ""))
            {
                Remito.CargarRemitos(C, dataGridRemitos, idclienteL, desde, hasta);
            }
            else
            {
                //se podria agregar un cartel por si una la fecha desde es mayor a la fecha hasta
                dataGridRemitos.Rows.Clear();
            }
        }

        private void dateFechaHasta_ValueChanged(object sender, EventArgs e)
        {
            ActualizarRemitosLiquidar();
            textBoxTotal.Text = (Remito.calcTotalRemitos(dataGridRemitos).ToString());
        }

        private void chkAllRemitos_CheckedChanged(object sender, EventArgs e)
        {
            textBoxTotal.Text = (Remito.calcTotalRemitos(dataGridRemitos).ToString());
            if (chkAllRemitos.Checked)
            {
                String desde = "1800-01-01";
                String hasta = "2500-12-31";

                ActualizarRemitosLiquidar(desde,hasta);
            }
            else
            {
                ActualizarRemitosLiquidar();
            }
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

        private void btnCancelarLiquidacion_Click(object sender, EventArgs e)
        {
            Funciones.limpiarCampos(tabPage4);
        }

        private void ActualizarMontos()
        {
            try
            {
                int count = 0;
                float flete = 0;
                float seguro = 0;
                DataSet dsC = new DataSet();

                C.CargarDatos(dsC, "dsC", "select porcentaje_vd,porcentaje_cr from clientes where idcliente = " + idclienteR);
                while (count < dataGridProdVtaRemito.Rows.Count)
                {
                    flete = flete + (Utils.parseAndRound(dataGridProdVtaRemito.Rows[count].Cells[0].Value.ToString()) * Utils.parseAndRound(dataGridProdVtaRemito.Rows[count].Cells[3].Value.ToString()));
                    count++;
                }
                flete = (float)Math.Round(flete, 2);
                seguro = (float)Math.Round((Utils.parseAndRound(txtVD.Text) * Utils.parseAndRound(dsC.Tables[0].Rows[0][0].ToString())) / 100 + ((Utils.parseAndRound(txtCR.Text) * Utils.parseAndRound(dsC.Tables[0].Rows[0][1].ToString())) / 100),2);

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
            catch { }
        }

        private void txtVD_Leave(object sender, EventArgs e)
        {
            try
            {
                
                Funciones.ValidarNumero(txtVD, "Por favor ingrese un número.", "Gestión - Remito");
                Console.WriteLine(txtVD.Text);
                ActualizarMontos();
            }
            catch (Exception ex) { Console.WriteLine(ex.Message.ToString()); }
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

        private void txtCUITr_Leave(object sender, EventArgs e)
        {
            if ((!(txtCUITr.Text == "  -        -")) && (!Funciones.CuiltValido(txtCUITr.Text)))
            {
                MessageBox.Show("Por favor ingrese un CUIT/L válido.", "Gestión - Remito", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCUITr.Text = "";
            }
        }

        private void txtCUITd_Leave(object sender, EventArgs e)
        {
            if ((!(txtCUITd.Text == "  -        -")) && (!Funciones.CuiltValido(txtCUITd.Text)))
            {
                MessageBox.Show("Por favor ingrese un CUIT/L válido.", "Gestión - Remito", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCUITd.Text = "";
            }
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

        public void CargarDatosCantidadPrecio(String cantidad, String precio)
        {
            dataGridProdVtaRemito.SelectedRows[0].Cells[0].Value = cantidad;
            dataGridProdVtaRemito.SelectedRows[0].Cells[3].Value = precio;
            ActualizarMontos();
        }

        private void dataGridProdVtaRemito_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            AbrirFormulario(formCantidadPrecio, "cantidad_precio", null);
        }

        public void ActualizarCadenaConexion(String cadena,String cadena_ciudad,String cadena_user)
        {
            cadena_conexion = cadena;
            cadena_conexion_ciudad = cadena_ciudad;
            cadena_conexion_user = cadena_user;
        }

        public void ActualizarUsuarioActual(String user)
        {
            iduser = user;
            //si es el usuario administrador tiene opciones adicionales sobre el sistema.
            gestiónDeUsuariosToolStripMenuItem.Visible = iduser == "1";
            datosDeLaEmpresaToolStripMenuItem.Visible = iduser == "1";
        }

        private void frmPrincipal2_FormClosing(object sender, FormClosingEventArgs e)
        {
            C.CerrarConexion();
            C_city.CerrarConexion();
            C_user.CerrarConexion();
            Application.Exit();
        }

        private void cerrarSesiónToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("¿Esta seguro que desea cerrar la sesión?", "Gestión - Sesión", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                C.CerrarConexion();
                C_city.CerrarConexion();
                C_user.CerrarConexion();
                Application.Restart();
            }
        }

        private void datosDeLaEmpresaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            formEmpresa = new frmEmpresa(this,C);
            formEmpresa.Show();
        }

        private void gestiónDeUsuariosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            formGestionUsuarios = new frmGestionUsuario(this);
            formGestionUsuarios.Show();
        }

        private void sALIRToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("¿Esta seguro que desea abandonar la aplicación?", "Gestión", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                C.CerrarConexion();
                C_city.CerrarConexion();
                Application.Exit();
            }
        }

        private void btnGrabarLiquidacion_Click(object sender, EventArgs e)
        {

            try
            {
                List<String> datos = new List<string>(3);
                datos.Add((Convert.ToDateTime(dateFechaDesde.Value.ToString())).ToString("yyyy-MM-dd"));
                datos.Add((Convert.ToDateTime(dateFechaHasta.Value.ToString())).ToString("yyyy-MM-dd"));
                if (!Remito.hayRemitos(C, dataGridRemitos))
                    MessageBox.Show("No hay ningun remito seleccionado", "Gestion - Factura", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                else
                {
                    AbrirFormulario(formPrintFactura, "printfactura", datos);
                }
            }
            catch {
              
            }
            
        }


        private void printResumenClienteProd_PrintPage(object sender, PrintPageEventArgs e)
        {
            int nuevoindex;
            nuevoindex = Impresion.ResumenClientes(C, index_cliente, e);
            index_cliente = nuevoindex;
        }


        private void dataGridRemitos_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            textBoxTotal.Text = (Remito.calcTotalRemitos(dataGridRemitos)).ToString();
        }

        private void btnListaClientes_Click(object sender, EventArgs e)
        {
            try
            {
                if (printDialogResumenClienteProd.ShowDialog() == DialogResult.OK)
                {
                    printResumenClienteProd.PrinterSettings = printDialogResumenClienteProd.PrinterSettings;
                    index_cliente = 0;
                    printResumenClienteProd.Print();
                }
            }
            catch { }
        }

        private void txtNroCliente_Leave(object sender, EventArgs e)
        {
            DataSet dsClienteRemitoAux = new DataSet();

            try
            {
                Funciones.ValidarNumero(txtNroCliente, "Ingrese un número de cliente valido", "Gestión - Remito");
                C.CargarDatos(dsClienteRemitoAux, "dsClienteRemitoAux", "select nombre,apellido,direccion,ciudad,cuil from clientes where idcliente = " + txtNroCliente.Text);                
                CargarClienteRemito(dsClienteRemitoAux.Tables[0].Rows[0][0].ToString(), dsClienteRemitoAux.Tables[0].Rows[0][1].ToString(),dsClienteRemitoAux.Tables[0].Rows[0][2].ToString(),dsClienteRemitoAux.Tables[0].Rows[0][3].ToString(),dsClienteRemitoAux.Tables[0].Rows[0][4].ToString(),txtNroCliente.Text);
            }
            catch (Exception)
            {
                MessageBox.Show("Ingrese un número de cliente válido", "Gestión - Remito", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void txtNroClienteL_Leave(object sender, EventArgs e)
        {
            DataSet dsClienteRemitoAux = new DataSet();

            try
            {
                Funciones.ValidarNumero(txtNroClienteL, "Ingrese un número de cliente valido", "Gestión - Liquidación");
                C.CargarDatos(dsClienteRemitoAux, "dsClienteRemitoAux", "select nombre,apellido from clientes where idcliente = " + txtNroClienteL.Text);
                CargarClienteLiquidacion(dsClienteRemitoAux.Tables[0].Rows[0][0].ToString(), dsClienteRemitoAux.Tables[0].Rows[0][1].ToString(), txtNroClienteL.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ingrese un número de cliente válido", "Gestión - Liquidación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnCancelarRemito_Click(object sender, EventArgs e)
        {
            Funciones.limpiarCampos(tabPage3);
        }

        private void ciudadestoolStripMenuItem_Click(object sender, EventArgs e)
        {
            formCiudad = new frmCiudad(this, C_city);
            formCiudad.Show();
        }

        public void CargarClienteInfoRemitos(String Nombre, String Apellido, String idc)
        {
            txtNombreRemitos.Text = Nombre;
            txtApellidoRemitos.Text = Apellido;
            txtNroClienteRemitos.Text = idc;
            idclienteInfoRemitos = idc;

            //Remito.CargarAllRemitos(C, dataGridInfoRemitos, idclienteInfoRemitos, dateFechaDesdeRemitos.Value.ToString("yyyy-MM-dd"), dateFechaHastaRemitos.Value.ToString("yyyy-MM-dd"), FiltroRemitos);
            //textBoxTotalInfoRemitos.Text = (Remito.calcTotalRemitos(dataGridInfoRemitos).ToString());
            ActualizarInfoRemitos();
        }

        private void btnBuscarClienteRemitos_Click(object sender, EventArgs e)
        {
            destino[0] = "info remitos";
            AbrirFormulario(formBusquedaCliente, "busquedacliente", destino);
        }

        private void txtNroClienteRemitos_Leave(object sender, EventArgs e)
        {
            DataSet dsClienteRemitoAux = new DataSet();
            try
            {
                Funciones.ValidarNumero(txtNroClienteRemitos, "Ingrese un número de cliente valido", "Gestión - Info Remitos");
                C.CargarDatos(dsClienteRemitoAux, "dsClienteRemitoAux", "select nombre,apellido from clientes where idcliente = " + txtNroClienteRemitos.Text);
                CargarClienteInfoRemitos(dsClienteRemitoAux.Tables[0].Rows[0][0].ToString(), dsClienteRemitoAux.Tables[0].Rows[0][1].ToString(), txtNroClienteRemitos.Text);
            }
            catch (Exception)
            {
                MessageBox.Show("Ingrese un número de cliente válido", "Gestión - Info Remitos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public void ActualizarInfoRemitos()
        {
            try
            {
                Remito.CargarAllRemitos(C, dataGridInfoRemitos, idclienteInfoRemitos, dateFechaDesdeRemitos.Value.ToString("yyyy-MM-dd"), dateFechaHastaRemitos.Value.ToString("yyyy-MM-dd"), FiltroRemitos);
                textBoxTotalNetoInfoRemitos.Text = (Math.Round(Utils.parseAndRound(Remito.calcTotalRemitos(dataGridInfoRemitos).ToString()),2)).ToString();
            }
            catch (Exception)
            {
                dataGridInfoRemitos.Rows.Clear();
            }
        }

        private void dateFechaDesdeRemitos_ValueChanged(object sender, EventArgs e)
        {
            ActualizarInfoRemitos();
        }

        private void dateFechaHastaRemitos_ValueChanged(object sender, EventArgs e)
        {
            ActualizarInfoRemitos();
        }

        private void rbtnTodos_Click(object sender, EventArgs e)
        {
            FiltroRemitos = "Todos";
            ActualizarInfoRemitos();
        }

        private void rbtnLiquidados_Click(object sender, EventArgs e)
        {
            FiltroRemitos = "Liquidados";
            ActualizarInfoRemitos();
        }

        private void rbtnNoLiquidados_Click(object sender, EventArgs e)
        {
            FiltroRemitos = "NO liquidados";
            ActualizarInfoRemitos();
        }

        private void btnGrabarInfoRemitos_Click(object sender, EventArgs e)
        {
            int i=0;
            Boolean encontre =false;
            String fecha = DateTime.Now.ToShortDateString();

            if (dataGridInfoRemitos.RowCount >= 1){

                while((i < dataGridInfoRemitos.RowCount) && (!encontre))
                {
                    encontre = (dataGridInfoRemitos.Rows[i].Cells[0].Value.ToString() == "True");
                    i++;
                }

                if (encontre)
                {
                    cargarClientes();

                    if (printDialogInfoRemitos.ShowDialog() == DialogResult.OK)
                    {
                        printInfoRemitos.PrinterSettings = printDialogInfoRemitos.PrinterSettings;
                        indexInfoRemitos = 0;
                        printInfoRemitos.DefaultPageSettings.Landscape = true;
                        printInfoRemitos.Print();
                    }
                }
                else
                {
                    MessageBox.Show("No hay ningun remito seleccionado", "Gestión - Remitos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }else{
                MessageBox.Show("No hay ningun remito seleccionado", "Gestión - Remitos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public String fechaInicialLiquidacion()
        {
            return (dateFechaDesde.Value.ToString());
        }

        public String fechaFinalLiquidacion()
        {
            return (dateFechaHasta.Value.ToString());
        }

        private void printInfoRemitos_PrintPage(object sender, PrintPageEventArgs e)
        {
            int nuevoindex;
            //llamo sin período de impresión, porque acá el tipo elige lo que se la canta el orto
            nuevoindex = Impresion.InfoRemitos(C, dataGridInfoRemitos, indexInfoRemitos, txtNroClienteRemitos.Text, txtNombreRemitos.Text + " " + txtApellidoRemitos.Text, dateFechaDesdeRemitos.Value.ToString().Remove(10), dateFechaHastaRemitos.Value.ToString().Remove(10), e);
            indexInfoRemitos = nuevoindex;
        }

        private void btnCancelarInfoRemitos_Click(object sender, EventArgs e)
        {
            Funciones.limpiarCampos(tabPage5);
        }

        private void dataGridInfoRemitos_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            textBoxTotalNetoInfoRemitos.Text = Math.Round(Utils.parseAndRound(Remito.calcTotalRemitos(dataGridInfoRemitos).ToString()),2).ToString();
        }

        public void CargarClienteFactura(String Nombre, String Apellido, String idc)
        {
            txtNombreFactura.Text = Nombre;
            txtApellidoFactura.Text = Apellido;
            txtNroClienteFactura.Text = idc;
            idclienteInfoFactura = idc;

            ActualizarFacturas();
        }

        public void ActualizarFacturas()
        {
            DataSet dsFacturasAux = new DataSet();
            double total;
            try
            {
    
                if (FiltroFactura != "Todos")
                {
                   

                    C.CargarDatos(dataGridFacturas, dsFacturasAux, "dsFacturasAux",
                        "select idfactura as 'Numero',fechaCreacion as 'Fecha de Liquidacion',fechaFirst as 'Fecha Inicial',fechaLast as 'Fecha Final',cond_vta as CondVenta,descripcion as Descripcion,total as 'Total con IVA',tipo as Tipo,iduser as 'Creada por' from factura where " +
                            "idcliente = " + txtNroClienteFactura.Text +
                            " and fechaCreacion >= '" + dateDesdeFactura.Value.ToString("yyyy-MM-dd") + "'" +
                            " and fechaCreacion <= '" + dateHastaFactura.Value.ToString("yyyy-MM-dd") + "'" +
                            " and cond_vta = '" + FiltroFactura + "' order by fechaCreacion");
                }
                else
                {
                    C.CargarDatos(dataGridFacturas, dsFacturasAux, "dsFacturasAux",
                        "select idfactura as 'Numero',fechaCreacion as 'Fecha de Liquidacion',fechaFirst as 'Fecha Inicial',fechaLast as 'Fecha Final',cond_vta as CondVenta,descripcion as Descripcion,total as 'Total con IVA',tipo as Tipo,iduser as 'Creada por' from factura where " +
                            "idcliente = " + txtNroClienteFactura.Text +
                            " and fechaCreacion >= '" + dateDesdeFactura.Value.ToString("yyyy-MM-dd") + "'" +
                            " and fechaCreacion <= '" + dateHastaFactura.Value.ToString("yyyy-MM-dd") + "' order by fechaCreacion");
                }
                /////
                dataGridFacturas.Columns[0].Visible = true;
                dataGridFacturas.Columns["Creada por"].Visible = false;
                dataGridFacturas.Columns[3].Width = 100;
                dataGridFacturas.Columns[4].Width = 270;
                dataGridFacturas.Columns[5].Width = 150;
                dataGridFacturas.Columns["Creada por"].Width = 150;
                total = calcTotalFacturas();
                txtTotalNetoFactura.Text =  Math.Round((total / 1.21),2).ToString();
                txtTotalIVAFactura.Text = total.ToString();
                /////

                //Muestra de un color las facturas pagas y en otro las impagas
                MarcarFacturas();


            }
            catch(Exception ex)
            {
                System.Console.WriteLine(ex.Message.ToString());
            }
        }

        private void btnBuscarClienteFactura_Click(object sender, EventArgs e)
        {
            Funciones.limpiarCampos(tabPage6);
            destino[0] = "factura";
            AbrirFormulario(formBusquedaCliente, "busquedacliente", destino);

          
        }

        private void dateDesdeFactura_ValueChanged(object sender, EventArgs e)
        {
            ActualizarFacturas();
        }

        private void dateHastaFactura_ValueChanged(object sender, EventArgs e)
        {
            ActualizarFacturas();
        }

        private void txtNroClienteFactura_Leave(object sender, EventArgs e)
        {
            DataSet dsClienteFacturaAux = new DataSet();
            try
            {
                Funciones.ValidarNumero(txtNroClienteFactura, "Ingrese un número de cliente valido", "Gestión - Facturas");
                C.CargarDatos(dsClienteFacturaAux, "dsClienteFacturaAux", "select nombre,apellido from clientes where idcliente = " + txtNroClienteFactura.Text);
                CargarClienteFactura(dsClienteFacturaAux.Tables[0].Rows[0][0].ToString(), dsClienteFacturaAux.Tables[0].Rows[0][1].ToString(), txtNroClienteFactura.Text);
            }
            catch (Exception)
            {
                Funciones.limpiarCampos(tabPage6);
                MessageBox.Show("Ingrese un número de cliente válido", "Gestión - Facturas", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void rbtnTodosFactura_Click(object sender, EventArgs e)
        {
            FiltroFactura = "Todos";
            ActualizarFacturas();
        }

        private void rbtnContadoFactura_Click(object sender, EventArgs e)
        {
            FiltroFactura = "Contado";
            ActualizarFacturas();
        }

        private void rbtnCtaCorrienteFactura_Click(object sender, EventArgs e)
        {
            FiltroFactura = "Cuenta corriente";
            ActualizarFacturas();
        }

        private void btnBorrarFacturas_Click(object sender, EventArgs e)
        {
            Funciones.limpiarCampos(tabPage6);
        }

        private void textBoxTotal_TextChanged(object sender, EventArgs e)
        {
            try
            {
                txtTotalLiquidacion.Text = (Math.Round(Utils.parseAndRound(textBoxTotal.Text) * 1.21, 2)).ToString();
            }
            catch { }
        }

        private void textBoxTotalNetoInfoRemitos_TextChanged(object sender, EventArgs e)
        {
            try
            {
                txtTotalIVAInfoRemito.Text = (Math.Round((Utils.parseAndRound(textBoxTotalNetoInfoRemitos.Text) * 1.21), 2)).ToString();
            }
            catch { }
        }

        private double calcTotalFacturas()
        {

            double total = 0;
            for (int i = 0; i < dataGridFacturas.Rows.Count; i++)
            {                
                    if (dataGridFacturas.Rows[i].Cells[6].Value != null)
                    {
                        total = total + Convert.ToDouble((dataGridFacturas.Rows[i].Cells[6].Value).ToString());
                    }
            }
            return (total);

        }

        private void dataGridFacturas_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                formRemitosXFactura = new frmRemitosXFactura(C, this,dataGridFacturas.SelectedRows[0].Cells[0].Value.ToString());
                formRemitosXFactura.Show();
            }
            catch { }
        }

        private void dataGridInfoRemitos_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                DataSet dsUsuario = new DataSet();
                C_user.CargarDatos(dsUsuario, "dsUsuario", "select nombre from usuario where iduser = " + dataGridInfoRemitos.SelectedRows[0].Cells[7].Value.ToString());
                txtGeneradoPor.Text = dsUsuario.Tables[0].Rows[0][0].ToString();
            }
            catch { }
        }

        private void btnGuardarFacturas_Click(object sender, EventArgs e)
        {
            if (dataGridFacturas.RowCount >= 1)
            {
                if (printDialogFactura.ShowDialog() == DialogResult.OK)
                {
                    printFactura.PrinterSettings = printDialogFactura.PrinterSettings;
                    printFactura.Print();
                }
            }
            else
            {
                MessageBox.Show("No hay ninguna factura seleccionada", "Gestion - Factura", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void printFactura_PrintPage(object sender, PrintPageEventArgs e)
        {
            List<String> datos = new List<string>(2);

            DataSet recuperarFactura = new DataSet();
            String cond_vta;
            String tipoFact;
            
            String idfactura;
            
            C.CargarDatos(recuperarFactura, "recuperarFactura", "select idfactura,cond_vta,tipo from factura where idfactura=" + dataGridFacturas.SelectedRows[0].Cells[0].Value.ToString());
            idfactura = recuperarFactura.Tables[0].Rows[0][0].ToString();
            tipoFact = recuperarFactura.Tables[0].Rows[0][2].ToString();
            cond_vta = recuperarFactura.Tables[0].Rows[0][1].ToString();

            datos.Add(idfactura);
            if (cond_vta == "Contado")
            {
                switch (tipoFact)
                {
                    case "A": Impresion.FacturaAContado(C, Remito, e,idfactura);
                        break;
                    case "B": Impresion.FacturaBContado(C, Remito, e,idfactura);
                        break;
   
                }

            }
            else
            {
                switch (tipoFact)
                {
                    case "A":   Impresion.FacturaACuentaCorriente(C, Remito, e,idfactura);
                                break;
                    case "B":   Impresion.FacturaBCuentaCorriente(C, Remito,e,idfactura);
                                break;

                }
            }


            //acá hay que levantar un form que pida el tipo de factura, si es factura A solamente hay que llamar con
            //Impresion.FacturaA(C, Remito, dataGridFacturas.SelectedRows[0].Cells[3].Value.ToString(), e);
            //y si es B, con
            //Impresion.FacturaB(C, Remito, dataGridFacturas.SelectedRows[0].Cells[3].Value.ToString(), e);
        }

        private void dataGridFacturas_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            try
            {
                DataSet dataUser = new DataSet();

                C_user.CargarDatos(dataUser, "dataUser", "select nombre from usuario where iduser=" + dataGridFacturas.Rows[e.RowIndex].Cells["Creada por"].Value.ToString());

                txtFacturadoPor.Text = dataUser.Tables[0].Rows[0][0].ToString();
            }
            catch { }
        }

       

        private void MostrarUser()
        {

            try
            {
                DataSet dsAux = new DataSet();

                C_user.CargarDatos(dsAux, "dsAux", "select nombre from usuario where iduser = " + iduser);
                this.Text = "Gestión - Usuario: " + dsAux.Tables[0].Rows[0][0].ToString();
              
            }
            catch { }

        }

        private void tabControl1_Deselected(object sender, TabControlEventArgs e)
        {
            cargarCiudades();
            cargarClientes();
            cargarProductos();
        }

        private void dataGridInfoRemitos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DataSet dsAux = new DataSet();

            try
            {
                C.CargarDatos(dsAux, "dsAux", "select cond_vta,liquidado from remito where idremito = " + dataGridInfoRemitos.SelectedRows[0].Cells[6].Value.ToString());

                if ((dsAux.Tables[0].Rows[0][0].ToString() != "Contado") && (dsAux.Tables[0].Rows[0][1].ToString() != "1"))
                {
                    frmBMRemito formBMRemito = new frmBMRemito(C, C_city, this, dataGridInfoRemitos.SelectedRows[0].Cells[6].Value.ToString(), iduser);
                    formBMRemito.Show();
                }
                else
                {
                    MessageBox.Show("No se puede modificar un remito contado o uno ya liquidado.", "Gestión - Remito", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch { }
        }

        public String verNroClienteFactura()
        {
            return txtNroClienteFactura.Text;
        }

        public String verNombreClienteFactura()
        {
            return txtNombreFactura.Text;
        }

        public String verApellidoClienteFactura()
        {
            return txtApellidoFactura.Text;
        }

        public String verPeriodoInicialFactura()
        {
            try
            {
                return dataGridFacturas.SelectedRows[0].Cells["Fecha Inicial"].Value.ToString().Remove(10);
            }
            catch
            {
                return "";
            }
        }

        public String verPeriodoFinalFactura()
        {
            try
            {
                return dataGridFacturas.SelectedRows[0].Cells["Fecha Final"].Value.ToString().Remove(10);
            }
            catch
            {
                return "";
            }
        }

        private void btnEliminarRemito_Click(object sender, EventArgs e)
        {
            DataSet dsAux = new DataSet();

            try
            {
                C.CargarDatos(dsAux, "dsAux", "select cond_vta,idfactura,liquidado from remito where idremito = " + dataGridInfoRemitos.SelectedRows[0].Cells[6].Value.ToString());

                if (dsAux.Tables[0].Rows[0][0].ToString() != "Contado")
                {
                    if (dsAux.Tables[0].Rows[0][2].ToString() != "1")
                    {
                        DialogResult result = MessageBox.Show("¿Esta seguro que desea eliminar este remito?", "Gestión - Remito", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (result == DialogResult.Yes)
                        {
                            C.InsertOrUpdate("delete from remito where idremito = " + dataGridInfoRemitos.SelectedRows[0].Cells[6].Value.ToString());
                            C.InsertOrUpdate("delete from productos_vendidos where idremito = " + dataGridInfoRemitos.SelectedRows[0].Cells[6].Value.ToString());
                            ActualizarInfoRemitos();
                        }
                    }
                    else
                    {
                        MessageBox.Show("No se puede eliminar un remito cuenta corriente ya liquidado", "Gestión - Remito", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {                     
                    DialogResult result = MessageBox.Show("Este remito es contado, si lo elimina tambien se eliminara la factura ¿Desea eliminar el remito y la factura?", "Gestión - Remito", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        C.InsertOrUpdate("delete from remito where idremito = " + dataGridInfoRemitos.SelectedRows[0].Cells[6].Value.ToString());
                        C.InsertOrUpdate("delete from productos_vendidos where idremito = " + dataGridInfoRemitos.SelectedRows[0].Cells[6].Value.ToString());
                        C.InsertOrUpdate("delete from factura where idfactura= " + dsAux.Tables[0].Rows[0][1].ToString());
                        ActualizarInfoRemitos();
                    }
                  
                }
            }
            catch {}
        }

        private void btnRegistrarPagosFactura_Click(object sender, EventArgs e)
        {
            try
            {
                frmPagosFactura formPagosFactura = new frmPagosFactura(this, C, dataGridFacturas.SelectedRows[0].Cells[0].Value.ToString());
                formPagosFactura.Show();
            }
            catch { }
        }

        private void MarcarFacturas()
        {
            int i;

            DataSet dsAux = new DataSet();

            C.CargarDatos(dsAux, "dsAux", "select idfactura,pagado from pago_factura");
            
            for (i= 0;i< dataGridFacturas.Rows.Count; i++)
            {
                //MessageBox.Show((dsAux.Tables[0].Select("idfactura = " + dataGridFacturas.Rows[i].Cells[0].Value.ToString()).ElementAt(0)[1].ToString()));
                if ((dsAux.Tables[0].Select("idfactura = " + dataGridFacturas.Rows[i].Cells[0].Value.ToString()).ElementAt(0)[1].ToString()) == "1")
                {
                    dataGridFacturas.Rows[i].DefaultCellStyle.BackColor = Color.LightGreen;
                }
                else
                {
                    dataGridFacturas.Rows[i].DefaultCellStyle.BackColor = Color.LightSalmon;
                }
                /*if ((int.Parse(dataGridFacturas.Rows[i].Cells[0].Value.ToString()) %  2) == 0)
                {
                    dataGridFacturas.Rows[i].DefaultCellStyle.BackColor = Color.LightGreen;
                }
                else
                {
                    dataGridFacturas.Rows[i].DefaultCellStyle.BackColor = Color.LightSalmon;
                }*/
            }
        }

        private void btnCambiarNumFactura_Click(object sender, EventArgs e)
        {
            String numFact;
            List<String> datos=new List<string>();
          
            try
            {

                numFact = dataGridFacturas.SelectedRows[0].Cells[0].Value.ToString();
                datos.Add(numFact);

                AbrirFormulario(formActualizarFactura, "actualizar_factura", datos);
                
            }
            catch { }
           
        }

        private void btnResumenFacturas_Click(object sender, EventArgs e)
        {
            AbrirFormulario(formResumenFacturas, "resumen_facturas", new List<string>());
        }

        private void btnBorrarFactura_Click(object sender, EventArgs e)
        {
            DialogResult resultado;
            String idFact = dataGridFacturas.SelectedRows[0].Cells["Numero"].Value.ToString();
            resultado = MessageBox.Show("¿Está seguro de borrar la factura número " + idFact + "?", "Gestion - Borrar Factura", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (resultado == DialogResult.Yes)
            {
                C.InsertOrUpdate("delete from pago_cheque where idfactura = " + idFact);
                C.InsertOrUpdate("delete from pago_contado where idfactura = " + idFact);
                C.InsertOrUpdate("delete from pago_factura where idfactura = " + idFact);
                C.InsertOrUpdate("update remito set liquidado = '0' where idfactura = " + idFact);
                C.InsertOrUpdate("update remito set idfactura = NULL where idfactura = " + idFact);
                C.InsertOrUpdate("delete from factura where idfactura = " + idFact);
                ActualizarFacturas();
            }
        }

    }
}