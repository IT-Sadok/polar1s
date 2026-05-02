using Booker.Application.Commands;
using Booker.Domain.Entity;
using Booker.Infrastructure.Persistence;

const string FILEPATH = "books.json";

List<Book> books = JsonFileManager.ReadFromJson(FILEPATH);
string? menuChoice;

do
{
    DisplayMenu();
    menuChoice = Console.ReadLine()?.Trim();

    bool mutated = false;

    switch (menuChoice)
    {
        case "1":
            mutated = new AddBookCommand().Execute(books);
            break;
        case "2":
            mutated = new DeleteBookCommand().Execute(books);
            break;
        case "3":
            PrintBooks(new SearchBookCommand().Execute(books));
            break;
        case "4":
            PrintBooks(new ViewAllBooksCommand().Execute(books));
            break;
        case "5":
            mutated = new BorrowBookCommand().Execute(books);
            break;
        case "6":
            mutated = new ReturnBookCommand().Execute(books);
            break;
        case "7":
            Console.WriteLine("Goodbye!");
            break;
        default:
            Console.WriteLine("Invalid option. Try again.");
            break;
    }

    if (mutated)
    {
        JsonFileManager.WriteToJson(books, FILEPATH);
    }

    Console.WriteLine();
}
while (menuChoice != "7");

void DisplayMenu()
{
    Console.WriteLine("=== Booker ===");
    Console.WriteLine("1. Add Book");
    Console.WriteLine("2. Delete Book");
    Console.WriteLine("3. Search Book");
    Console.WriteLine("4. View All Books");
    Console.WriteLine("5. Borrow a Book");
    Console.WriteLine("6. Return a Book");
    Console.WriteLine("7. Exit");
    Console.Write("Choose an option: ");
}

void PrintBooks(List<Book> bookList)
{
    if (bookList.Count == 0)
    {
        Console.WriteLine("No books found.");
        return;
    }

    foreach (var book in bookList)
    {
        Console.WriteLine(book);
    }
}
