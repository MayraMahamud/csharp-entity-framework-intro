﻿namespace exercise.webapi.DTO
{
    public class DTOPublisher
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<DTOBook> books { get; set; } 
    }
}
