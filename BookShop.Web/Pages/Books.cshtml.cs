using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using BookShop.Domain.Entities;
using BookShop.Application.Services;
using System.Collections.Generic;

namespace BookShop.Web.Pages
{
    public class BooksModel : PageModel
    {
        private readonly BookService _bookService;
        private readonly ILogger<BooksModel> _logger;

        [BindProperty]
        public Book NewBook { get; set; } = new();

        [BindProperty]
        public Book EditBook { get; set; } = new();

        public IEnumerable<Book> Books { get; set; } = new List<Book>();

        public BooksModel(BookService bookService, ILogger<BooksModel> logger)
        {
            _bookService = bookService;
            _logger = logger;
        }

        public async Task OnGetAsync()
        {
            try
            {
                _logger.LogInformation("Fetching all books from database");
                Books = await _bookService.GetAllBooks();
                _logger.LogInformation($"Successfully retrieved {Books.Count()} books");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching books");
                throw;
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                _logger.LogInformation($"Adding new book: {NewBook.Title}");
                await _bookService.AddBook(NewBook);
                _logger.LogInformation("Book added successfully");
                return RedirectToPage();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while adding the book");
                ModelState.AddModelError("", "An error occurred while adding the book. Please try again.");
                Books = await _bookService.GetAllBooks();
                return Page();
            }
        }

        public async Task<IActionResult> OnPostEditAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                _logger.LogInformation($"Updating book: {EditBook.Title}");
                await _bookService.UpdateBook(EditBook);
                _logger.LogInformation("Book updated successfully");
                return RedirectToPage();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating the book");
                ModelState.AddModelError("", "An error occurred while updating the book. Please try again.");
                Books = await _bookService.GetAllBooks();
                return Page();
            }
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            try
            {
                _logger.LogInformation($"Deleting book with ID: {id}");
                await _bookService.DeleteBook(id);
                _logger.LogInformation("Book deleted successfully");
                return RedirectToPage();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting the book");
                ModelState.AddModelError("", "An error occurred while deleting the book. Please try again.");
                Books = await _bookService.GetAllBooks();
                return Page();
            }
        }

        public async Task<IActionResult> OnGetEditAsync(int id)
        {
            var book = await _bookService.GetBookById(id);
            if (book == null)
            {
                return NotFound();
            }
            EditBook = book;
            return Partial("_EditBookPartial", EditBook);
        }
    }
}