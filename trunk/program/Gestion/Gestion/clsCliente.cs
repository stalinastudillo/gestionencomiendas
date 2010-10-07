using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Gestion
{
    class clsCliente
    {
        public void Insertar(clsConexion C, List<String> datos)
        {
            try
            {
                C.InsertOrUpdate("insert into clientes(cuil, nombre, apellido, direccion, ciudad, codigo_postal, telefono,celular,email,porcentaje_vd,porcentaje_cr,credito) values ('" +
                                 datos[0] + "','" + datos[1] + "','" + datos[2] + "','" + datos[3] + "','" + datos[4] + "','" + datos[5] + "','" +
                                 datos[6] + "','" + datos[7] + "','" + datos[8] + "'," + datos[9] + "," + datos[10] + "," + datos[11] + ")");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void Eliminar(clsConexion C,String idcliente)
        {
            try
            {
                C.InsertOrUpdate("delete from clientes where idcliente = " + idcliente);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void Actualizar(clsConexion C,List<String> datos)
        {
            try
            {
                C.InsertOrUpdate("update clientes set cuil = '" + datos[0] + "'" +
                                    ", nombre = '" + datos[1] + "'" +
                                    ", apellido = '" + datos[2] + "'" +
                                    ", direccion = '" + datos[3] + "'" +
                                    ", ciudad = '" + datos[4] + "'" +
                                    ", codigo_postal = '" + datos[5] + "'" +
                                    ", telefono = '" + datos[6] + "'" +
                                    ", celular = '" + datos[7] + "'" +
                                    ", email = '" + datos[8] + "'" +
                                    ", porcentaje_vd = " + datos[9] + 
                                    ", porcentaje_cr = " + datos[10] + 
                                    ", credito = " + datos[11] + " WHERE idcliente = " + datos[12]);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

    }
}
