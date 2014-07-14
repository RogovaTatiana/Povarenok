using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalProject.ViewModels
{
    class TodayMenuViewModel
    {
        public TodayMenuViewModel()
        {
            using (Model1Container db = new Model1Container())
            {
                DateTime yesterday = DateTime.Now.AddDays(-1);
                DateTime today = DateTime.Now;

                var todayMenu = (from t in db.MenuDay
                                 where t.Date_ >= yesterday && t.Date_ <= today
                                 select t).FirstOrDefault();
                if (todayMenu.Meal!= null)
                {
                    BreakfastName = todayMenu.Meal.Name;
                    BreakfastRecept = todayMenu.Meal.Recept;
                    BreakfastIngred = "";
                    foreach (var ing in todayMenu.Meal.MealProducts.ToList())
                    {
                        BreakfastIngred += ing.Product.Name + " - " + ing.Quantity + " " + ing.Product.Measurement + "\r\n";
                    }

                }
                else
                {
                    BreakfastName = "";
                    BreakfastRecept = "";
                    BreakfastIngred = "";
                }
                if (todayMenu.Meal1!= null)
                {
                    DinnerFirst = todayMenu.Meal1.Name;
                    DinnerFirstRecept = todayMenu.Meal1.Recept;
                    DinnerFirstIngred = "";
                    foreach (var ing in todayMenu.Meal1.MealProducts.ToList())
                    {
                        DinnerFirstIngred += ing.Product.Name + " - " + ing.Quantity + " " + ing.Product.Measurement + "\r\n";
                    }
                }
                else
                {
                    DinnerFirst = "";
                    DinnerFirstRecept = "";
                    DinnerFirstIngred = "";
                }
                if (todayMenu.Meal2!= null)
                {
                    DinnerGarnir = todayMenu.Meal2.Name;
                    DinnerGarnirRecept = todayMenu.Meal2.Recept;
                    DinnerGarnirIngred = "";
                    foreach (var ing in todayMenu.Meal2.MealProducts.ToList())
                    {
                        DinnerGarnirIngred += ing.Product.Name + " - " + ing.Quantity + " " + ing.Product.Measurement + "\r\n";
                    }
                }
                else
                {
                    DinnerGarnir = "";
                    DinnerGarnirRecept = "";
                    DinnerGarnirIngred = "";
                }
                if (todayMenu.Meal3!= null)
                {
                    DinnerMain = todayMenu.Meal3.Name;
                    DinnerMainRecept = todayMenu.Meal3.Recept;
                    DinnerMainIngred = "";
                    foreach (var ing in todayMenu.Meal3.MealProducts.ToList())
                    {
                        DinnerMainIngred += ing.Product.Name + " - " + ing.Quantity + " " + ing.Product.Measurement + "\r\n";
                    }
                }
                else
                {
                    DinnerMain = "";
                    DinnerMainRecept = "";
                    DinnerMainIngred = "";
                }
                if (todayMenu.Meal4 != null)
                {
                    SupperName = todayMenu.Meal4.Name;
                    SupperRecept = todayMenu.Meal4.Recept;
                    SupperIngred = "";
                    foreach (var ing in todayMenu.Meal4.MealProducts.ToList())
                    {
                        SupperIngred += ing.Product.Name + " - " + ing.Quantity + " " + ing.Product.Measurement + "\r\n";
                    }
                }
                else
                {
                    SupperName = "";
                    SupperRecept = "";
                    SupperIngred = "";
                }
            }
        }

        #region Properties

        public string BreakfastName {get;set;}
        public string BreakfastRecept { get; set; }
        public string BreakfastIngred { get; set; }
        public string DinnerFirst { get; set; }
        public string DinnerFirstRecept { get; set; }
        public string DinnerFirstIngred { get; set; }
        public string DinnerGarnir { get; set; }
        public string DinnerGarnirRecept { get; set; }
        public string DinnerGarnirIngred { get; set; }
        public string DinnerMain { get; set; }
        public string DinnerMainRecept { get; set; }
        public string DinnerMainIngred { get; set; }
        public string SupperName { get; set; }
        public string SupperRecept { get; set; }
        public string SupperIngred { get; set; }

        #endregion
    }
}
