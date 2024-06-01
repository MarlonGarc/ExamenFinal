using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace ExamenFinal.Clase.Models
{
    internal class Libros
    {
        public int ID { get; set; }
        public string Titulo { get; set; }
        public string Autor { get; set; }
        public DateTime Año_de_Publicación { get; set; }
        public string Editorial { get; set; }
        public string Genero { get; set; }
        public int Paginas { get; set; }
        public long ISBN { get; set; }
        public bool Alquilar { get; set; }

        //Constructor con Parametros
        public Libros(int id, string titulo, string autor, DateTime año_de_Publicación, string editorial, string genero, int paginas, long isbn, bool alquilar)
        {
            ID = id;
            Titulo = titulo;
            Autor = autor;
            Año_de_Publicación = año_de_Publicación;
            Editorial = editorial;
            Genero = genero;
            Paginas = paginas;
            ISBN = isbn;
            Alquilar = false;
        }

        //Costructor sin Parametros
        public Libros()
        {
        }
    }
}
