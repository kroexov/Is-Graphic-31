using System;

namespace Lab1.Models;

public class FileHeaderInfo
{
    public string FileFormat { get; }
    public int Width { get; }
    public int Height { get; }
    public int MaxColorLevel { get; }

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
    }
}