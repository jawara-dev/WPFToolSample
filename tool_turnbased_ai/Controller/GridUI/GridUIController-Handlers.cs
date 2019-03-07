using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tool_turnbased_ai.Event;

namespace tool_turnbased_ai.Controller
{
    // handlers
    partial class GridUIController : IController
    {
        public void UpdateGridUIDataHandler(object sender, RoutedEventWithDataArgs routedEventWithDataArgs)
        {
            List<string> MessageContents = EnveloperOpener.OpenStringEnvelope(routedEventWithDataArgs.Content);
            int ConvertedValue = 0;
            StringConverters.StrToInt32(MessageContents[0], ref ConvertedValue);

            if (MessageContents[0] == "Row")
            {
                this.RowDelta = this.GetDelta(this.Rows, ConvertedValue);
                this.Rows = ConvertedValue;
            }
            else if (MessageContents[0] == "Column")
            {
                this.ColumnDelta = this.GetDelta(this.Columns, ConvertedValue);
                this.Columns = ConvertedValue;
            }
            routedEventWithDataArgs.Handled = true;
        }

        public void RedrawGridUIHandler(object sender, RoutedEventWithDataArgs routedEventWithDataArgs)
        {
            // update row
            if(this.RowDelta > 0)
            {
                for(int i = 0; i < this.RowDelta; ++i)
                {
                    this.AddNewRow();
                }
            } else if(this.RowDelta < 0)
            {
                for (int i = this.RowDelta; i < 0; ++i)
                {
                    this.RemoveLastRow();
                }
            }
            
            // update column
            if(this.ColumnDelta > 0)
            {
                for(int i = 0; i < this.ColumnDelta; ++i)
                {
                    this.AddNewColumn();
                }
            } else if(this.ColumnDelta < 0)
            {
                for(int i = this.ColumnDelta; i < 0 ; ++i)
                {
                    this.RemoveLastColumn();
                }
            }

            // update draw call
            this.Update();

            routedEventWithDataArgs.Handled = true;
        }

        // for explicit updates
        public void UpdateStartPositionHandler(object sender, RoutedEventWithDataArgs routedEventWithDataArgs)
        {

        }

        public void ResetGridUIHandler(object sender, RoutedEventWithDataArgs routedEventWithDataArgs)
        {

        }

        public void EnableEditModeHandler(object sender, RoutedEventWithDataArgs routedEventWithDataArgs)
        {

        }

        public void EnableReportModeHandler(object sender, RoutedEventWithDataArgs routedEventWithDataArgs)
        {

        }
    }
}
