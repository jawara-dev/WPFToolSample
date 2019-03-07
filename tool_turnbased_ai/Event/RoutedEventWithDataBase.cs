using System;
using System.Windows;

namespace tool_turnbased_ai.Event
{
    public delegate void RoutedEventWithDataHandler(object sender, RoutedEventWithDataArgs routedEventWithData);
    
    public class RoutedEventWithDataArgs : RoutedEventArgs
    {
        public readonly string Content;
        public readonly int    NumericData;
        public readonly bool   BinaryFlag;
                
        public RoutedEventWithDataArgs(RoutedEvent routedEvent, string Content, int NumericData = -1, bool BinaryFlag = false)
        {
            this.Content     = Content;
            this.NumericData = NumericData;
            this.BinaryFlag  = BinaryFlag;
            this.RoutedEvent = routedEvent;
            this.Source      = null;
        }

        public void SetRoutedEvent(RoutedEvent routedEvent)
        {            
            this.RoutedEvent = routedEvent;            
        }
    }
}
