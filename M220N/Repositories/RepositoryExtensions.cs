using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace M220N.Repositories
{
    public static class RepositoryExtensions
    {
        public static void RegisterMongoDbRepositories(this IServiceCollection servicesBuilder)
        {
            servicesBuilder.AddSingleton<IMongoClient, MongoClient>(s =>
            {
                var uri = s.GetRequiredService<IConfiguration>()["MongoUri"];
                var settings = MongoClientSettings.FromConnectionString(uri);
                settings.MaxConnectionPoolSize = 50;

                return new MongoClient(settings);
            });
            servicesBuilder.AddSingleton<MoviesRepository>();
            servicesBuilder.AddSingleton<UsersRepository>();
            servicesBuilder.AddSingleton<CommentsRepository>();
            servicesBuilder.AddSingleton(s => s.GetRequiredService<IConfiguration>()["JWT_SECRET"]);
        }
    }
}
