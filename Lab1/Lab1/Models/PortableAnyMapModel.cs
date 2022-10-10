using System;

namespace Lab1.Models;

public class PortableAnyMapModel
{
    // вся логика

    public void AfterOpenFileLogic()
    {
        //ModelErrorHappened?.Invoke("кринж");
    }
    
    public event Action<string>? ModelErrorHappened;
}