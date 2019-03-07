using System;
using System.Windows;
using System.Windows.Input;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tool_turnbased_ai.ViewModel.WindowViewModels;
using System.Diagnostics;
using tool_turnbased_ai.Event;
using System.Windows.Controls;

namespace tool_turnbased_ai.Controller
{
    public delegate void UpdateAlgorithmSelectionEvent(Object sender, int SelectionIndex);

    class ApplicationController : Window
    {
        private static int DefaultRowCount    = 5;
        private static int DefaultColumnCount = 4;

        public enum ApplicationState { EditMode = 0, PathfindingMode, ReportMode }

        #region UI EventListening
        // Custom application side events
        public static readonly RoutedEvent ChangeApplicationStateEvent    = EventManager.RegisterRoutedEvent("ChangeApplicationState", 
                                                                                                              RoutingStrategy.Direct, 
                                                                                                              typeof(RoutedEventHandler), 
                                                                                                              typeof(ApplicationController)
                                                                                                             );
        public static readonly RoutedEvent UpdateAlgorithmSelectionEvent  = EventManager.RegisterRoutedEvent("UpdateAlgorithmSelect", 
                                                                                                              RoutingStrategy.Direct, 
                                                                                                              typeof(RoutedEventWithDataHandler), 
                                                                                                              typeof(ApplicationController)
                                                                                                             );        
        public static readonly RoutedEvent UpdateGridDataEvent            = EventManager.RegisterRoutedEvent("UpdateGrid", 
                                                                                                              RoutingStrategy.Direct, 
                                                                                                              typeof(RoutedEventWithDataHandler), 
                                                                                                              typeof(ApplicationController)
                                                                                                            );

        public static readonly RoutedEvent UpdateStatusBarEvent          = EventManager.RegisterRoutedEvent("UpdateStatusBar",
                                                                                                              RoutingStrategy.Direct,
                                                                                                              typeof(RoutedEventWithDataHandler),
                                                                                                              typeof(ApplicationController)
                                                                                                            );

        public static readonly RoutedEvent RedrawGridEvent                = EventManager.RegisterRoutedEvent("RedrawGrid", 
                                                                                                              RoutingStrategy.Direct, 
                                                                                                              typeof(RoutedEventWithDataHandler), 
                                                                                                              typeof(ApplicationController)
                                                                                                            );
        public static readonly RoutedEvent ResetApplicationEvent          = EventManager.RegisterRoutedEvent("ResetApplication", 
                                                                                                              RoutingStrategy.Direct, 
                                                                                                              typeof(RoutedEventWithDataHandler), 
                                                                                                              typeof(ApplicationController)
                                                                                                             );
        public static readonly RoutedEvent EnableEditModeEvent            = EventManager.RegisterRoutedEvent("EnableEditMode", 
                                                                                                              RoutingStrategy.Direct, 
                                                                                                              typeof(RoutedEventWithDataHandler), 
                                                                                                              typeof(ApplicationController)
                                                                                                             );
        public static readonly RoutedEvent EnablePathfindingModeEvent     = EventManager.RegisterRoutedEvent("EnablePathfindingMode", 
                                                                                                              RoutingStrategy.Direct, 
                                                                                                              typeof(RoutedEventWithDataHandler), 
                                                                                                              typeof(ApplicationController)
                                                                                                             );
        public static readonly RoutedEvent EnableReportModeEvent          = EventManager.RegisterRoutedEvent("EnableReportMode", 
                                                                                                              RoutingStrategy.Direct, 
                                                                                                              typeof(RoutedEventWithDataHandler), 
                                                                                                              typeof(ApplicationController)
                                                                                                             );
        /*********************************************************************************************************************************************
         * Event Name   :
         * Purpose      : 
         * Delegate Type:
         **********************************************************************************************************************************************/
        public event RoutedEventWithDataHandler UpdateAlgorithmSelection
        {
            add    { AddHandler(UpdateAlgorithmSelectionEvent, value); }
            remove { RemoveHandler(UpdateAlgorithmSelectionEvent, value); }
        }
        /*********************************************************************************************************************************************
         * Event Name   :
         * Purpose      : 
         * Delegate Type:
         **********************************************************************************************************************************************/
        public event RoutedEventWithDataHandler UpdateGridData
        {
            add { AddHandler(UpdateGridDataEvent, value); }
            remove { RemoveHandler(UpdateGridDataEvent, value); }
        }
        /*********************************************************************************************************************************************
         * Event Name   :
         * Purpose      : 
         * Delegate Type:
         **********************************************************************************************************************************************/
        public event RoutedEventWithDataHandler RedrawGrid
        {
            add    { AddHandler(RedrawGridEvent, value); }
            remove { RemoveHandler(RedrawGridEvent, value); }
        }
        /*********************************************************************************************************************************************
         * Event Name   :
         * Purpose      : 
         * Delegate Type:
         **********************************************************************************************************************************************/
        public event RoutedEventWithDataHandler UpdateStatusBar
        {
            add { AddHandler(UpdateStatusBarEvent, value); }
            remove { RemoveHandler(UpdateStatusBarEvent, value); }
        }
        /*********************************************************************************************************************************************
         * Event Name   :
         * Purpose      : 
         * Delegate Type:
         **********************************************************************************************************************************************/
        public event RoutedEventWithDataHandler ResetApplication
        {
            add    { AddHandler(ResetApplicationEvent, value); }
            remove { RemoveHandler(ResetApplicationEvent, value); }
        }
        /*********************************************************************************************************************************************
         * Event Name   :
         * Purpose      : 
         * Delegate Type:
         **********************************************************************************************************************************************/
        public event RoutedEventWithDataHandler EnableEditMode
        {
            add    { AddHandler(EnableEditModeEvent, value); }
            remove { RemoveHandler(EnableEditModeEvent, value); }
        }
        /*********************************************************************************************************************************************
         * Event Name   :
         * Purpose      : 
         * Delegate Type:
         **********************************************************************************************************************************************/
        public event RoutedEventWithDataHandler EnablePathfindingMode
        {
            add    { AddHandler(EnablePathfindingModeEvent, value); }
            remove { RemoveHandler(EnablePathfindingModeEvent, value); }
        }
        /*********************************************************************************************************************************************
         * Event Name   :
         * Purpose      : 
         * Delegate Type:
         **********************************************************************************************************************************************/
        public event RoutedEventWithDataHandler EnableReportMode
        {
            add    { AddHandler(EnableReportModeEvent, value); }
            remove { RemoveHandler(EnableReportModeEvent, value); }
        }
        // Custom application side events
        #endregion 

        #region ViewModels
        private MainWindowViewModel          mainWindowViewModel;
        private StatusBarController          statusBarController;
        private LegendController             legendController;
        private GridConfigurationController  GridConfigurationController;
        private GridUIController             GridUIController;
        private AlgorithmSelectionController AlgorithmSelectionController;
        #endregion

        #region StateBools
        private bool isEditMode;          //tracks edit mode state
        private bool isReportMode;       // tracks report mode state
        private bool isPathfindingMode; //  tracks pathfinding mode state
        #endregion

        // private int StartPoint;       
        Application application; // our reference to the application

        #region Constructor
        /*********************************************************************************************************************************************
         * 
         *********************************************************************************************************************************************/
        public ApplicationController(Application application)
        { 
            // init all the view models
            this.mainWindowViewModel           = new MainWindowViewModel(DefaultRowCount, DefaultColumnCount);
            this.statusBarController           = new StatusBarController(this.mainWindowViewModel.StatusBar);
            this.legendController              = new LegendController(this.mainWindowViewModel.LegendViewModel);
            this.GridConfigurationController   = new GridConfigurationController(this.mainWindowViewModel.GridConfiguration);
            this.GridUIController              = new GridUIController((Grid)(application.MainWindow.FindName("GridUI")), 
                                                                                this.mainWindowViewModel.GridUIViewModel,
                                                                                DefaultRowCount, DefaultColumnCount
                                                                     );
            this.AlgorithmSelectionController  = new AlgorithmSelectionController(this.mainWindowViewModel.AlgorithmSelectionViewModel,
                                                                                  (ComboBox)application.MainWindow.FindName("AlgorithmSelection")
                                                                                 );

            // initialize the grid ui and adjacency list            
        //    this.GridUIController.InitCellAdjancencyList();
           // this.GridUIController.InitCellButtonAdjacencyList();
            //this.GridUIController.InitGrid();           

            // set data context and store ref to app
            application.MainWindow.DataContext = this.mainWindowViewModel;
            this.application                   = application;
            this.application.MainWindow.Title  = "AI Display Pathfinding Tool";

            // set initial state
            this.ChangeApplicationState(ApplicationState.EditMode);

            // Assign event handlers
            ((MainWindow)this.application.MainWindow).ChangeApplicationState   += HandleChangeApplicationStateEvent;
            ((MainWindow)this.application.MainWindow).UpdateRowAndColumn       += HandleUpdateRowColumnEvent;
            ((MainWindow)this.application.MainWindow).UpdateAlgorithmSelection += HandleUpdateAlgorithmSelection;

            // assign the internal application events to their controller handlers
            this.AssignSubControllerConnections();
        }
        #endregion
        #region Update All Controllers
        /*********************************************************************************************************************************************
         * 
         *********************************************************************************************************************************************/
        private void UpdateControllerStates()
        {

        }
        #endregion
        #region ChangeApplicationState
        /*********************************************************************************************************************************************
         * 
         *********************************************************************************************************************************************/
        private void ChangeApplicationState(ApplicationState applicationState)
        {            
           switch(applicationState)
            {
                case ApplicationState.EditMode:
                    {
                        this.isReportMode      = false;
                        this.isEditMode        = true;
                        this.isPathfindingMode = false;
                        break;
                    }
                case ApplicationState.PathfindingMode:
                    {
                        this.isEditMode        = false;
                        this.isPathfindingMode = true;                        
                        this.isReportMode      = false;
                        break;
                    }
                case ApplicationState.ReportMode:
                    {
                        this.isPathfindingMode = false;
                        this.isReportMode      = true;                        
                        this.isEditMode        = false;
                        break;
                    }
            }
        }
        #endregion
        #region UIEventHandlers
        /*********************************************************************************************************************************************
         * 
         *********************************************************************************************************************************************/
        public void HandleChangeApplicationStateEvent(object sender, EventArgs a)
        {
            Debug.WriteLine("Custom event recieved. Handliing....");

            // call App Conn StateChange func

            // Call select controllers update function
            this.legendController.Update();


            // handle
        }
        /*********************************************************************************************************************************************
         * 
         *********************************************************************************************************************************************/
         
        /*********************************************************************************************************************************************
         * 
         *********************************************************************************************************************************************/
        public void HandleUpdateRowColumnEvent(object sender, RoutedEventWithDataArgs routedEventWithData)
        {
            // create event for the grid config controller             
            RoutedEventWithDataArgs InternalUpdateGridConfig = routedEventWithData;
            InternalUpdateGridConfig.SetRoutedEvent(UpdateGridDataEvent);

            // create he follow up event
            RoutedEventWithDataArgs ContentUpdateForStatusBar = new RoutedEventWithDataArgs(UpdateStatusBarEvent, "GridUpdate;Info");

            // raise the event
            this.ExecuteCustomEvent(sender, InternalUpdateGridConfig);

            // handle the events
            InternalUpdateGridConfig.Handled = true;
            routedEventWithData.Handled      = true;

            // raise custome event
            this.ExecuteCustomEvent(sender, ContentUpdateForStatusBar);
        }
        /*********************************************************************************************************************************************
         * 
         *********************************************************************************************************************************************/
        public void HandleUpdateAlgorithmSelection(object sender, RoutedEventWithDataArgs AlgorithmConfigData)
        {
            // create event for the algo controller             
            RoutedEventWithDataArgs InternalUpdateAlgoSelection = AlgorithmConfigData;
            InternalUpdateAlgoSelection.SetRoutedEvent(UpdateAlgorithmSelectionEvent);

            // raise event and callt he associated update function on the controller
            this.ExecuteCustomEvent(sender, InternalUpdateAlgoSelection);
            this.AlgorithmSelectionController.Update();

            // handle the events
            InternalUpdateAlgoSelection.Handled = true;
            AlgorithmConfigData.Handled         = true;
        }
        /*********************************************************************************************************************************************
         * 
         *********************************************************************************************************************************************/
         private void AssignSubControllerConnections()
         {
            #region Update Algorithm
            // Update Algorithm Handlers
            this.UpdateAlgorithmSelection += this.AlgorithmSelectionController.UpdateAlgorithmSelectionHandler;
            #endregion

            #region Grid Data Update Event          
            // send grid data update event
            this.UpdateGridData += this.GridConfigurationController.UpdateGridDataHandler;
            this.UpdateGridData += this.GridUIController.UpdateGridUIDataHandler;
            #endregion

            #region Redraw event
            // trigger grid ui controller event to redraw            
            this.RedrawGrid += this.GridUIController.RedrawGridUIHandler;
            #endregion

            #region Reset Grid to default
            this.ResetApplication += this.GridUIController.ResetGridUIHandler;
            this.ResetApplication += this.GridConfigurationController.ResetGridDataHandler;
            #endregion

            #region Update StatusBar
            this.UpdateStatusBar += this.statusBarController.UpdateStatusBarHandler;
            #endregion

            #region Edit Mode Event
            this.EnableEditMode += this.legendController.EnableEditModeHandler;
            this.EnableEditMode += this.GridConfigurationController.EnableEditModeHandler;
            this.EnableEditMode += this.statusBarController.EnableEditModeHandler;
            this.EnableEditMode += this.GridUIController.EnableEditModeHandler;
            #endregion

            #region Pathfinding Mode Event
            #endregion

            #region Report Mode Event
            this.EnableEditMode += this.legendController.EnableReportModeHandler;
            this.EnableEditMode += this.GridConfigurationController.EnableReportModeHandler;
            this.EnableEditMode += this.statusBarController.EnableReportModeHandler;
            this.EnableEditMode += this.GridUIController.EnableReportModeHandler;
            #endregion
        }
        /*********************************************************************************************************************************************
         * 
         *********************************************************************************************************************************************/
        protected virtual void ExecuteCustomEvent(object sender, RoutedEventWithDataArgs routedEventWithDataArgs)
         {
            Debug.WriteLine("Executing Application Controller -> Algo. Select Controller event. ");
            RaiseEvent(routedEventWithDataArgs);
            routedEventWithDataArgs.Handled = true;
         }        
        #endregion
    }
}
