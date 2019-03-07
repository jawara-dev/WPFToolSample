using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace tool_turnbased_ai.ViewModel
{
    class SearchAlgorithmViewModel
    {
        public ComboBox AlgorithmSelection { get; private set; }
        public string   MovePointText      { get; private set; }
        public string   MovementPointValue { get; private set; }

        SearchAlgorithmViewModel()
        {
            this.AlgorithmSelection = new ComboBox();
            this.MovePointText      = "Move Points:";
            this.MovementPointValue = "6";
        }

        public void UpdateMovementPointValue(string Text)
        {
            this.SetMovementPointValue(Text);
        }

        private void SetMovementPointValue(string Text)
        {           
            this.MovementPointValue = Text;
        }

        public void UpdateMovePointText(string Text)
        {
            this.SetMovePointText(Text);
        }

        private void SetMovePointText(string Text)
        {
            this.MovePointText = Text;
        }
    }
}
