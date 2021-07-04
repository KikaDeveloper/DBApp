using Avalonia;
using Avalonia.Input;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Interactivity;

using Helper;
using System;
using System.Collections.Generic;

namespace DBApp
{
    public partial class MainWindow : Window
    {
        TextBox NameField {get; set;}
        TextBox UrlField{get; set;}
        TextBox DescriptionField {get; set;}
        DataGrid Grid {get; set;}

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
            Grid = this.Find<DataGrid>("List");
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        private async void GetAllDocuments(){
            Grid.Items = await MongoDriver.GetAllDocuments();
        }

        public void OnButtonClick(object sender, RoutedEventArgs e){
            AddDocument();          
        }

        private async void AddDocument(){
            if(CheckFields()){
                var item = new Item(NameField.Text, UrlField.Text, DescriptionField.Text);
                await MongoDriver.AddDocument(item);

                ShowMessage("Запись добавлена.", "right");
                ClearTextBoxes();

            }else ShowMessage("Поля Имя и URL обязательны.", "wrong");
        }

        private void ShowMessage(string mess, string style){
            Label mes = new Label(){
                Content = mess,
            };
            mes.Classes.Add(style);
            mes.Classes.Add("mess"); 

            this.Find<StackPanel>("console").Children.Clear();
            this.Find<StackPanel>("console").Children.Add(mes);
        }

        private void ClearTextBoxes(){
            NameField.Text = "";
            UrlField.Text = "";
            DescriptionField.Text = "";
        }

        private bool CheckFields(){
            return (NameField.Text != null && NameField.Text != "" && UrlField.Text != null && UrlField.Text != "");
        }

        public void TabListGotFocus(object sender, GotFocusEventArgs e){
            GetAllDocuments();
        }
    }
}