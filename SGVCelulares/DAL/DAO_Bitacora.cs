using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DAO_Bitacora : Conexion,IABMC
    {
        SqlCommand cm;
        public DAO_Bitacora() : base() 
        { 
            cm = new SqlCommand("select * from bitacora", con); 
        }
        public void Agregar(params object[] T)
        {
            cm.Parameters.Clear();
            cm.Parameters.Add("@dni", SqlDbType.VarChar).Value = T[0];
            cm.Parameters.Add("@fecha", SqlDbType.DateTime).Value = T[1];
            cm.Parameters.Add("@modulo", SqlDbType.VarChar).Value = T[2];
            cm.Parameters.Add("@evento", SqlDbType.VarChar).Value = T[3];
            cm.Parameters.Add("@criticidad", SqlDbType.TinyInt).Value = T[4];

            cm.CommandText = "INSERT INTO bitacora(dni,fecha,modulo,evento,criticidad) values (@dni,@fecha,@modulo,@evento,@criticidad)";
            con.Open();
            cm.ExecuteNonQuery();
            con.Close();
        }

        public void Borrar(string id)
        {
            throw new NotImplementedException();
        }

        public void Modificar(string id, params object[] T)
        {
            throw new NotImplementedException();
        }
        public SqlDataReader Consultar()
        {
            cm.Parameters.Clear();
            cm.CommandText = "SELECT * FROM bitacora";
            con.Open();
            return cm.ExecuteReader(CommandBehavior.CloseConnection);
        }

        public SqlDataReader ConsultarPorId(string id)
        {
            throw new NotImplementedException();
        }

        public bool ValidarRepetido(string id)
        {
            throw new NotImplementedException();
        }

    }
}
