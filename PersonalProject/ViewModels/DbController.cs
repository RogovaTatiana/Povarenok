using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalProject.ViewModels
{
    
    public class DbController
    {
        public static int AddMeal(string name, string groupName)
        {
            using (Model1Container db = new Model1Container())
            {
                var groupId = (from groups in db.GroupMeal
                              where groups.Name.Equals(groupName)
                              select groups.GroupMealPK).FirstOrDefault();
                              
                Meal newM = db.Meal.Where(c => c.Name == name).FirstOrDefault();
                newM = new Meal() { Name = name, GroupMealFK = groupId};
                db.Meal.Add(newM);
                db.SaveChanges();
                db.MealProducts.Add(new MealProducts() { MealFK = newM.MealPK });
                db.SaveChanges();
                return newM.MealPK;
            }
        }

        public static void AddMealProduct(Nullable<int> idMeal, string name, Nullable<long> quant, int id)
        {
            using (Model1Container db = new Model1Container())
            {
                Nullable<int> prodId;
                if (name!=null)
                {
                    prodId = (from prods in db.Product
                                  where prods.Name.Equals(name)
                                  select prods.ProductPK).FirstOrDefault();
                }
                else
                    prodId = null;
                if (id == 0)
                {
                    db.MealProducts.Add(new MealProducts()
                    {
                        MealFK = idMeal,
                        ProductFK = prodId,
                        Quantity = quant
                    });
                    db.SaveChanges();
                }
                else
                {
                    MealProducts mp = db.MealProducts.Where(c => c.MealProducts1 == id).FirstOrDefault();
                    mp.MealFK = idMeal;
                    mp.ProductFK = prodId;
                    mp.Quantity = quant;
                    db.SaveChanges();
                }
            }
        }

        public static int AddMenu(DateTime dateSt, DateTime dateEn)
        {
            using (Model1Container db = new Model1Container())
            {
                Menu newM = new Menu() { DateStart = dateSt, DateFinish = dateEn };
                db.Menu.Add(newM);
                db.SaveChanges();
                AddMenuDay((Nullable<int>)newM.MenuPK, dateSt);
                return newM.MenuPK;
            }
        }

        public static void AddMenuDay(Nullable<int> idMenu, DateTime dateSt)
        {
            string[] days = new string[7];
            days = Enum.GetNames(typeof(Days));
            using (Model1Container db = new Model1Container())
            {
                MenuDay[] newMD = new MenuDay[7];
                for (int i = 0; i < 7; i++)
                {
                    newMD[i] = new MenuDay() { MenuFK = idMenu, Date_ = dateSt.AddDays(i), Day = days[i] };
                    db.MenuDay.Add(newMD[i]);
                    db.SaveChanges();
                }
            }
        }

        public static void ChangeMenuDay(Nullable<int> idMenu, string day, int[] portions, Nullable<int> br, Nullable<int> dn1, Nullable<int> dn2, Nullable<int> dn3, Nullable<int> sp)
        {
            using (Model1Container db = new Model1Container())
            {
                MenuDay newMD = (from md in db.MenuDay
                                 where ((md.MenuFK == idMenu) && (md.Day == day))
                                 select md).FirstOrDefault();
                newMD.BreakfFK = br;
                newMD.Dinner1FK = dn1;
                newMD.Dinner2FK = dn2;
                newMD.Dinner3FK = dn3;
                newMD.SupperFK = sp;
                newMD.QuantPortionsB = portions[0];
                newMD.QuantPortionsD = portions[1];
                newMD.QuantPortionsS = portions[2];
                db.SaveChanges();
            }
        }

        public static void ChangeBreakfast(Nullable<int> idMenu, string day, string br)
        {
            using (Model1Container db = new Model1Container())
            {
                MenuDay newMD = (from md in db.MenuDay
                                 where ((md.MenuFK == idMenu) && (md.Day == day))
                                 select md).FirstOrDefault();
                var brFk = (from m in db.Meal
                            where m.Name.Equals(br)
                            select m.MealPK).FirstOrDefault();
                newMD.BreakfFK = brFk;
                db.SaveChanges();
            }
        }

        public static void ChangeDinnerF(Nullable<int> idMenu, string day, string dn1)
        {
            using (Model1Container db = new Model1Container())
            {
                MenuDay newMD = (from md in db.MenuDay
                                 where ((md.MenuFK == idMenu) && (md.Day == day))
                                 select md).FirstOrDefault();
                var dn1Fk = (from m in db.Meal
                            where m.Name.Equals(dn1)
                            select m.MealPK).FirstOrDefault();
                newMD.Dinner1FK = dn1Fk;
                db.SaveChanges();
            }
        }

        public static void ChangeDinnerG(Nullable<int> idMenu, string day, string dn2)
        {
            using (Model1Container db = new Model1Container())
            {
                MenuDay newMD = (from md in db.MenuDay
                                 where ((md.MenuFK == idMenu) && (md.Day == day))
                                 select md).FirstOrDefault();
                var dn2Fk = (from m in db.Meal
                            where m.Name.Equals(dn2)
                            select m.MealPK).FirstOrDefault();
                newMD.Dinner2FK = dn2Fk;
                db.SaveChanges();
            }
        }

        public static void ChangeDinnerM(Nullable<int> idMenu, string day, string dn3)
        {
            using (Model1Container db = new Model1Container())
            {
                MenuDay newMD = (from md in db.MenuDay
                                 where ((md.MenuFK == idMenu) && (md.Day == day))
                                 select md).FirstOrDefault();
                var dn3Fk = (from m in db.Meal
                             where m.Name.Equals(dn3)
                            select m.MealPK).FirstOrDefault();
                newMD.Dinner3FK = dn3Fk;
                db.SaveChanges();
            }
        }

        public static void ChangeSupper(Nullable<int> idMenu, string day, string sp)
        {
            using (Model1Container db = new Model1Container())
            {
                MenuDay newMD = (from md in db.MenuDay
                                 where ((md.MenuFK == idMenu) && (md.Day == day))
                                 select md).FirstOrDefault();
                var spFk = (from m in db.Meal
                            where m.Name.Equals(sp)
                            select m.MealPK).FirstOrDefault();
                newMD.SupperFK = spFk;
                db.SaveChanges();
            }
        }

        public static void AddProductList(Nullable<int> idMenu)
        {
            using (Model1Container db = new Model1Container())
            {
                var prL = (from p in db.ProductList
                           where p.MenuFK == idMenu
                           select p.MenuFK).FirstOrDefault();
                if (prL == 0)
                {
                    var menuDay = (from md in db.MenuDay
                                   where md.MenuFK == idMenu
                                   select md).ToList<MenuDay>();
                    List<T> mealList = new List<T>();
                    foreach (var p in menuDay)
                    {
                        mealList.Add(new T() { quant = (double)(double)(p.QuantPortionsB / p.Meal.Portions), meal = p.Meal });
                        mealList.Add(new T() { quant = (double)((double)p.QuantPortionsD / p.Meal1.Portions), meal = p.Meal1 });
                        mealList.Add(new T() { quant = (double)((double)p.QuantPortionsD / p.Meal2.Portions), meal = p.Meal2 });
                        mealList.Add(new T() { quant = (double)((double)p.QuantPortionsD / p.Meal3.Portions), meal = p.Meal3 });
                        mealList.Add(new T() { quant = (double)((double)p.QuantPortionsS / p.Meal4.Portions), meal = p.Meal4 });
                    }
                    foreach (var m in mealList)
                    {
                        var prList = (from mp in db.MealProducts
                                      where mp.MealFK == m.meal.MealPK
                                      select new
                                      {
                                          ProdID = mp.ProductFK,
                                          ProdQ = mp.Quantity * m.quant
                                      }).ToList();
                        foreach (var pl in prList)
                        {
                            ProductList prodList = (from p in db.ProductList
                                                    where p.MenuFK == idMenu && p.ProductFK == pl.ProdID
                                                    select p).FirstOrDefault();
                            if (prodList != null)
                                prodList.Quantity += (Nullable<decimal>)pl.ProdQ;
                            else
                            {
                                prodList = new ProductList() { MenuFK = (int)idMenu, ProductFK = pl.ProdID, Quantity = (Nullable<decimal>)pl.ProdQ };
                                db.ProductList.Add(prodList);
                            }
                            db.SaveChanges();
                        }
                    }
                }
            }
        }
    }
}
