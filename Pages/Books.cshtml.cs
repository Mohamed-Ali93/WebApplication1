using Microsoft.AspNetCore.Mvc.RazorPages;
using BookShop.Application.Services;
using BookShop.Domain.Entities;
using System.Collections.Generic;

public class BooksModel : PageModel
{
    private readonly BookService _bookService;

    public BooksModel(BookService bookService)
    {
        _bookService = bookService;
    }

    public List<Book> Books { get; set; }

    public void OnGet()
    {
        Books = _bookService.GetAllBooks();
    }
}