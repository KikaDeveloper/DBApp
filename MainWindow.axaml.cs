using Avalonia;
using Avalonia.Input;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Interactivity;

using System;
using System.Threading.Tasks;

using Helper;

namespace DBApp
{
    public partial class MainWindow : Window
    {
        TextBox NameField {get; set;}
        TextBox UrlField{get; set;}
        TextBox DescriptionField {get; set;}

        Driver MongoDriver {get; set;} 

        public MainWindow()
        {
            InitializeComponent();

            #if DEBUG
            this.AttachDevTools();
            #endif

            MongoDriver = new Driver("mongodb://localhost:27017", "data");
            NameField = this.Find<TextBox>("NameField");
            UrlField = this.Find<TextBox>("UrlField");
            DescriptionField = this.Find<TextBox>("DescriptionField");

            // NameField.TextInput += GotFocusNameField;
            // UrlField.TextInput += GotFocusUrlField;
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public void OnButtonClick(object sender, RoutedEventArgs e){
            Console.WriteLine("Click");
            Console.WriteLine($"{NameField.Text} : {UrlField.Text} : {DescriptionField.Text}");

            if(CheckFields()){
                var item = new Item(NameField.Text, UrlField.Text, DescriptionField.Text);
                MongoDriver?.AddDocument(item);
            }            
        }

        public void GotFocusNameField(object sender, TextInputEventArgs e){
            Console.WriteLine("Typing text");
            if(NameField.Text != null || NameField.Text != ""){
                this.Find<Label>("LabelName").Classes.Remove("normal");
                this.Find<Label>("LabelName").Classes.Add("right");
            } else {
                this.Find<Label>("LabelName").Classes.Remove("right");
                this.Find<Label>("LabelName").Classes.Add("wrong");
            }
        }

        public void GotFocusUrlField(object sender, TextInputEventArgs e){

        }

        private bool CheckFields(){
            return ((NameField.Text != "" || NameField.Text != "") && (UrlField.Text != " " || UrlField.Text != ""));
        }

    }
}