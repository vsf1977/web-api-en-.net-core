using Microsoft.AspNetCore.Mvc;
using Prueba.Models;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Net;
using System.Net.Http;
using System;
using System.Web;


namespace Prueba.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TicketController : ControllerBase
    {
        Database database = new Database();
        SqlCommand comando;
        SqlDataReader cursor;


        [HttpGet("/ticket")]
        public List<Ticket> Get()
        {
            comando = new SqlCommand("select * from ticket", database.Conectar());
            cursor = comando.ExecuteReader();
            List<Ticket> Tickets = new List<Ticket>();
            if (cursor.HasRows)
            {
                while (cursor.Read())
                {
                    Ticket Ticket = new Ticket();
                    Ticket.id = cursor.GetString(0);
                    Ticket.usuario = cursor.GetString(1);
                    Ticket.fecha_creacion = cursor.GetDateTime(2);
                    Ticket.fecha_actualizacion = cursor.GetDateTime(3);
                    Tickets.Add(Ticket);
                }
            }
            else
            {
                Tickets = null;
            }
            database.Desconectar();
            return Tickets;
        }

        [HttpGet("/ticket/{id}")]
        public Ticket Get(string id)
        {
            comando = new SqlCommand(null, database.Conectar());
            comando.CommandText = "select * from ticket where id = @id";
            
            SqlParameter idParam = new SqlParameter("@id", System.Data.SqlDbType.VarChar, 50);

            idParam.Value = id;
            
            comando.Parameters.Add(idParam);
            comando.Prepare();
            Ticket Ticket = new Ticket();
            cursor = comando.ExecuteReader();
            
            if (cursor.HasRows)
            {
                cursor.Read();
                Ticket.id = cursor.GetString(0);
                Ticket.usuario = cursor.GetString(1);
                Ticket.fecha_creacion = cursor.GetDateTime(2);
                Ticket.fecha_actualizacion = cursor.GetDateTime(3);
            }
            else
                Ticket = null;
            database.Desconectar();
            return Ticket;
        }

        [HttpPost("/ticket")]
        public HttpResponseMessage Post([FromForm] Ticket data)
        {
            Ticket Ticket = new Ticket();
            Ticket.id = data.id;
            Ticket.usuario = data.usuario;
            Ticket.fecha_creacion = data.fecha_creacion;
            Ticket.fecha_actualizacion = data.fecha_actualizacion;
            Ticket.estatus = data.estatus;
            try
            {
                Ticket.insertar();
                return new HttpResponseMessage(HttpStatusCode.Created);
            }
            catch (SqlException ex)
            {
                HttpResponseMessage resp = new HttpResponseMessage();
                resp.StatusCode = HttpStatusCode.InternalServerError;
                string v = ex.Message.Trim();
                int index = v.IndexOf("\r");
                v = v.Substring(0, index - 1);
                resp.ReasonPhrase = v;
                return resp;

            }
        }

        [HttpPut("/ticket")]
        public HttpResponseMessage Put([FromForm] Ticket data)
        {
            Ticket Ticket = new Ticket();
            Ticket.id = data.id;
            Ticket.usuario = data.usuario;
            Ticket.fecha_creacion = data.fecha_creacion;
            Ticket.fecha_actualizacion = data.fecha_actualizacion;
            Ticket.estatus = data.estatus;
            try
            {
                Ticket.actualizar();
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            catch (SqlException ex)
            {
                HttpResponseMessage resp = new HttpResponseMessage();
                resp.StatusCode = HttpStatusCode.InternalServerError;
                string v = ex.Message.Trim();
                int index = v.IndexOf("\r");
                v = v.Substring(0, index - 1);
                resp.ReasonPhrase = v;
                return resp;
            }
        }


        public HttpResponseMessage Delete([FromForm] Ticket data)
        {
            Ticket Ticket = new Ticket();
            Ticket.id = data.id;           
            try
            {
                Ticket.borrar();
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            catch (SqlException ex)
            {
                HttpResponseMessage resp = new HttpResponseMessage();
                resp.StatusCode = HttpStatusCode.InternalServerError;
                string v = ex.Message.Trim();
                int index = v.IndexOf("\r");
                v = v.Substring(0, index - 1);
                resp.ReasonPhrase = v;
                return resp;
            }
        }



    }
                
}
