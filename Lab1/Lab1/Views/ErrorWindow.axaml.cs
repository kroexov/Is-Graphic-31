using System.ComponentModel;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Lab1.Views;

public partial class ErrorWindow : Window, INotifyPropertyChanged
{
    public ErrorWindow()
    {
        InitializeComponent();
    }
    
    public ErrorWindow(string str)
    {
        InitializeComponent();
        Error = str;
    }

    private string _error;

    public string Error
    {
        get => _error;
        set
        {
            _error = value;
            RaisePropertyChanged(nameof(Error));
        }
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
    
    public event PropertyChangedEventHandler PropertyChanged;
    private void RaisePropertyChanged(string propertyName)
    {
        if (PropertyChanged != null)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}