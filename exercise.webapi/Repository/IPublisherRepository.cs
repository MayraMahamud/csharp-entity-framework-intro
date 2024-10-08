﻿using exercise.webapi.Models;

namespace exercise.webapi.Repository
{
    public interface IPublisherRepository
    {
        public Task<List<Publisher>> GetPublishers();
        Task<Publisher> GetAPublisher(int id);
       
    }
}