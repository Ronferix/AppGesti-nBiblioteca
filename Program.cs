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
            
            if (!string.IsNullOrWhiteSpace(getYear) && int.TryParse(getYear, out year))
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

    public static void EditBook(Book bookToEdit)
    {
        while (true) { 
            Console.WriteLine("Select one option to edit (use numbers to select the option):\n" +
                "1. Title.\n" +
                "2. Author.\n" +
                "3. Year.\n" +
                "4. Description.\n" +
                "5. Copies.\n" +
                "6. Exit editor.\n");

            int selection;
            while (true)
            {
                Console.Write("Option: ");
                string getSelection = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(getSelection) && int.TryParse(getSelection, out selection))
                {
                    break;
                }
                Console.WriteLine("Error! incorrect input, use numbers");
            }

            string input;
            int inputToInt;
            switch (selection){
                case 1:
                    Console.WriteLine($"The actual title is: {bookToEdit.Title}\nEnter the new title: ");
                    input = Console.ReadLine();
                    bookToEdit.Title = input;
                    input = "";
                    break;
                case 2:
                    Console.WriteLine($"The actual author is: {bookToEdit.Author}\nEnter the new author: ");
                    input = Console.ReadLine();
                    bookToEdit.Author = input;
                    input = "";
                    break;
                case 3:
                    while (true)
                    {
                        Console.Write($"The actual year is: {bookToEdit.YearOfPublication}\nEnter the new year (use numbers): ");
                        input = Console.ReadLine();
                        if (!string.IsNullOrWhiteSpace(input) && int.TryParse(input, out inputToInt))
                        {
                            break;
                        }

                        Console.WriteLine("Error! incorrect input, use numbers");
                    }
                    bookToEdit.YearOfPublication = inputToInt;
                    input = "";
                    inputToInt = 0;
                    break;
                case 4:
                    Console.WriteLine($"The actual description is: {bookToEdit.Description}\nEnter the new description: ");
                    input = Console.ReadLine();
                    bookToEdit.Description = input;
                    input = "";
                    break;
                case 5:
                    while (true)
                    {
                        Console.WriteLine($"The actual number of copies is: {bookToEdit.Copies}\nEnter the new number of copies (use numbers): ");
                        input = Console.ReadLine();
                        if (!string.IsNullOrWhiteSpace(input) && int.TryParse(input, out inputToInt))
                        {
                            break;
                        }

                        Console.WriteLine("Error! incorrect input, use numbers");
                    }
                    bookToEdit.Copies = inputToInt;
                    input = "";
                    inputToInt = 0;
                    break;
                case 6:
                    Console.WriteLine("Ending edition...");
                    return;
                default:
                    Console.WriteLine("Unknown ERROR - Ending edition...");
                    return;
            }
        }
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

