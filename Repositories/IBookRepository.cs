using BookAPI.Model;

namespace BookAPI.Repositories
{
    public interface IBookRepository
    {
        Task<IEnumerable<Book>> Get();
        Task<Book> GetBookById(int Id);
        Task<Book> Create(Book book);
        Task<Book> Update(Book book);
        Task Delete(int Id);


    }
}
