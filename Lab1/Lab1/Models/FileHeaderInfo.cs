using System;

namespace Lab1.Models;

public class FileHeaderInfo
{
    private string _fileFormat;
    private int _width;  //or rows
    private int _height;  //or columns
    private int _maxGreyLevel;

    public FileHeaderInfo(string header)
    {
        string[] headerItems = header.Trim().Split();
        if (headerItems.Length != 4)
        {
            throw new Exception("Damaged file: header doesn't match required format");
        }

        _fileFormat = headerItems[0];
        _width = Convert.ToInt32(headerItems[1]);
        _height = Convert.ToInt32(headerItems[2]);
        _maxGreyLevel = Convert.ToInt32(headerItems[3]);
    }
}