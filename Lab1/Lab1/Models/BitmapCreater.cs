using System;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;

namespace Lab1.Models
{
    /// <summary>
    /// класс который создает нам bitmap
    /// </summary>
    public class BitmapCreate
    {
        /// <summary>
        /// хранит в себе формат пикселя чб/цв
        /// </summary>
        private PixelFormat _pixelFormat;
        
        /// <summary>
        /// ширина картинки
        /// </summary>
        private int _columns;
        
        /// <summary>
        /// высота картинки
        /// </summary>
        private int _rows;
        
        /// <summary>
        /// хз
        /// </summary>
        private int _stride;
        
        /// <summary>
        /// путь до картинки
        /// </summary>
        private string _path;

        /// <summary>
        /// создает файл формата p5, 8 бит на цвет 
        /// </summary>
        /// <param name="header">распарсинный header картинки.</param>
        /// <param name="bytes">информации о пикселях в картинке.</param>
        /// <returns>созданный bitmap на основе файла</returns>
        public Bitmap CreateP5Bit8(FileHeaderInfo header, byte[] bytes)
        {
            _pixelFormat = PixelFormat.Format8bppIndexed;
            _columns = header.Width;
            _rows = header.Height;
            _stride = _columns;

            Bitmap im = new Bitmap(_columns, _rows, _stride, 
                _pixelFormat, 
                Marshal.UnsafeAddrOfPinnedArrayElement(bytes, 0));
            
            ColorPalette _palette = im.Palette;
            Color[] _entries = _palette.Entries;
            for (int i = 0; i < 256; i++)
            {
                Color b = new Color();
                b = Color.FromArgb((byte)i, (byte)i, (byte)i);
                _entries[i] = b;
            }
            im.Palette = _palette;

            return im;
        }

        /// <summary>
        /// создает файл формата p6, 8 бит на цвет 
        /// </summary>
        /// <param name="header">распарсинный header картинки.</param>
        /// <param name="bytes">информации о пикселях в картинке.</param>
        /// <returns>созданный bitmap на основе файла</returns>
        public Bitmap CreateP6Bit8(FileHeaderInfo header, byte[] bytes)
        {
            _pixelFormat = PixelFormat.Format24bppRgb;
            
            _columns = header.Width;
            _rows = header.Height;
            _stride = _columns * 3;

            var bytePtr = Marshal.AllocHGlobal(bytes.Length);
            Marshal.Copy(bytes, 0, bytePtr, bytes.Length);
            var scan0 = Marshal.UnsafeAddrOfPinnedArrayElement(bytes, 0);

            Bitmap im = new Bitmap(_columns, _rows, _stride, 
                _pixelFormat, 
                scan0);

            return im;
        }
    }
}