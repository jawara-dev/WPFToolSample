using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using tool_turnbased_ai;
using tool_turnbased_ai.DataModels;
using tool_turnbased_ai.Event;
using tool_turnbased_ai.ViewModel;

namespace tool_turnbased_ai.Controller
{
    public class AlgorithmSelectionController : IController
    {        
        private AlgorithmSelectionViewModel AlgorithmSelectionViewModel;
        private ComboBox                    AlgorithmSelectionComboBox;
        public  AlgorithmSelectionDataModel AlgorithmSelectionDataModel;
        private List<string>                FullyQualifiedAlgorithmNames;
        private Dictionary<string, string>  AlgorithmSelectionDescriptions; // ConstraintText, Value
        private AlgorithmCollection         CurrentAlgorithm; // string = algo name, index as indentifier
       
        public AlgorithmSelectionController(AlgorithmSelectionViewModel algorithmSelectionViewModel, ComboBox AlgoComboBox)
        {
            //  Link references
            this.AlgorithmSelectionViewModel = algorithmSelectionViewModel;
            this.AlgorithmSelectionComboBox  = AlgoComboBox;

            // Serialize the contrainers
            this.SerializeAlgorithmDescriptions(ref this.AlgorithmSelectionDescriptions);
            this.SerializeAlgorithmNameList(ref this.FullyQualifiedAlgorithmNames);
            this.CurrentAlgorithm = AlgorithmCollection.DefaultText;

            // Data Model
            this.AlgorithmSelectionDataModel                  = new AlgorithmSelectionDataModel();
            this.AlgorithmSelectionDataModel.CurrentAlgorithm = this.CurrentAlgorithm;

            // View Model
            this.AlgorithmSelectionViewModel.UpdateComboBoxAnchor(this.AlgorithmSelectionComboBox);
            this.AlgorithmSelectionViewModel.UpdateSelectionsList(ref this.FullyQualifiedAlgorithmNames);
            this.AlgorithmSelectionViewModel.UpdateAlgorithmName(this.FullyQualifiedAlgorithmNames[0]);
                        
            this.Update();
        }

        private void SerializeAlgorithmDescriptions(ref Dictionary<string, string> AlgorithmDescriptions)
        {
            AlgorithmDescriptions = new Dictionary<string, string>();
            AlgorithmDescriptions.Add("", "");
            AlgorithmDescriptions.Add("Move Points: ", "6");
        }

        private void SerializeAlgorithmNameList(ref List<string> AlgorithmNames)
        {
            AlgorithmNames = new List<string>() { "Select Algorithm", "Harebreained Schemes Test Q3" };
        }

        public void Update()
        {
            // Constraint Text
            this.AlgorithmSelectionViewModel.UpdateAlgorithmSupportTextContent(this.AlgorithmSelectionDescriptions
                                                                                   .Keys.ElementAt((int)this.CurrentAlgorithm)
                                                                              );

            // Constraint Value
            this.AlgorithmSelectionViewModel.UpdateAlgorithmSupportTextValue(this.AlgorithmSelectionDescriptions
                                                                                 .Values.ElementAt((int)this.CurrentAlgorithm)
                                                                            );

            // call the VM update
            this.AlgorithmSelectionViewModel.Update();
        }

        private void UpdateControllerData(int SelectionIndex)
        {
            // the current algorithm
            this.CurrentAlgorithm = (AlgorithmCollection)SelectionIndex;
        }

        private void UpdateViewModel(int SelectionIndex)
        {            
            // Update the name on the ViewModel
            this.AlgorithmSelectionViewModel.UpdateAlgorithmName(this.FullyQualifiedAlgorithmNames[(int)this.CurrentAlgorithm]);            
        }

        private void UpdateDataModel(int SelectionIndex)
        {                  
            this.AlgorithmSelectionDataModel.CurrentAlgorithm = this.CurrentAlgorithm;
        }

        public void UpdateAlgorithmSelectionHandler(object sender, RoutedEventWithDataArgs routedEventWithData)
        {
            this.UpdateControllerData(routedEventWithData.NumericData);
            this.UpdateViewModel(routedEventWithData.NumericData);
            this.UpdateDataModel(routedEventWithData.NumericData);
            routedEventWithData.Handled = true;
        }

        public void SetAlgorithmVMCombobox(ComboBox comboBox)
        {
            this.AlgorithmSelectionComboBox = comboBox;
        }
    }        
}
