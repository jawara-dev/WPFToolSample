using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tool_turnbased_ai.Controller
{
    partial class GridUIController : IController
    {
/*********************************************************************************************************************************************
* 
*********************************************************************************************************************************************/
        public void Update()
        {
            this.gridSpaceViewModel.Update();
        }

        private void UpdateRows()
        {
            if (this.RowDelta > 0)
            {
                for (int i = this.RowDelta; i > 0; --i)
                {
                    // remove
                    this.RemoveLastRow();
                }
            }
            else if (this.RowDelta < 0)
            {
                for (int i = 0; i < this.RowDelta; ++i)
                {

                }
            }
        }

        private void UpdateColumns()
        {
            if (this.ColumnDelta > 0)
            {
                for (int i = this.ColumnDelta; i > 0; --i)
                {
                    this.RemoveLastColumn();
                }
            }
            else if (this.ColumnDelta < 0)
            {
                // add
                for (int i = 0; i < this.ColumnDelta; ++i)
                {

                }
            }
        }
    }
}
