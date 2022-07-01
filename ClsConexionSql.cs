using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace AplicacionConsola
{
    class ClsConexionSql
    {

        static private string cadenaConexion = "server=AGN5\\SQL ; database = local_test; integrated security = true ";

        private SqlConnection conexion = new SqlConnection(cadenaConexion);

        public SqlConnection AbrirConexion()
        {
            if (conexion.State == ConnectionState.Closed)
                conexion.Open();
            return conexion;
        }

        public SqlConnection CerrarConexion()
        {
            if (conexion.State == ConnectionState.Open)
                conexion.Close();
            return conexion;
        }
    }
}
