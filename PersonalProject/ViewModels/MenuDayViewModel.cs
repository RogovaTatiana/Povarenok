using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalProject.ViewModels
{
    public class MenuDays : INotifyPropertyChanged
    {
        #region OnProperty
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion

        public string Day { get; set; }
        public string Breakfast { get; set; }
        public string DinnerFirst { get; set; }
        public string DinnerGarn { get; set; }
        public string DinnerMain { get; set; }
        public string Supper { get; set; }
    }

    public class MenuDayViewModel : ObservableCollection<MenuDays>, INotifyPropertyChanged
    {
        #region OnProperty
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion

        public Nullable<int> IdMenu { get; set; }
        private List<MenuDays> _menuDays;

        public MenuDayViewModel(int id)
        {
            IdMenu = id;
            using (Model1Container db = new Model1Container())
            {
                _menuDays = (from menus in db.MenuDay
                             where menus.MenuFK == IdMenu
                             select new MenuDays()
                            {
                                Day = menus.Day,
                                Breakfast = menus.Meal.Name,
                                DinnerFirst = menus.Meal1.Name,
                                DinnerGarn = menus.Meal2.Name,
                                DinnerMain = menus.Meal3.Name,
                                Supper = menus.Meal4.Name
                            }).ToList<MenuDays>();
            }
            foreach (var menus in _menuDays)
            {
                this.Add(menus);
            }
        }

    }
}
