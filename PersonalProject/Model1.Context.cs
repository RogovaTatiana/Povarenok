﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class Model1Container : DbContext
    {
        public Model1Container()
            : base("name=Model1Container")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<GroupProduct> GroupProduct { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<GroupMeal> GroupMeal { get; set; }
        public DbSet<Meal> Meal { get; set; }
        public DbSet<MealProducts> MealProducts { get; set; }
        public DbSet<Menu> Menu { get; set; }
        public DbSet<MenuDay> MenuDay { get; set; }
        public DbSet<ProductList> ProductList { get; set; }
    }
}
