using System;
using Xamarin.Forms;

namespace TodoASMX
{
    public partial class TodoListPage : ContentPage
    {

        public TodoListPage()
        {
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

          
        }

        async void OnAddItemClicked(object sender, EventArgs e)
        {
          
            var todoPage = new TodoItemPage(true);
          
            await Navigation.PushAsync(todoPage);
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
          
            var todoPage = new TodoItemPage();
          
            await Navigation.PushAsync(todoPage);
        }
    }
}
