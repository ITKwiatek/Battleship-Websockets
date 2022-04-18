using Battleship_Websockets.Model;
using Battleship_Websockets.Model.Ship;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Battleship_Websockets.Service
{
    public class ShipArrangeService
    {
        private List<ShipModel> ships = new();
        private BattleFieldModel _battleField;
        private int[,] Cells;

        public ShipArrangeService(BattleFieldModel battleField)
        {
            _battleField = battleField;
            Cells = new int[_battleField.Columns, _battleField.Rows];
            FillCells();
        }
        public List<ShipModel> CreateShips(List<ShipInitialDto> shipsDto)
        {
            shipsDto.ForEach(shipDto =>
            {
                for(int i=0; i<shipDto.ShipCount; i++)
                {
                    var ship = ShipBuilderService.CreateShipType(shipDto);
                    ships.Add(ship);
                }
            });

            ArrangeShips();
            return ships;
        }

        private void ArrangeShips()
        {
            
            foreach(var ship in ships)
            {
                List<(int col, int row)> cells = PrepareCells(ship);
                ship.ColumnBegin = cells[0].col;
                ship.RowBegin = cells[0].row;

                ship.ShipParts = CreateShipParts(ship);
            }
        }

        private List<ShipPart> CreateShipParts(ShipModel ship)
        {
            List<ShipPart> shipParts = new();
            for(int i=0; i<ship.Length; i++)
            {
                    if(ship.Orientation == Orientation.Horizontal)
                    {
                        var shipPart = new ShipPart(i, ship.RowBegin, ship.ColumnBegin + i, ship);
                        shipParts.Add(shipPart);
                    }
                    else if (ship.Orientation == Orientation.Vertical)
                    {
                        var shipPart = new ShipPart(i, ship.RowBegin + i, ship.ColumnBegin, ship);
                        shipParts.Add(shipPart);
                    }
            }

            return shipParts;
        }

        private List<(int,int)> PrepareCells(ShipModel ship)
        {
            List<(int col, int row)> cells = new();
                cells = LookForFreeCells(ship.Orientation, ship.Length);

                if(areAllCellsAvailable(cells))
                {
                    markTakenFields(cells);
                }
                else
                {
                    cells = PrepareCells(ship);
                }

            return cells;
        }

        private void markTakenFields(List<(int col, int row)> cells)
        {
            foreach(var cell in cells)
            {
                if (Cells[cell.col, cell.row] == 1)
                    throw new NotImplementedException();

                Cells[cell.col, cell.row] = 1;
            }
        }

        private (int, int) LookForFreeCell()
        {
            (int col, int row) cell = RandomizeFreeCell(_battleField.Columns, _battleField.Rows);
            if (!isCellAvailable((cell.col, cell.row)))
            {
                cell = LookForFreeCell();
            }

            return cell;
        }

        private List<(int,int)> LookForFreeCells(Orientation orientation, int length)
        {
            List<(int, int)> cells = new();
            bool allCellsAreAvailable = true;
            if (orientation == Orientation.Horizontal)
            {
                (int col, int row) cell = RandomizeFreeCell(_battleField.Columns - length + 1, _battleField.Rows);

                for (int i=0; i<length; i++)
                {
                    if(isCellAvailable((cell.col + i, cell.row)))
                    {
                        cells.Add((cell.col + i, cell.row));
                    }
                    else
                    {
                        allCellsAreAvailable = false;
                        break;
                    }
                }
            }
            else if (orientation == Orientation.Vertical)
            {
                (int col, int row) cell = RandomizeFreeCell(_battleField.Columns, _battleField.Rows - length + 1);

                for (int i = 0; i < length; i++)
                {
                    if (isCellAvailable((cell.col, cell.row + i)))
                    {
                        cells.Add((cell.col, cell.row + i));
                    }
                    else
                    {
                        allCellsAreAvailable = false;
                        break;
                    }
                }
            }
            if (!allCellsAreAvailable)
            {
                cells = LookForFreeCells(orientation, length);
            }

            return cells;
        }

        private (int,int) RandomizeFreeCell(int maxCol, int maxRow)
        {
            Random random = new();
            (int col, int row) cell;
            cell.col = random.Next(0, maxCol);
            cell.row = random.Next(0, maxRow);

            if(!isCellAvailable(cell))
            {
                cell = RandomizeFreeCell(maxCol, maxRow);
            }
            return cell;
        }

        private bool areAllCellsAvailable(List<(int col, int row)> cells)
        {
            foreach(var cell in cells)
            {
                if (!isCellAvailable(cell))
                    return false;
            }
            return true;
        }

        private bool isCellAvailable((int col, int row) cell)
        {
            bool available = false;
            if (Cells[cell.col, cell.row] == 0)
                available = true;

            return available;
        }

        private void FillCells()
        {
            for(int i=0; i<Cells.GetLength(0);i++)
            {
                for (int j = 0; j < Cells.GetLength(1); j++)
                {
                    Cells[i,j] = 0;
                }
            }
        }
    }
}
