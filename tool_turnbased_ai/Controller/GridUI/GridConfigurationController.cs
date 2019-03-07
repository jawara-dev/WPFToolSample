using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tool_turnbased_ai.Event;
using tool_turnbased_ai.ViewModel;

namespace tool_turnbased_ai.Controller
{
    class GridConfigurationController : IController
    {        
        private GridCongifurationViewModel gridCongifurationViewModel;
        // update grid event
        // add event

        public  bool isEditMode;
        private int  Row;
        private int  Col;
        
        public GridConfigurationController(GridCongifurationViewModel gridCongifuration)
        {
            this.gridCongifurationViewModel = gridCongifuration;
            this.isEditMode = true;
            this.Row = this.gridCongifurationViewModel.Row;
            this.Col = this.gridCongifurationViewModel.Column;
        }

        public void Update()
        {
            if(this.isEditMode)
            {
                this.gridCongifurationViewModel.UpdateRow(this.Row);
                this.gridCongifurationViewModel.UpdateColumn(this.Col);                
            }
        }

        public void UpdateRow(string Input)
        {
            int Temp = 0;            
            if (StringConverters.StrToInt32(Input, ref Temp)) { this.Row = Temp; }
        }

        public void UpdateColumn(string Input)
        {
            int Temp = 0;            
            if(StringConverters.StrToInt32(Input, ref Temp)) { this.Col = Temp; }
        }

        // handles event from app controller
        public void UpdateGridDataHandler(object sender, RoutedEventWithDataArgs routedEventWithDataArgs)
        {
            List<string> MessageContents = EnveloperOpener.OpenStringEnvelope(routedEventWithDataArgs.Content);

            if (MessageContents[0] == "Row")
            {
                this.UpdateRow(MessageContents[1]);
            }
            else if (MessageContents[0] == "Column")
            {
                this.UpdateColumn(MessageContents[1]);
            }
        }

        public void ResetGridDataHandler(object sender, RoutedEventWithDataArgs routedEventWithDataArgs)
        {
            // set row and column back to default
        }

        public void EnableEditModeHandler(object sender, RoutedEventWithDataArgs routedEventWithDataArgs)
        {

        }

        public void EnableReportModeHandler(object sender, RoutedEventWithDataArgs routedEventWithDataArgs)
        {

        }
    }
}
