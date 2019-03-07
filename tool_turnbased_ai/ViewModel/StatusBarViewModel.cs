using System;
using System.ComponentModel;
using System.Windows.Media;

namespace tool_turnbased_ai.ViewModel
{
    public class StatusBarViewModel : INotifyPropertyChanged
    {
        public string InfoBarText { get; private set; }
        public Brush Background   { get; private set; }
        public event PropertyChangedEventHandler PropertyChanged;

        public StatusBarViewModel(string Content, Brush Brush)
        {
            this.InfoBarText = Content;
            this.Background  = Brush;
        }

        public void UpdateContent(string Content)
        {
            this.SetContent(Content);
        }

        private void SetContent(string Content)
        {
            this.InfoBarText = Content;
            OnPropertyChanged("InfoBarText");
        }

        public void UpdateBrush(Brush brush)
        {
            this.SetBrush(brush);
        }

        private void SetBrush(Brush brush)
        {
            this.Background = brush;
            OnPropertyChanged("Background");
        }

        protected void OnPropertyChanged(String propertyName)
        {
            PropertyChanged?.DynamicInvoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }    
}
