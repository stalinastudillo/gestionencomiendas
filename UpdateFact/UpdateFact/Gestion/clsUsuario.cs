using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data;

namespace Gestion
{
    class clsUsuario
    {
        DataSet dsUsuario = new DataSet();

        public void Insertar(clsConexion C, String nombre,String password)
        {
            try
            {
                C.CargarDatos(dsUsuario,"dsUsuario","select iduser from usuario where nombre = '" + nombre + "'");
                if (dsUsuario.Tables[0].Rows.Count == 0)
                {
                    C.InsertOrUpdate("insert into usuario (nombre,password) values ('" + nombre + "','" + password + "')");
                }
                else 
                {
                    MessageBox.Show("El nombre de usuario ya existe en el sistema","Gestión - Administración de Usuarios",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void Eliminar(clsConexion C, String nombre)
        {
            try
            {
                C.InsertOrUpdate("delete from usuario where nombre = '" + nombre + "'");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void Actualizar(clsConexion C, String old_name, String nombre, String password)
        {
            try
            {
                C.CargarDatos(dsUsuario,"dsUsuario","select iduser from usuario where nombre = '" + nombre + "'");
                if (dsUsuario.Tables[0].Rows.Count != 0)
                {
                    C.InsertOrUpdate("update usuario set nombre = '" + nombre + "', password = '" + password + "' WHERE nombre = '" + old_name + "'");
                }
                else
                {
                    MessageBox.Show("El nombre de usuario ya existe en el sistema","Gestión - Administración de Usuarios",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

    }
}
