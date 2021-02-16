using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;

namespace DBconnectShop.Addons {
    /// <summary>
    /// Zarządzanie obrazkami
    /// </summary>
    public class Image {
        /// <summary>
        /// Znane formaty plików
        /// </summary>
        private static readonly List<byte[]> imageTypes = new List<byte[]> {
            new byte[]{ 0xFF, 0xD8 }, //JPEG
            new byte[]{ 0x42, 0x4D }, //BMP
            new byte[]{ 0x47, 0x49, 0x46 }, //GIF
            new byte[]{ 0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A } //PNG
        };
        /// <summary>
        /// Obrazek zapisany w formie bloba
        /// </summary>
        public byte[] BlobImage { get; }
        /// <summary>
        /// Obrazek w formie bitmapy
        /// </summary>
        public Bitmap bitmap {
            get {
                using var ms = new MemoryStream(BlobImage);
                return new Bitmap(ms);
            }
        }
        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="FileLocation">Lokalizacja obrazka</param>
        public Image(string FileLocation) {
            if(!File.Exists(FileLocation)) {
                string test = Path.Combine(Environment.CurrentDirectory, FileLocation);
                if(!File.Exists(FileLocation))
                    throw new ImageException("Nie znaleźiono podanego pliku");
                else
                    FileLocation = test;
            }

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
        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="BlobArray">Obrazek w formie bloba</param>
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

        /// <summary>
        /// Domyślny obrazek
        /// </summary>
        public static Image Default =>
            new Image("Images\\no-image.png");
    }

    public class ImageException : Exception {
        private string message;

        public ImageException(string message) {
            this.message = message;
        }

        public override string Message => message;
    }
}
