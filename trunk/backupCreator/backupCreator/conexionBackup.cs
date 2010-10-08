using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Odbc;
using System.Data;


namespace WindowsFormsApplication1
{
    class conexionBackup
    {
        OdbcConnection Conect;
        OdbcCommand Command;
        OdbcDataAdapter Adapter;

        

        public void CerrarConexion()
        {
            try
            {
                Conect.Close();
            }
            catch
            {
            }
        }

        public void Conectar(String conexion)
        {
            Conect = new OdbcConnection(conexion);
            Command = new OdbcCommand();
            Adapter = new OdbcDataAdapter();

            Conect.Open();
            Command.Connection = Conect;

        }

        public void InsertOrUpdate(String datos)
        {
            Command.CommandText = datos;

            try
            {
                Command.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }

  
}
