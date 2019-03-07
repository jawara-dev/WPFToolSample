using tool_turnbased_ai.Event;
using tool_turnbased_ai.ViewModel;

namespace tool_turnbased_ai.Controller
{
    public class LegendController : Controller
    {
        private LegendViewModel legendViewlModel;

        public LegendController(LegendViewModel LegendViewModel)
        {
            this.isEditMode       = true;
            this.legendViewlModel = LegendViewModel;
        }

        public override void Update()
        {
            this.isEditMode = !this.isEditMode;

            if(this.isEditMode)
            {
                this.EnableEditMode();
            }
            else
            {
                this.EnableReportMode();
            }

            this.legendViewlModel.Update();
        }

        protected override void EnableEditMode()
        {
            this.legendViewlModel.EnableEditMode();
        }

        protected override void EnableReportMode()
        {
            this.legendViewlModel.EnableReportMode();
        }

        public void EnableEditModeHandler(object sender, RoutedEventWithDataArgs routedEventWithDataArgs)
        {

        }

        public void EnableReportModeHandler(object sender, RoutedEventWithDataArgs routedEventWithDataArgs)
        {

        }

        public void ResetLegendHandler(object sender, RoutedEventWithDataArgs routedEventWithDataArgs)
        {

        }
    }
}
