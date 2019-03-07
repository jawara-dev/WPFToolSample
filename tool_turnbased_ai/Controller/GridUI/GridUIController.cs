using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using tool_turnbased_ai.Event;
using tool_turnbased_ai.ViewModel;

namespace tool_turnbased_ai.Controller
{
    partial class GridUIController : IController
    {       
        private GridUIViewModel gridSpaceViewModel;
        public  Grid            GridControl;

        private int Rows;
        private int Columns;
        private int RowDelta;
        private int ColumnDelta;

        private int SearchOriginPoint;

        private List<List<CellViewModel>> UICellAdjajencyList;
        private List<List<Button>>        CellButtonAdjacencyList;

        #region Initialization
        public GridUIController(Grid GridUI, GridUIViewModel gridSpace, 
                                int Rows, int Columns
                               )
        {
            this.GridControl        = GridUI;
            this.gridSpaceViewModel = gridSpace;
            this.Rows               = Rows;
            this.Columns            = Columns;
            this.RowDelta           = Rows;
            this.ColumnDelta        = Columns;

            // Cell Data
            this.UICellAdjajencyList     = new List<List<CellViewModel>>();
            this.CellButtonAdjacencyList = new List<List<Button>>();

            this.InitCellAdjancencyList();
            this.InitCellButtonAdjacencyList();
            this.InitGrid();
        }

        private void InitCellAdjancencyList()
        {
            // add one row of columns until we have all the expected rows
            for(int i = 0; i < this.Rows; ++i)
            {
                this.UICellAdjajencyList.Add(this.CreateRowOfCells());                
            }
        }

        private void InitCellButtonAdjacencyList()
        {
            foreach(List<CellViewModel> CellRow in this.UICellAdjajencyList)
            {
                // create a list of the buttons from the current row in order
                List<Button> ButtonsInRow = CellRow.Select(Cell => Cell.Button).ToList();

                // add buttons to the button adj. list
                this.CellButtonAdjacencyList.Add(ButtonsInRow);                
            }            
        }

        private void InitGrid()
        {
            this.gridSpaceViewModel.SetGrid(this.GridControl);
            this.gridSpaceViewModel.CreateGridSpaceUI(this.Columns, this.Rows);
            this.AddAllCurrentCellsToView();
            this.gridSpaceViewModel.Update();
        }
        #endregion
        #region Create Row, Column, and Cell
        private CellViewModel CreateCell()
        {
            CellViewModel Cell = new CellViewModel();
            Cell.ToggleEditMode();
            return Cell;
        }

        private List<CellViewModel> CreateRowOfCells()
        {
            List<CellViewModel> NewRow = new List<CellViewModel>();

            for(int i = 0; i < this.ColumnDelta; ++i)
            {
                NewRow.Add(this.CreateCell());
            }

            return NewRow;
        }

        private List<CellViewModel> CreateColumnOfCells()
        {
            List<CellViewModel> NewColumn = new List<CellViewModel>();
            
            for(int i = 0; i < this.RowDelta; ++i)
            {
                NewColumn.Add(this.CreateCell());
            }

            return NewColumn;
        }

        #endregion
        #region Controller: Add Cells, Rows, and Columns
        /*********************************************************************************************************************************************
         * 
         *********************************************************************************************************************************************/
        private void AddAllCurrentCellsToView()
        {
            this.gridSpaceViewModel.UpdateAllCells(this.CellButtonAdjacencyList, this.Columns, this.Rows );
        }
        #endregion
        /*********************************************************************************************************************************************
         * 
         *********************************************************************************************************************************************/
        #region ViewModel: add / remove items

        private void AddNewRow()
        { 
            // create new row and add it
            this.UICellAdjajencyList.Add(this.CreateRowOfCells());

            // update the list of buttons
            List<Button> ButtonsInRow = this.UICellAdjajencyList.Last().Select(Cell => Cell.Button).ToList();
            this.CellButtonAdjacencyList.Add(ButtonsInRow);

            this.gridSpaceViewModel.AddRowOfCells(this.CellButtonAdjacencyList.Last(), this.Rows, this.Columns);
        }

        private void RemoveLastRow()
        {          
            foreach(CellViewModel Cell in this.UICellAdjajencyList.ElementAt(this.Rows))
            {
                // remove the children
                this.gridSpaceViewModel.RemoveCell(Cell.Button);
            }

            // remove the row definition
            this.gridSpaceViewModel.RemoveRowDefinition();

            // delete the last row
            this.UICellAdjajencyList.Remove(this.UICellAdjajencyList.ElementAt(this.Rows));
        }

        private void AddNewColumn()
        {
            List<CellViewModel> NewColumn =  this.CreateColumnOfCells();            

            for(int i = 0; i < this.Rows; ++i)
            {
                this.UICellAdjajencyList[i].Add(NewColumn[i]);
                this.CellButtonAdjacencyList[i].Add(NewColumn[i].Button);
            }

            // add to the view model
            this.gridSpaceViewModel.AddColumnOfCells(this.CellButtonAdjacencyList.Last(), this.Rows, this.Columns);
        }

        private void RemoveLastColumn()
        {
            foreach (CellViewModel Cell in this.GetLastColumnList())
            {
                // remove the children
                this.gridSpaceViewModel.RemoveCell(Cell.Button);

                // delete the last row
                this.UICellAdjajencyList.Remove(this.UICellAdjajencyList.ElementAt(Rows));
            }

            // remove column definition
            this.gridSpaceViewModel.RemoveColumnDefinition();
        }
        #endregion        
    }
}
