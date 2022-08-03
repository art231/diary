using Diary.Infrastructure.ApplicationDbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace Diary.Api.Extensions
{
    public static class ServiceRegistryExtensions
    {
        public static readonly ILoggerFactory MyLoggerFactory = LoggerFactory.Create(x => { x.AddConsole(); });

        public static void AddEntityFramework(this IServiceCollection services, string connectionString)
        {
            //var serverVersion = ServerVersion.AutoDetect(connectionString);
            //services.AddDbContext<DiaryDbContext>(

            //options => options.UseMySQL(connectionString));
            //x =>
            //{
            //    x.UseNetTopologySuite();
            //    x.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
            //})
            //.UseLoggerFactory(MyLoggerFactory)
            //.EnableDetailedErrors()
            //.EnableSensitiveDataLogging());

        }
    }
}
