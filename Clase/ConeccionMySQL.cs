using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ExamenFinal.Clase.Models;
using MySql.Data.MySqlClient;

namespace ExamenFinal.Clase
{
    internal class ConeccionMySQL
    {
        //se conecta a la base de datos mysql
        string connectionString = "server=localhost;database=examenfinalcrud;user=root;password=Casttle32057881";
        //se crea objeto de tipo mysqlconecccion
        private MySqlConnection connection;

        //Cosntructor
        public ConeccionMySQL()
        {
            //inicializamos la coneccion a la base de datos
            connection = new MySqlConnection(connectionString);
        }


        //creamos el metodo para insertar libros
        public void InsertarLibros(Libros lbr)
        {
            try
            {
                //se crea la consulta SQL para insertar datos
                string query = "INSERT INTO libros (Titulo, Autor, Año_de_Publicación, Editorial, Genero, Paginas, ISBN, Alquilar) VALUES (@Titulo, @Autor, @Año_de_Publicación, @Editorial, @Genero, @Paginas, @ISBN, @Alquilar)";
               //se envia el comando a mysql
                MySqlCommand cmd = new MySqlCommand(query, connection);
                //se setean los datos a las variables de envio a la DB 
                cmd.Parameters.AddWithValue("@Titulo", lbr.Titulo);
                cmd.Parameters.AddWithValue("@Autor", lbr.Autor);
                cmd.Parameters.AddWithValue("@Año_de_Publicación", lbr.Año_de_Publicación);
                cmd.Parameters.AddWithValue("@Editorial", lbr.Editorial);
                cmd.Parameters.AddWithValue("@Genero", lbr.Genero);
                cmd.Parameters.AddWithValue("@Paginas", lbr.Paginas);
                cmd.Parameters.AddWithValue("@ISBN", lbr.ISBN);
                cmd.Parameters.AddWithValue("@Alquilar", lbr.Alquilar);

                
                connection.Open();
                //se hace el insert en la base de datos
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al agregar el registro: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }


        public void ActualizarLibro(Libros lbr)
        {
            try
            {
                //se crea la consulta sql para actualizar registro
                string query = "UPDATE libros SET Titulo = @Titulo, Autor = @Autor, Año_de_Publicación = @Año_de_Publicación, Editorial = @Editorial, Genero = @Genero, Paginas = @Paginas, ISBN = @ISBN, Alquilar = @Alquilar WHERE ID = @ID";
                //comando para sql
                MySqlCommand cmd = new MySqlCommand(query, connection);
                //se seten los valores a actualizarse
                cmd.Parameters.AddWithValue("@ID", lbr.ID);
                cmd.Parameters.AddWithValue("@Titulo", lbr.Titulo);
                cmd.Parameters.AddWithValue("@Autor", lbr.Autor);
                cmd.Parameters.AddWithValue("@Año_de_Publicación", lbr.Año_de_Publicación);
                cmd.Parameters.AddWithValue("@Editorial", lbr.Editorial);
                cmd.Parameters.AddWithValue("@Genero", lbr.Genero);
                cmd.Parameters.AddWithValue("@Paginas", lbr.Paginas);
                cmd.Parameters.AddWithValue("@ISBN", lbr.ISBN);
                cmd.Parameters.AddWithValue("@Alquilar", lbr.Alquilar);

                connection.Open();
                //se ejecuta el comando
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar el registro: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }


        public void EliminarLibro(int id)
        {
            try
            {
                //creamos la sentencia SQL
                string query = "DELETE FROM libros WHERE ID = @ID";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                //se setea el valor para buscar el registro a eliminar
                cmd.Parameters.AddWithValue("@ID", id);

                connection.Open();
                //se ejecuta el comando sql
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar el registro: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }


        //metodo para listar

        public List<Libros> Verlibros()
        {
            List<Libros> libreria = new List<Libros>();
            //se crea sentencia para traer todos los registros
            string query = "SELECT * FROM libros";
            MySqlCommand cmd = new MySqlCommand(query, connection);

            try
            {
                connection.Open();
                //se crea objeto reader para leer todos los datos de la tabla
                MySqlDataReader reader = cmd.ExecuteReader();


                //asignamos los datos de la tabla al objeto libro 
                while (reader.Read())
                {
                    Libros libreria1 = new Libros
                    (
                        id: reader.GetInt32("ID"),
                        titulo: reader.GetString("Titulo"),
                        autor: reader.GetString("Autor"),
                        año_de_Publicación: reader.GetDateTime("Año_de_Publicación"),
                        editorial: reader.GetString("Editorial"),
                        genero: reader.GetString("Genero"),
                        paginas: reader.GetInt32("Paginas"),
                        isbn: reader.GetInt64("ISBN"),
                        alquilar: reader.GetBoolean("Alquilar")
                    );


                    //agregamos a la lista de libros cada libro.
                    libreria.Add(libreria1);
                }
                //se cierra la instancia del reader
                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            connection.Close();
            //se retorna la lista
            return libreria;
        }

    }
}
