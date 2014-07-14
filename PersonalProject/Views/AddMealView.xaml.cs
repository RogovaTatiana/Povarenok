using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using PersonalProject.ViewModels;

namespace PersonalProject.Views
{
    /// <summary>
    /// Логика взаимодействия для AddMeal.xaml
    /// </summary>
    public partial class AddMealView : Window
    {
        public AddMealView(int id)
        {
            InitializeComponent();
            viewModel = new ShowMealViewModel(id);
            this.DataContext = viewModel;
        }
        ShowMealViewModel viewModel;

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            foreach (var p in viewModel.Ingreds)
            {
                DbController.AddMealProduct(viewModel.Id, p.Name, p.Quantity, p.ID);
            }
            this.Close();
        }
    }
}
