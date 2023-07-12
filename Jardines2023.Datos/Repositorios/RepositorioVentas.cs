using Jardines2023.Entidades.Dtos;
using Jardines2023.Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace Jardines2023.Datos.Repositorios
{
    public class RepositorioVentas : IRepositorioVentas
    {
        private string cadenaConexion;
        public RepositorioVentas()
        {
            cadenaConexion = ConfigurationManager.ConnectionStrings["MiConexion"].ToString();
        }

        public void Agregar(Venta venta)
        {
            using (var conn = new SqlConnection(cadenaConexion))
            {
                conn.Open();
                string addQuery = @"INSERT INTO Ventas (FechaVenta, ClienteId, TransaccionId, Total, EstadoOrden)
                                VALUES (@FechaVenta, @ClienteId, @TransaccionId, @Total, @EstadoOrden); SELECT SCOPE_IDENTITY()";
                using (var cmd = new SqlCommand(addQuery, conn))
                {
                    cmd.Parameters.Add("@FechaVenta", SqlDbType.DateTime);
                    cmd.Parameters["@FechaVenta"].Value = venta.FechaVenta;

                    cmd.Parameters.Add("@ClienteId", SqlDbType.Int);
                    cmd.Parameters["@ClienteId"].Value = venta.ClienteId;

                    cmd.Parameters.Add("@TransaccionId", SqlDbType.Int);
                    cmd.Parameters["@TransaccionId"].Value = venta.TransaccionId;

                    cmd.Parameters.Add("@Total", SqlDbType.Decimal);
                    cmd.Parameters["@Total"].Value = venta.Total;

                    cmd.Parameters.Add("@EstadoOrden", SqlDbType.Int);
                    cmd.Parameters["@EstadoOrden"].Value = venta.EstadoOrden;

                    int id = Convert.ToInt32(cmd.ExecuteScalar());
                    venta.VentaId = id;
                }
            }
        }

        public void Borrar(int ventaId)
        {
            try
            {
                using (var conn = new SqlConnection(cadenaConexion))
                {
                    conn.Open();
                    string deleteQuery = "DELETE FROM Ventas WHERE VentaId=@VentaId";
                    using (var comando = new SqlCommand(deleteQuery, conn))
                    {
                        comando.Parameters.Add("@VentaId", SqlDbType.Int);
                        comando.Parameters["@VentaId"].Value = ventaId;

                        comando.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {

                if (ex.Message.Contains("REFERENCE"))
                {
                    throw new Exception("Registro relacionado");
                }
            }
        }

        public void Editar(Venta venta)
        {
            using (var conn = new SqlConnection(cadenaConexion))
            {
                conn.Open();
                string updateQuery = @"UPDATE Ventas SET FechaVenta=@FechaVenta, ClienteId=@ClienteId, 
                                 TransaccionId=@TransaccionId, Total=@Total, EstadoOrden=@EstadoOrden
                                WHERE VentaId=@VentaId";
                using (var cmd = new SqlCommand(updateQuery, conn))
                {
                    cmd.Parameters.Add("@FechaVenta", SqlDbType.DateTime);
                    cmd.Parameters["@FechaVenta"].Value = venta.FechaVenta;

                    cmd.Parameters.Add("@ClienteId", SqlDbType.Int);
                    cmd.Parameters["@ClienteId"].Value = venta.ClienteId;

                    cmd.Parameters.Add("@TransaccionId", SqlDbType.Int);
                    cmd.Parameters["@TransaccionId"].Value = venta.TransaccionId;

                    cmd.Parameters.Add("@Total", SqlDbType.Decimal);
                    cmd.Parameters["@Total"].Value = venta.Total;

                    cmd.Parameters.Add("@EstadoOrden", SqlDbType.Int);
                    cmd.Parameters["@EstadoOrden"].Value = venta.EstadoOrden;

                    cmd.Parameters.Add("@VentaId", SqlDbType.Int);
                    cmd.Parameters["@VentaId"].Value = venta.VentaId;

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public bool Existe(Venta venta)
        {
            try
            {
                var cantidad = 0;
                using (var conn = new SqlConnection(cadenaConexion))
                {
                    conn.Open();
                    string selectQuery;
                    if (venta.VentaId == 0)
                    {
                        selectQuery = "SELECT COUNT(*) FROM Ventas WHERE ClienteId=@ClienteId";

                    }
                    else
                    {
                        selectQuery = "SELECT COUNT(*) FROM Ventas WHERE ClienteId=@ClienteId AND VentaId=@VentaId";

                    }
                    using (var comando = new SqlCommand(selectQuery, conn))
                    {
                        comando.Parameters.Add("@ClienteId", SqlDbType.Int);
                        comando.Parameters["@ClienteId"].Value = venta.ClienteId;

                        if (venta.VentaId != 0)
                        {
                            comando.Parameters.Add("@VentaId", SqlDbType.Int);
                            comando.Parameters["@VentaId"].Value = venta.VentaId;

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

        public List<VentaListDto> Filtrar(Venta venta)
        {
            throw new NotImplementedException();
        }

        public int GetCantidad()
        {
            try
            {
                int cantidad = 0;
                using (var conn = new SqlConnection(cadenaConexion))
                {
                    conn.Open();
                    string selectQuery = "SELECT COUNT(*) FROM Ventas";
                    using (var comando = new SqlCommand(selectQuery, conn))
                    {
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

        public Venta GetVentaPorId(int ventaId)
        {
            Venta venta = null;
            using (var con = new SqlConnection(cadenaConexion))
            {
                con.Open();
                string selectQuery = @"SELECT VentaId, FechaVenta, ClienteId, TransaccionId, Total, EstadoOrden
                        FROM Ventas WHERE VentaId=@VentaId";
                using (var cmd = new SqlCommand(selectQuery, con))
                {
                    cmd.Parameters.Add("@VentaId", SqlDbType.Int);
                    cmd.Parameters["@VentaId"].Value = ventaId;
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            reader.Read();
                            venta = ConstruirVenta(reader);
                        }
                    }
                }
            }
            return venta;

        }

        private Venta ConstruirVenta(SqlDataReader reader)
        {
            return new Venta()
            {
                VentaId = reader.GetInt32(0),
                FechaVenta = reader.GetDateTime(1),
                ClienteId = reader.GetInt32(2),
                TransaccionId = reader.GetInt32(3),
                Total = reader.GetDecimal(4),
                EstadoOrden = reader.GetInt32(5)
            };
        }

        public List<VentaListDto> GetVentas()
        {
            List<VentaListDto> lista = new List<VentaListDto>();
            using (var con = new SqlConnection(cadenaConexion))
            {
                con.Open();
                string selectQuery = @"SELECT VentaId, FechaVenta, Nombres, Total 
                               FROM Ventas INNER JOIN Clientes ON Clientes.ClienteId=Ventas.ClienteId ";
                using (var cmd = new SqlCommand(selectQuery, con))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var ventaDto = ConstruirVentaDto(reader);
                            lista.Add(ventaDto);
                        }
                    }
                }
            }
            return lista;
        }

        public List<VentaListDto> GetVentas(Cliente clienteFiltro)
        {
            List<VentaListDto> lista = new List<VentaListDto>();
            try
            {
                using (var conn = new SqlConnection(cadenaConexion))
                {
                    conn.Open();
                    string selectQuery = @"SELECT VentaId, FechaVenta, ClienteId, Total FROM Ventas";
                    using (var cmd = new SqlCommand(selectQuery, conn))
                    {
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var ventaDto = ConstruirVentaDto(reader);
                                lista.Add(ventaDto);
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

        public List<VentaListDto> GetVentasPorPagina(int cantidad, int paginas)
        {
            List<VentaListDto> lista = new List<VentaListDto>();
            try
            {
                using (var conn = new SqlConnection(cadenaConexion))
                {
                    conn.Open();
                    string selectQuery = @"SELECT VentaId, FechaVenta, Nombres, Total FROM Ventas INNER JOIN Clientes ON Clientes.ClienteId=Ventas.ClienteId                        
                        OFFSET @cantidadRegistros ROWS 
                        FETCH NEXT @cantidadPorPagina ROWS ONLY";
                    using (var comando = new SqlCommand(selectQuery, conn))
                    {
                        comando.Parameters.Add("@cantidadRegistros", SqlDbType.Int);
                        comando.Parameters["@cantidadRegistros"].Value = cantidad * (paginas - 1);

                        comando.Parameters.Add("@cantidadPorPagina", SqlDbType.Int);
                        comando.Parameters["@cantidadPorPagina"].Value = cantidad;
                        using (var reader = comando.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var venta = ConstruirVentaDto(reader);
                                lista.Add(venta);
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
        private VentaListDto ConstruirVentaDto(SqlDataReader reader)
        {
            return new VentaListDto()
            {
                VentaId = reader.GetInt32(0),
                FechaVenta = reader.GetDateTime(1),
                NombreCliente = reader.GetString(2),
                Total = reader.GetDecimal(3)
            };
        }
    }
}
