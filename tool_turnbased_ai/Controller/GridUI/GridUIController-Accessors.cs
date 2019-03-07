using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using tool_turnbased_ai.ViewModel;

namespace tool_turnbased_ai.Controller
{
    partial class GridUIController : IController
    {
        private int GetDelta(int OldValue, int NewValue)
        {
            if (OldValue > NewValue)
            {
                return NewValue - OldValue;
            }
            else
            {
                return OldValue - NewValue;
            }
        }

        private List<Button> GetAllCellButtons()
        {
            List<Button> ButtonList = new List<Button>();
            return ButtonList;
        }

        private List<CellViewModel> GetLastColumnList()
        {
            List<CellViewModel> LastColumn = new List<CellViewModel>(this.Columns - 1);

            foreach (List<CellViewModel> Row in this.UICellAdjajencyList)
            {
                LastColumn.Add(Row.Last());
                Row.Remove(Row.Last());
            }

            return LastColumn;
        }
    }
}
