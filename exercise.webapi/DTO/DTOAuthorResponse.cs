using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace exercise.webapi.DTO
{
    public class DTOAuthorResponse
    {
        public List<DTOAuthor> Authors { get; set;   } = new List<DTOAuthor>();  
    }
}
