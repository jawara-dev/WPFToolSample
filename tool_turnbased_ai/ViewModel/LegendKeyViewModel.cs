using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace tool_turnbased_ai.ViewModel
{
    public class LegendKeyViewModel
    {
        public string Text      { get; private set; }
        public Brush Background { get; private set; }

        public LegendKeyViewModel(string text, Brush brush)
        {
            this.Text       = text;
            this.Background = brush;
        }
    }
}
