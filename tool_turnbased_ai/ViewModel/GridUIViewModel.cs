using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace tool_turnbased_ai.ViewModel
{    
    public class GridUIViewModel : INotifyPropertyChanged
    {
        public  Grid gridSpaceUI { get; private set; }

        public GridUIViewModel() {}

        public event PropertyChangedEventHandler PropertyChanged;

        public void Update()
        {
            this.NotifyPropertChanged();
        }

        public void SetGrid(Grid grid)
        {
            this.LinkRefToGrid(grid);
        }

        private void LinkRefToGrid(Grid grid)
        {
            this.gridSpaceUI = grid;
        }

        public void CreateGridSpaceUI(int Columns, int Rows)
        {
            this.AddRow(Rows);
            this.AddColumn(Columns);
        }

        public void AddRow(int RowsToAdd = 1)
        {
            while(RowsToAdd > 0)
            {
                this.AddRowDefinition();
                --RowsToAdd;
            }
        }

        public void AddColumn(int ColumnsToAdd = 1)
        {
            while(ColumnsToAdd > 0)
            {
                this.AddColumnDefinition();
                --ColumnsToAdd;
            }
        }        

        // add to bottom - adds it at given row
        public void AddRowOfCells(List<Button> CellRowButtons, int Row, int TotalColumns)
        {
            for(int i = 0; i < TotalColumns; ++i)
            {
                this.gridSpaceUI.Children.Add(CellRowButtons[i]);
                this.SetTheCellRowAndCoulmn(CellRowButtons[i], Row, i);
            }
        }

        // add to end
        public void AddColumnOfCells(List<Button> CellColumnButtons, int TotalRows, int Column)
        {
            for(int i = 0; i < TotalRows; ++i)
            {
                this.gridSpaceUI.Children.Add(CellColumnButtons[i]);
                this.SetTheCellRowAndCoulmn(CellColumnButtons[i], i, Column);
            }
        }

        // takes a full list and repops the board - 
        public void UpdateAllCells(List<List<Button>> AllCellButtons, int Columns, int Rows)
        {
            // exterior list is rows
            for(int i = 0; i < Rows; ++i)
            {
                // interior list is columns
                for(int j = 0; j < Columns; ++j)
                {
                    this.gridSpaceUI.Children.Add(AllCellButtons[i][j]);
                    this.SetTheCellRowAndCoulmn(AllCellButtons[i][j], i, j );
                }
            }
        }

        private void SetTheCellRowAndCoulmn(Button Cell, int Row, int Column)
        {
            Grid.SetRow(Cell, Row);
            Grid.SetColumn(Cell, Column);
        }

        public void RemoveColumnDefinition()
        {
            this.RemoveColumnDefinition();
        }

        public void RemoveRowDefinition()
        {
            this.RemoveRowDefinition();
        }
        
        public void RemoveCell(Button CellButton)
        {
            this.RemoveChildCell(CellButton);
        }

        private void RemoveChildCell(Button CellButton)
        {
            // remove the child
            this.gridSpaceUI.Children.Remove(CellButton);
        }

        private void AddRowDefinition()
        {
            this.gridSpaceUI.RowDefinitions.Add(new RowDefinition());
        }

        private void RemoveRowDefinition(int RowsToRemove)
        {
            this.gridSpaceUI.RowDefinitions.RemoveAt(RowsToRemove);
        }

        private void AddColumnDefinition()
        {
            this.gridSpaceUI.ColumnDefinitions.Add(new ColumnDefinition());
        }

        private void RemoveColumnDefinition(int ColumnToRemove)
        {
            this.gridSpaceUI.ColumnDefinitions.RemoveAt(ColumnToRemove);
        }

        private void NotifyPropertChanged()
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("GridUI"));
        }
    }
}
