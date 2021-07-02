using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Interactivity;

using System;

namespace DBApp
{
    public partial class MainWindow : Window
    {

        TextBox? NameField {get; set;}
        TextBox? UrlField{get; set;}
        TextBox? DescriptionField {get; set;}

        public MainWindow()
        {
            InitializeComponent();

            #if DEBUG
            this.AttachDevTools();
            #endif

            InitializeControls();
        }

        private void InitializeControls(){
            NameField = this.Find<TextBox>("NameField");
            UrlField = this.Find<TextBox>("UrlField");
            DescriptionField = this.Find<TextBox>("DescriptionField");
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public void OnButtonClick(object sender, RoutedEventArgs e){
            Console.WriteLine("Click");
            Console.WriteLine($"{NameField?.Text} : {UrlField?.Text} : {DescriptionField?.Text}");
        }

    }
}