using Ecommerce.Abstractions.BLL;
using Ecommerce.Abstractions.Repositories;
using Ecommerce.BLL.Base;
using Ecommerce.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ecommerce.BLL
{
    public class CommentsManager : Manager<Comment>, ICommentsManager
    {
        private ICommentsRepository _commentsRepository;
        private IProductRepository _productRepository;
       

        public CommentsManager(ICommentsRepository commentsRepository, IProductRepository productRepository) : base(commentsRepository)
        {
            _commentsRepository = commentsRepository;
            _productRepository = productRepository;
       
        }
    }
}
