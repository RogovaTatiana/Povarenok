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
using System.Windows.Navigation;
using System.Windows.Shapes;
using PersonalProject.ViewModels;

namespace PersonalProject.Views
{
    /// <summary>
    /// Логика взаимодействия для ViewMeal.xaml
    /// </summary>
    public partial class ViewMeal : UserControl
    {
        public ViewMeal()
        {
            InitializeComponent();
            id = 0;
            viewModel = new ShowMealViewModel(id);
            this.DataContext = viewModel;
        }
        int id;
        ShowMealViewModel viewModel;

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            foreach (var p in viewModel.Ingreds)
            {
                DbController.AddMealProduct(viewModel.Id, p.Name, p.Quantity, p.ID);
            }
        }
    }
}
