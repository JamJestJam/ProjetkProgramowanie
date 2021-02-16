using DBconnectShop.Addons;
using DBconnectShop.Table;
using System.Collections.Generic;
using System.Linq;

namespace DBconnectShop.Access {
    public partial class AdminControl {
        /// <summary>
        /// Pobiera listę obrazków danego produktu
        /// </summary>
        /// <param name="id">Id produktu</param>
        /// <returns>Zwraca listę obrazków</returns>
        public List<Product_image> GetImages(int id) {
            using var db = new Shop();

            return db
                .Product_Images
                .Where(a => a.Product_id == id)
                .ToList();
        }

        /// <summary>
        /// Zmienia stan obrazka
        /// </summary>
        /// <param name="product">Obrazek do zmiany</param>
        /// <param name="value">Wartość stanu</param>
        public void ChangeActive(Product_image product, bool value) {
            using var db = new Shop();
            db.Product_Images.Attach(product);

            product.Product_image_active = value;
            db.SaveChanges();
        }

        /// <summary>
        /// Zmienia obrazek
        /// </summary>
        /// <param name="id">ID obrazka</param>
        /// <param name="file">Ścieżka do nowego obrazka</param>
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

        /// <summary>
        /// Tworzy nowy obrazek
        /// </summary>
        /// <param name="id">ID produktu do którego jest dołączony</param>
        /// <returns>Obrazek</returns>
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
