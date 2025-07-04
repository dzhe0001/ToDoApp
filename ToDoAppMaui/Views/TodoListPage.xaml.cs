using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoAppMaui.Models;
using ToDoAppMaui.Services;

namespace ToDoAppMaui.Views;

public partial class TodoListPage : ContentPage
{
    private User _user;
    private ApiService _api = new();
    
    public TodoListPage(User user)
    {
        InitializeComponent();
        _user = user;
        LoadTodos();
    }

    private async Task LoadTodos()
    {
        try
        {
            var todos = await _api.GetTodos(_user);
            todoList.ItemsSource = todos;
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Failed to load todos: {ex.Message}", "OK");
        }
    }

    private async void OnAddClicked(object? sender, EventArgs e)
    {
        await Navigation.PushAsync(new AddTodoPage(_user));
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await LoadTodos();
    }
}