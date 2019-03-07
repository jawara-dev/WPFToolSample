using System;
using System.ComponentModel;
using System.Diagnostics;

namespace tool_turnbased_ai.ViewModel
{
    public class GridCongifurationViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;       
                
        public int Column { get; private set; }
        public int Row    { get; private set; }

        public GridCongifurationViewModel(int Column, int Row)
        {
            this.Column = Column;
            this.Row    = Row;
        }

        public void UpdateColumn(int Value)
        {
            this.SetColumn(Value);
        }

        public void UpdateRow(int Value)
        {
            this.SetRow(Value);
        }

        private void SetColumn(int Value)
        {
            this.Column = Value;
            this.NotifyPropertyChanged("Column");
            Debug.WriteLine("Update Column Value: " + Value);
        }

        private void SetRow(int Value)
        {
            this.Row = Value;
            this.NotifyPropertyChanged("Row");
            Debug.WriteLine("Update Row Value: " + Value);
        }

        private void NotifyPropertyChanged(String propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}