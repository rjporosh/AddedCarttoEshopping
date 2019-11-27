using System;
using System.Collections.Generic;
using System.Text;
using Ecommerce.Abstractions.BLL;
using Ecommerce.Abstractions.Repositories;
using Ecommerce.BLL.Base;
using Ecommerce.Models;

namespace Ecommerce.BLL
{
    public class ReviewManager : Manager<Review>, IReviewManager
    {
        private IReviewRepository _reviewRepository;
        private IProductRepository _productRepository;
        public ReviewManager(IReviewRepository reviewRepository, IProductRepository productRepository) : base(reviewRepository)
        {
            _reviewRepository = reviewRepository;
            _productRepository = productRepository;
        }
    }
}
