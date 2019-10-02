using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ecommerce.Abstractions.BLL;
using Ecommerce.Abstractions.Repositories;
using Ecommerce.Abstractions.Repositories.Base;
using Ecommerce.BLL.Base;
using Ecommerce.Models;
using Ecommerce.Models.APIViewModels;

namespace Ecommerce.BLL
{
    public class ProductManager:Manager<Product>,IProductManager
    {
        private IProductRepository _productManger;
       

        public ProductManager(IProductRepository productRepository):base(productRepository)
        {
            _productManger = productRepository;
        }
        public bool Add(Product entity)
        {
            return _productManger.Add(entity);
        }

        public ICollection<Product> GetAll()
        {
            return _productManger.GetAll().ToList();
             
        }

        public Product GetById(long id)
        {
            return _productManger.GetById(id);
        }

        public bool Update(Product entity)
        {
            return _productManger.Update(entity);
        }

        public bool Remove(Product entity)
        {
            return _productManger.Remove(entity);
        }

        public List<Category> list()
        {
           return (List<Category>)_productManger.list();
            
            //return _categoryRepository.GetAll().ToList();
        }

        public ICollection<Product> GetByPrice(double price)
        {
            return _productManger.GetByPrice(price);
        }

        public ICollection<Product> GetByName(string Name)
        {
            return _productManger.GetByName(Name);
        }

        public ICollection<Product> GetByCategory(string CategoryName)
        {
            return _productManger.GetByCategory(CategoryName);
        }

        public Product Find(long Id)
        {
            return _productManger.Find(Id);
        }

        public ICollection<Product> GetByCriteria(ProductSearchCriteriaVM criteria)
        {
            return _productManger.GetByCriteria(criteria);
        }

        //public ICollection<Product> GetByCriteria(ProductSearchCriteriaVM criteria)
        //{
        //    return _productManger.GetByCriteria(criteria);
        //}


    }
}
