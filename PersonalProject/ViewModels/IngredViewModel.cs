using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalProject.ViewModels
{
    public class Ingred : INotifyPropertyChanged
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

        public int ID { get; set; }
        public Nullable<long> Quantity
        { get; set; }

        public string Measurement
        { get; set; }
        public Nullable<int> IdMeal
        { get; set;}

        public string Name
        {get;set;}
    }
    
    class IngredViewModel : ObservableCollection<Ingred>, INotifyPropertyChanged
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

        public IngredViewModel(int id)
        {
            IdMeal = id;
            using (Model1Container db = new Model1Container())
            {
                _ingreds = (from ingreds in db.MealProducts
                            where ingreds.MealFK == IdMeal
                            select new Ingred()
                            {
                                Name = ingreds.Product.Name,
                                ID = ingreds.MealProducts1,
                                IdMeal = this.IdMeal,
                                Quantity = (int)ingreds.Quantity,
                                Measurement = ingreds.Product.Measurement
                            }).ToList<Ingred>();
            }
            foreach (var ing in _ingreds)
            {
                this.Add(ing);
            }
        }
        public Nullable<int> IdMeal { get; set; }
        private List<Ingred> _ingreds;
        
    }
}
