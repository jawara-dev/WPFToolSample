using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tool_turnbased_ai.DataModels
{
    public class AlgorithmSelectionDataModel
    {
        public AlgorithmCollection CurrentAlgorithm;

        public AlgorithmSelectionDataModel()
        {
            this.CurrentAlgorithm = AlgorithmCollection.DefaultText;
        }
    }
}
