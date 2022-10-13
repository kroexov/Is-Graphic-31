using System;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using Lab1.Models;

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
        _bytesOfImage = new byte[_bytes.Length - _index];
        for (var i = _index; i < _bytes.Length; i++)
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
        try
        {
            _header = new FileHeaderInfo(headerInfo);
        }
        catch (Exception e)
        {
            throw;
        }
        
        ExtractImageBytes();
    }

    public string AfterOpenFileLogic(string filePath)
    {
        BitmapCreate cr = new BitmapCreate();
        Bitmap newImage = new Bitmap(3, 3);
        string fileName = Path.GetFileName(filePath);
        fileName = fileName.Substring(0, fileName.Length - 3) + "bmp";
        string pathSaveFile = AppDomain.CurrentDomain.BaseDirectory;
        pathSaveFile = pathSaveFile.Substring(0, pathSaveFile.Length - 17);
        string fullFileName = pathSaveFile + "\\imgFiles\\" + fileName;
        
        switch (_header.FileFormat)
        {
            case "P6" when _header.MaxColorLevel == 255:
            {
                newImage = cr.CreateP6Bit8(_header, _bytesOfImage);
                break;
            }
            case "P6" when _header.MaxColorLevel == 65535:
            {
                newImage = cr.CreateP6Bit16(_header, _bytesOfImage);
                break;
            }
            case "P5" when _header.MaxColorLevel == 255:
            {
                newImage = cr.CreateP5Bit8(_header, _bytesOfImage);
                break;
            }
            case "P5" when _header.MaxColorLevel == 65535:
            {
                newImage = cr.CreateP5Bit16(_header, _bytesOfImage);
                break;
            }
        }
        
        
        newImage.Save(fullFileName, ImageFormat.Bmp);
        return fullFileName;
    }

    public event Action<string>? ModelErrorHappened;
}