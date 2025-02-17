using System;
using System.Numerics;

public class Book
{
    public string Title { get; set; }
    public string Description { get; set; }
    public string Author { get; set; }
    public int YearOfPublication { get; set; }
    public int Copies { get; set; }
    public int AvailableCopies { get; set; }
    public int ID { get; set; }
    public List<string> Genres { get; set; } = new List<string>();
    public List<string> UsersWithCopies { get; set; } = new List<string>();

    public Book(string title, string description, string author, int yearOfPublication, int id, int copies, int availableCopies, List<string> genres, List<string> usersWithCopies)
    {
        Title = title; Description = description; Author = author;
        YearOfPublication = yearOfPublication; AvailableCopies = availableCopies; Copies = copies;
        Genres = genres; UsersWithCopies = usersWithCopies;

    }

    public Book()
    {

    }


    public void ShowBookInfo()
    {
        Console.WriteLine($@"Book Information:\n" +
            $"Title: {Title}\n" +
            $"Author: {Author}\n" +
            $"Year: {YearOfPublication}\n" +
            $"Description: {Description}\n" +
            $"Genres: {Genres}" +
            $"Copies: {Copies}\n" +
            $"Available Copies: {AvailableCopies}" +
            $"Users with copies: {UsersWithCopies}" +
            $"ID: {ID}");
    }

}

class AppGestiónBiblioteca
{
    static List<Book> booksInventory = new List<Book>();
    static int IdForNextBook = 1;
    static Queue<int> availableIDs = new Queue<int>();

    static void Main()
    {
        AddBook();
    }
    
    public static void AddBook()
    {
        Console.Write("Enter book title: ");
        string title = Console.ReadLine();
        Console.Write("Enter author name: ");
        string author = Console.ReadLine();
        int year;
        
        while (true)
        {
            Console.Write("Enter year of publication (use numbers): ");
            string getYear = Console.ReadLine();
            
            if (int.TryParse(getYear, out year))
            {
                break;
            }

            Console.WriteLine("Error! incorrect input, use numbers");
        }

        Console.Write("Enter Book description: ");
        string description = Console.ReadLine();
        Console.Write("Enter Book genres: ");
        List <string> genres = GetGenresFromUser();
        int id = (availableIDs.Count > 0) ? availableIDs.Dequeue() : IdForNextBook++;

        int copies;
        while (true)
        {
            Console.Write("Enter the number of copies (use numbers): ");
            string getCopies = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(getCopies) && int.TryParse(getCopies, out copies))
            {
                break;
            }

            Console.WriteLine("Error! incorrect input, use numbers");
        }

        Book newBook = new Book();
        newBook.Title = title;
        newBook.Author = author;
        newBook.Description = description;
        newBook.YearOfPublication = year;
        newBook.Copies = copies;
        newBook.AvailableCopies = copies;
        newBook.Genres = genres;
        newBook.ID = id;
        booksInventory.Append(newBook);
    }

    public static void RemoveBook()
    {

    }

    public static void EditBook()
    {

    }

    static List<string> GetGenresFromUser()
    {
        List<string> genres = new List<string>();

        while (true)
        {
            Console.Write("Enter a genre (or type 'done' to finish): ");
            string genre = Console.ReadLine();

            if (genre.ToLower() == "done")
            {
                break;
            }

            if (!string.IsNullOrWhiteSpace(genre))
            {
                genres.Add(genre);
            }
            else
            {
                Console.WriteLine("Genre cannot be empty. Try again.");
            }
        }

        return genres;
    }
}

