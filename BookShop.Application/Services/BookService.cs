using BookShop.Domain.Entities;
using BookShop.Domain.Interfaces;

namespace BookShop.Application.Services
{
    public class BookService
    {
        private readonly IBookRepository _repository;

        public BookService(IBookRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Book>> GetAllBooks() => await _repository.GetAllAsync();
        public async Task<Book?> GetBookById(int id) => await _repository.GetByIdAsync(id);
        public async Task AddBook(Book book) => await _repository.AddAsync(book);
        public async Task UpdateBook(Book book) => await _repository.UpdateAsync(book);
        public async Task DeleteBook(int id) => await _repository.DeleteAsync(id);
    }
}