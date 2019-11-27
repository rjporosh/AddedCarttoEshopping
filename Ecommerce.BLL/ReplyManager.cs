using Ecommerce.Abstractions.BLL;
using Ecommerce.Abstractions.Repositories;
using Ecommerce.BLL.Base;
using Ecommerce.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ecommerce.BLL
{
    public class ReplyManager : Manager<Reply>, IReplyManager
    {
        private IReplyRepository _replyRepository;
        private IProductRepository _productRepository;
        public ReplyManager(IReplyRepository replyRepository, IProductRepository productRepository) : base(replyRepository)
        {
            _replyRepository = replyRepository;
            _productRepository = productRepository;
        }
    }
}
