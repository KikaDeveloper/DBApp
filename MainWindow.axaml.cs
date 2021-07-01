using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Interactivity;

using System;

namespace DBApp
{
    public partial class MainWindow : Window
    {

        TextBox Name {get; set;}
        TextBox Url{get; set;}
        TextBox Description {get; set;}


        public MainWindow()
        {
            InitializeComponent();
            
            #if DEBUG
            this.AttachDevTools();
            #endif

            InitializeControls();
        }

        private void InitializeControls(){
            Name = this.Find<TextBox>("NameField");
            Url = this.Find<TextBox>("UrlField");
            Description = this.Find<TextBox>("DescriptionField");
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public void OnButtonClick(object sender, RoutedEventArgs e){
            Console.WriteLine("Click");
            Console.WriteLine(Name.Text);
            Console.WriteLine(Url.Text);
            Console.WriteLine(Description.Text);
        }

    }
}