//------------------------------------------------------------------------------
// <auto-generated>
//    Этот код был создан из шаблона.
//
//    Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//    Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PersonalProject
{
    using System;
    using System.Collections.Generic;
    
    public partial class Meal
    {
        public Meal()
        {
            this.MealProducts = new HashSet<MealProducts>();
            this.MenuDay = new HashSet<MenuDay>();
            this.MenuDay1 = new HashSet<MenuDay>();
            this.MenuDay2 = new HashSet<MenuDay>();
            this.MenuDay3 = new HashSet<MenuDay>();
            this.MenuDay4 = new HashSet<MenuDay>();
        }
    
        public int MealPK { get; set; }
        public string Name { get; set; }
        public string Recept { get; set; }
        public Nullable<int> ReadyTime { get; set; }
        public Nullable<int> GroupMealFK { get; set; }
        public int Portions { get; set; }
    
        public virtual GroupMeal GroupMeal { get; set; }
        public virtual ICollection<MealProducts> MealProducts { get; set; }
        public virtual ICollection<MenuDay> MenuDay { get; set; }
        public virtual ICollection<MenuDay> MenuDay1 { get; set; }
        public virtual ICollection<MenuDay> MenuDay2 { get; set; }
        public virtual ICollection<MenuDay> MenuDay3 { get; set; }
        public virtual ICollection<MenuDay> MenuDay4 { get; set; }
    }
}
