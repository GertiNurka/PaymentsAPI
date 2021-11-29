using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MediatR;
using PaymentsAPI.Services;
using PaymentsAPI.Application.Mapping;
using System.Reflection;
using PaymentsDomain.AggregatesModel.PaymentAggregate;
using PaymentsInfrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.IO;
using Microsoft.OpenApi.Models;
using FluentValidation;
using FluentValidation.AspNetCore;
using PaymentsAPI.Application.CQRS.Commands;
using static PaymentsAPI.Application.CQRS.Commands.CreatePaymentCommand;
using PaymentsInfrastructure.Context;

namespace PaymentsAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Register the Swagger generator
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Payments API", Version = "v1" });
            });

            services.AddMvcCore()
                //Register fluent validation
                .AddFluentValidation();

            #region Register comand validators

            services.AddTransient<IValidator<CreatePaymentCommand>, CreatePaymentCommandValidator>();

            #endregion

            services.AddControllers();            

            //Register MediatR for supporting CQRS
            services.AddMediatR(typeof(Startup));

            //Add AutoMapper
            services.AddAutoMapper(typeof(Startup).Assembly, typeof(ApplicationMappingProfile).GetTypeInfo().Assembly);

            //Add DbContext
            services.AddDbContext<PaymentsDbContext>(x => x.UseSqlite("Data Source=LocalDatabase.db"));

            #region Register dependencies in order to achive IoC

            services.AddScoped<IPaymentsService, PaymentsService>();
            services.AddScoped<IPaymentsRepository, PaymentsRepository>();

            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, PaymentsDbContext dbContext)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            #region Seed Data
            //This section doesn't represent best practice.
            //Is used just to create some data in order for the user to see something once he runs the app.
            //I didn't use the seed functionality of entity framework core because it has the limitation that the ID of the entity needs to be manually specified.
            
            string dbName = "LocalDatabase.db";
            if (File.Exists(dbName))
            {
                File.Delete(dbName);
            }

            //Ensure database is created
            dbContext.Database.EnsureCreated();
            if (!dbContext.Payments.Any())
            {
                dbContext.Payments.AddRange(new Payment[]
                    {
                             new Payment(10, new Card("Test", "1234432112344321", "1223", "123"), new BillingAddress("Line1", "Line2", "Line3", "PostCode1")),
                             new Payment(100, new Card("Test2", "1234432112344322", "1224", "124"), new BillingAddress("Line1", "Line2", "Line3", "PostCode2")),
                             new Payment(200, new Card("Test3", "1234432112344323", "1225", "125"), new BillingAddress("Line1", "Line2", "Line3", "PostCode3")),
                             new Payment(300, new Card("Test4", "1234432112344324", "1226", "126"), new BillingAddress("Line1", "Line2", "Line3", "PostCode4"))
                    });
                dbContext.SaveChanges();
            }

            #endregion

            #region Register swagger

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger()
            // Enable middleware to serve swagger-ui
            .UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("v1/swagger.json", "API v1");
            });

            #endregion

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
