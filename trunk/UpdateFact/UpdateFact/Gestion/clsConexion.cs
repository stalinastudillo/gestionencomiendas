using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Odbc;
using System.Data;
using System.Windows.Forms;


namespace Gestion
{
    public class clsConexion
    {
        
        OdbcConnection Conect;
        OdbcCommand Command;
        OdbcDataAdapter Adapter;
        
        public clsConexion ()
        {}

        public void Conectar(String conexion)
        {
            Conect = new OdbcConnection(conexion);
            Command = new OdbcCommand();
            Adapter = new OdbcDataAdapter();

            Conect.Open();
            Command.Connection = Conect;
            
        }

        public void CerrarConexion()
        {
            Conect.Close();
        }

        public void CargarDatos(DataGridView grid, DataSet data,String str,String consulta)
        {
            try
            {
                Command.CommandText = consulta;
                Adapter.SelectCommand = Command;
                data.Clear();

                Adapter.Fill(data, str);
                grid.DataSource = data;
                grid.DataMember = str;
            }
            catch { }
           
        }

        public void CargarDatos(ComboBox cmb, DataSet data, String str,String consulta)
        {
            try
            {
                Command.CommandText = consulta;
                Adapter.SelectCommand = Command;
                data.Clear();
                Adapter.Fill(data, str);

                cmb.Items.Clear();
                foreach (DataRow r in data.Tables[0].Rows)
                {
                    if (r[1].ToString() != "")
                    {
                        cmb.Items.Add(r[2].ToString() + " - " + r[1].ToString() + "," + r[0].ToString());
                    }
                    else
                    {
                        cmb.Items.Add(r[2].ToString() + " - " + r[0].ToString());
                    }
                }
            }
            catch { }
          
        }

        public void CargarDatos(DataSet data, String str,String consulta)
        {
            try
            {
                Command.CommandText = consulta;
                Adapter.SelectCommand = Command;
                data.Clear();
                Adapter.Fill(data, str);
            }
            catch { }
          
        }

        public void CargarDatos(ListBox lstbox, DataSet data, String str, String consulta)
        {
            try
            {
                Command.CommandText = consulta;
                Adapter.SelectCommand = Command;
                data.Clear();
                Adapter.Fill(data, str);

                lstbox.Items.Clear();
                foreach (DataRow r in data.Tables[0].Rows)
                {
                    lstbox.Items.Add(r[0].ToString());
                }
            }
            catch{ }
        
        }

        public void InsertOrUpdate(String datos)
        {
            Command.CommandText = datos;

            try
            {
                Command.ExecuteNonQuery();
            }
            catch { }
        }
    

        public void AjustarProducto(String codigo, String porcentaje)
        {
            Command.CommandText = "call aumento_producto('" + codigo + "'," + porcentaje + ")";
            Command.ExecuteNonQuery();
        }

        
    }
}
