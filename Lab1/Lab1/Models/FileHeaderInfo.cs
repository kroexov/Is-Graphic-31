using System;
using System.Net;

namespace Lab1.Models;


/// <summary>
/// класс который парсит header
/// </summary>
public class FileHeaderInfo
{
    /// <summary>
    /// формат картинки p5/p6
    /// </summary>
    public string FileFormat { get; }
    /// <summary>
    /// ширина
    /// </summary>
    public int Width { get; }
    /// <summary>
    /// высота
    /// </summary>
    public int Height { get; }
    /// <summary>
    /// максимальное значения цвета
    /// </summary>
    public int MaxColorLevel { get; }
    /// <summary>
    /// размер пикселя
    /// </summary>
    public int PixelSize { get; }

    /// <summary>
    /// формат картинки p5/p6
    /// </summary>
    /// <param name="header">header картинки, который нужно парсить.</param>
    public FileHeaderInfo(string header)
    {
        string[] headerItems = header.Trim().Split();
        if (headerItems.Length != 4)
        {
            throw new Exception("Damaged file: header doesn't match required format");
        }

        FileFormat = headerItems[0];
        Width = Convert.ToInt32(headerItems[1]);
        Height = Convert.ToInt32(headerItems[2]);
        MaxColorLevel = Convert.ToInt32(headerItems[3]);
        PixelSize = 1;
        if (FileFormat.Equals("P6"))
        {
            PixelSize *= 3;
        }

        if (MaxColorLevel == 65535)
        {
            PixelSize *= 2;
        }
    }
}