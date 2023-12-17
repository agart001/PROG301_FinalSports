using SportsLibrary.Interfaces;
using SportsLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsLibrary.ViewModels
{
    public class PersonViewModel : BaseViewModel<BasePerson>
    {
        public PersonViewModel()
        {
        }

        public PersonViewModel(BasePerson data) : base(data)
        {
        }

        public string? FirstName
        {
            get { return Model?.FirstName; }
            set
            {
                if (Model != null)
                {
                    Model.FirstName = value;
                    OnPropertyChanged();
                }
            }
        }

        public string? LastName
        {
            get { return Model?.LastName; }
            set
            {
                if (Model != null)
                {
                    Model.LastName = value;
                    OnPropertyChanged();
                }
            }
        }

        public int? Age
        {
            get { return Model?.Age; }
            set
            {
                if (Model != null)
                {
                    Model.Age = value;
                    OnPropertyChanged();
                }
            }
        }

        public string? Position
        {
            get { return Model?.Position; }
            set
            {
                if (Model != null)
                {
                    Model.Position = value;
                    OnPropertyChanged();
                }
            }
        }

        public ICollection<string>? Duties
        {
            get { return Model?.Duties; }
            set
            {
                if (Model != null)
                {
                    Model.Duties = value;
                    OnPropertyChanged();
                }
            }
        }

        public decimal? Salary
        {
            get { return Model?.Salary; }
            set
            {
                if (Model != null)
                {
                    Model.Salary = value;
                    OnPropertyChanged();
                }
            }
        }

        public void SetFirstName(string firstName)
        {
            if (Model != null)
            {
                Model.SetFirstName(firstName);
                OnPropertyChanged(nameof(FirstName));
            }
        }

        public void SetLastName(string lastName)
        {
            if (Model != null)
            {
                Model.SetLastName(lastName);
                OnPropertyChanged(nameof(LastName));
            }
        }

        public void SetAge(int age)
        {
            if (Model != null)
            {
                Model.SetAge(age);
                OnPropertyChanged(nameof(Age));
            }
        }

        public void SetPosition(string position)
        {
            if (Model != null)
            {
                Model.SetPosition(position);
                OnPropertyChanged(nameof(Position));
            }
        }

        public void SetDuties(ICollection<string> duties)
        {
            if (Model != null)
            {
                Model.SetDuties(duties);
                OnPropertyChanged(nameof(Duties));
            }
        }

        public void SetSalary(decimal salary)
        {
            if (Model != null)
            {
                Model.SetSalary(salary);
                OnPropertyChanged(nameof(Salary));
            }
        }
    }
}
