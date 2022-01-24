using Avalonia;
using Avalonia.Input;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Interactivity;

using Helper;
using System.Threading.Tasks;

namespace DBApp
{
    public partial class MainWindow : Window
    {
        private TextBox NameField { get; set; }
        private TextBox UrlField { get; set; }
        private TextBox DescriptionField { get; set; }
        private ItemsControl List { get; set; }

        private Driver MongoDriver { get; set; } 

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
            List = this.Find<ItemsControl>("List");
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        private async void GetAllDocuments(){
            List.Items = await MongoDriver.GetAllDocuments();
        }

        public void OnButtonClick(object sender, RoutedEventArgs e){
            Task.Run(async () => await AddDocument());          
        }

        public void TabListGotFocus(object sender, GotFocusEventArgs e){
            GetAllDocuments();
        }

        private async Task AddDocument(){
            var isChecked = CheckFields();

            if(isChecked)
            {
                var item = new Item(NameField.Text, UrlField.Text, DescriptionField.Text);
                await MongoDriver.AddDocument(item);

                ShowMessage("Запись добавлена.", "right");
                ClearTextBoxes();
            }
            else ShowMessage("Поля Имя и URL обязательны.", "wrong");
        }

        private void ShowMessage(string message, string style){
            Label mes = new Label(){
                Content = message,
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

    }
}