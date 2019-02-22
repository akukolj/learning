using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DutchTreat.Data.Entities;
using DutchTreat.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;
using Order = DutchTreat.Data.Entities.Order;

namespace DutchTreat.Data
{

    public class DutchRepository : IDutchRepository
    {
        private readonly DutchContext _dutchContext;
        private readonly ILogger<DutchRepository> _logger;

        public DutchRepository(DutchContext ctx,ILogger<DutchRepository> logger)
        {
            _logger = logger;
            _dutchContext = ctx;
        }

        public IEnumerable<Product> GetAllProducts()
        {
            _logger.LogInformation("test");
            return _dutchContext.Products.OrderBy(c => c.Category).ToList();

        }

        public IEnumerable<Product> GetProducts(string category)
        {
            return _dutchContext.Products.Where(c => c.Category == category).ToList();
        }

        public bool SaveAll()
        {
            return _dutchContext.SaveChanges() > 0;
        }

        public IEnumerable<Order> GetAllOrders()
        {
            return _dutchContext.Orders.Include(o=>o.Items).ThenInclude(o=>o.Product).ToList();
        }

        public Order GetOrderById(int id)
        {
            return _dutchContext.Orders.Include(o => o.Items).ThenInclude(o => o.Product).FirstOrDefault(d => d.Id == id);
        }

        public void AddEntity(object model)
        {
            _dutchContext.Add(model);
        }
    }
}
