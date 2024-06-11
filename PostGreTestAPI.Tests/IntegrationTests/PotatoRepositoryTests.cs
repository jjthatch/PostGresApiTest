using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PostGreTestAPI.DataAccess;
using PostGreTestAPI.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostGreTestAPI.Tests.IntegrationTests
{
    [TestClass]
    public class PotatoRepositoryTests
    {
        private ServiceProvider _serviceProvider;
        private ApplicationDbContext _context;
        private IPotatoRepository _potatoRepository;

        [TestInitialize]
        public async Task Initialize()
        {
            var serviceCollection = new ServiceCollection();

            serviceCollection.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql("Host=localhost;Port=5432;Database=mydatabase;Username=username;Password=password"));

            serviceCollection.AddScoped<IPotatoDataProvider, PotatoDataProvider>();
            serviceCollection.AddScoped<IPotatoRepository, PotatoRepository>();

            _serviceProvider = serviceCollection.BuildServiceProvider();

            _context = _serviceProvider.GetRequiredService<ApplicationDbContext>();
            _potatoRepository = _serviceProvider.GetRequiredService<IPotatoRepository>();

            await _context.Database.EnsureCreatedAsync();
            await SeedDatabaseAsync();
        }

        [TestCleanup]
        public void Cleanup()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
            _serviceProvider.Dispose();
        }

        private async Task SeedDatabaseAsync()
        {
            var potatoes = new List<Potato>
        {
            new Potato { Id = Guid.NewGuid(), Name = "Potato1", StartTime = DateTime.UtcNow.AddDays(-1), EndTime = DateTime.UtcNow, PotatoStatus = "Fresh" },
            new Potato { Id = Guid.NewGuid(), Name = "Potato2", StartTime = DateTime.UtcNow.AddDays(-2), EndTime = DateTime.UtcNow, PotatoStatus = "Stale" },
            // Add more seed data if needed
        };

            await _context.potatoes.AddRangeAsync(potatoes);
            await _context.SaveChangesAsync();
        }

        [TestMethod]
        public async Task GetPotatoesAsync_ReturnsPagedResult()
        {
            var result = await _potatoRepository.GetPotatoesAsync(null, null, null, 1, 10);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.TotalItems > 0);
        }

        [TestMethod]
        public async Task GetPotatoesAsync_ReturnsFilteredResult()
        {
            var result = await _potatoRepository.GetPotatoesAsync(DateTime.UtcNow.AddDays(-2), DateTime.UtcNow, "Fresh", 1, 10);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Items.All(p => p.PotatoStatus == "Fresh"));
        }

    }
}
