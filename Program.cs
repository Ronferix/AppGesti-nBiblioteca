﻿using System;
using System.Numerics;
using System.Runtime.InteropServices;

class App
{
    static void Main()
    {
        var library = new Library();
        library.AddBook();
        var menus = new Menus();
        int mainMenu = menus.showMainMenu();
        int bookMenu = menus.showBookMenu();
        int userMenu = menus.showUserMenu();
    }
}

class Library
{
    List<Book> booksInventory = new List<Book>();
    int IdForNextBook = 1;
    Queue<int> availableIDs = new Queue<int>();

    public void AddBook()
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

    public void RemoveBook()
    {

    }

    public void EditBook(Book bookToEdit, int selection)
    {
        while (true) 
        { 
            Console.WriteLine("Select one option to edit (use numbers to select the option):\n" +
                "1. Title.\n" +
                "2. Author.\n" +
                "3. Year.\n" +
                "4. Description.\n" +
                "5. Copies.\n" +
                "6. Exit editor.\n");

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

            switch (selection){
                case 1:
                    EditTitle(bookToEdit);
                    break;
                case 2:
                    EditAuthor(bookToEdit);
                    break;
                case 3:
                    EditYear(bookToEdit);
                    break;
                case 4:
                    EditDescription(bookToEdit);
                    break;
                case 5:
                    EditCopies(bookToEdit);
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

    private void EditTitle(Book bookToEdit)
    {
        Console.WriteLine($"The actual title is: {bookToEdit.Title}\nEnter the new title: ");
        bookToEdit.Title = Console.ReadLine();
    }

    private void EditAuthor(Book bookToEdit)
    {
        Console.WriteLine($"The actual author is: {bookToEdit.Author}\nEnter the new author: ");
        bookToEdit.Author = Console.ReadLine();
    }

    private void EditDescription(Book bookToEdit)
    {
        Console.WriteLine($"The actual description is: {bookToEdit.Description}\nEnter the new description: ");
        string input = Console.ReadLine();
        bookToEdit.Title = input;
    }

    private void EditYear(Book bookToEdit)
    {
        while (true)
        {
            Console.Write($"The actual year is: {bookToEdit.YearOfPublication}\nEnter the new year (use numbers): ");
            var input = Console.ReadLine();
            
            if (!string.IsNullOrWhiteSpace(input) && int.TryParse(input, out int inputToInt)) 
            {
                bookToEdit.YearOfPublication = inputToInt;
                return;
            }
            Console.WriteLine("Error! incorrect input, use numbers");
        }
        
    }

    private void EditCopies(Book bookToEdit)
    {
        while (true)
        {
            Console.Write($"The actual number of copies is: {bookToEdit.Copies}\nEnter the new number of copies (use numbers): ");
            var input = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(input) && int.TryParse(input, out int inputToInt))
            {
                bookToEdit.Copies = inputToInt;
                return;
            }
            Console.WriteLine("Error! incorrect input, use numbers");
        }

    }

    List<string> GetGenresFromUser()
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

class Menus
{
    public int showMainMenu()
    {
        while (true)
        {
            Console.WriteLine($"Welcome to the library app!\n-----------------------------------\n" +
            $"Select one of the following options:\n" +
            $"1 - Books\n" +
            $"2 - Users\n" +
            $"3 - Exit");

            var input = Console.ReadLine();

            if ((!string.IsNullOrWhiteSpace(input) && int.TryParse(input, out int inputToInt)) && (inputToInt > 0 && inputToInt < 4))
            {
                return inputToInt;
            }

            Console.WriteLine("Error! incorrect input, use numbers");
        }
    }

    public int showBookMenu()
    {
        while (true)
        {
            Console.WriteLine($"Welcome to the library app!\n-----------------------------------\n" +
            $"Select one of the following options:\n" +
            $"1 - Add book\n" +
            $"2 - Edit book\n" +
            $"3 - Delete book\n" +
            $"4 - Search book\n" +
            $"5 - Return to main menu\n" +
            $"6 - Exit");

            var input = Console.ReadLine();

            if ((!string.IsNullOrWhiteSpace(input) && int.TryParse(input, out int inputToInt)) && (inputToInt > 0 && inputToInt < 7))
            {
                return inputToInt;
            }

            Console.WriteLine("Error! incorrect input, use numbers or select a valid option");
        }
    }

    public int showUserMenu()
    {
        while (true)
        {
            Console.WriteLine($"Welcome to the library app!\n-----------------------------------\n" +
            $"Select one of the following options:\n" +
            $"1 - Add user\n" +
            $"2 - Edit user\n" +
            $"3 - Delete user\n" +
            $"4 - Search user\n" +
            $"5 - Return to main menu\n" +
            $"6 - Exit");

            var input = Console.ReadLine();

            if ((!string.IsNullOrWhiteSpace(input) && int.TryParse(input, out int inputToInt)) && (inputToInt > 0 && inputToInt < 7))
            {
                return inputToInt;
            }

            Console.WriteLine("Error! incorrect input, use numbers or select a valid option");
        }
    }  
}