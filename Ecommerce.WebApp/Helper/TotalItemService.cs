
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.WebApp.Helper
{
    public class TotalItemService 
    {
        private IHttpContextAccessor contextAccessor;
        public TotalItemService(IHttpContextAccessor ctxAccessor)
        {
            contextAccessor = ctxAccessor;
        }
        public int TotalProductPurchased { get 
            {
                
                var allItems = SessionHelper.GetObjectFromJson<List<Item>>(contextAccessor.HttpContext.Session, "cart");

                if(allItems == null)
                {
                    return 0;
                }
                return allItems.Sum(i => i.Quantity);
            } 
        }
    }
}
