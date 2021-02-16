using DBconnectShop.Table;
using System.Collections.Generic;
using System.Linq;

namespace DBconnectShop.Access {
    public partial class AdminControl {
        public List<Product_specification> GetSpecyfication(int id) {
            using var db = new Shop();

            return db
                .Product_Specifications
                .Where(a => a.Product_id == id)
                .ToList();
        }

        public Product_specification NewSpecyfication(int id) {
            using var db = new Shop();

            Product_specification product = new Product_specification() {
                Product_id = id,
                Product_specification_name = "",
                Product_specification_value = ""
            };

            db.Product_Specifications.Add(product);
            db.SaveChanges();

            return product;
        }

        public void ChangeName(Product_specification product, string value) {
            using var db = new Shop();

            db.Product_Specifications.Attach(product);
            product.Product_specification_name = value;

            db.SaveChanges();
        }

        public void ChangeValue(Product_specification product, string value) {
            using var db = new Shop();

            db.Product_Specifications.Attach(product);
            product.Product_specification_value = value;

            db.SaveChanges();
        }
    }
}
