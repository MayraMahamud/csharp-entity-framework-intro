using exercise.webapi.DTO;
using exercise.webapi.Models;
using exercise.webapi.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using static System.Reflection.Metadata.BlobBuilder;

namespace exercise.webapi.Endpoints
{
    public static class PublisherApi
    {
        public static void ConfigurePublisherApi(this WebApplication app)
        {
            var publishers = app.MapGroup("publishers");

            publishers.MapGet("/", GetPublishers);
            publishers.MapGet("/{id}", GetAPublisher);




        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        public static async Task<IResult> GetPublishers(IPublisherRepository publisherRepository)
        {
            var publishers = await publisherRepository.GetPublishers();
            var DTOPublisher = publishers.Select(p => new DTOPublisher
            {
                Id = p.Id,
                Name = p.Name,
                books = p.Books.Select(b => new DTOBook
                {
                    Title = b.Title,
                    Id = b.Id,
                    AuthorName = $"{b.Author.FirstName} {b.Author.LastName}"
                }).ToList()
            });
            return Results.Ok(DTOPublisher);






            var publishers2 = await publisherRepository.GetPublishers();
            DTOPublisherResponse response = new DTOPublisherResponse();
            foreach (var publisher in publishers)
            {
                DTOPublisher p = new DTOPublisher();
                p.Name = $"{publisher.Name}";
                foreach (var b in publisher.Books)
                {
                    DTOBook book = new DTOBook();
                    book.Title = b.Title;
                    p.books.Add(book);
                }
                response.Publishers.Add(p);

            }

            return TypedResults.Ok(response);




        }


        public static async Task<IResult> GetAPublisher(IPublisherRepository publisherRepository, int id)
        {
            var publisher = await publisherRepository.GetAPublisher(id);
            if (publisher == null)
            {
                return Results.NotFound();
            }

            var DTOPublisher = new DTOPublisher();
            //{
            //    Id = publisher.Id;
            //    Name = publisher.Name;
            //    Books = publisher.Books.Select(b => new DTOBook)
            //    {
            //        Id = b.Id,
            //        Title = b.Title,
            //        AuthorName = $"{b.Author.FirstName} {b.Author.LastName}"}).ToList()

            //};
            return Results.Ok(DTOPublisher);
        }
    }
}

            

         