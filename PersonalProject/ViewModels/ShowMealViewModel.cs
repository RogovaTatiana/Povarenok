using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalProject.ViewModels
{
  
    
    class ShowMealViewModel : INotifyPropertyChanged
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

        public ShowMealViewModel(int id)
        {
            Id = id;
            using (Model1Container db = new Model1Container())
            {
                var _groups = (from groups in db.GroupMeal
                               select groups.Name).ToList<string>();
                Groups = new ObservableCollection<string>(_groups);
            }
            Ingreds = new IngredViewModel(id);
            using (Model1Container db = new Model1Container())
            {
                _products = new ObservableCollection<Product>((from prods in db.Product
                                                                                             select prods).ToList());
            }
        }
        public ObservableCollection<string> Groups { get; set; }

        private ObservableCollection<Product> _products;
        public ObservableCollection<Product> Products
        {
            get
            {
                return _products;
            }
            set
            {
                _products = value;
            }
        }

        private IngredViewModel ingreds;
        public IngredViewModel Ingreds
        {
            get
            {
                return ingreds;
            }
            set
            {
                ingreds = value;
            }
        }
        public int Id { get; set; }

        public int Portions
        {
            get
            {
                using (Model1Container db = new Model1Container())
                {
                    var _portions = (from meals in db.Meal
                                 where meals.MealPK == Id
                                 select meals.Portions).FirstOrDefault();
                    return _portions;
                }
            }
            set
            {
                using (Model1Container db = new Model1Container())
                {
                    Meal newM = db.Meal.Where(c => c.MealPK == Id).FirstOrDefault();
                    newM.Portions = value;
                    db.SaveChanges();
                }
                OnPropertyChanged("Portions");
            }
        }
        public string Name
        {
            get
            {
                using (Model1Container db = new Model1Container())
                {
                    var _name = (from meals in db.Meal
                                 where meals.MealPK == Id
                                 select meals.Name).FirstOrDefault();
                    return _name;
                }
            }
            set
            {
                using (Model1Container db = new Model1Container())
                {
                    Meal newM = db.Meal.Where(c => c.MealPK == Id).FirstOrDefault();
                    newM.Name = value;
                    db.SaveChanges();
                }
                OnPropertyChanged("Name");
            }
        }
        
        public string Group
        {
            get
            {
                using (Model1Container db = new Model1Container())
                {
                    var _group = (from meals in db.Meal
                                   where meals.MealPK == Id
                                   select meals.GroupMeal.Name).FirstOrDefault();
                    return _group;
                }
            }
            set
            {
                using (Model1Container db = new Model1Container())
                {
                    Meal newM = db.Meal.Where(c => c.MealPK == Id).FirstOrDefault();
                    var gr = (from groups in db.GroupMeal
                              where groups.Name == value
                              select groups.GroupMealPK).FirstOrDefault();
                    newM.GroupMealFK = gr;
                    db.SaveChanges();
                }
                OnPropertyChanged("Group");
            }
        }

        public string Recept
        {
            get
            {
                using (Model1Container db = new Model1Container())
                {
                    var _recept = (from meals in db.Meal
                                 where meals.MealPK == Id
                                 select meals.Recept).FirstOrDefault();
                    return _recept;
                }
            }
            set
            {
                using (Model1Container db = new Model1Container())
                {
                    Meal newM = db.Meal.Where(c => c.MealPK == Id).FirstOrDefault();
                    newM.Recept = value;
                    db.SaveChanges();
                }
                OnPropertyChanged("Recept");
            }
        }

        public Nullable<int> ReadyTime
        {
            get
            {
                using (Model1Container db = new Model1Container())
                {
                    var _time = (from meals in db.Meal
                                   where meals.MealPK == Id
                                   select meals.ReadyTime).FirstOrDefault();
                    return _time;
                }
            }
            set
            {
                using (Model1Container db = new Model1Container())
                {
                    Meal newM = db.Meal.Where(c => c.MealPK == Id).FirstOrDefault();
                    newM.ReadyTime = value;
                    db.SaveChanges();
                }
                OnPropertyChanged("ReadyTime");
            }
        }

    }
}
