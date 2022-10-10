using System;

namespace Lab1.Models;

public class FileHeaderInfo
{
    private string _fileFormat { get; }
    private int _width { get; }
    private int _height { get; }
    private int _maxColorLevel { get; }

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
        _maxColorLevel = Convert.ToInt32(headerItems[3]);
    }
}