using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using ProyectoRolesPermisos.Models;
using System.Data;
using System.Data.SqlClient;

namespace ProyectoRolesPermisos.Datos
{
    public class LogicaDB
    {
        //En esta clase voy a tener la logica para obtener la informacion de la base de datos
        //se utilizara ADO .NET para hacer la conexion


        //String con la cadena de conexion
        private string cadenaDeConexion = "Data Source=GERA\\SQLEXPRESS;" + "Initial Catalog=ProyectoLogin;" + "User id=sa;" + "Password=1234;" + "Trusted_Connection=True;" + "TrustServerCertificate=True;" + "MultipleActiveResultSets=True;";


        // Método para obtener todos los datos
        public List<Usuario> ObtenerDatos()
        {
            List<Usuario> usuariosConsulta = new List<Usuario>();
            //Query para hacer la consulta a la base de datos
            string query = "SELECT * FROM Usuario";

            using (SqlConnection conexion= new SqlConnection(cadenaDeConexion))
            {
                SqlCommand comando = new SqlCommand(query, conexion);

                try
                {
                    //Apertura de la conexion
                    conexion.Open();

                    //Aqui se ejecuta el query apenas
                    SqlDataReader reader = comando.ExecuteReader();
                    while (reader.Read())
                    {
                        Usuario usuario = new Usuario
                        {
                            Id = (int)reader["Id"],
                            Nombre = reader["Nombre"].ToString(),
                            Correo = reader["Correo"].ToString(),
                            Clave = reader["Clave"].ToString(),
                            Roles = reader["Roles"].ToString().Split(',')
                        };

                    usuariosConsulta.Add(usuario);


                        Console.WriteLine("{0},{1},{2},{3},{4}", reader[0], reader[1], reader[2], reader[3], reader[4]);
                    }

                    //Cerramos la conexion
                    conexion.Close();
                }
                catch (Exception ex) 
                {
                    Console.WriteLine(ex.Message);  
                }
            }
            return usuariosConsulta;
        }


        //Metodo que nos va a validar si un usuario existe en la base de datos respecto al correo y a la clave que recibimos
        public Usuario existeEnBD(string _correo, string _clave)
        {
            // Query para hacer la consulta a la base de datos
            string query = "SELECT Id, Nombre, Correo, Clave, Roles FROM Usuario WHERE Correo = @Correo AND Clave = @Clave";

            using (SqlConnection conexion = new SqlConnection(cadenaDeConexion))
            {
                using (SqlCommand comando = new SqlCommand(query, conexion))
                {
                    
                    comando.Parameters.AddWithValue("@Correo", _correo);
                    comando.Parameters.AddWithValue("@Clave", _clave);

                    try
                    {
                        // Apertura de la conexión
                        conexion.Open();

                        // Ejecutar el query y leer los datos
                        using (SqlDataReader lector = comando.ExecuteReader())
                        {
                            if (lector.Read())
                            {
                                Usuario usuario = new Usuario
                                {
                                    Id = (int)lector["Id"],
                                    Nombre = lector["Nombre"].ToString(),
                                    Correo = lector["Correo"].ToString(),
                                    Clave = lector["Clave"].ToString(),
                                    Roles = lector["Roles"].ToString().Split(',')
                                };
                                return usuario;
                            }
                        }
                        //Cerramos la conexion
                        conexion.Close();
                    }
                    catch (Exception ex)
                    {
                        // Manejo de la excepción (por ejemplo, registrar el error)
                        Console.WriteLine(ex.Message);
                    }
                }
                return null;
            }
        }
    }
}
