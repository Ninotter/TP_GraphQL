using TP_WebServicesGraphQL_Docker.Model;

namespace TP_WebServicesGraphQL_Docker.Context
{
    public class Query
    {
        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public List<Game> GetGames([Service] ApiContext dbContext)
        {
            return dbContext.Games.ToList();
        }

        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public List<Editor> GetEditors([Service] ApiContext dbContext)
        {
            return dbContext.Editors.ToList();
        }

        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public List<Studio> GetStudios([Service] ApiContext dbContext)
        {
            return dbContext.Studios.ToList();
        }
    }
}
