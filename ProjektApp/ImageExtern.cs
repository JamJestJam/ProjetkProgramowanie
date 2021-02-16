using System.IO;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using ImageAddon = DBconnectShop.Addons.Image;


namespace ProjektApp {
    public static class ImageExtern {
        public static Image ToImage(this ImageAddon imageAddon) {
            var Image = new Image();

            Image.Source = imageAddon.ToBitmap();
            return Image;
        }

        public static BitmapImage ToBitmap(this ImageAddon imageAddon) {
            var image = new BitmapImage();
            using var mem = new MemoryStream(imageAddon.BlobImage);
            mem.Position = 0;
            image.BeginInit();
            image.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
            image.CacheOption = BitmapCacheOption.OnLoad;
            image.UriSource = null;
            image.StreamSource = mem;
            image.EndInit();
            image.Freeze();

            return image;
        }
    }
}
