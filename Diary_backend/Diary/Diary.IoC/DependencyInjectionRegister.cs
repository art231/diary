using Diary.Application;
using Diary.Application.Interfaces;
using Diary.Domain.Contracts;
using Diary.Infrastructure.ApplicationDbContext;
using Diary.Infrastructure.Queries;
using Diary.Infrastructure.Repositories;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Globalization;
using System.Reflection;

namespace Diary.IoC
{

    public static class DependencyInjectionRegister
    {
        public static void RegisterDependencyInjection(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IBus, Bus>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped(typeof(IQueryBase<>), typeof(QueryBase<>));
            services.AddMediatR(typeof(IBus));
            services.AddAutoMapper(typeof(IBus));
            services.AddValidatorsFromAssembly(Assembly.GetAssembly(typeof(IBus)));
            ValidatorOptions.Global.LanguageManager.Culture = new CultureInfo("ru");
        }
    }
}
