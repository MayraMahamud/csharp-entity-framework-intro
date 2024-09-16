using exercise.webapi.Models;
using exercise.webapi.Repository;
using exercise.webapi.DTO;
using exercise.webapi.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using static System.Reflection.Metadata.BlobBuilder;
using NpgsqlTypes;

namespace exercise.webapi.Endpoints
{
    public static class BookApi
    {
        public static void ConfigureBooksApi(this WebApplication app)
        {
            var books = app.MapGroup("books");
            books.MapGet("/", GetAllBooks);
            books.MapGet("/aBook", GetABook);
            books.MapPut("/", UpdateABook);
            books.MapPost("/", CreateBook);
            books.MapDelete("/", DeleteBook);
            books.MapPost("/assignauthor", AssignAuthorToBook);
        }


        public static async Task<IResult> GetABook(IBookRepository bookRepository, int id)
        {
            var book = await bookRepository.GetById(id);
            if (book == null)
            {
                return Results.NotFound();
            }

            DTOBookResponse bookResponse = new DTOBookResponse();
            DTOBook dTOBook = new DTOBook
            {
                Title = $"{book.Title}",
               
                Id = book.AuthorId


            };

            return TypedResults.Ok(book);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        public static async Task<IResult> GetAllBooks(IBookRepository bookRepository)
        {
            var books = await bookRepository.GetAllBooks();
            DTOBookResponse response = new DTOBookResponse();
            foreach (var book in books)
            {
                DTOBook b = new DTOBook();
                b.Id = book.Id;
                b.Title = book.Title;
                b.PublisherId = book.PublisherId;
                b.AuthorName = $"{book.Author.FirstName} {book.Author.LastName}";
                b.Publisher = book.Publisher.Name;

              
                response.Books.Add(b);

            }
            return TypedResults.Ok(response);

        }




        public static async Task<IResult> UpdateABook(IBookRepository bookRepository, int bookId, int newAuthorId, IAuthorRepository authorRepository)
          {
            var book = await bookRepository.GetById(bookId);
            if (book == null)
            {
                return Results.NotFound();
            }
            var author = await authorRepository.GetAuthorById(newAuthorId);
            book.AuthorId = newAuthorId;
            await bookRepository.UpdateBook(book);
            DTOBook dTOBook = new DTOBook()
            {
                Title = book.Title,
                AuthorName = $"{author.FirstName}{author.LastName}"
            };
            DTOBookResponse response = new DTOBookResponse();
            response.Books.Add(dTOBook);
            return TypedResults.Ok(response);




        }

        public static async Task<IResult> CreateBook(BookCreateDTO bookCreateDTO, IBookRepository bookRepository, IAuthorRepository authorRepository)
        {
            if(string .IsNullOrWhiteSpace(bookCreateDTO.Title) || bookCreateDTO.AuthorId == 0) 
            {
                return Results.BadRequest();
            }
          
                
                var author = await authorRepository.GetAuthorById(bookCreateDTO.AuthorId);
            if(author == null)
            {

            return Results.NotFound(); 
            }
            Book newBook = new Book
            {
                Title = bookCreateDTO.Title,
                AuthorId = bookCreateDTO.AuthorId,
            };
            await bookRepository.CreateBook(newBook);
            DTOBook dTOBook1 = new DTOBook()
            {
                Title = newBook.Title,
                AuthorName = $"{author.FirstName} {author.LastName}",
            };
            return TypedResults.Created($"{newBook.Id}", dTOBook1);



        }


        public static async Task<IResult> DeleteBook(int bookID, IBookRepository bookRepository)
        {
            var book = await bookRepository.GetById(bookID);
            if (book == null)
            {
                return Results.NotFound();
            }
            await bookRepository.DeleteBook(book);
            return Results.NoContent();
        }


        public static async Task<IResult> AssignAuthorToBook(int bookID,int authorId, IBookRepository bookRepository, IAuthorRepository authorRepository)
        {
            var book = await bookRepository.GetById(bookID);
            if(book == null)
            {
                return Results.NotFound();
            }

            var author = await authorRepository.GetAuthorById(authorId);
            if (author == null) 
            { 
                return Results.NotFound();
            }

            book.AuthorId = authorId;
            await bookRepository.SaveChangesAsync();
            return TypedResults.Ok();


        }


        public static async Task<IResult> GetBookWithAuthorAndPublisher(IBookRepository bookRepository, int id)
        {
            var book = await bookRepository.GetBookWithAuthorAndPublisher(id);
            if (book == null)
            { return Results.NotFound(); }
            var bookDTO = new BookWithAuthorAndPublisherDTO
            {
                Id = book.Id,
                Title = book.Title,
                Author = new DTOAuthor
                {
                    Id = book.Author.Id,
                    FirstName = book.Author.FirstName,
                    LastName = book.Author.LastName

                },
                Publisher = new DTOPublisher
                {
                    Id = book.Publisher.Id,
                    Name = book.Publisher.Name
                }
            };

            return Results.Ok(bookDTO);
        }



    }
}


     

