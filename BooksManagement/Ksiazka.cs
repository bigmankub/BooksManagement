using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksManagement
{
    public class Ksiazka
    {
        public int Id { get; set; }
        public string? Tytul { get; set; }
        public string? Autor { get; set; }
        public int RokWydania { get; set; }
        public string? Gatunek { get; set; }
    }
}
