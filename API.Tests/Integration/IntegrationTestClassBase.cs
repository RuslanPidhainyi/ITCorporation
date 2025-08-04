using API.Data;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace API.Tests.Integration;

[TestClass]
//INFO: WebApplicationFactory - brings up a real server in-memory without running on a real port
public class IntegrationTestClassBase : WebApplicationFactory<Program> 
{
    //INFO: Sending HTTP requests to the server
    protected HttpClient _client = null!;

    //INFO: Database access (in InMemory)
    protected AppDbContext _context = null!;

    protected override IHost CreateHost(IHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            //INFO: Removing PostgreSQL registration in AppDbContext
            var descriptors = services.Where(
                d => d.ServiceType.FullName != null &&
                d.ServiceType.FullName.Contains("AppDbContext")).ToList();

            foreach (var descriptor in descriptors)
            {
                services.Remove(descriptor);
            }

            //INFO: Adding InMemory database for testing
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseInMemoryDatabase("TestInMemoryDb");
            });

            //INFO: Initialization of the InMemory database
            var sp = services.BuildServiceProvider();
            using var scope = sp.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();
        });

        return base.CreateHost(builder);
    }

    [TestInitialize]
    public void Setup()
    {
        //INFO: Create a client to send HTTP requests to the server
        _client = this.CreateClient();

        //INFO: Create a scope to access the database context
        var scope = this.Services.CreateScope();

        //INFO: Get the AppDbContext from the service provider
        _context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    }

    [TestCleanup]
    public void Cleanup()
    {
        //INFO: Cleaning the database after each test
        _context.Database.EnsureDeleted();
        _context.Dispose();
    }
}