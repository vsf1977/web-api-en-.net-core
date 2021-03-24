using System;
using System.Data.SqlClient;

namespace Prueba.Models
{
    public class Ticket
    {
        SqlCommand comando;
        Database database = new Database();
        public string id { get; set; }
        public string usuario { get; set; }
        public DateTime fecha_creacion { get; set; }
        public DateTime fecha_actualizacion { get; set; }
        public string estatus { get; set; }

        public void insertar()
        {
            comando = new SqlCommand(null, database.Conectar());
            comando.CommandText = "insert into ticket values (@id, @usuario, @fecha_creacion, @fecha_actualizacion, @estatus)";

            SqlParameter idParam = new SqlParameter("@id", System.Data.SqlDbType.VarChar, 10);
            SqlParameter usuarioParam = new SqlParameter("@usuario", System.Data.SqlDbType.VarChar, 50);
            SqlParameter fecha_creacionParam = new SqlParameter("@fecha_creacion", System.Data.SqlDbType.Date);
            SqlParameter fecha_actualizacionParam = new SqlParameter("@fecha_actualizacion", System.Data.SqlDbType.Date);
            SqlParameter estatusParam = new SqlParameter("@estatus", System.Data.SqlDbType.VarChar, 10);

            idParam.Value = id;
            usuarioParam.Value = usuario;
            fecha_creacionParam.Value = fecha_creacion;
            fecha_actualizacionParam.Value = fecha_actualizacion;
            estatusParam.Value  = estatus;

            comando.Parameters.Add(idParam);
            comando.Parameters.Add(usuarioParam);
            comando.Parameters.Add(fecha_creacionParam);
            comando.Parameters.Add(fecha_actualizacionParam);
            comando.Parameters.Add(estatusParam);

            comando.Prepare();
            comando.ExecuteNonQuery();
            database.Desconectar();
        }


        public void actualizar()
        {
            comando = new SqlCommand(null, database.Conectar());
            comando.CommandText = "update ticket set usuario = @usuario, fecha_creacion = @fecha_creacion, fecha_actualizacion = @fecha_actualizacion, estatus = @estatus where id = @id";

            SqlParameter idParam = new SqlParameter("@id", System.Data.SqlDbType.VarChar, 10);
            SqlParameter usuarioParam = new SqlParameter("@usuario", System.Data.SqlDbType.VarChar, 50);
            SqlParameter fecha_creacionParam = new SqlParameter("@fecha_creacion", System.Data.SqlDbType.Date);
            SqlParameter fecha_actualizacionParam = new SqlParameter("@fecha_actualizacion", System.Data.SqlDbType.Date);
            SqlParameter estatusParam = new SqlParameter("@estatus", System.Data.SqlDbType.VarChar, 10);

            idParam.Value = id;
            usuarioParam.Value = usuario;
            fecha_creacionParam.Value = fecha_creacion;
            fecha_actualizacionParam.Value = fecha_actualizacion;
            estatusParam.Value = estatus;

            comando.Parameters.Add(idParam);
            comando.Parameters.Add(usuarioParam);
            comando.Parameters.Add(fecha_creacionParam);
            comando.Parameters.Add(fecha_actualizacionParam);
            comando.Parameters.Add(estatusParam);

            comando.Prepare();
            comando.ExecuteNonQuery();
            database.Desconectar();
        }


        public void borrar()
        {
            comando = new SqlCommand(null, database.Conectar());
            comando.CommandText = "delete from ticket where id = @id";
            SqlParameter idParam = new SqlParameter("@id", System.Data.SqlDbType.VarChar, 10);         
            idParam.Value = id;            
            comando.Parameters.Add(idParam);           
            comando.Prepare();
            comando.ExecuteNonQuery();
            database.Desconectar();
        }


    }


}
