using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data;

namespace Gestion
{
    public class clsRemito
    {
        DataSet dsRemito = new DataSet();
        DataSet dsProdR = new DataSet();
        DataSet dsAllRemito = new DataSet();
        DataSet dsAllProd = new DataSet();
        String stringStoreProc;

        String reempIfNula(String cadena)
        {
            if (cadena == "") cadena = "''";
                    return (cadena);
        }

        public bool Insertar(clsConexion C, List<String> datos, DataGridView productos)
        {
            bool success = true;
            try
            {
                C.InsertOrUpdate("truncate table productos_vender"); 
                stringStoreProc=      
                    "call crear_remito(" +"'"+reempIfNula(datos[0])+"'"+","+
                    "'"+reempIfNula(datos[1])+"'"+","+
                    "'"+reempIfNula(datos[2])+"'"+","+
                    "'"+reempIfNula(datos[3])+"'"+","+
                    "'"+reempIfNula(datos[4])+"'"+","+
                    "'"+reempIfNula(datos[5])+"'"+","+
                    "'"+reempIfNula(datos[6])+"'"+","+
                    "'"+reempIfNula(datos[7])+"'"+","+
                    "'"+reempIfNula(datos[8])+"'"+","+
                    "'"+reempIfNula(datos[9])+"'"+","+
                    "'" + reempIfNula(datos[10]) + "'" + "," +
                        reempIfNula(datos[11])+","+
                        reempIfNula(datos[12])+ "," +
                        reempIfNula(datos[13])+ "," +
                        reempIfNula(datos[14])+ "," +
                    "'"+reempIfNula(datos[15])+"'"+","+
                        reempIfNula(datos[16])+ "," +
                    "'"+reempIfNula(datos[17])+"'"+","+
                        reempIfNula(datos[18])+")";
            
            InsertarProductosRemito(C, productos);
            Console.WriteLine(stringStoreProc);
          
                C.InsertOrUpdate(stringStoreProc);

                if (productos.Rows.Count == 0)
                {
                    MessageBox.Show("No hay productos");
                    success = false;
                }
            }
            catch 
            {
                success = false;
            }
            return success;
        }

        private void InsertarProductosRemito(clsConexion C, DataGridView productos)
        {

            DataGridViewCell dgc;
            String cantidad;
            String descripcion;
            String kg;
            String pu;
            String codigo;
           
            for (int i = 0; i < productos.Rows.Count; i++)
            {
                dgc = productos.Rows[i].Cells[0];
                cantidad=(dgc.Value).ToString();

                dgc = productos.Rows[i].Cells[1];
               descripcion = (dgc.Value).ToString();

                dgc = productos.Rows[i].Cells[2];
                kg = (dgc.Value).ToString().Replace(",", ".");

                dgc = productos.Rows[i].Cells[3];
                pu = (dgc.Value).ToString().Replace(",",".");

                dgc = productos.Rows[i].Cells[4];
                codigo = (dgc.Value).ToString();
           
                String insertProd = "insert into productos_vender(codprod,cant,descr,kgs,prec) values (" + codigo + "," + cantidad + "," + "'" + descripcion + "'" + ","
                    + kg + "," + pu + ")";

                
                C.InsertOrUpdate(insertProd);

                
            }
        }

        private void InsertarRemitosALiquidar(clsConexion C, DataGridView remitos)
        {

            DataGridViewCell dgc;
            String remitoSeleccionado;
            String idremito;
           
            for (int i = 0; i < remitos.Rows.Count; i++)
            {
                dgc = remitos.Rows[i].Cells[6];
                remitoSeleccionado = (remitos.Rows[i].Cells[0]).Value.ToString();
                if (remitoSeleccionado == "True")
                {
                    idremito = (dgc.Value).ToString();
                    C.InsertOrUpdate("insert into remitosaLiquidar(idremito) values (" + idremito + ");");
                }
            }
        }

        public Boolean hayRemitos(clsConexion C, DataGridView remitos)
        {

            DataGridViewCell dgc;
            String checkRemito;
            Boolean hay = false;
            for (int i = 0; i < remitos.Rows.Count; i++)
            {
                dgc = remitos.Rows[i].Cells[0];
                checkRemito = (dgc.Value).ToString();
                if (checkRemito == "True")
                {
                    hay = true;
                    break;
                }
            }
            return (hay);
        }

        public Boolean liquidarCuentaCorriente(clsConexion C, String fechaDesde, String fechaHasta,String fechaLiquidacion, DataGridView dataGridRemitos, String descripcion, String tipoFact, String iduser, String numFact)
        {
            bool succes = true;
            try
            {
                C.InsertOrUpdate("truncate table remitosaLiquidar");
                //create procedure crear_factura(in facturaA boolean,in nro_factura varchar(8), in fecha varchar(10))

                if (hayRemitos(C, dataGridRemitos))
                {
                    InsertarRemitosALiquidar(C, dataGridRemitos);
                    String stringStoreProc = "call crear_factura(" + "'" + fechaLiquidacion + "'" + "," + "'" + fechaDesde + "'" + "," + "'" + fechaHasta + "'" + "," + "'" + descripcion + "'" + "," + "'" + tipoFact + "'" + "," + "'" + iduser + "'" + "," +
                     numFact + ")";
                    C.InsertOrUpdate(stringStoreProc);
                }
                else
                    succes = false;
            }
            catch
            {
            }
            return succes;
        }

        public Boolean liquidarContado(clsConexion C, String fechaDesde,String fechaHasta, String numRemito, String descripcion, String tipoFact,String iduser,String numFact)
        {
            bool succes = true; ;
            try
            {
                String fechaAct = DateTime.Now.ToString("yyyy-MM-dd");

                C.InsertOrUpdate("truncate remitosaLiquidar");
                C.InsertOrUpdate("insert into remitosaLiquidar values(" + numRemito + ");");
                String stringStoreProc = "call crear_factura(" + "'" + fechaAct + "'" + "," + "'" + fechaDesde + "'" + "," + "'" + fechaHasta + "'" + "," + "'" + descripcion + "'" + "," + "'" + tipoFact + "'" + "," + "'" + iduser + "'" + "," + numFact + ");";
                C.InsertOrUpdate(stringStoreProc);
                System.Console.WriteLine(stringStoreProc);
            }
            catch
            {
                succes = false;
            }
            return (succes);
           

        }

        public double calcTotalRemitos(DataGridView gridremitos){

             double total = 0;
             for (int i = 0; i < gridremitos.Rows.Count ; i++)
            {
                 if(gridremitos.Rows[i].Cells[0].Value.ToString()=="True")

                     if (gridremitos.Rows[i].Cells[4].Value != null)
                     {
                         total = total + Convert.ToDouble((gridremitos.Rows[i].Cells[4].Value).ToString());
                     }
            }
            return (total);        
        }

        public String getInfoRemito(clsConexion C,String idremito)
        {
            DataSet dsProdAux = new DataSet();
            String detalle = "";
          //cargo en el dataset todos los productos de un dado remito
            C.CargarDatos(dsProdAux, "dsProdAux", "select cantidad,descripcion from productos_vendidos where idremito = " + idremito);
            detalle = "";
            int count_prod = 0;
            while (count_prod < dsProdAux.Tables[0].Rows.Count)
            {
                detalle = detalle + dsProdAux.Tables[0].Rows[count_prod][0].ToString() + " " + dsProdAux.Tables[0].Rows[count_prod][1].ToString();
                
                if (count_prod < dsProdAux.Tables[0].Rows.Count - 1)
                {
                    detalle = detalle + ";";
                }
                count_prod++;
            }

    
            return (detalle);
        }

        public void CargarAllRemitos(clsConexion C, DataGridView gridremitos, String idcliente, String fmin, String fmax, String OpcionBusqueda)
        {
            int count_remitos = 0;
            int count_prod;
            String detalle = "";
            String idremito = "";

            gridremitos.Rows.Clear();

            if (OpcionBusqueda == "Liquidados")
            {
                C.CargarDatos(dsAllRemito, "dsAllRemito", "select idremito,liquidado,nro_remito,fecha,flete,seguro,total,cond_vta,iduser from remito where idcliente = " + idcliente + " and liquidado = '1' and fecha >= '" + fmin + "' and fecha <= '" + fmax + "' order by fecha");
            }
            else
            {
                if (OpcionBusqueda == "NO liquidados")
                    C.CargarDatos(dsAllRemito, "dsAllRemito", "select idremito,liquidado,nro_remito,fecha,flete,seguro,total,cond_vta,iduser from remito where idcliente = " + idcliente + " and liquidado = '0' and fecha >= '" + fmin + "' and fecha <= '" + fmax + "' order by fecha");
                else
                    C.CargarDatos(dsAllRemito, "dsAllRemito", "select idremito,liquidado,nro_remito,fecha,flete,seguro,total,cond_vta, iduser from remito where idcliente = " + idcliente + " and fecha >= '" + fmin + "' and fecha <= '" + fmax + "' order by fecha");
            }

                while (count_remitos < dsAllRemito.Tables[0].Rows.Count)
                {
                    idremito = dsAllRemito.Tables[0].Rows[count_remitos][0].ToString();
                    //cargo en el dataset todos los productos de un dado remito
                    C.CargarDatos(dsAllProd, "dsAllProd", "select cantidad,descripcion from productos_vendidos where idremito = " + idremito);
                    detalle = "";
                    count_prod = 0;
                    while (count_prod < dsAllProd.Tables[0].Rows.Count)
                    {
                        detalle = detalle + dsAllProd.Tables[0].Rows[count_prod][0].ToString() + " " + dsAllProd.Tables[0].Rows[count_prod][1].ToString();
                       
                        if (count_prod < dsAllProd.Tables[0].Rows.Count - 1)
                        {
                            detalle = detalle + ";";
                        }
                        count_prod++;
                    }
                    gridremitos.Rows.Add();
                    //queda seteado para liquidar por defecto
                    gridremitos.Rows[count_remitos].Cells[0].Value = false;
                    //cargo el nro de remito
                    gridremitos.Rows[count_remitos].Cells[1].Value = dsAllRemito.Tables[0].Rows[count_remitos][2].ToString();
                    //cargo la fecha del remito
                    gridremitos.Rows[count_remitos].Cells[2].Value = dsAllRemito.Tables[0].Rows[count_remitos][3].ToString().Remove(10);
                    //cargo el detalle
                    gridremitos.Rows[count_remitos].Cells[3].Value = detalle;
                    //cargo el importe a liquidar del remito

                    gridremitos.Rows[count_remitos].Cells[4].Value = dsAllRemito.Tables[0].Rows[count_remitos][6].ToString();

                    //Guardo el idremito para usarlo en el momento de la liquidacion- no lo muestro
                    gridremitos.Rows[count_remitos].Cells[6].Value = idremito;
                    try
                    {
                        //le pongo por ahora el iduser que creo el remito
                        gridremitos.Rows[count_remitos].Cells[7].Value = dsAllRemito.Tables[0].Rows[count_remitos][8].ToString();
                    }
                    catch { }


                    count_remitos++;
                }
        }


            //cargo en el dataset todos los remitos segun fechas y segun idcliente

        public void CargarRemitos(clsConexion C, DataGridView gridremitos,String idcliente,String fmin,String fmax)
        {
            int count_remitos = 0;
            int count_prod;
            String detalle = "";
            String idremito = "";           

            

            gridremitos.Rows.Clear();
            //cargo en el dataset todos los remitos segun fechas y segun idcliente
            C.CargarDatos(dsRemito, "dsRemito", "select idremito,liquidado,nro_remito,fecha,flete,seguro,total,cond_vta from remito" + " where liquidado = '0' and idcliente = " + idcliente + " and fecha >= '" + fmin + "' and fecha <= '" + fmax + "' order by fecha");

            while (count_remitos < dsRemito.Tables[0].Rows.Count)
            {
                idremito = dsRemito.Tables[0].Rows[count_remitos][0].ToString();
                //cargo en el dataset todos los productos de un dado remito
                C.CargarDatos(dsProdR, "dsProdR", "select cantidad,descripcion from productos_vendidos where idremito = " + idremito);
                detalle = "";
                count_prod = 0;
                while (count_prod < dsProdR.Tables[0].Rows.Count)
                {
                    detalle = detalle + dsProdR.Tables[0].Rows[count_prod][0].ToString() + " " + dsProdR.Tables[0].Rows[count_prod][1].ToString();

                    if (count_prod < dsProdR.Tables[0].Rows.Count - 1)
                    {
                        detalle = detalle + ";";
                    }
                    count_prod++;
                }
                gridremitos.Rows.Add();
                //queda seteado para liquidar por defecto
                gridremitos.Rows[count_remitos].Cells[0].Value = true;
                //cargo el nro de remito
                gridremitos.Rows[count_remitos].Cells[1].Value = dsRemito.Tables[0].Rows[count_remitos][2].ToString();
                //cargo la fecha del remito
                gridremitos.Rows[count_remitos].Cells[2].Value = dsRemito.Tables[0].Rows[count_remitos][3].ToString().Remove(10);
                //cargo el detalle
                gridremitos.Rows[count_remitos].Cells[3].Value = detalle;
                //cargo el importe a liquidar del remito

                gridremitos.Rows[count_remitos].Cells[4].Value = dsRemito.Tables[0].Rows[count_remitos][6].ToString();
            
               //Guardo el idremito para usarlo en el momento de la liquidacion- no lo muestro
                gridremitos.Rows[count_remitos].Cells[6].Value = idremito;              
                
                count_remitos++;
            }
            
        }


        public void CargarRemitosSegunFactura(clsConexion C, DataGridView gridremitos, String idfactura)
        {
            int count_remitos = 0;
            int count_prod;
            String detalle = "";
            String idremito = "";
            DataSet dsAllRemito = new DataSet();
            DataSet dsAllProd = new DataSet();

            gridremitos.Rows.Clear();
            C.CargarDatos(dsAllRemito, "dsAllRemito", "select idremito,liquidado,nro_remito,fecha,flete,seguro,total,cond_vta from remito where idfactura = " + idfactura + " order by fecha");


            while (count_remitos < dsAllRemito.Tables[0].Rows.Count)
            {
                idremito = dsAllRemito.Tables[0].Rows[count_remitos][0].ToString();
                //cargo en el dataset todos los productos de un dado remito
                C.CargarDatos(dsAllProd, "dsAllProd", "select cantidad,descripcion from productos_vendidos where idremito = " + idremito);
                detalle = "";
                count_prod = 0;
                while (count_prod < dsAllProd.Tables[0].Rows.Count)
                {
                    detalle = detalle + dsAllProd.Tables[0].Rows[count_prod][0].ToString() + " " + dsAllProd.Tables[0].Rows[count_prod][1].ToString();

                    if (count_prod < dsAllProd.Tables[0].Rows.Count - 1)
                    {
                        detalle = detalle + ";";
                    }
                    count_prod++;
                }
                gridremitos.Rows.Add();
                //queda seteado para liquidar por defecto
                gridremitos.Rows[count_remitos].Cells[0].Value = true;
                //cargo el nro de remito
                gridremitos.Rows[count_remitos].Cells[1].Value = dsAllRemito.Tables[0].Rows[count_remitos][2].ToString();
                //cargo la fecha del remito
                gridremitos.Rows[count_remitos].Cells[2].Value = dsAllRemito.Tables[0].Rows[count_remitos][3].ToString().Remove(10);
                //cargo el detalle
                gridremitos.Rows[count_remitos].Cells[3].Value = detalle;
                //cargo el importe a liquidar del remito

                gridremitos.Rows[count_remitos].Cells[4].Value = dsAllRemito.Tables[0].Rows[count_remitos][6].ToString();

                //Guardo el idremito para usarlo en el momento de la liquidacion- no lo muestro
                gridremitos.Rows[count_remitos].Cells[6].Value = idremito;


                count_remitos++;
            }
 
        }
    }
}
