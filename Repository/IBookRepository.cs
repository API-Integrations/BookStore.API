using System.Collections.Generic;
using System.Threading.Tasks;
using BookStore.API.Models;
using Microsoft.AspNetCore.JsonPatch;

namespace BookStore.API.Repository
{
    public interface IBookRepository
    {
        Task<List<BookModel>> GetAllBooksAsync();
        Task<BookModel> GetBookByIdAsync(int id);
        Task<int> AddBookAsync(BookModel book);
        Task UpdateBookAsync(int id, BookModel book);
        Task UpdateBookPatchAsync(int id, JsonPatchDocument document);
        Task DeleteBookAsync(int id);
    }
}
