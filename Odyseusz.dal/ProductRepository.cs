using System;
using System.Collections.Generic;
using System.Linq;
using Odyseusz.dal;
using Odyseusz.domain;

namespace Odyseusz.dal
{
    public class ProductRepository
    {
        private readonly AppDbContext _context;

        public ProductRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Produkt> GetAll()
        {
            return _context.Products.ToList();
        }
    }
}
