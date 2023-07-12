using Jardines2023.Comun.Interfaces;
using Jardines2023.Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Jardines2023.Comun.Repositorios
{
    public class RepositorioCiudades : IRepositorioCiudades
    {

        private readonly string cadenaConexion;
        public RepositorioCiudades()
        {
            cadenaConexion = ConfigurationManager.ConnectionStrings["MiConexion"].ToString();
        }


        public void Agregar(Ciudad ciudad)
        {
            using (var conn = new SqlConnection(cadenaConexion))
            {
                conn.Open();
                string insertQuery = "INSERT INTO Ciudades (NombreCiudad, PaisId) VALUES(@NombreCiudad, @PaisId); SELECT SCOPE_IDENTITY()";
                using (var comando = new SqlCommand(insertQuery, conn))
                {
                    comando.Parameters.Add("@NombreCiudad", SqlDbType.NVarChar);
                    comando.Parameters["@NombreCiudad"].Value = ciudad.NombreCiudad;

                    comando.Parameters.Add("@PaisId", SqlDbType.Int);
                    comando.Parameters["@PaisId"].Value = ciudad.PaisId;

                    int id = Convert.ToInt32(comando.ExecuteScalar());
                    ciudad.CiudadId = id;
                }
            }
        }

        public void Borrar(int ciudadId)
        {
            try
            {
                using (var conn = new SqlConnection(cadenaConexion))
                {
                    conn.Open();
                    string deleteQuery = "DELETE FROM Ciudades WHERE CiudadId=@CiudadId";
                    using (var comando = new SqlCommand(deleteQuery, conn))
                    {
                        comando.Parameters.Add("@CiudadId", SqlDbType.Int);
                        comando.Parameters["@CiudadId"].Value = ciudadId;

                        comando.ExecuteNonQuery();
                    }
                }

            }
            catch (Exception ex)
            {

                if (ex.Message.Contains("REFERENCE"))
                {
                    throw new Exception("Registro relacionado... Baja denegada");
                }
            }
        }

        public void Editar(Ciudad ciudad)
        {
            using (var conn = new SqlConnection(cadenaConexion))
            {
                conn.Open();
                string updateQuery = "UPDATE Ciudades SET NombreCiudad=@NombreCiudad, PaisId=@PaisId WHERE CiudadId=@CiudadId";
                using (var cmd = new SqlCommand(updateQuery, conn))
                {
                    cmd.Parameters.Add("@NombreCiudad", SqlDbType.NVarChar);
                    cmd.Parameters["@NombreCiudad"].Value = ciudad.NombreCiudad;
                    cmd.Parameters.Add("@PaisId", SqlDbType.Int);
                    cmd.Parameters["@PaisId"].Value = ciudad.PaisId;

                    cmd.Parameters.Add("@CiudadId", SqlDbType.Int);
                    cmd.Parameters["@CiudadId"].Value = ciudad.CiudadId;

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public bool Existe(Ciudad ciudad)
        {
            int cantidad;
            using (var con = new SqlConnection(cadenaConexion))
            {
                con.Open();
                string selectQuery;
                if (ciudad.CiudadId == 0)
                {
                    selectQuery = "SELECT COUNT(*) FROM Ciudades WHERE NombreCiudad=@NombreCiudad AND PaisId=@PaisId";
                }
                else
                {
                    selectQuery = "SELECT COUNT(*) FROM Ciudades WHERE NombreCiudad=@NombreCiudad AND PaisId=@PaisId AND CiudadId!=@CiudadId";

                }
                using (var cmd = new SqlCommand(selectQuery, con))
                {
                    cmd.Parameters.Add("@NombreCiudad", SqlDbType.NVarChar);
                    cmd.Parameters["@NombreCiudad"].Value = ciudad.NombreCiudad;

                    cmd.Parameters.Add("@PaisId", SqlDbType.Int);
                    cmd.Parameters["@PaisId"].Value = ciudad.PaisId;
                    if (ciudad.CiudadId != 0)
                    {
                        cmd.Parameters.Add("@CiudadId", SqlDbType.Int);
                        cmd.Parameters["@CiudadId"].Value = ciudad.CiudadId;
                    }
                    cantidad = Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
            return cantidad > 0;
        }

        public List<Ciudad> Filtrar(Pais pais)
        {
            List<Ciudad> lista = new List<Ciudad>();
            try
            {
                using (var conn = new SqlConnection(cadenaConexion))
                {
                    conn.Open();
                    string selectQuery = "SELECT CiudadId, NombreCiudad, PaisId FROM Ciudades WHERE PaisId=@PaisId ORDER BY PaisId, NombreCiudad";
                    using (var cmd = new SqlCommand(selectQuery, conn))
                    {
                        cmd.Parameters.Add("@PaisId", SqlDbType.Int);
                        cmd.Parameters["@PaisId"].Value = pais.PaisId;

                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var ciudad = ConstruirCiudad(reader);
                                lista.Add(ciudad);
                            }
                        }
                    }
                }
                return lista;
            }
            catch (Exception)
            {

                throw;
            }

        }

        private Ciudad ConstruirCiudad(SqlDataReader reader)
        {
            return new Ciudad()
            {
                CiudadId = reader.GetInt32(0),
                NombreCiudad = reader.GetString(1),
                PaisId = reader.GetInt32(2)
            };
        }

        public int GetCantidad(int? paisId)
        {
            int cantidad = 0;
            using (var con = new SqlConnection(cadenaConexion))
            {
                con.Open();
                string selectQuery;
                if (paisId == null)
                {
                    selectQuery = "SELECT COUNT(*) FROM Ciudades";

                }
                else
                {
                    selectQuery = "SELECT COUNT(*) FROM Ciudades WHERE PaisId=@PaisId";
                }
                using (var cmd = new SqlCommand(selectQuery, con))
                {
                    if (paisId.HasValue)
                    {
                        cmd.Parameters.Add("@PaisId", SqlDbType.Int);
                        cmd.Parameters["@PaisId"].Value = paisId.Value;

                    }

                    cantidad = Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
            return cantidad;
        }

        public List<Ciudad> GetCiudades()
        {
            List<Ciudad> lista = new List<Ciudad>();
            try
            {
                using (var conn = new SqlConnection(cadenaConexion))
                {
                    conn.Open();
                    string selectQuery = "SELECT CiudadId, NombreCiudad, PaisId FROM Ciudades ORDER BY PaisId, NombreCiudad";
                    using (var cmd = new SqlCommand(selectQuery, conn))
                    {
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var ciudad = ConstruirCiudad(reader);
                                lista.Add(ciudad);
                            }
                        }
                    }
                }
                return lista;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<Ciudad> GetCiudadesPorPagina(int cantidad, int paginaActual)
        {
            List<Ciudad> lista = new List<Ciudad>();
            try
            {
                using (var conn = new SqlConnection(cadenaConexion))
                {
                    conn.Open();
                    string selectQuery = @"SELECT CiudadId, NombreCiudad, PaisId FROM Ciudades
                        ORDER BY PaisId, NombreCiudad
                        OFFSET @cantidadRegistros ROWS 
                        FETCH NEXT @cantidadPorPagina ROWS ONLY";
                    using (var comando = new SqlCommand(selectQuery, conn))
                    {
                        comando.Parameters.Add("@cantidadRegistros", SqlDbType.Int);
                        comando.Parameters["@cantidadRegistros"].Value = cantidad * (paginaActual - 1);

                        comando.Parameters.Add("@cantidadPorPagina", SqlDbType.Int);
                        comando.Parameters["@cantidadPorPagina"].Value = cantidad;
                        using (var reader = comando.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var ciudad = ConstruirCiudad(reader);
                                lista.Add(ciudad);
                            }
                        }
                    }
                }
                return lista;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Ciudad GetCiudadPorId(int ciudadId)
        {
            Ciudad ciudad = null;
            try
            {
                using (var conn = new SqlConnection(cadenaConexion))
                {
                    conn.Open();
                    string selectQuery = "SELECT CiudadId, NombreCiudad, PaisId FROM Ciudades WHERE CiudadId=@CiudadId";
                    using (var cmd = new SqlCommand(selectQuery, conn))
                    {
                        cmd.Parameters.Add("@CiudadId", SqlDbType.Int);
                        cmd.Parameters["@CiudadId"].Value = ciudadId;
                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                reader.Read();
                                ciudad = ConstruirCiudad(reader);
                            }
                        }
                    }
                }
                return ciudad;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<Ciudad> GetCiudades(int? paisId)
        {
            List<Ciudad> lista = new List<Ciudad>();
            try
            {
                using (var conn = new SqlConnection(cadenaConexion))
                {
                    conn.Open();
                    string selectQuery;
                    if (paisId == null)
                    {
                        selectQuery = @"SELECT CiudadId, NombreCiudad, PaisId 
                            FROM Ciudades 
                                ORDER BY NombreCiudad";
                    }
                    else
                    {
                        selectQuery = @"SELECT CiudadId, NombreCiudad, PaisId 
                            FROM Ciudades WHERE PaisId=@PaisId
                                ORDER BY NombreCiudad";
                    }
                    using (var cmd = new SqlCommand(selectQuery, conn))
                    {
                        if (paisId.HasValue)
                        {
                            cmd.Parameters.Add("@PaisId", SqlDbType.Int);
                            cmd.Parameters["@PaisId"].Value = paisId.Value;

                        }

                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var ciudad = ConstruirCiudad(reader);
                                lista.Add(ciudad);
                            }
                        }
                    }
                }
                return lista;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
