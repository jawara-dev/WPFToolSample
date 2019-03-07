using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace tool_turnbased_ai.ViewModel
{
    public enum CellType { Closed = 0, Open = 1, Origin = 2, PathHighlight = 3, DirectPath = 4, EndPoint = 5, StartPoint = 6 }

    public class CellViewModel
    {
        private CellType CellType;
        public  Button   Button;     
        private string   Content;        
        private bool isEditModeEnabled = true;

        public CellViewModel() 
        {
            this.CellType   = CellType.Open;            

            // Create the button as open
            this.Button = new Button
            {
                Height      = double.NaN,                
                BorderBrush = Brushes.Black,
                Margin      = new System.Windows.Thickness(10),
                Background  = Brushes.White,
                Foreground  = Brushes.Black                
            };

            this.Button.Click += this.OnClick;
        }                

        public void Update(CellType cellType)
        {
            if(this.isEditModeEnabled)
            {
                if (cellType != CellType.Origin)
                {
                    ToggleOpenClosed();
                } else
                {
                    this.SetAsOriginPoint();
                }
            } else
            {
                if(cellType != CellType.EndPoint)
                {
                    this.TogglePathHighlighting(cellType);
                } else
                {
                    this.SetAsEndPoint();
                }
            }            
        }

        // needs work to qualify code path
        public void ToggleEditMode()
        {
            this.isEditModeEnabled = (this.isEditModeEnabled) ? false : true;            
        }        

        private void ToggleOpenClosed()
        {
            // if open then close
            if (this.CellType == CellType.Open)
            {
                this.UpdateCellStyle(CellType.Closed);
            }
            else
            {
                this.UpdateCellStyle(CellType.Open);
            }
        }

        private void SetAsOriginPoint()
        {
            this.UpdateCellStyle(CellType.Origin);
        }

        private void SetAsStartPoint()
        {
            this.UpdateCellStyle(CellType.StartPoint);
        }

        private void TogglePathHighlighting(CellType cellType)
        {
            this.UpdateCellStyle(cellType);
        }

        private void SetAsEndPoint()
        {
            this.UpdateCellStyle(CellType.EndPoint);
        }

        private void UpdateCellStyle(CellType cellType)
        {
            switch(cellType)
            {
                case CellType.Closed:
                    {
                        this.CellType          = CellType.Closed;
                        this.Button.Background = Brushes.Gray;
                        this.Button.Margin     = new System.Windows.Thickness(0);
                        UpdateButtonContent();
                        break;
                    }
                case CellType.Open:
                    {
                        this.CellType          = CellType.Open;
                        this.Button.Background = Brushes.White;
                        this.Button.Margin     = new System.Windows.Thickness(10);
                        UpdateButtonContent();
                        break;
                    }
                case CellType.Origin:
                    {
                        this.CellType = CellType.Origin;
                        this.Button.Background = Brushes.Aqua;
                        this.Button.Margin     = new System.Windows.Thickness(10);
                        this.Button.FontSize   = 60;
                        this.UpdateButtonContent("X");                        
                        break;
                    }
                case CellType.PathHighlight:
                    {
                        this.CellType          = CellType.PathHighlight;
                        this.Button.Background = Brushes.LightGreen;
                        this.Button.Margin     = new System.Windows.Thickness(10);
                        break;
                    }
                case CellType.DirectPath:
                    {
                        this.CellType          = CellType.DirectPath;
                        this.Button.Background = Brushes.LightPink;
                        this.Button.Margin     = new System.Windows.Thickness(10);                        
                        break;
                    }
                case CellType.EndPoint:
                    {
                        this.CellType          = CellType.EndPoint;
                        this.Button.Background = Brushes.Red;
                        this.Button.Margin     = new System.Windows.Thickness(10);
                        this.Button.Content    = this.Content;
                        break;
                    }
                case CellType.StartPoint:
                    {
                        this.CellType = CellType.StartPoint;
                        this.Button.Background = Brushes.Green;
                        this.Button.Margin = new System.Windows.Thickness(10);
                        break;
                    }
            }
        }

        public void UpdateButtonContent(string newContent = "")
        {
            this.Content = newContent;
        }

        private void OnClick(object sender, RoutedEventArgs e)
        {
            if(isEditModeEnabled)
            {
                if(Keyboard.Modifiers == ModifierKeys.Control && this.CellType != CellType.Origin)
                {
                    this.SetAsOriginPoint();
                } else
                {
                    this.ToggleOpenClosed();
                }                
            }
            else
            {
                if (Keyboard.Modifiers == ModifierKeys.Control && this.CellType == CellType.PathHighlight)
                {
                    this.SetAsEndPoint();
                } else
                {
                    this.TogglePathHighlighting(CellType.PathHighlight);
                }                    
            }
            e.Handled = true;
        } 
    }
}
