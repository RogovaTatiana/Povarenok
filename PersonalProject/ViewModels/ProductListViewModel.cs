using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalProject.ViewModels
{
    public class T
    {
        public Meal meal;
        public double quant;
    }

    public class ProductList_ : INotifyPropertyChanged
    {
        public string ProductGroup { get; set; }
        public string ProductName { get; set; }
        public double Quantity { get; set; }
        public string Measur { get; set; }

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
    }

    public class ProductListViewModel : ObservableCollection<ProductList_>, INotifyPropertyChanged
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

        #region Properties
       
        #endregion

        public ProductListViewModel(int selList)
        {
            DateTime start = DateTime.Today;
            DateTime finish = DateTime.Today;
            int end;
            switch (selList)
            {
                case 0:
                    start = DateTime.Today.AddDays(1);
                    end = Convert.ToInt32(DateTime.Now.DayOfWeek) + 1;
                    finish = DateTime.Today.AddDays(7 - end);
                    break;
                case 1:
                    int st = Convert.ToInt32(DateTime.Now.DayOfWeek);
                    start = DateTime.Today.AddDays(8 - st);
                    finish = start.AddDays(6);
                    break;
                case 2:
                    start = DateTime.Today.AddDays(1);
                    finish = DateTime.Today.AddDays(1);
                    break;
            }

            using (Model1Container db = new Model1Container())
            {
                var pr = (from md in db.MenuDay
                          where md.Date_ >= start && md.Date_ <= finish
                           select md).ToList<MenuDay>();
                List<T> mealList=this.GetMealList(pr);
                this.GetProductList(mealList);
            }
        }

        public List<T> GetMealList(List<MenuDay> pr)
        {
            List<T> mealList = new List<T>();
            foreach (var p in pr)
            {
                if (p.BreakfFK!=null)
                    mealList.Add(new T() { quant = (double)((double)p.QuantPortionsB / p.Meal.Portions), meal = p.Meal });
                if (p.Dinner1FK!=null)
                    mealList.Add(new T() { quant = (double)((double)p.QuantPortionsD / p.Meal1.Portions), meal = p.Meal1 });
                if (p.Dinner2FK!=null)
                    mealList.Add(new T() { quant = (double)((double)p.QuantPortionsD / p.Meal2.Portions), meal = p.Meal2 });
                if (p.Dinner3FK!=null)
                    mealList.Add(new T() { quant = (double)((double)p.QuantPortionsD / p.Meal3.Portions), meal = p.Meal3 });
                if (p.SupperFK!=null)
                    mealList.Add(new T() { quant = (double)((double)p.QuantPortionsS / p.Meal4.Portions), meal = p.Meal4 });
            }
            return mealList;
        }

        public void GetProductList(List<T> mealList)
        {
            foreach (var m in mealList)
            {
                using (Model1Container db = new Model1Container())
                {
                    var prList = (from mp in db.MealProducts
                                  where mp.MealFK == m.meal.MealPK
                                  select new ProductList_
                                  {
                                      ProductGroup = mp.Product.GroupProduct.Name,
                                      ProductName = mp.Product.Name,
                                      Quantity = (double)mp.Quantity * m.quant,
                                      Measur = mp.Product.Measurement
                                  }).ToList();
                    foreach (var pl in prList)
                    {
                        ProductList_ prodList = (from p in this
                                                 where p.ProductName == pl.ProductName
                                                 select p).FirstOrDefault();
                        if (prodList != null)
                            prodList.Quantity += (double)pl.Quantity;
                        else
                            this.Add(pl);
                    }
                }
            }
        }

    }
}
