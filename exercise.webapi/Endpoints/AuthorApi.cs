using exercise.webapi.DTO;
using exercise.webapi.Models;
using exercise.webapi.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using static System.Reflection.Metadata.BlobBuilder;

namespace exercise.webapi.Endpoints
{
    public static class AuthorApi
    {
        public static void ConfigureAuthorApi(this WebApplication app)
        {
            var authors = app.MapGroup("authors");
           
            authors.MapGet("/", GetAuthors);
            authors.MapGet("/{id}", GetAnAuthor);
            authors.MapDelete("/removeauthor", RemoveAuthorFromBook);



        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        public static async Task<IResult> GetAuthors(IAuthorRepository authorRepository)
        {
            var authors = await authorRepository.GetAllAuthors();
            DTOAuthorResponse response = new DTOAuthorResponse();
            foreach (var author in authors) 
            {
                DTOAuthor a = new DTOAuthor();
                a.Id = author.Id;
                a.FirstName = $"{author.FirstName} ";
                a.LastName =  $"{author.LastName}";
                foreach (var b in author.Books) 
                {
                    DTOBook book = new DTOBook();
                    book.Title = b.Title;
                    //a.books.Add(book);
                }
                response.Authors.Add(a);    

            }

            return TypedResults.Ok(response);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        public static async Task<IResult> GetAnAuthor(IAuthorRepository authorRepository, int id)
        {
            var author = await authorRepository.GetAuthorById(id);
            if (author == null) 
            {
                return Results.NotFound();
            }

            DTOAuthorResponse authorResponse = new DTOAuthorResponse();
            DTOAuthor dTOAuthor = new DTOAuthor
            {
                Id = author.Id,
                FirstName = $"{author.FirstName}",
                LastName = $"{author.LastName}"

            };

            foreach (var b in author.Books )
            {
                DTOBook bBook = new DTOBook
                {
                    Title = b.Title,
                };
                //dTOAuthor.books.Add(bBook); 
            }
            authorResponse.Authors.Add(dTOAuthor);  
            return TypedResults.Ok(authorResponse);

        }

        public static async Task<IResult> RemoveAuthorFromBook(int bookID, int authorId, IBookRepository bookRepository, IAuthorRepository authorRepository)
        {
            var book = await bookRepository.GetById(bookID);
            if (book == null)
            {
                return Results.NotFound();
            }
            //book.AuthorId = null;
            await bookRepository.SaveChangesAsync();

            return TypedResults.Ok(book);

        }

        //public static async Task<IResult> GetAuthorById(IAuthorRepository authorRepository, int id)
        //{
        //    var author = await authorRepository.GetAuthorById(id);
        //    if (author == null)
        //    { return Results.NotFound(); }
        //    var authorDTO = new DTOAuthor
        //    {
        //        Id = author.Id,
        //        FirstName = author.FirstName,
        //        LastName = author.LastName,
        //        Books = author.Books.Select(b => new BookWithPublisherDTO
        //        {
        //            Title = b.Title,
        //            Id = b.Id,
        //            Publisher = new DTOPublisher
        //            {
        //                Id = b.PublisherId,
        //                Name = b.Publisher.Name
        //            }
        //        }).ToList()
        //    };
        //    return Results.Ok(authorDTO);
        //}










    }


}
