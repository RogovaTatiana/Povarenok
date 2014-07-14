using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using PersonalProject.Views;

namespace PersonalProject.ViewModels
{
    public class MealGroup
    {
      
        public string Name { get; set; }
        public int ID { get; set; }
        private List<MealShow> _children;
        public ObservableCollection<MealShow> Children 
        {
            get
            {
                using (Model1Container db = new Model1Container())
                {
                    _children = (from meals in db.Meal
                                 where meals.GroupMealFK == ID
                                 select new MealShow()
                                 {
                                     Name = meals.Name,
                                     ID = meals.MealPK
                                 }).ToList<MealShow>();
                }
                return new ObservableCollection<MealShow>(_children);
            }
        }
    }

    public class MenuBookViewModel : INotifyPropertyChanged
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

        public ICommand AddMeal { get; set; }
        public ICommand ViewMeal { get; set; }
        public ICommand CorrectMeal { get; set; }

        #endregion   

        private List<MealGroup> _groups;
        public ObservableCollection<MealGroup> Parent
        {
            get
            {
                using (Model1Container db = new Model1Container())
                {
                    _groups = (from groups in db.GroupMeal
                               select new MealGroup()
                               {
                                   Name = groups.Name,
                                   ID = groups.GroupMealPK
                               }).ToList<MealGroup>();
                }
                return new ObservableCollection<MealGroup>(_groups);
            }
        }

        public MenuBookViewModel()
        {
            using (Model1Container db = new Model1Container())
            {
                _groups = (from groups in db.GroupMeal
                               select new MealGroup()
                               {
                                   Name = groups.Name,
                                   ID = groups.GroupMealPK
                               }).ToList<MealGroup>();
            }

            AddMeal = new Command(arg => AddMealMethod());
            ViewMeal = new Command(arg => ViewMealMethod());
            CorrectMeal = new Command(arg => CorrectMealMethod());

        }
        string selected;
        public string Selected
        {
            get; set;
        }
        public int SelectedId
        {
            get;
            set;
        }

        #region Methods

        public void AddMealMethod()
        {
            AddMealView am = new AddMealView(1);
            am.ShowDialog();
        }

        public void ViewMealMethod()
        {
            AddMealView am = new AddMealView(1);
            am.ShowDialog();
        }

        public void CorrectMealMethod()
        {
            AddMealView am = new AddMealView(1);
            am.ShowDialog();
        }

        #endregion
    }
}
