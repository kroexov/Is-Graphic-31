using System;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using Avalonia.Platform;

namespace Lab1.Models;

public class PortableAnyMapModel
{
    private byte[] _bytes;
    private byte[] _bytesOfImage;
    private int _index = 0;
    private FileHeaderInfo _header;
    
    private String ExtractHeaderInfo()
    {
        String header = "";
        int lineBreakCounter = 0;
        const int codeOfLineBreakChar = 10;

        while (lineBreakCounter != 3)
        {
            if (_bytes[_index] == codeOfLineBreakChar)
            {
                lineBreakCounter++;
                header += " ";
            }
            else
            {
                header += Convert.ToChar(_bytes[_index]);
            }

            _index++;
        }

        return header;
    }

    private void ExtractImageBytes()
    {
        for (int i = _index; i < _bytes.Length; i++)
        {
            _bytesOfImage[i - _index] = _bytes[i];
        }
    }

    public void ReadFile(String filePath)
    {
        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException();
        }

        _bytes = File.ReadAllBytes(filePath);
        string headerInfo = ExtractHeaderInfo();
        _header = new FileHeaderInfo(headerInfo);
    }

    public void AfterOpenFileLogic(string filePath)
    {
        //ModelErrorHappened?.Invoke("кринж");
    }

    public event Action<string>? ModelErrorHappened;
}