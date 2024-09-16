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
                Books = p.Books.Select(b => new BookWithAuthorDTO
                {
                    Title = b.Title,
                    Id = b.Id,
                    Author = new DTOAuthor
                    
                    {
                        Id = b.Author.Id,
                        FirstName = b.Author.FirstName,
                        LastName = b.Author.LastName
                    }
                }).ToList()
            }).ToList();
            return Results.Ok(DTOPublisher);

        }


        public static async Task<IResult> GetAPublisher(IPublisherRepository publisherRepository, int id)
        {
            var publisher = await publisherRepository.GetAPublisher(id);
            if (publisher == null)
            {
                return Results.NotFound();
            }

            var DTOPublisher = new DTOPublisher
            {
                Id = publisher.Id,
                Name = publisher.Name,
                Books = publisher.Books.Select(b => new BookWithAuthorDTO
                {
                    Id = b.Id,
                    Title = b.Title,
                    Author = new DTOAuthor
                    {
                        Id = b.Author.Id,
                        FirstName = b.Author.FirstName,
                        LastName = b.Author.LastName
                    }
                }).ToList(),
            };
            return Results.Ok(DTOPublisher);
                     
        }

       





    }
}

            

         