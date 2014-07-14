using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalProject.ViewModels
{
    public class MealShow : INotifyPropertyChanged
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
        
        public string Name { get; set; }
        public int ID { get; set; }
    }
    
    class MainViewModel: INotifyPropertyChanged
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

        public MainViewModel(int sel, int selList)
        {
            curWeek = new List<string>();
            curWeek.Add("Текущая неделя");
            curWeek.Add("Следующая неделя");
            curList = new List<string>();
            curList.Add("До конца недели");
            curList.Add("На следующую неделю");
            curList.Add("На завтра");
            Selected = sel;
            SelectedList = selList;
            dateStart = DateStartCount(Selected);
            dateEnd = dateStart.AddDays(6);
            Id = GetMenuId();
            Menus = new MenuDayViewModel(Id);
            Products = new ProductListViewModel(selList);
            Today = new TodayMenuViewModel();
        }

        int portion;
        public int Portion
        {
            get
            {
                return portion;
            }
            set
            {
                portion = value;
                OnPropertyChanged("Portion");
            }
        }

        DateTime dateStart;
        public string DateStart
        {
            get
            {
                
                return dateStart.ToLongDateString();
            }
            set
            {
                dateStart = DateStartCount(Selected);
                OnPropertyChanged("DateStart");
            }
        }

        DateTime dateEnd;
        public string DateEnd
        {
            get
            {
                return dateEnd.ToLongDateString();
            }
            set
            {
                dateEnd = dateStart.AddDays(6);
                OnPropertyChanged("DateEnd");
            }
        }

        private MenuDayViewModel menus;
        public MenuDayViewModel Menus
        {
            get
            {
                return menus;
            }
            set
            {
                menus = value;
            }
        }

        private ProductListViewModel products;
        public ProductListViewModel Products
        {
            get
            {
                return products;
            }
            set
            {
                products = value;
            }
        }

        private TodayMenuViewModel today;
        public TodayMenuViewModel Today
        {
            get
            {
                return today;
            }
            set
            {
                today = value;
                OnPropertyChanged("Today");
            }
        }

        private List<string> curWeek;
        public List<string> CurWeek
        {
            get
            {
                return curWeek;
            }
            set
            {
                curWeek = value;
                OnPropertyChanged("CurWeek");
            }
        }

        private List<string> curList;
        public List<string> CurList
        {
            get
            {
                return curList;
            }
            set
            {
                curList = value;
                OnPropertyChanged("curList");
            }
        }
        private int selected;
        public int Selected
        {
            get
            {
                return selected;
            }
            set
            {
                selected = value;
                OnPropertyChanged("Selected");
            }
        }
        private int selectedList;
        public int SelectedList
        {
            get
            {
                return selectedList;
            }
            set
            {
                selectedList = value;
                OnPropertyChanged("SelectedList");
            }
        }

        private int id;
        public int Id 
        {
            get
            {
                return id;
            }
            set
            { 
                id = GetMenuId();
                OnPropertyChanged("Id");
            }
        }
        
        public DateTime DateStartCount(int selected)
        {
            DateTime today = DateTime.Now.Date;
            DateTime dateStart;
            int week = 7 * selected;
            string todayDay = DateTime.Now.DayOfWeek.ToString();
            switch (todayDay)
            {
                case "Monday":
                    dateStart = today.AddDays(week);
                    break;
                case "Tuesday":
                    dateStart = today.AddDays(-1+week);
                    break;
                case "Wednesday":
                    dateStart = today.AddDays(-2 + week);
                    break;
                case "Thursday":
                    dateStart = today.AddDays(-3 + week);
                    break;
                case "Friday":
                    dateStart = today.AddDays(-4 + week);
                    break;
                case "Saturday":
                    dateStart = today.AddDays(-5 + week);
                    break;
                default:
                    dateStart = today.AddDays(-6 + week);
                    break;
            }
            return dateStart;
        }

        public int GetMenuId()
        {
            using (Model1Container db = new Model1Container())
            {
                var _menu = (from m in db.Menu
                             where m.DateStart == dateStart
                             select m).FirstOrDefault();
                if (_menu != null)
                    return _menu.MenuPK;
                else
                {
                    return DbController.AddMenu(dateStart, dateEnd);
                }

            }
        }

        public void MakeMenu(int[,] portions, List<int> groupsBr, List<int> groupsDn1, List<int> groupsDn2, List<int> groupsDn3, List<int> groupsSp)
        {
            string[] days = new string[7];
            days = Enum.GetNames(typeof(Days));
            Random r;
            for (int k = 0; k < 7; k++)
            {
                using (Model1Container db = new Model1Container())
                {
                    Nullable<int> breakfast = null;
                    if (groupsBr.Count != 0)
                    {
                        List<Meal> breakfasts = (from m in db.Meal
                                                 where groupsBr.Contains((int)m.GroupMealFK)
                                                 select m).ToList<Meal>();
                        r = new Random();
                        breakfast = breakfasts[r.Next(0, breakfasts.Count)].MealPK;
                    }
                    Nullable<int> dinner1 = null;
                    if (groupsDn1.Count != 0)
                    {
                        List<Meal> dinnerFirsts = (from m in db.Meal
                                                   where groupsDn1.Contains((int)m.GroupMealFK)
                                                   select m).ToList<Meal>();
                        r = new Random();
                        dinner1 = dinnerFirsts[r.Next(0, dinnerFirsts.Count)].MealPK;
                    }
                    Nullable<int> dinner2 = null;
                    if (groupsDn2.Count != 0)
                    {
                        List<Meal> dinnerGarns = (from m in db.Meal
                                                  where groupsDn2.Contains((int)m.GroupMealFK)
                                                  select m).ToList<Meal>();
                        r = new Random();
                        dinner2 = dinnerGarns[r.Next(0, dinnerGarns.Count)].MealPK;
                    }
                    Nullable<int> dinner3 = null;
                    if (groupsDn3.Count != 0)
                    {
                        List<Meal> dinnerMains = (from m in db.Meal
                                                  where groupsDn3.Contains((int)m.GroupMealFK)
                                                  select m).ToList<Meal>();
                        r = new Random();
                        dinner3 = dinnerMains[r.Next(0, dinnerMains.Count)].MealPK;
                    }
                    Nullable<int> supper = null;
                    if (groupsSp.Count != 0)
                    {
                        List<Meal> suppers = (from m in db.Meal
                                              where groupsSp.Contains((int)m.GroupMealFK)
                                              select m).ToList<Meal>();
                        r = new Random();
                        supper = suppers[r.Next(0, suppers.Count)].MealPK;
                    }
                    int[] ports = new int[3];
                    for (int i = 0; i < 3; i++)
                        ports[i] = portions[k, i];

                    DbController.ChangeMenuDay(Id, days[k], ports, breakfast, dinner1, dinner2, dinner3, supper);
                }
            }
            menus = new MenuDayViewModel(Id);
        }

    }
}
