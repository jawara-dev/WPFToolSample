using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace tool_turnbased_ai.ViewModel
{
    public class LegendViewModel : INotifyPropertyChanged
    {
        // event
        public event PropertyChangedEventHandler PropertyChanged;

        // Edit Mode Legend
        public LegendKeyViewModel OpenKey   { get; private set; }
        public LegendKeyViewModel ClosedKey { get; private set; }
        public LegendKeyViewModel OriginKey { get; private set; }

        // Report Mode Legend
        public LegendKeyViewModel PathHightlightKey { get; private set; }
        public LegendKeyViewModel DirectPathKey     { get; private set; }
        public LegendKeyViewModel EndpointKey       { get; private set; }

        // databound positions
        public LegendKeyViewModel ActiveLegendPosition0 { get; private set; }
        public LegendKeyViewModel ActiveLegendPosition1 { get; private set; }
        public LegendKeyViewModel ActiveLegendPosition2 { get; private set; }

        public LegendViewModel()
        {
            // Edit Mode
            this.OpenKey   = new LegendKeyViewModel("Open",   ColorGuideService.NodeColorGuide["Traversible"]);
            this.ClosedKey = new LegendKeyViewModel("Closed", ColorGuideService.NodeColorGuide["Wall"]);
            this.OriginKey = new LegendKeyViewModel("Origin", ColorGuideService.NodeColorGuide["Origin"]);
            

            // Report Mode
            this.PathHightlightKey = new LegendKeyViewModel("Accessible", ColorGuideService.NodeColorGuide["AllReachableNodes"]);
            this.DirectPathKey     = new LegendKeyViewModel("Path",       ColorGuideService.NodeColorGuide["PathToDestionation"]);
            this.EndpointKey       = new LegendKeyViewModel("Goal",       ColorGuideService.NodeColorGuide["Destionation"]);
            
            // set the active legend
            this.EnableEditMode();
            this.Update();
        }

        public void Update()
        {
            this.OnPropertyChanged("ActiveLegendPosition0");
            this.OnPropertyChanged("ActiveLegendPosition1");
            this.OnPropertyChanged("ActiveLegendPosition2");
        }        

        public void EnableEditMode()
        {
            this.ActiveLegendPosition0 = this.OpenKey;
            this.ActiveLegendPosition1 = this.ClosedKey;
            this.ActiveLegendPosition2 = this.OriginKey;
        }

        public void EnableReportMode()
        {
            this.ActiveLegendPosition0 = this.PathHightlightKey;
            this.ActiveLegendPosition1 = this.DirectPathKey;
            this.ActiveLegendPosition2 = this.EndpointKey;
        }

        protected void OnPropertyChanged(String propertyName)
        {
           PropertyChanged?.DynamicInvoke(this, new PropertyChangedEventArgs(propertyName));            
           Debug.WriteLine(propertyName);
        }
    }
}