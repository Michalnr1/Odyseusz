using System.Collections.Generic;
using Odyseusz.dal;
using Odyseusz.domain;

namespace Odyseusz.bll
{
    public class ProductService
    {
        private readonly ProductRepository _productRepository;
        public ProductService(ProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public IEnumerable<Produkt> GetAllProducts()
        {
            return _productRepository.GetAll();
        }
    }
}
