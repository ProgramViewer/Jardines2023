using Jardines2023.Comun.Interfaces;
using Jardines2023.Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Jardines2023.Comun.Repositorios
{
    public class RepositorioPaises : IRepositorioPaises
    {
        private readonly string cadenaConexion;
        public RepositorioPaises()
        {
            cadenaConexion = ConfigurationManager.ConnectionStrings["MiConexion"].ToString();
        }


        public void Agregar(Pais pais)
        {
            try
            {
                using (var conn = new SqlConnection(cadenaConexion))
                {
                    conn.Open();
                    string insertQuery = "INSERT INTO Paises (NombrePais) VALUES(@NombrePais); SELECT SCOPE_IDENTITY()";
                    using (var comando = new SqlCommand(insertQuery, conn))
                    {
                        comando.Parameters.Add("@NombrePais", SqlDbType.NVarChar);
                        comando.Parameters["@NombrePais"].Value = pais.NombrePais;

                        int id = Convert.ToInt32(comando.ExecuteScalar());
                        pais.PaisId = id;
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public void Borrar(int paisId)
        {
            try
            {
                using (var conn = new SqlConnection(cadenaConexion))
                {
                    conn.Open();
                    string deleteQuery = "DELETE FROM Paises WHERE PaisId=@PaisId";
                    using (var comando = new SqlCommand(deleteQuery, conn))
                    {
                        comando.Parameters.Add("@PaisId", SqlDbType.Int);
                        comando.Parameters["@PaisId"].Value = paisId;

                        comando.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public void Editar(Pais pais)
        {
            try
            {
                using (var conn=new SqlConnection(cadenaConexion))
                {
                    conn.Open();
                    string updateQuery = "UPDATE Paises SET NombrePais=@NombrePais WHERE PaisId=@PaisId";
                    using (var cmd=new SqlCommand(updateQuery,conn))
                    {
                        cmd.Parameters.Add("@NombrePais", SqlDbType.NChar);
                        cmd.Parameters["@NombrePais"].Value = pais.NombrePais;

                        cmd.Parameters.Add("@PaisId", SqlDbType.Int);
                        cmd.Parameters["@PaisId"].Value = pais.PaisId;

                        cmd.ExecuteNonQuery ();
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public List<Pais> GetPaises()
        {
            try
            {
                List<Pais> lista = new List<Pais>();
                using (var conn = new SqlConnection(cadenaConexion))
                {
                    conn.Open();
                    string selectQuery = "SELECT PaisId, NombrePais FROM Paises ORDER BY NombrePais";
                    using (var comando = new SqlCommand(selectQuery, conn))
                    {
                        using (var reader = comando.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var pais = ConstruirPais(reader);
                                lista.Add(pais);
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

        //public int GetCantidad()
        //{
        //    try
        //    {
        //        int cantidad = 0;
        //        using (var conn = new SqlConnection(cadenaConexion))
        //        {
        //            conn.Open();
        //            string selectQuery = "SELECT COUNT(*) FROM Paises";
        //            using (var comando = new SqlCommand(selectQuery, conn))
        //            {
        //                cantidad = (int)comando.ExecuteScalar();
        //            }
        //        }
        //        return cantidad;

        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}
        public int GetCantidad(string textoFiltro=null)
        {
            try
            {
                int cantidad = 0;
                using (var conn = new SqlConnection(cadenaConexion))
                {
                    conn.Open();
                    string selectQuery;
                    if (textoFiltro==null)
                    {
                        selectQuery = "SELECT COUNT(*) FROM Paises";
                    }
                    else
                    {
                        selectQuery = "SELECT COUNT(*) FROM Paises WHERE NombrePais LIKE @textoFiltro";
                    }
                    using (var comando = new SqlCommand(selectQuery, conn))
                    {
                        if (textoFiltro!=null)
                        {
                            comando.Parameters.Add("@textoFiltro", SqlDbType.NVarChar);
                            comando.Parameters["@textoFiltro"].Value = $"{textoFiltro}%";
                        }

                        cantidad = (int)comando.ExecuteScalar();
                    }
                }
                return cantidad;

            }
            catch (Exception)
            {

                throw;
            }

        }

        public bool Existe(Pais pais)
        {
            try
            {
                var cantidad = 0;
                using (var conn = new SqlConnection(cadenaConexion))
                {
                    conn.Open();
                    string selectQuery;
                    if (pais.PaisId==0)
                    {
                        selectQuery = "SELECT COUNT(*) FROM Paises WHERE NombrePais=@NombrePais";

                    }
                    else
                    {
                        selectQuery = "SELECT COUNT(*) FROM Paises WHERE NombrePais=@NombrePais AND PaisId!=@PaisId";

                    }
                    using (var comando = new SqlCommand(selectQuery, conn))
                    {
                        comando.Parameters.Add("@NombrePais", SqlDbType.NVarChar);
                        comando.Parameters["@NombrePais"].Value = pais.NombrePais;
                        if (pais.PaisId!=0)
                        {
                            comando.Parameters.Add("@PaisId", SqlDbType.Int);
                            comando.Parameters["@PaisId"].Value = pais.PaisId;


                        }

                        cantidad = (int)comando.ExecuteScalar();
                    }
                }
                return cantidad > 0;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public Pais ConstruirPais(SqlDataReader reader)
        {
            return new Pais()
            {
                PaisId = reader.GetInt32(0),
                NombrePais = reader.GetString(1)
            };
        }

        public Pais GetPaisPorId(int paisId)
        {
            Pais pais = null;
            using (var con=new SqlConnection(cadenaConexion))
            {
                con.Open();
                string selectQuery = "SELECT PaisId, NombrePais FROM Paises WHERE PaisId=@PaisId";
                using (var cmd=new SqlCommand(selectQuery,con))
                {
                    cmd.Parameters.Add("@PaisId", SqlDbType.Int);
                    cmd.Parameters["@PaisId"].Value=paisId;

                    using (var reader=cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            reader.Read();
                            pais = ConstruirPais(reader);
                        }
                    }
                }
            }
            return pais;
        }

        public List<Pais> GetPaisesPorPagina(int cantidad, int paginaActual, string textoFiltro=null)
        {
            try
            {
                List<Pais> lista = new List<Pais>();
                using (var conn = new SqlConnection(cadenaConexion))
                {
                    conn.Open();
                    string selectQuery;
                    if (textoFiltro==null)
                    {
                        selectQuery = @"SELECT PaisId, NombrePais FROM Paises
                            ORDER BY NombrePais 
                            OFFSET @cantidadRegistros ROWS FETCH NEXT @cantidadPorPagina ROWS ONLY";

                    }
                    else
                    {
                        selectQuery = @"SELECT PaisId, NombrePais FROM Paises WHERE NombrePais LIKE @textoFiltro
                            ORDER BY NombrePais 
                            OFFSET @cantidadRegistros ROWS FETCH NEXT @cantidadPorPagina ROWS ONLY";

                    }
                    using (var comando = new SqlCommand(selectQuery, conn))
                    {
                        if (textoFiltro!=null)
                        {
                            comando.Parameters.Add("@textoFiltro", SqlDbType.NVarChar);
                            comando.Parameters["@textoFiltro"].Value = $"{textoFiltro}%";

                        }
                        comando.Parameters.Add("@cantidadRegistros",SqlDbType.Int);
                        comando.Parameters["@cantidadRegistros"].Value = cantidad*(paginaActual-1);

                        comando.Parameters.Add("@cantidadPorPagina", SqlDbType.Int);
                        comando.Parameters["@cantidadPorPagina"].Value = cantidad;
                        using (var reader = comando.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var pais = ConstruirPais(reader);
                                lista.Add(pais);
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

        public List<Pais> GetPaises(string textoFiltro)
        {
            try
            {
                List<Pais> lista = new List<Pais>();
                using (var conn = new SqlConnection(cadenaConexion))
                {
                    conn.Open();
                    string selectQuery = "SELECT PaisId, NombrePais FROM Paises WHERE NombrePais LIKE @textoFiltro ORDER BY NombrePais";
                    using (var comando = new SqlCommand(selectQuery, conn))
                    {
                        comando.Parameters.Add("@textoFiltro", SqlDbType.NVarChar);
                        comando.Parameters["@textoFiltro"].Value = $"{textoFiltro}%";
                        using (var reader = comando.ExecuteReader())
                        {
                            
                            while (reader.Read())
                            {
                                var pais = ConstruirPais(reader);
                                lista.Add(pais);
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
