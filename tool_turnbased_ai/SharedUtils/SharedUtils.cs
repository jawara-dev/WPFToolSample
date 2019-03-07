using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace tool_turnbased_ai
{
    public enum AlgorithmCollection { DefaultText, HBSQ3 }
    
    public static class ColorGuideService
    {
        public static readonly Dictionary<string, SolidColorBrush> StatusBarColorGuide 
            = new Dictionary<string, SolidColorBrush>
        {
            { "Info",         Brushes.LightSteelBlue },
            { "CallToAction", Brushes.LightGreen },
            { "Requirements", Brushes.LightYellow },
            { "Error",        Brushes.LightCoral }
        };

        public static readonly Dictionary<string, SolidColorBrush> NodeColorGuide
            = new Dictionary<string, SolidColorBrush>
        {
            { "Traversible",        Brushes.White },
            { "Wall",               Brushes.Gray  },
            { "Origin",             ColorGuideService.ConvertHexToSolidBrush("#0BD22B") },
            { "AllReachableNodes",  ColorGuideService.ConvertHexToSolidBrush("#009DE9") },
            { "PathToDestionation", ColorGuideService.ConvertHexToSolidBrush("#E9D700") },            
            { "Destionation",       ColorGuideService.ConvertHexToSolidBrush("#E91A15") }
        };

        private static SolidColorBrush ConvertHexToSolidBrush(string HexColor)
        {
            return (SolidColorBrush)(new BrushConverter().ConvertFrom(HexColor));
        }
    }

    public static class EnveloperOpener
    {
        public static List<string> OpenStringEnvelope(string StringEnvelope)
        {
            return StringEnvelope.Split(new char[] { ';' }).ToList();
        }
    }

    public static class StringConverters
    {
        // conversion
        public static bool StrToInt32(string Input, ref int Value)
        {
            return Int32.TryParse(Input, out Value);
        }
    }
}