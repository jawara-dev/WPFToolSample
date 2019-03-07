using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using tool_turnbased_ai.ViewModel;
using tool_turnbased_ai.ViewModel.WindowViewModels;
using tool_turnbased_ai.Event;
using System.Text.RegularExpressions;

namespace tool_turnbased_ai 
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static readonly Regex NumericStringValidator = new Regex("[^0-9]");

        public static readonly RoutedEvent ChangeApplicationStateEvent   = EventManager.RegisterRoutedEvent("ChangeApplicationState", RoutingStrategy.Direct, typeof(RoutedEventHandler), typeof(MainWindow));
        public static readonly RoutedEvent UpdateRowColumnEvent          = EventManager.RegisterRoutedEvent("UpdateColumnEvent", RoutingStrategy.Direct, typeof(RoutedEventWithDataHandler), typeof(MainWindow));
       // public static readonly RoutedEvent UpdateRowEvent                = EventManager.RegisterRoutedEvent("UpdateRowEvent", RoutingStrategy.Direct, typeof(RoutedEventWithDataHandler), typeof(MainWindow));
        public static readonly RoutedEvent UpdateAlgorithmSelectionEvent = EventManager.RegisterRoutedEvent("UpdateAlgorithmSelectionEvent", RoutingStrategy.Direct, typeof(RoutedEventWithDataHandler), typeof(MainWindow));

        public event RoutedEventHandler ChangeApplicationState
        {
            add    { AddHandler(ChangeApplicationStateEvent, value); }
            remove { RemoveHandler(ChangeApplicationStateEvent, value); }
        }

        public event RoutedEventWithDataHandler UpdateRowAndColumn
        {
            add    { AddHandler(UpdateRowColumnEvent, value); }
            remove { RemoveHandler(UpdateRowColumnEvent, value); }
        }

        public event RoutedEventWithDataHandler UpdateAlgorithmSelection
        {
            add    { AddHandler(UpdateAlgorithmSelectionEvent, value); }
            remove { RemoveHandler(UpdateAlgorithmSelectionEvent, value); }
        }

        public MainWindow()
        {
            InitializeComponent();
        }        

        private void ApplicationStateChangeHandler(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            RoutedEventArgs routedEventArgs = new RoutedEventArgs(ChangeApplicationStateEvent);
            RaiseEvent(routedEventArgs);
            e.Handled = true;
        }

        private void TextInputHandler(object sender, KeyEventArgs keyEventArgs)
        {
            UIElement Sender  = (UIElement)sender;
            string SenderName = Sender.GetValue(FrameworkElement.NameProperty) as string;
            string Content    = (sender as TextBox).Text;
            RoutedEventWithDataArgs routedEventArgsWithDataArgs = new RoutedEventWithDataArgs(UpdateRowColumnEvent, 
                                                                                              this.PackageStringEnvelope(SenderName, Content)
                                                                                             );

            // Receipt Data
            Debug.WriteLine("");
            Debug.WriteLine("Sender: "  + SenderName);
            Debug.WriteLine("Content: " + Content);
            Debug.WriteLine("");
            // Receipt Data

            if (keyEventArgs.Key == Key.Return)
            {
                RaiseEvent((RoutedEventArgs)routedEventArgsWithDataArgs);
            }

            keyEventArgs.Handled = true;            
        }

        // preview text box input, restrict non numeric, validation
        public void LiveTextValidation(object sender, TextCompositionEventArgs textCompositionEventArgs)
        {
            textCompositionEventArgs.Handled = !this.InputValidator(textCompositionEventArgs.Text);
        }

        private bool InputValidator(string Input)
        {
            return !NumericStringValidator.IsMatch(Input);
        }

        private string PackageStringEnvelope(string SenderName, string Content)
        {
            return SenderName + ";" + Content;
        }

        private void UpdateAlgorithmSelectionHandler(object sender, SelectionChangedEventArgs selectionChangedEventArgs)
        {
            ComboBox comboBox = (ComboBox)sender;
            RoutedEventWithDataArgs AlgorithmSelectionData = new RoutedEventWithDataArgs(UpdateAlgorithmSelectionEvent, "AlgorithmSelectionData", comboBox.SelectedIndex);            
            RaiseEvent((RoutedEventArgs)AlgorithmSelectionData);
            selectionChangedEventArgs.Handled = true;
        }
    }
}
