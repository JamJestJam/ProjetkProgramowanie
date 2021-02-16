using DBconnectShop.Table;
using System.Linq;

namespace DBconnectShop.Access {
    public partial class AdminControl {
        /// <summary>
        /// Tworzy nową kategorie
        /// </summary>
        /// <returns>Zwraca nową kategorię</returns>
        public Product_categori NewCategori() {
            using var db = new Shop();

            Product_categori product = new Product_categori() {
                Product_category_name = "",
                Product_sub_category = null
            };

            db.Product_Categories.Add(product);
            db.SaveChanges();

            return product;
        }

        /// <summary>
        /// Zmienia nazwe kategorii
        /// </summary>
        /// <param name="product">Kategoria której zmienia nazwe</param>
        /// <param name="value">Wartość na jaką ma zmienić</param>
        public void ChangeCategoryName(Product_categori product, string value) {
            using var db = new Shop();

            db.Product_Categories.Attach(product);
            product.Product_category_name = value;

            db.SaveChanges();
        }

        /// <summary>
        /// Zmienia rodzica kategorii
        /// </summary>
        /// <param name="product">Kategoria do zmiany</param>
        /// <param name="value">Nazwa rodzica</param>
        public void ChangeParent(Product_categori product, string value) {
            using var db = new Shop();
            int? id = null;
            var tmp = db.Product_Categories
                .FirstOrDefault(a => a.Product_category_name == value);

            if(tmp != null)
                id = tmp.ID;
            if(id == product.ID)
                return;

            db.Product_Categories.Attach(product);
            product.Product_sub_category = id;

            db.SaveChanges();
        }
    }
}
