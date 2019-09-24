using System;
using System.Collections.Generic;
using System.Text;
using Ecommerce.Abstractions.BLL;
using Ecommerce.Abstractions.Repositories;
using Ecommerce.BLL.Base;
using Ecommerce.Models;

namespace Ecommerce.BLL
{
    public class CategoryManager:Manager<Category>,ICategoryManager
    {
        private ICategoryRepository _categoryRepository;
        private IProductRepository _productRepository;
        public CategoryManager(ICategoryRepository customerRepository) : base(customerRepository)
        {
            _categoryRepository = customerRepository;
        }
        public bool Add(Category entity)
        {
            return _categoryRepository.Add(entity);
        }

        public ICollection<Category> GetAll()
        {
            return _categoryRepository.GetAll();
        }

        public Category GetById(long id)
        {
            return _categoryRepository.GetById(id);
        }

        public bool Update(Category entity)
        {
            return _categoryRepository.Update(entity);
        }

        public bool Remove(Category entity)
        {
            return _categoryRepository.Remove(entity);
        }

        public List<Product> productList()
        {
            return _categoryRepository.productList();
            // return (List<Product>) _productRepository.GetAll();
        }
    }
}
