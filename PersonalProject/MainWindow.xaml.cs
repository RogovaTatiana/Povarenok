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
using System.Collections.ObjectModel;
using PersonalProject.ViewModels;
using PersonalProject.Views;
using System.Reflection;
using DevExpress.Xpf.Grid;
using DevExpress.Xpf.Printing;
using DevExpress.Xpf.NavBar;

namespace PersonalProject
{
    enum Days { Понедельник, Вторник, Среда, Четверг, Пятница, Суббота, Воскресенье };

    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            // Выбор недели в для формирования меню

            CB1.SelectedIndex = 0;
            sel = CB1.SelectedIndex;

            // Выбор недели в для формирования списка
            CB2.SelectedIndex = 0;
            selList = CB2.SelectedIndex;

            // ВьюМодел главного окна (передаем выбранные недели)
            viewModelMain = new MainViewModel(sel, selList);
            this.DataContext = viewModelMain;

            // ViewModel для пользовательского элемента "повареная книга"
            bookViewModel = new MenuBookViewModel();
            MBV1.DataContext = bookViewModel;
            MBV1.TreeMeals.SelectedItemChanged += TreeView_OnSelectedItemChanged;

            // ViewModel для пользовательского элемента "просмотр блюда"
            showMealViewModel = new ShowMealViewModel(0);
            VM1.DataContext = showMealViewModel;
            VM1.Visibility = Visibility.Hidden;
            VM1.save.Click += Button_Click;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DevExpress.Xpf.Core.ThemeManager.ApplicationThemeName = "Office2013"; // Тема оформления
        }

        MainViewModel viewModelMain;
        MenuBookViewModel bookViewModel;
        ShowMealViewModel showMealViewModel;

        int sel;
        int selList;
        int[,] portions;
        List<int> groupsBr;
        List<int> groupsDn1;
        List<int> groupsDn2;
        List<int> groupsDn3;
        List<int> groupsSp;

        private void TreeView_OnSelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            MealShow treeViewItem;
            if (e.NewValue is MealShow)
            {
                treeViewItem = e.NewValue as MealShow;
                bookViewModel.SelectedId = treeViewItem.ID;
                showMealViewModel = new ShowMealViewModel(bookViewModel.SelectedId);
                VM1.DataContext = showMealViewModel;
                VM1.Visibility = Visibility.Visible;
            }
            else
                VM1.Visibility = Visibility.Hidden;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            foreach (var p in showMealViewModel.Ingreds)
            {
                DbController.AddMealProduct(showMealViewModel.Id, p.Name, p.Quantity, p.ID);
            }
        }

        private void Generate(object sender, RoutedEventArgs e)
        {
            GetPortions();
            GetGroupsBr();
            GetGroupsDn1();
            GetGroupsDn2();
            GetGroupsDn3();
            GetGroupsSp();
            viewModelMain.MakeMenu(portions, groupsBr, groupsDn1, groupsDn2, groupsDn3, groupsSp);
            viewModelMain = new MainViewModel(sel, selList);
            this.DataContext = viewModelMain;
        }

        public void GetGroupsBr()
        {
            groupsBr = new List<int>();
            ObservableCollection<object> ListBrItems = ListBr.SelectedItems;
            foreach (var lbi in ListBrItems)
            {
                GroupMeal gm = lbi as GroupMeal;
                groupsBr.Add(gm.GroupMealPK);
            }
        }

        public void GetGroupsDn1()
        {
            groupsDn1 = new List<int>();
            ObservableCollection<object> ListDn1Items = ListDn1.SelectedItems;
            foreach (var lbi in ListDn1Items)
            {
                GroupMeal gm = lbi as GroupMeal;
                groupsDn1.Add(gm.GroupMealPK);
            }
        }

        public void GetGroupsDn2()
        {
            groupsDn2 = new List<int>();
            ObservableCollection<object> ListDn2Items = ListDn2.SelectedItems;
            foreach (var lbi in ListDn2Items)
            {
                GroupMeal gm = lbi as GroupMeal;
                groupsDn2.Add(gm.GroupMealPK);
            }
        }

        public void GetGroupsDn3()
        {
            groupsDn3 = new List<int>();
            ObservableCollection<object> ListDn3Items = ListDn3.SelectedItems;
            foreach (var lbi in ListDn3Items)
            {
                GroupMeal gm = lbi as GroupMeal;
                groupsDn3.Add(gm.GroupMealPK);
            }
        }

        public void GetGroupsSp()
        {
            groupsSp = new List<int>();
            ObservableCollection<object> ListSpItems = ListSp.SelectedItems;
            foreach (var lbi in ListSpItems)
            {
                GroupMeal gm = lbi as GroupMeal;
                groupsSp.Add(gm.GroupMealPK);
            }
        }

        public void GetPortions()
        {
            portions = new int[7,3];
            portions[0, 0] = Convert.ToInt32(tb11.Text);
            portions[1, 0] = Convert.ToInt32(tb12.Text);
            portions[2, 0] = Convert.ToInt32(tb13.Text);
            portions[3, 0] = Convert.ToInt32(tb14.Text);
            portions[4, 0] = Convert.ToInt32(tb15.Text);
            portions[5, 0] = Convert.ToInt32(tb16.Text);
            portions[6, 0] = Convert.ToInt32(tb17.Text);
            portions[0, 1] = Convert.ToInt32(tb21.Text);
            portions[1, 1] = Convert.ToInt32(tb22.Text);
            portions[2, 1] = Convert.ToInt32(tb23.Text);
            portions[3, 1] = Convert.ToInt32(tb24.Text);
            portions[4, 1] = Convert.ToInt32(tb25.Text);
            portions[5, 1] = Convert.ToInt32(tb26.Text);
            portions[6, 1] = Convert.ToInt32(tb27.Text);
            portions[0, 2] = Convert.ToInt32(tb31.Text);
            portions[1, 2] = Convert.ToInt32(tb32.Text);
            portions[2, 2] = Convert.ToInt32(tb33.Text);
            portions[3, 2] = Convert.ToInt32(tb34.Text);
            portions[4, 2] = Convert.ToInt32(tb35.Text);
            portions[5, 2] = Convert.ToInt32(tb36.Text);
            portions[6, 2] = Convert.ToInt32(tb37.Text);
        }

        void mi_Click(object sender, RoutedEventArgs e)
        {
            MenuItem mi = sender as MenuItem;
            ContextMenu cm = mi.Parent as ContextMenu;
            DataGridCell curCell = (DataGridCell)cm.PlacementTarget;
            string colName = (string)curCell.Column.Header;
            MenuDays md = (MenuDays)curCell.DataContext;
            ChoseMealView mbv = new ChoseMealView(md, viewModelMain.Id, colName);
            mbv.ShowDialog();
            viewModelMain = new MainViewModel(sel, selList);
            this.DataContext = viewModelMain;
        }

        void do_Click(object sender, MouseButtonEventArgs e)
        {
            DataGrid mi = sender as DataGrid;
            DataGridCellInfo curCell=(DataGridCellInfo)mi.CurrentCell;
            string colName = (string)curCell.Column.Header;
            if (!colName.Equals("День"))
            {
                MenuDays md = (MenuDays)curCell.Item;
                ChoseMealView mbv = new ChoseMealView(md, viewModelMain.Id, colName);
                mbv.ShowDialog();
                viewModelMain = new MainViewModel(sel, selList);
                this.DataContext = viewModelMain;
            }
        }

        void add_Click(object sender, RoutedEventArgs e)
        {
            string groupName = "Супы";
            int mealId = DbController.AddMeal("Новое блюдо", groupName);
            AddMealView am = new AddMealView(mealId);
            am.ShowDialog();
            bookViewModel = new MenuBookViewModel();
            MBV1.TreeMeals.DataContext = bookViewModel;
        }

        private void ComboBoxEdit_SelectedIndexChanged(object sender, RoutedEventArgs e)
        {
            sel = CB1.SelectedIndex;
            viewModelMain = new MainViewModel(sel, selList);
            this.DataContext = viewModelMain;
        }

        private void ListProdCB_SelectedIndexChanged(object sender, RoutedEventArgs e)
        {
            selList = CB2.SelectedIndex;
            viewModelMain = new MainViewModel(sel, selList);
            this.DataContext = viewModelMain;
        }

        private void PreviewGrid(object sender, RoutedEventArgs e)
        {
            list.ShowPrintPreviewDialog(this);
        }
        private void ExportGrid(object sender, RoutedEventArgs e)
        {
            list.ExportToXlsx(@"c:\test\grid_export.xlsx");
        }
    }
}
