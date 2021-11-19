using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCN.ComputerComponents
{
    public class SSD : ComputerComponent
    {
        private RelayCommand _filterInfoCommand;

        private string _filterCommand = "";

        private string _maker;
        private string _storage;
        private string _connectionInterface;
        private string _startPrice;
        private string _lastPrice;

        public string Maker
        {
            get => _maker;
            set
            {
                _maker = value;
                OnPropertyChanged(nameof(Maker));
            }
        }

        public string Storage
        {
            get => _storage;
            set
            {
                _storage = value;
                OnPropertyChanged(nameof(Storage));
            }
        }

        public string ConnectionInterface
        {
            get => _connectionInterface;
            set
            {
                _connectionInterface = value;
                OnPropertyChanged(nameof(ConnectionInterface));
            }
        }

        public string StartPrice
        {
            get => _startPrice;
            set
            {
                _startPrice = value;
                OnPropertyChanged(nameof(StartPrice));
            }
        }

        public string LastPrice
        {
            get => _lastPrice;
            set
            {
                _lastPrice = value;
                OnPropertyChanged(nameof(LastPrice));
            }
        }

        public SSD()
        {
            SetImage("ssd.jpg");
            UpdateInfo("SSD накопители");
        }

        private void FilterInfo()
        {
            FilterMaker();
            FilterStorage();
            FilterConnectionInterface();
            FilterPrice();

            if (_filterCommand == "")
                _filterCommand = "select * from [SSD Накопители]";

            FilterTheInfo(_filterCommand);

            _filterCommand = "";
        }

        private void FilterMaker()
        {
            if (!string.IsNullOrWhiteSpace(Maker))
            {
                if (_filterCommand == "")
                    _filterCommand = $"select * from [SSD Накопители] where Производитель like '%{Maker}%'";
                else
                    _filterCommand += $" and Производитель like '%{Maker}%'";
            }
        }

        private void FilterStorage()
        {
            if (!string.IsNullOrWhiteSpace(Storage))
            {
                if (_filterCommand == "")
                    _filterCommand = $"select * from [SSD Накопители] where '{Storage}' = [Объем]";
                else
                    _filterCommand += $" and '{Storage}' = [Объем]";
            }
        }

        private void FilterConnectionInterface()
        {
            if (!string.IsNullOrWhiteSpace(ConnectionInterface))
            {
                if (_filterCommand == "")
                    _filterCommand = $"select * from [SSD Накопители] where [Физический интерфейс] = '{ConnectionInterface}'";
                else
                    _filterCommand += $" and [Физический интерфейс] = '{ConnectionInterface}'";
            }
        }

        private void FilterPrice()
        {
            if (!string.IsNullOrWhiteSpace(StartPrice) && !string.IsNullOrWhiteSpace(LastPrice) && Convert.ToInt32(StartPrice) <= Convert.ToInt32(LastPrice))
            {
                if (_filterCommand == "")
                    _filterCommand = $"select * from [SSD Накопители] where {StartPrice} <= Цена and Цена <= {LastPrice}";
                else
                    _filterCommand += $" and {StartPrice} <= Цена and Цена <= {LastPrice}";
            }
        }

        public RelayCommand FilterInfoCommand
        {
            get => _filterInfoCommand ?? (_filterInfoCommand = new RelayCommand(obj => FilterInfo()));
        }
    }
}
