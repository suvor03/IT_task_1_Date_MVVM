using System;
using System.ComponentModel;
using System.Windows.Input;
using WpfApplicationData.Models;

namespace WpfApplicationData.ViewModels
{
    public class DateViewModel : INotifyPropertyChanged
    {
        private readonly Date _date;

        public int Day
        {
            get => _date.Day;
            set
            {
                _date.Day = value;
                OnPropertyChanged(nameof(Day));
            }
        }

        public int Month
        {
            get => _date.Month;
            set
            {
                _date.Month = value;
                OnPropertyChanged(nameof(Month));
            }
        }

        public int Year
        {
            get => _date.Year;
            set
            {
                _date.Year = value;
                OnPropertyChanged(nameof(Year));
            }
        }

        public ICommand CalculateDaysCommand { get; }
        public string DaysInMonth { get; private set; }

        public ICommand CheckDateValidityCommand { get; }
        public string IsDateValid { get; private set; }

        public ICommand AddDaysMonthsYearsCommand { get; }
        public int DaysToAdd { get; set; }
        public int MonthsToAdd { get; set; }
        public int YearsToAdd { get; set; }
        public string UpdatedDate { get; private set; }

        public event PropertyChangedEventHandler? PropertyChanged;

        public DateViewModel()
        {
            _date = new Date(1, 1, 2024);

            CalculateDaysCommand = new RelayCommand(CalculateDays);
            CheckDateValidityCommand = new RelayCommand(CheckDateValidity);
            AddDaysMonthsYearsCommand = new RelayCommand(AddDaysMonthsYears);
        }

        private void CalculateDays()
        {
            if (_date.Day == 0 || _date.Month == 0 || _date.Year == 0)
            {
                DaysInMonth = "Введите дату!";
                OnPropertyChanged(nameof(DaysInMonth));
            }
            else
            {
                DaysInMonth = _date.CalculateDaysInMonth().ToString();
                OnPropertyChanged(nameof(DaysInMonth));
            }
        }

        private void CheckDateValidity()
        {
            if (_date.Day == 0 || _date.Month == 0 || _date.Year == 0)
            {
                IsDateValid = "Введите дату!";
                OnPropertyChanged(nameof(IsDateValid));
            }
            else
            {
                IsDateValid = _date.IsDateValid();
                OnPropertyChanged(nameof(IsDateValid));
            }
        }

        private void AddDaysMonthsYears()
        {
            if (_date.Day == 0 || _date.Month == 0 || _date.Year == 0)
            {
                UpdatedDate = "Введите дату!";
                OnPropertyChanged(nameof(UpdatedDate));
            }
            else
            {
                _date.AddDaysMonthsYears(DaysToAdd, MonthsToAdd, YearsToAdd);
                UpdatedDate = $"{_date.Day:D2}.{_date.Month:D2}.{_date.Year}";
                OnPropertyChanged(nameof(UpdatedDate));
            }
        }

        private void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }

    public class RelayCommand(Action action) : ICommand
    {
        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            action();
        }
    }
}