using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace AplicacionConsola
{

    /*
         CREATE TABLE [dbo].[tblNames](
	    [FirstName] [nvarchar](100) NULL,
	    [LastName] [nvarchar](100) NULL
        ) ON [PRIMARY]
        GO
        CREATE TYPE [dbo].[tptblNames] AS TABLE(
	    [FirstName] [nvarchar](100) NULL,
	    [LastName] [nvarchar](100) NULL
        )
        GO
        CREATE PROCEDURE [dbo].[prInsertNames] (
        @Names tptblNames READONLY -- Nota: Debe especificar READONLY
            )
            AS
            BEGIN
	            INSERT INTO dbo.TblNames (FirstName, LastName)
		            SELECT FirstName, LastName
		            FROM @Names;

	            SELECT * FROM TblNames;
            END
                 GO
     */
    internal class Program
    {
        static void Main(string[] args)
        {

            DataTable dtNames = new DataTable();

            dtNames.Columns.Add("FirstName", typeof(string));
            dtNames.Columns.Add("LastName", typeof(string));

            for (int i = 0; i < 3; i++)
            {

                DataRow drName = dtNames.NewRow();
                drName["FirstName"] = $"fernando - {i}";
                drName["LastName"] = $"jaramillo - {i}";

                dtNames.Rows.Add(drName);
            }
            //Console.WriteLine(dtNames.Rows[0]["FirstName"]);
            //Console.WriteLine(dtNames.Rows[0]["LastName"]);

            try
            {
                ClsConexionSql conecctionTest;
                SqlCommand insertCommand;

                conecctionTest = new ClsConexionSql();
                insertCommand = new SqlCommand("prInsertNames", conecctionTest.AbrirConexion());


                insertCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter tvpParam = insertCommand.Parameters.AddWithValue("@Names", dtNames);
                tvpParam.SqlDbType = SqlDbType.Structured;
                
                /**/
                SqlDataAdapter da = new SqlDataAdapter(insertCommand);
                DataTable dt = new DataTable();

                da.Fill(dt);
                //Console.WriteLine(dt.ToString());
                //foreach (DataRow dr in dt.Rows)
                //{
                //    foreach(DataColumn colum in dt.Columns)
                //    {
                //        Console.WriteLine(dr[colum]);
                //    }

                //}

                string data = string.Empty;
                StringBuilder sb = new StringBuilder();
                foreach (DataRow dataRow in dt.Rows)
                {
                    foreach (var item in dataRow.ItemArray)
                    {
                        //Console.WriteLine(item);
                        sb.Append(item);
                        sb.Append(" | ");

                    }
                    sb.AppendLine();
                }
                data = sb.ToString();
                Console.WriteLine(data);
                /**/


                // Execute the command.  
                //int ejecucion = insertCommand.ExecuteNonQuery();

                //Console.WriteLine("Cantidad registros insertados: " + ejecucion);
                conecctionTest.CerrarConexion();

                

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }


        }
    }
}
