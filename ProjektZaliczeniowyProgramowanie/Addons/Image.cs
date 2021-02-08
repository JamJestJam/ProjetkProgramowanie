using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;

namespace DBconnectShop.Addons {
    public class Image {
        private static readonly List<byte[]> imageTypes = new List<byte[]> {
            new byte[]{ 0xFF, 0xD8 }, //JPEG
            new byte[]{ 0x42, 0x4D }, //BMP
            new byte[]{ 0x47, 0x49, 0x46 }, //GIF
            new byte[]{ 0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A } //PNG
        };

        public byte[] BlobImage { get; }
        public Bitmap bitmap {
            get {
                using var ms = new MemoryStream(BlobImage);
                return new Bitmap(ms);
            }
        }

        public Image(string FileLocation) {
            if(!File.Exists(FileLocation))
                throw new ImageException("Nie znaleźiono podanego pliku");

            ReadOnlySpan<byte> data = File.ReadAllBytes(FileLocation);

            foreach(var check in imageTypes) {
                if(data.Length >= check.Length) {
                    var slice = data.Slice(0, check.Length);
                    if(slice.SequenceEqual(check)) {
                        this.BlobImage = data.ToArray();
                        return;
                    }
                }
            }

            throw new ImageException("Proszę podać plik graficzny.");
        }

        public Image(byte[] BlobArray) {
            ReadOnlySpan<byte> data = BlobArray;

            foreach(var check in imageTypes) {
                if(data.Length >= check.Length) {
                    var slice = data.Slice(0, check.Length);
                    if(slice.SequenceEqual(check)) {
                        this.BlobImage = data.ToArray();
                        return;
                    }
                }
            }

            throw new ImageException("Proszę podać plik graficzny.");
        }
    }

    public class ImageException : Exception {
        private string message;

        public ImageException(string message) {
            this.message = message;
        }

        public override string Message => message;
    }
}
