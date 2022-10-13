using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using Avalonia.Controls;
using Lab1.Models;
using Lab1.Views;
using ReactiveUI;

namespace Lab1.ViewModels
{
    public class MainWindowViewModel : ViewModelBase, INotifyPropertyChanged
    {
        private string _data;

        private string _selectedImage = string.Empty;

        private ObservableCollection<string> _items = new ObservableCollection<string>();

        private PortableAnyMapModel _model;

        private bool _errorOccured = false;
        
        private string _errorText = "Неизвестная ошибка";

        public string SelectedImage
        {
            get => _selectedImage;
            set
            {
                this.RaiseAndSetIfChanged(ref _selectedImage, value);
                RaisePropertyChanged(nameof(IsImageSelected));
            }
        }

        public bool IsErrorOccured
        {
            get => _errorOccured;
            set
            {
                this.RaiseAndSetIfChanged(ref _errorOccured, value);
            }
        }
        
        public string ErrorText
        {
            get => _errorText;
            set
            {
                this.RaiseAndSetIfChanged(ref _errorText, value);
            }
        }

        public bool IsImageSelected => SelectedImage != String.Empty;

        public MainWindowViewModel(PortableAnyMapModel model)
        {
            _model = model;
            model.ModelErrorHappened += OnErrorHappened;
        }

        public ObservableCollection<string> Items
        {
            get => _items;
            set
            {
                this.RaiseAndSetIfChanged(ref _items, value);
                RaisePropertyChanged(nameof(IsImageSelected));
            }
        }

        public string Data
        {
            get => _data;
            set => this.RaiseAndSetIfChanged(ref _data, value);
        }
        
        public async void AddNewFile()
        {
            var ofd = new OpenFileDialog();
            string[]? result = null;
            ofd.Filters.Add(new FileDialogFilter() {Name = "Другой файл", Extensions = {"*"}});
            ofd.Filters.Add(new FileDialogFilter() {Name = "Файлы P6", Extensions = {"ppm"}});
            ofd.Filters.Add(new FileDialogFilter() {Name = "Файлы P5", Extensions = {"pgm"}});
            
            result = await ofd.ShowAsync(new Window());
            if (result != null)
            {
                if (!Items.Contains(result.First()))
                {
                    _items.Add(result.First());
                    Data = File.ReadAllText(result.First());
                    return;
                }
                OnErrorHappened("Вы уже выбирали этот файл!");
            }
        }

        public async void SaveFile()
        {
            var ofd = new SaveFileDialog();
            ofd.Filters.Add(new FileDialogFilter() {Name = "Другой файл", Extensions = {"*"}});
            ofd.Filters.Add(new FileDialogFilter() {Name = "Файлы P6", Extensions = {"ppm"}});
            ofd.Filters.Add(new FileDialogFilter() {Name = "Файлы P5", Extensions = {"pgm"}});
            
            var result = await ofd.ShowAsync(new Window());
            if (result != null)
            {
                File.WriteAllText(result, Data);
            }
        }

        public void DeleteFile()
        {
            if (_items.Contains(_selectedImage))
            {
                _items.Remove(_selectedImage);
                _selectedImage = String.Empty;
                RaisePropertyChanged(nameof(IsImageSelected));
            }
        }

        public void OpenFile()
        {
            _model.ReadFile(_selectedImage);
            string _pathFile = _model.AfterOpenFileLogic(_selectedImage); // прописать всю логику
            
            // передать ссылку на созданный .bmp
            ImageDisplayWindow imageDisplayWindow = new ImageDisplayWindow()
            {
                //todo: поменять на нормальный путь
                DataContext = new ImageDisplayViewModel(_pathFile) //вместо _selectedImage эту ссылку
            };
            imageDisplayWindow.Show();
        }

        public event Action<string> OnErrorHappened;
        
        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}

