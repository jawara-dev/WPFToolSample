using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace tool_turnbased_ai.ViewModel
{
    public class AlgorithmSelectionViewModel :  INotifyPropertyChanged
    {
        public ComboBox  AlgorithmSelection    { get; set; }
        public TextBlock AlgorithmSupportText  { get; set; }
        public TextBlock AlgorithmSupportValue { get; set; }

        public string AlgorithmName                      { get; set; }
        public string AlgorithmSupportTextContent        { get; set; }
        public string AlgorithmSupportTextValueContent   { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public AlgorithmSelectionViewModel()
        {
            this.AlgorithmSelection    = new ComboBox();
            this.AlgorithmSupportText  = new TextBlock();
            this.AlgorithmSupportValue = new TextBlock();                                  
        }

        public void Update()
        {
            // update the support text box
            this.AlgorithmSupportText.Text = this.AlgorithmSupportTextContent;

            // update the support text value
            this.AlgorithmSupportValue.Text = this.AlgorithmSupportTextValueContent;

            // fire event
            this.NotifyPropertyChanged("AlgorithmSelection");
            this.NotifyPropertyChanged("AlgorithmSupportText");
            this.NotifyPropertyChanged("AlgorithmSupportValue");
        }

        public void UpdateComboBoxAnchor(ComboBox comboBox)
        {
            this.SetCombobox(comboBox);
        }

        private void SetCombobox(ComboBox comboBox)
        {
            this.AlgorithmSelection            = comboBox;
            this.AlgorithmSelection.IsEditable = true;
            this.AlgorithmSelection.IsReadOnly = true;
        }

        public void UpdateAlgorithmName(string Content)
        {
            this.SetAlgorithmName(Content);
        }

        private void SetAlgorithmName(string Content)
        {
            this.AlgorithmName = Content;
        }

        public void UpdateAlgorithmSupportTextContent(string Content)
        {
            this.SetAlgorithmSupportTextContent(Content);
        }

        private void SetAlgorithmSupportTextContent(string Content)
        {
            this.AlgorithmSupportTextContent = Content;
        }

        public void UpdateAlgorithmSupportTextValue(string Content)
        {
            this.AlgorithmSupportTextValueContent = Content;
        }

        private void SetAlgorithmSupportTextValue(string Content)
        {
            this.AlgorithmSupportTextValueContent = Content;
        }

        public void UpdateSelectionsList(ref List<string> Items)
        {
            this.SetComboBoxItemsList(ref Items);
        }

        private void SetComboBoxItemsList(ref List<string> Items)
        {
            this.AlgorithmSelection.ItemsSource = Items;
            // disable item 0 
        }

        public void NotifyPropertyChanged(string PropertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }
    }
}
