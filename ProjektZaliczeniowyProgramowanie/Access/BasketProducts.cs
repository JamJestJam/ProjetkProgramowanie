using DBconnectShop.Addons;
using DBconnectShop.Table;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DBconnectShop.Access {
    public class BasketProducts {
        Basket Basket { get; }
        public List<BasketProduct> Products { get; private set; }

        public BasketProducts(Basket basket) {
            Basket = basket;

            Reload();
        }

        public void Reload() {
            using var db = new Shop();

            var productsQuery = db.Products
                .Include(a => a.Products_Prices)
                .Include(a => a.Product_Images)
                .Where(a => Basket.ProductList.Keys.Any(b => b == a.Product_id));

#if DEBUG
            Console.WriteLine(productsQuery.ToQueryString());
#endif

            Products = new List<BasketProduct>();
            foreach(var product in productsQuery)
                Products.Add(new BasketProduct(Basket, product));
        }
    }

    public class BasketProduct {
        Basket Basket { get; }
        Product Product { get; }

        public uint Count {
            get => Basket.ProductList[Product.ID];
            set => Basket.SetCount(Product.ID, value);
        }

        public string Name =>
            Product.TrueName;

        public decimal Price =>
            Product.ActualPrice;

        public decimal Sum =>
            Price * Count;

        public string Image =>
            "/Images/no-image.png";

        internal BasketProduct(Basket basket, Product product) {
            Basket = basket;
            Product = product;
        }
    }
}
