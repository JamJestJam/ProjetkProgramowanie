using DBconnectShop.Addons;
using DBconnectShop.Table;
using System.Collections.Generic;
using System.Linq;

namespace DBconnectShop.Access {
    partial class Admin {
        public List<Product_image> GetImages(int id) {
            using var db = new Shop();

            return db
                .Product_Images
                .Where(a => a.Product_id == id)
                .ToList();
        }

        public void ChangeActive(Product_image product, bool value) {
            using var db = new Shop();
            db.Product_Images.Attach(product);

            product.Product_image_active = value;
            db.SaveChanges();
        }

        public void ChangeImage(int id, string file) {
            using var db = new Shop();

            var image = new Image(file);
            var product = db.Product_Images.Where(a => a.Product_image_id == id).FirstOrDefault();

            db.Product_Images.Attach(product);
            product.Product_Image = image.BlobImage;

            try {
                int code = db.SaveChanges();
                if(code != 1)
                    throw new AddElementException("Wystąpił problem z przesłanym avatarem.");
            } catch {
                throw new AddElementException("Wystąpił problem z przesłanym avatarem.");
            }
        }

        public Product_image NewImage(int id) {
            using var db = new Shop();
            var tmp = Image.Default.BlobImage;

            Product_image product = new Product_image() {
                Product_image_active = false,
                Product_id = id,
                Product_Image = tmp
            };

            db.Product_Images.Add(product);
            db.SaveChanges();

            return product;
        }
    }
}
