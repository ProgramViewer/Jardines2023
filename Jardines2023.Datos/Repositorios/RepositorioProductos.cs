using Jardines2023.Comun.Interfaces;
using Jardines2023.Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using Jardines2023.Entidades.Dtos.Producto;

namespace Jardines2023.Datos.Repositorios
{
    public class RepositorioProductos : IRepositorioProductos
    {
        private readonly string cadenaConexion;
        public RepositorioProductos()
        {
            cadenaConexion = ConfigurationManager.ConnectionStrings["MiConexion"].ToString();
        }


        public void Agregar(Producto producto)
        {
            try
            {
                using (var conn = new SqlConnection(cadenaConexion))
                {
                    conn.Open();
                    string insertQuery = @"INSERT INTO Productos (NombreProducto, NombreLatin, 
                        ProveedorId, CategoriaId, PrecioUnitario, UnidadesEnStock, UnidadesEnPedido,
                        NivelDeReposicion, Suspendido, Imagen) VALUES(@NombreProducto, @NombreLatin,
                        @ProveedorId, @CategoriaId, @PrecioUnitario, @UnidadesEnStock, 
                        @UnidadesEnPedido, @NivelDeReposicion, @Suspendido, @Imagen); SELECT SCOPE_IDENTITY()";
                    using (var comando = new SqlCommand(insertQuery, conn))
                    {
                        comando.Parameters.Add("@NombreProducto", SqlDbType.NVarChar);
                        comando.Parameters["@NombreProducto"].Value = producto.NombreProducto;

                        comando.Parameters.Add("@NombreLatin", SqlDbType.NVarChar);
                        comando.Parameters["@NombreLatin"].Value = producto.NombreLatin;

                        comando.Parameters.Add("@ProveedorId", SqlDbType.Int);
                        comando.Parameters["@ProveedorId"].Value = producto.ProveedorId;

                        comando.Parameters.Add("@CategoriaId", SqlDbType.Int);
                        comando.Parameters["@CategoriaId"].Value = producto.CategoriaId;

                        comando.Parameters.Add("@PrecioUnitario", SqlDbType.Decimal);
                        comando.Parameters["@PrecioUnitario"].Value = producto.PrecioUnitario;

                        comando.Parameters.Add("@UnidadesEnStock", SqlDbType.Int);
                        comando.Parameters["@UnidadesEnStock"].Value = producto.UnidadesEnStock;

                        comando.Parameters.Add("@UnidadesEnPedido", SqlDbType.Int);
                        comando.Parameters["@UnidadesEnPedido"].Value = producto.UnidadesEnPedido;

                        comando.Parameters.Add("@NivelDeReposicion", SqlDbType.Int);
                        comando.Parameters["@NivelDeReposicion"].Value = producto.NivelDeReposicion;

                        comando.Parameters.Add("@Suspendido", SqlDbType.Bit);
                        comando.Parameters["@Suspendido"].Value = producto.Suspendido;

                        comando.Parameters.Add("@Imagen", SqlDbType.NVarChar);
                        comando.Parameters["@Imagen"].Value = producto.Imagen;


                        int id = Convert.ToInt32(comando.ExecuteScalar());
                        producto.ProductoId = id;
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public void Borrar(int productoId)
        {
            try
            {
                using (var conn = new SqlConnection(cadenaConexion))
                {
                    conn.Open();
                    string deleteQuery = "DELETE FROM Productos WHERE ProductoId=@ProductoId";
                    using (var comando = new SqlCommand(deleteQuery, conn))
                    {
                        comando.Parameters.Add("@ProductoId", SqlDbType.Int);
                        comando.Parameters["@ProductoId"].Value = productoId;

                        comando.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public void Editar(Producto producto)
        {
            try
            {
                using (var conn = new SqlConnection(cadenaConexion))
                {
                    conn.Open();
                    string updateQuery = @"UPDATE Productos SET NombreProducto=@NombreProducto, 
                        NombreLatin=@NombreLatin, ProveedorId=@ProveedorId, CategoriaId=@CategoriaId,
                        PrecioUnitario=@PrecioUnitario, UnidadesEnStock=@UnidadesEnStock, 
                        UnidadesEnPedido=@UnidadesEnPedido, NivelDeReposicion=@NivelDeReposicion,
                        Suspendido=@Suspendido, Imagen=@Imagen WHERE ProductoId=@ProductoId";
                    using (var comando = new SqlCommand(updateQuery, conn))
                    {
                        comando.Parameters.Add("@NombreProducto", SqlDbType.NVarChar);
                        comando.Parameters["@NombreProducto"].Value = producto.NombreProducto;

                        comando.Parameters.Add("@NombreLatin", SqlDbType.NVarChar);
                        comando.Parameters["@NombreLatin"].Value = producto.NombreLatin;

                        comando.Parameters.Add("@ProveedorId", SqlDbType.Int);
                        comando.Parameters["@ProveedorId"].Value = producto.ProveedorId;

                        comando.Parameters.Add("@CategoriaId", SqlDbType.Int);
                        comando.Parameters["@CategoriaId"].Value = producto.CategoriaId;

                        comando.Parameters.Add("@PrecioUnitario", SqlDbType.Decimal);
                        comando.Parameters["@PrecioUnitario"].Value = producto.PrecioUnitario;

                        comando.Parameters.Add("@UnidadesEnStock", SqlDbType.Int);
                        comando.Parameters["@UnidadesEnStock"].Value = producto.UnidadesEnStock;

                        comando.Parameters.Add("@UnidadesEnPedido", SqlDbType.Int);
                        comando.Parameters["@UnidadesEnPedido"].Value = producto.UnidadesEnPedido;

                        comando.Parameters.Add("@NivelDeReposicion", SqlDbType.Int);
                        comando.Parameters["@NivelDeReposicion"].Value = producto.NivelDeReposicion;

                        comando.Parameters.Add("@Suspendido", SqlDbType.Bit);
                        comando.Parameters["@Suspendido"].Value = producto.Suspendido;

                        comando.Parameters.Add("@Imagen", SqlDbType.NVarChar);
                        comando.Parameters["@Imagen"].Value = producto.Imagen;

                        comando.Parameters.Add("@ProductoId", SqlDbType.Int);
                        comando.Parameters["@ProductoId"].Value = producto.ProductoId;


                        comando.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }

        }
        public List<ProductoListDto> GetProductos()
        {
            try
            {
                List<ProductoListDto> lista = new List<ProductoListDto>();
                using (var conn = new SqlConnection(cadenaConexion))
                {
                    conn.Open();
                    conn.Open();
                    string selectQuery = @"SELECT ProductoId, NombreProducto, NombreCategoria, PrecioUnitario
                        UnidadesEnStock, Suspendido FROM Productos INNER JOIN Categorias
                        ON Productos.CategoriaId=Categorias.CategoriaId
                        ORDER BY NombreProducto";
                    using (var comando = new SqlCommand(selectQuery, conn))
                    {
                        using (var reader = comando.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var producto = ConstruirProductoDto(reader);
                                lista.Add(producto);
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

        public int GetCantidad()
        {
            try
            {
                int cantidad = 0;
                using (var conn = new SqlConnection(cadenaConexion))
                {
                    conn.Open();
                    string selectQuery = "SELECT COUNT(*) FROM Productos";
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

        public bool Existe(Producto producto)
        {
            try
            {
                var cantidad = 0;
                using (var conn = new SqlConnection(cadenaConexion))
                {
                    conn.Open();
                    string selectQuery;
                    if (producto.ProductoId == 0)
                    {
                        selectQuery = "SELECT COUNT(*) FROM Productos WHERE NombreProducto=@NombreProducto";

                    }
                    else
                    {
                        selectQuery = "SELECT COUNT(*) FROM Productos WHERE NombreProducto=@NombreProducto AND ProductoId<>@ProductoId";

                    }
                    using (var comando = new SqlCommand(selectQuery, conn))
                    {
                        comando.Parameters.Add("@NombreProducto", SqlDbType.NVarChar);
                        comando.Parameters["@NombreProducto"].Value = producto.NombreProducto;

                        if (producto.ProductoId != 0)
                        {
                            comando.Parameters.Add("@ProductoId", SqlDbType.Int);
                            comando.Parameters["@ProductoId"].Value = producto.ProductoId;

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

        public List<ProductoListDto> GetProductosPorPagina(int cantidad, int pagina)
        {
            List<ProductoListDto> lista = new List<ProductoListDto>();
            try
            {
                using (var conn = new SqlConnection(cadenaConexion))
                {
                    conn.Open();
                    string selectQuery = @"SELECT ProductoId, NombreProducto, NombreCategoria, PrecioUnitario,
                        UnidadesEnStock, Suspendido FROM Productos INNER JOIN Categorias
                        ON Productos.CategoriaId=Categorias.CategoriaId
                        ORDER BY NombreProducto
                        OFFSET @cantidadRegistros ROWS 
                        FETCH NEXT @cantidadPorPagina ROWS ONLY";
                    using (var comando = new SqlCommand(selectQuery, conn))
                    {
                        comando.Parameters.Add("@cantidadRegistros", SqlDbType.Int);
                        comando.Parameters["@cantidadRegistros"].Value = cantidad * (pagina - 1);

                        comando.Parameters.Add("@cantidadPorPagina", SqlDbType.Int);
                        comando.Parameters["@cantidadPorPagina"].Value = cantidad;
                        using (var reader = comando.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var productoDto = ConstruirProductoDto(reader);
                                lista.Add(productoDto);
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

        private ProductoListDto ConstruirProductoDto(SqlDataReader reader)
        {
            return new ProductoListDto
            {
                ProductoId = reader.GetInt32(0),
                NombreProducto = reader.GetString(1),
                Categoria = reader.GetString(2),
                PrecioUnitario = reader.GetDecimal(3),
                UnidadesEnStock = reader.GetInt32(4),
                Suspendido = reader.GetBoolean(5)
            };
        }

        private Producto ConstruirProducto(SqlDataReader reader)
        {
            return new Producto()
            {
                ProductoId = reader.GetInt32(0),
                NombreProducto = reader.GetString(1),
                NombreLatin = reader[2] == DBNull.Value ? string.Empty : reader.GetString(2),
                ProveedorId = reader.GetInt32(3),
                CategoriaId = reader.GetInt32(4),
                PrecioUnitario = reader.GetDecimal(5),
                UnidadesEnStock = reader.GetInt32(6),
                UnidadesEnPedido = reader.GetInt32(7),
                NivelDeReposicion = reader.GetInt32(8),
                Suspendido = reader.GetBoolean(9),
                Imagen = reader[10]==DBNull.Value?string.Empty: reader.GetString(10)
            };
        }

        public Producto GetProductoPorId(int productoId)
        {
            try
            {
                Producto producto = null;
                using (var conn = new SqlConnection(cadenaConexion))
                {
                    conn.Open();
                    string selectQuery = @"SELECT ProductoId, NombreProducto, NombreLatin,
                        ProveedorId, CategoriaId, PrecioUnitario, UnidadesEnStock,
                        UnidadesEnPedido, NivelDeReposicion, Suspendido, Imagen 
                        FROM Productos WHERE ProductoId=@ProductoId";
                    using (var comando = new SqlCommand(selectQuery, conn))
                    {
                        comando.Parameters.Add("@ProductoId", SqlDbType.Int);
                        comando.Parameters["@ProductoId"].Value =productoId;
                        using (var reader = comando.ExecuteReader())
                        {
                            if(reader.HasRows)
                            {
                                reader.Read();
                                producto=ConstruirProducto(reader);
                            }
                        }
                    }
                }
                return producto;
            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}
