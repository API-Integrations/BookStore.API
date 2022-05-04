using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BookStore.API.Data;
using BookStore.API.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;

namespace BookStore.API.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly BookStoreContext _context;
        private readonly IMapper _mapper;

        public BookRepository(BookStoreContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<BookModel>> GetAllBooksAsync()
        {
            //var records = await _context.Books.Select<Book, BookModel>(book => new BookModel
            //{
            //    Id = book.Id,
            //    Title = book.Title,
            //    Description = book.Description
            //}
            // ).ToListAsync();
            var records = await _context.Books.ToListAsync();
            return _mapper.Map<List<BookModel>>(records);
        }

        public async Task<BookModel> GetBookByIdAsync(int id)
        {
            //var book = await _context.Books.Where<Book>(book => book.Id == id)
            //    .Select<Book, BookModel>(book => new BookModel
            //    {
            //        Id = book.Id,
            //        Title = book.Title,
            //        Description = book.Description
            //    }).FirstOrDefaultAsync<BookModel>();
            var book = await _context.Books.FindAsync(id);
            return _mapper.Map<BookModel>(book);
        }

        public async Task<int> AddBookAsync(BookModel bookModel)
        {
            var book = new Book
            {
                Title = bookModel.Title,
                Description = bookModel.Description
            };
            _context.Books.Add(book);
            await _context.SaveChangesAsync();
            return book.Id;
        }

        public async Task UpdateBookAsync(int id, BookModel bookModel)
        {
            //var book = await _context.Books.FindAsync(id);
            //if (book is not default(Book))
            //{
            //    book.Title = bookModel.Title;
            //    book.Description = bookModel.Description;
            //    await _context.SaveChangesAsync();
            //}
            var book = new Book
            {
                Id = id,
                Title = bookModel.Title,
                Description = bookModel.Description
            };
            _context.Books.Update(book);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateBookPatchAsync(int id, JsonPatchDocument bookModel)
        {
            var book = await _context.Books.FindAsync(id);
            if (book is not default(Book))
            {
                bookModel.ApplyTo(book);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteBookAsync(int id)
        {
            var book = new Book { Id = id };
            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
        }
    }
}
