using DBconnectShop.Table;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DBconnectShop {
    public class BuyableProducts {
        List<Product> Products { get; set; }

        public int ProductCount => Products.Count();


        public BuyableProducts() {
            Refresh();
        }

        public void Refresh() {
            using var db = new Shop();

            IQueryable<Product> products = db.Products
                .Where(a => a.Product_aviable);
#if DEBUG
            Console.WriteLine(products.ToQueryString());
#endif

            Products = products.ToList();
        }

        public List<string> GetProductName(int page = 0, int perPage = 18) {
            var resoult = new List<string>();

            for(int i = page * perPage; i < (page + 1) * perPage && i < Products.Count; i++) 
                resoult.Add(Products[i].Product_name.Trim());
            
            
            return resoult;
        }
    }
}
