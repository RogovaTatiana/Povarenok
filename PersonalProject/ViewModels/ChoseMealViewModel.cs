using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using PersonalProject.Views;

namespace PersonalProject.ViewModels
{
    class ChoseMealViewModel: INotifyPropertyChanged
    {
        #region OnProperty
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string name)
        {
            if (this.PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        #endregion

        #region Commands

        public ICommand ChangeMeal { get; set; }

        #endregion 

        MenuDays currentDay;
        Nullable<int> idMenu;
        string column;

        public ChoseMealViewModel(MenuDays md, Nullable<int> idMenu, string colName)
        {
            ChangeMeal = new Command(arg => ChangeMealMethod());
            currentDay = md;
            this.idMenu = idMenu;
            column = colName;
        }

        public string Selected{ get; set; }

        public void ChangeMealMethod()
        {
            if (Selected != null)
            {
                switch (column)
                {
                    case "Завтрак":
                        DbController.ChangeBreakfast(idMenu, currentDay.Day, Selected);
                        break;
                    case "Обед Первое":
                        DbController.ChangeDinnerF(idMenu, currentDay.Day, Selected);
                        break;
                    case "Обед Гарнир":
                        DbController.ChangeDinnerG(idMenu, currentDay.Day, Selected);
                        break;
                    case "Обед Основное":
                        DbController.ChangeDinnerM(idMenu, currentDay.Day, Selected);
                        break;
                    case "Ужин":
                        DbController.ChangeSupper(idMenu, currentDay.Day, Selected);
                        break;
                }
            }
        }
    }
}
