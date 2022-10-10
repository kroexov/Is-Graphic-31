using System;
using System.IO;

namespace Lab1.Models;

public class PortableAnyMapModel
{
    private byte[] _bytes;
    
    private String ExtractHeaderInfo()
    {
        String header = "";
        int lineBreakCounter = 0;
        int i = 0;
        const int codeOfLineBreakChar = 10;

        while (lineBreakCounter != 3)
        {
            if (_bytes[i] == codeOfLineBreakChar)
            {
                lineBreakCounter++;
                header += " ";
            }
            else
            {
                header += Convert.ToChar(_bytes[i]);
            }

            i++;
        }

        return header;
    }

    public void ReadFile(String filePath)
    {
        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException();
        }

        _bytes = File.ReadAllBytes(filePath);
    }

    public void AfterOpenFileLogic(string filePath)
    {
        //ModelErrorHappened?.Invoke("кринж");
    }
    
    public event Action<string>? ModelErrorHappened;
}