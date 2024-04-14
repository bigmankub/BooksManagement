using System.Linq.Expressions;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace BooksManagement
{
    public class Program
    {
        static void Main()
        {
            string path = "ksiazki.json";
            List<Ksiazka> ksiazki = new List<Ksiazka>();
            ksiazki = JsonSerializer.Deserialize<List<Ksiazka>>(File.ReadAllText(path));
            InitializeMenu();

            bool InitializeMenu()
            {
                Console.Clear();
                Console.WriteLine("Biblioteka menu:");
                string[] menu = { "Dodaj książkę", "Usuń książkę", "Wyświetl książki", "Zakmnij program" };
                int menuNumber = 1;
                foreach (var item in menu)
                {
                    Console.WriteLine($"{menuNumber++} - {item}");
                }
                int choice = Convert.ToInt16(Console.ReadLine());
                switch(choice)
                {
                    case 1:
                        AddBook();
                        break;
                    case 2:
                        DeleteBook();
                        break;
                    case 3:
                        LoadBooks();
                        break;
                    case 4:
                        break;
                    default:
                        break;
                }
                return true;
            }


            void LoadBooks()
            {
                Console.WriteLine("Podaj tytuł książki: (Brak odpowiedzi wyświetla wszystkie)");
                string titleInput = Console.ReadLine();
                if (titleInput == "") {
                    int temp = ksiazki.Count();
                    Console.Clear();
                    foreach (var k in ksiazki)
                    {
                        temp--;
                        Console.WriteLine($"ID: {k.Id}");
                        Console.WriteLine($"Tytul: {k.Tytul}");
                        Console.WriteLine($"Autor: {k.Autor}");
                        Console.WriteLine($"Rok Wydania: {k.RokWydania}");
                        Console.WriteLine($"Gatunek: {k.Gatunek}");
                        if(temp > 0)
                            Console.WriteLine("----------------------------");
                    }
                } else {
                    Console.Clear();
                    foreach (var k in ksiazki)
                    {
                        if (titleInput == k.Tytul)
                        {
                            Console.WriteLine($"ID: {k.Id}");
                            Console.WriteLine($"Tytul: {k.Tytul}");
                            Console.WriteLine($"Autor: {k.Autor}");
                            Console.WriteLine($"Rok Wydania: {k.RokWydania}");
                            Console.WriteLine($"Gatunek: {k.Gatunek}");
                        }
                    }
                }
                Console.ReadKey();
                InitializeMenu();
            }


            void AddBook()
            {

                Console.WriteLine("Podaj nazwę książki:");
                string title = Console.ReadLine();
                Console.WriteLine("Podaj autora książki:");
                string author = Console.ReadLine();
                Console.WriteLine("Podaj rok wydania:");
                int year = 0;
                try { year = Convert.ToInt32(Console.ReadLine()); }
                catch (Exception ex) { Console.WriteLine(ex.Message); }
                Console.WriteLine("Podaj gatunek książki:");
                string genre = Console.ReadLine();
                try
                {
                    ksiazki.Add(new Ksiazka
                    {
                        Id = ksiazki.Count() + 1,
                        Tytul = title,
                        Autor = author,
                        RokWydania = year,
                        Gatunek = genre
                    });
                    File.WriteAllText(path, JsonSerializer.Serialize(ksiazki));
                    Console.WriteLine("Książka dodana");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Nie udało się dodać książki: {ex.Message}");
                }
                Console.ReadKey();
                InitializeMenu();
            }

            void DeleteBook()
            {
                Console.WriteLine("Podaj ID książki, która ma być usunięta:");
                int deleteID = Convert.ToInt32(Console.ReadLine());

                ksiazki.Remove(ksiazki.Where(ksiazka => ksiazka.Id == deleteID).First());
                ksiazki.ForEach(id => { id.Id = ksiazki.IndexOf(id) + 1; });

                File.WriteAllText(path, JsonSerializer.Serialize(ksiazki));
                ksiazki = JsonSerializer.Deserialize<List<Ksiazka>>(File.ReadAllText(path));
                Console.WriteLine("Książka usunięta");
                Console.ReadKey();
                InitializeMenu();
            }


        }
    }

}