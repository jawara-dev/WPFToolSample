using System;
using System.Collections.Generic;
using System.Windows.Media;
using tool_turnbased_ai.Event;
using tool_turnbased_ai.ViewModel;

namespace tool_turnbased_ai.Controller
{
    class StatusBarController : Controller
    {
        public StatusBarViewModel          statusBarViewModel;
        private Dictionary<string, string> StatusBarTextLibrary;

        public StatusBarController(StatusBarViewModel statusBarViewModel)
        {
            this.statusBarViewModel = statusBarViewModel;
            this.SerializeStatusBarTextLibrary();
        }

        private void SerializeStatusBarTextLibrary()
        {
            this.StatusBarTextLibrary = new Dictionary<string, string>();

            // State text
            this.StatusBarTextLibrary.Add("DefaultText",        "Configure grid and search algorithm.");
            this.StatusBarTextLibrary.Add("EditMode",           "Click nodes to edit the node type.");            
            this.StatusBarTextLibrary.Add("PathfindingMode",    "Executing Pathfinding.");
            this.StatusBarTextLibrary.Add("ReportMode",         "Select cell to view path and details.");
            this.StatusBarTextLibrary.Add("Reset",              "Click to restart.");
            this.StatusBarTextLibrary.Add("EditExisiting",      "Click to edit this map.");

            // Call to action
            this.StatusBarTextLibrary.Add("ExecutePathfinding", "Click to execute pathfinding.");

            // Algorithm Requirements text  
            this.StatusBarTextLibrary.Add("HBSQ3",              "Requirments: Origin Point");

            // Region update text
            this.StatusBarTextLibrary.Add("GridUpdate",         "Click to Update Grid.");            
        }

        public void RegisterStatusBarTextPair(string key, string value)
        {
            this.StatusBarTextLibrary.Add(key, value);
        }

        public override void Update(){ }

        protected override void EnableEditMode()
        {
            // update brush
            this.statusBarViewModel.UpdateBrush(ColorGuideService.StatusBarColorGuide["Info"]);

            //update the content
            this.statusBarViewModel.UpdateContent(this.StatusBarTextLibrary["DefaultText"]);
        }

        protected override void EnableReportMode()
        {
            // update brush
            this.statusBarViewModel.UpdateBrush(ColorGuideService.StatusBarColorGuide["Info"]);

            //update the content
            this.statusBarViewModel.UpdateContent(this.StatusBarTextLibrary["ReportMode"]);
        }

        protected void EnablePathfindingMode()
        {
            // update brush
            this.statusBarViewModel.UpdateBrush(ColorGuideService.StatusBarColorGuide["Info"]);

            //update the content
            this.statusBarViewModel.UpdateContent(this.StatusBarTextLibrary["PathfindingMode"]);
        }

        public void UpdateStatusBarHandler(object sender, RoutedEventWithDataArgs StatusBarConfigData)
        {
            // slot 1 test, slot 2 color
            List<string> MessageContents = EnveloperOpener.OpenStringEnvelope(StatusBarConfigData.Content);

            // update brush
            this.statusBarViewModel.UpdateBrush(ColorGuideService.StatusBarColorGuide[MessageContents[1]]);

            //update the content
            this.statusBarViewModel.UpdateContent(this.StatusBarTextLibrary[MessageContents[0]]);
        }

        public void EnableEditModeHandler(object sender, RoutedEventWithDataArgs routedEventWithDataArgs)
        {

        }

        public void EnableReportModeHandler(object sender, RoutedEventWithDataArgs routedEventWithDataArgs)
        {

        }
    }
}
