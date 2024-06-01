using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ExamenFinal.Clase;
using ExamenFinal.Clase.Models;
using Org.BouncyCastle.Pqc.Crypto.Lms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using Cursor = ExamenFinal.Clase.Models.Cursor;

namespace ExamenFinal
{
    public partial class Form1 : Form
    {
        ConeccionMySQL cargar = new ConeccionMySQL();
        List<Libros> TodosLibros = new List<Libros>();
        Cursor next = new Cursor();
        
        public Form1()
        {
            InitializeComponent();
        }

        private void LimpiarCampos()
        {
            textBoxTitulo.Clear();
            textBoxAutor.Clear();
            textBoxEditorial.Clear();
            textBoxGenero.Clear();
            textBoxPaginas.Clear();
            textBoxISBN.Clear();
            checkBoxAlquilar.Checked = false;
            dateTimePickerAnioPublicacion.Value = DateTime.Now;
        }

        private void buttonVer_Click(object sender, EventArgs e)
        {
            TodosLibros = cargar.Verlibros();
            dataGridView1.DataSource = TodosLibros;
        
            //verificador de registros 
            if (TodosLibros.Count > 0)
            {
                next.totalRegistros = TodosLibros.Count;
                MessageBox.Show("Total de Registros: " + next.totalRegistros);
            }
            else
            {
                MessageBox.Show("No hay registros");
            }
        }
        

        private void MostrarRegistrosSiguiente()
        {
            if (next.current >= 0 && next.current < next.totalRegistros)
            {
                Libros L = TodosLibros[next.current];
                textBoxID.Text = L.ID.ToString();
                textBoxTitulo.Text = L.Titulo;
                textBoxAutor.Text = L.Autor;
                dateTimePickerAnioPublicacion.Value = L.Año_de_Publicación;
                textBoxEditorial.Text = L.Editorial;
                textBoxGenero.Text = L.Genero;
                textBoxPaginas.Text = L.Paginas.ToString();
                textBoxISBN.Text = L.ISBN.ToString();
                checkBoxAlquilar.Checked = L.Alquilar;

                //Es un Contador y valida que no se pase del total de registros
                next.current++;
                if (next.current >= next.totalRegistros)
                {
                    next.current = 0;
                    MessageBox.Show("Fin de los registros");
                }
            }
        }

        private void MostrarRegistrosAnterior()
        {
            if (next.current >= 0 && next.current < next.totalRegistros)
            {
                Libros L = TodosLibros[next.current];
                textBoxID.Text = L.ID.ToString();
                textBoxTitulo.Text = L.Titulo;
                textBoxAutor.Text = L.Autor;
                dateTimePickerAnioPublicacion.Value = L.Año_de_Publicación;
                textBoxEditorial.Text = L.Editorial;
                textBoxGenero.Text = L.Genero;
                textBoxPaginas.Text = L.Paginas.ToString();
                textBoxISBN.Text = L.ISBN.ToString();
                checkBoxAlquilar.Checked = L.Alquilar;

                //Es un Contador y valida que no se pase del total de registros
                next.current--;
                if (next.current >= next.totalRegistros)
                {
                    next.current = 0;
                    MessageBox.Show("Fin de los registros");
                }
            }
        }
        private void buttonSiguiente_Click(object sender, EventArgs e)
        {
            MostrarRegistrosSiguiente();
        }

        private void buttonAnterior_Click(object sender, EventArgs e)
        {
            MostrarRegistrosAnterior();
        }

        private void buttonAgregar_Click(object sender, EventArgs e)
        {
            // Verificamos que no hayan campos vacios
            if (string.IsNullOrWhiteSpace(textBoxTitulo.Text) ||
                string.IsNullOrWhiteSpace(textBoxAutor.Text) ||
                string.IsNullOrWhiteSpace(textBoxEditorial.Text) ||
                string.IsNullOrWhiteSpace(textBoxGenero.Text) ||
                string.IsNullOrWhiteSpace(textBoxPaginas.Text) ||
                string.IsNullOrWhiteSpace(textBoxISBN.Text))
            {
                MessageBox.Show("Debe llenar todos los espacios para agregar un nuevo libro.");
                return;
            }
            try
            {
                // Creo un Objeto Nuevo para Agregar el Libro
                Libros nuevoLibro = new Libros
                {
                    Titulo = textBoxTitulo.Text,
                    Autor = textBoxAutor.Text,
                    Año_de_Publicación = dateTimePickerAnioPublicacion.Value,
                    Editorial = textBoxEditorial.Text,
                    Genero = textBoxGenero.Text,
                    Paginas = int.Parse(textBoxPaginas.Text),
                    ISBN = long.Parse(textBoxISBN.Text),
                    Alquilar = checkBoxAlquilar.Checked
                };

                // Insertar el nuevo libro en la base de datos
                cargar.InsertarLibros(nuevoLibro);

                // Limpiar los campos después de la inserción
                LimpiarCampos();

                MessageBox.Show("Libro agregado con éxito.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al agregar el libro: " + ex.Message);
            }
        }

        private void buttonActualizar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxTitulo.Text) ||
            string.IsNullOrWhiteSpace(textBoxAutor.Text) ||
            string.IsNullOrWhiteSpace(textBoxEditorial.Text) ||
            string.IsNullOrWhiteSpace(textBoxGenero.Text) ||
            string.IsNullOrWhiteSpace(textBoxPaginas.Text) ||
            string.IsNullOrWhiteSpace(textBoxISBN.Text))
            {
                MessageBox.Show("Todos los campos deben estar llenos.");
                return;
            }

            if (MessageBox.Show("¿Estás seguro de que deseas actualizar este libro?", "Confirmar Actualización", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    // Crear un objeto Libro con los datos actualizados
                    Libros libroActualizado = new Libros
                    {
                        ID = int.Parse(textBoxID.Text),
                        Titulo = textBoxTitulo.Text,
                        Autor = textBoxAutor.Text,
                        Año_de_Publicación = dateTimePickerAnioPublicacion.Value,
                        Editorial = textBoxEditorial.Text,
                        Genero = textBoxGenero.Text,
                        Paginas = int.Parse(textBoxPaginas.Text),
                        ISBN = long.Parse(textBoxISBN.Text),
                        Alquilar = checkBoxAlquilar.Checked
                    };

                    // Actualizar el libro en la base de datos
                    cargar.ActualizarLibro(libroActualizado);

                    // Limpiar los campos después de la actualización
                    LimpiarCampos();

                    MessageBox.Show("Usted ha Actualizado el Libro, Gracias por la Actualizacion!!.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al actualizar el libro: " + ex.Message);
                }
            }
        }

        private void buttonEliminar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxID.Text))
            {
                MessageBox.Show("Debe seleccionar un libro para eliminar.");
                return;
            }

            if (MessageBox.Show("¿Estás seguro de que deseas eliminar este libro?", "Confirmar Eliminación", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    int libroID = int.Parse(textBoxID.Text);

                    // Eliminar el libro de la base de datos
                    cargar.EliminarLibro(libroID);

                    // Limpiar los campos después de la eliminación
                    LimpiarCampos();

                    MessageBox.Show("Libro eliminado con éxito.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al eliminar el libro: " + ex.Message);
                }
            }
        }
    }
}

