using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace tool_turnbased_ai.ViewModel.WindowViewModels
{    
    public class MainWindowViewModel
    {  
        public StatusBarViewModel          StatusBar                   { get; set; }
        public GridCongifurationViewModel  GridConfiguration           { get; set; }
        public LegendViewModel             LegendViewModel             { get; set; }
        public GridUIViewModel             GridUIViewModel             { get; set; }
        public AlgorithmSelectionViewModel AlgorithmSelectionViewModel { get; set; }

        public MainWindowViewModel(int DefaultRowCount, int DefaultColumnCount)
        {
            this.StatusBar                   = new StatusBarViewModel("Configure grid and search algorithm.", ColorGuideService.StatusBarColorGuide["Info"]);
            this.GridConfiguration           = new GridCongifurationViewModel(DefaultRowCount, DefaultColumnCount);
            this.LegendViewModel             = new LegendViewModel();
            this.GridUIViewModel             = new GridUIViewModel();
            this.AlgorithmSelectionViewModel = new AlgorithmSelectionViewModel();
        }
    }
}
