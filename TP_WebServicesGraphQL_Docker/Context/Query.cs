using TP_WebServicesGraphQL_Docker.Model;

namespace TP_WebServicesGraphQL_Docker.Context
{
    public class Query
    {
        public List<Game> GetGames([Service] ApiContext dbContext)
        {
            return dbContext.Games.ToList();
        }

        public List<Editor> GetEditors([Service] ApiContext dbContext)
        {
            return dbContext.Editors.ToList();
        }

        public List<Studio> GetStudios([Service] ApiContext dbContext)
        {
            return dbContext.Studios.ToList();
        }
    }
}
