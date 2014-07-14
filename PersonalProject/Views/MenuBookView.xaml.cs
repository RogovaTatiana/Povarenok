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
    /// Логика взаимодействия для MenuBookView.xaml
    /// </summary>
    public partial class MenuBookView : UserControl
    {
        public MenuBookView()
        {
            InitializeComponent();
            viewModel = new MenuBookViewModel();
            TreeMeals.DataContext = viewModel;
        }

        MenuBookViewModel viewModel;

        private void TreeView_OnSelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (e.NewValue is MealShow)
            {
                MealShow tvi = e.NewValue as MealShow;
                viewModel.Selected = tvi.Name;
            }
        }

        void mi_Click(object sender, RoutedEventArgs e)
        {
            MenuItem mi = sender as MenuItem;
            ContextMenu cm = mi.Parent as ContextMenu;
            TextBlock curTvi = (TextBlock)cm.PlacementTarget;
            string groupName = curTvi.Text;
            int mealId = DbController.AddMeal("Новое блюдо", groupName);
            AddMealView am = new AddMealView(mealId);
            am.ShowDialog();
            viewModel = new MenuBookViewModel();
            TreeMeals.DataContext = viewModel;
        }

        void mi_View(object sender, RoutedEventArgs e)
        {
            MenuItem mi = sender as MenuItem;
            ContextMenu cm = mi.Parent as ContextMenu;
            TextBlock curTvi = (TextBlock)cm.PlacementTarget;
            int mealId;
            using (Model1Container db = new Model1Container())
            {
                mealId = (from meal in db.Meal
                          where meal.Name.Equals(curTvi.Text)
                          select meal.MealPK).FirstOrDefault();
            }
            AddMealView am = new AddMealView(mealId);
            am.ShowDialog();
        }

        void mi_Corr(object sender, RoutedEventArgs e)
        {
            MenuItem mi = sender as MenuItem;
            ContextMenu cm = mi.Parent as ContextMenu;
            TextBlock curTvi = (TextBlock)cm.PlacementTarget;
            int mealId;
            using (Model1Container db = new Model1Container())
            {
                mealId = (from meal in db.Meal
                          where meal.Name.Equals(curTvi.Text)
                          select meal.MealPK).FirstOrDefault();
            }
            AddMealView am = new AddMealView(mealId);
            am.ShowDialog();
            viewModel = new MenuBookViewModel();
            TreeMeals.DataContext = viewModel;
        }
    }
}
