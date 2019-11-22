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
        private IProductVariantsRepository _productVariantRepository;
        private ISizeRepository _sizeRepository;
        private IStockRepository _stockRepository;
       

        public ProductManager(IProductRepository productRepository, IProductVariantsRepository productVariantRepository, ISizeRepository sizeRepository, IStockRepository stockRepository) :base(productRepository)
        {
            _productManger = productRepository;
            _productVariantRepository = productVariantRepository;
            _sizeRepository = sizeRepository;
            _stockRepository = stockRepository;
        }
        public override bool Add(Product entity)
        {
            return _productManger.Add(entity);
        }

        public override ICollection<Product> GetAll()
        {
            return _productManger.GetAll();
             
        }

        public override Product GetById(long id)
        {
            return _productManger.GetById(id);
        }

        public override bool Update(Product entity)
        {
            return _productManger.Update(entity);
        }

        public override bool Remove(Product entity)
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

        public ICollection<Product> GetByCatId(long Id)
        {
            return _productManger.GetByCatId(Id);
        }

        public Stock GetBySId(long? Id)
        {
            return _productManger.GetBySId(Id);
        }

        public ProductVariants GetByPVId(long? Id)
        {
            return _productManger.GetByPVId(Id);
        }

        public Size GetBySzId(long? Id)
        {
            return _productManger.GetBySzId(Id);
        }

        public Product ProductWithoutProductCode()
        {
            return _productManger.ProductWithoutProductCode();
        }

        //public ICollection<Product> GetByCriteria(ProductSearchCriteriaVM criteria)
        //{
        //    return _productManger.GetByCriteria(criteria);
        //}


    }
}
