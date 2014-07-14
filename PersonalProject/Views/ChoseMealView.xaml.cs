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
    /// Логика взаимодействия для MealsBookView.xaml
    /// </summary>
    public partial class ChoseMealView : Window
    {
        public ChoseMealView(MenuDays md, Nullable<int> idMenu, string colName)
        {
            InitializeComponent();
            viewModel = new MenuBookViewModel();
            MBV1.DataContext = viewModel;
            view = new ChoseMealViewModel(md, idMenu, colName);
            this.DataContext = view;
            MBV1.TreeMeals.SelectedItemChanged += TreeView_OnSelectedItemChanged;
        }

        MenuBookViewModel viewModel;
        ChoseMealViewModel view;

        private void TreeView_OnSelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (e.NewValue is MealShow)
            {
                MealShow tvi = e.NewValue as MealShow;
                view.Selected = tvi.Name;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }
}
