using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Gestion
{
    class clsProducto
    {
        public void Insertar(clsConexion C, List<String> datos)
        {
            try
            {
                C.InsertOrUpdate("insert into producto(nombre, precio,idcliente,descripcion) values ('" + datos[0] + "'," + datos[1] + "," + datos[2] + ",'" + datos[3] + "')");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void Eliminar(clsConexion C, String codigo)
        {
            try
            {
                C.InsertOrUpdate("delete from producto where codigo = " + codigo);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        public void Actualizar(clsConexion C, List<String> datos)
        {
            try
            {
                C.InsertOrUpdate("update producto set " +
                                 " nombre = '" + datos[1] + "'" +
                                 ", precio  = " + datos[2].Replace(",", ".") + 
                                 ", idcliente = " + datos[3] + 
                                 ", descripcion = '" + datos[4] + "' WHERE codigo = " + datos[0]);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
