/* Programmed by Ajith Gopinathan
 * ajithdevel@gmail.com
*/

using UnityEngine;
using System.Collections;

public class GameScript : MonoBehaviour {


    public Vector3 gridPosition;
    public int gridSizeX;
    public int gridSizeY;
    public float cellDiameter;

    public GameObject cellPrefab;

    public float updateDelay;
    Grid grid;


    void Start () {

        grid = new Grid(gridPosition, gridSizeX, gridSizeY, cellDiameter, cellPrefab);
        
	}
    

    float timer = 0;

	void Update () {
        
        timer += Time.deltaTime;
        if (timer > updateDelay)
        {
            timer = 0;
            ProcessCells();            
            Cell.OnFrameEnd();
            
        }
	}

   
    int GetAdjacentCellCount(int rowIndex, int colIndex)
    {
        int aliveCellCount = 0;
        int adjacentRowIndex = rowIndex - 1; //start from the left-
        int adjacentColIndex = colIndex - 1;    //bottom cell
        if (adjacentRowIndex < 0) //connect the edge
        {
            adjacentRowIndex = grid.CellRowCount - 1;
        }
        if (adjacentColIndex < 0) //connect the edge
        {
            adjacentColIndex = grid.CellColumnCount - 1;
        }

        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (!(adjacentRowIndex == rowIndex && adjacentColIndex == colIndex))
                {
                    if(!grid.Cells[adjacentRowIndex, adjacentColIndex].GetDeathStatus())
                    {
                        aliveCellCount++;
                    }
                }               
                
                adjacentColIndex++;
                if (adjacentColIndex == grid.CellColumnCount) //connect the edge
                {
                    adjacentColIndex = 0;
                }
            }
            adjacentColIndex = colIndex - 1;
            if (adjacentColIndex < 0) //connect the edge
            {
                adjacentColIndex = grid.CellColumnCount - 1;
            }
            adjacentRowIndex++;
            if (adjacentRowIndex == grid.CellRowCount) //connect the edge
            {
                adjacentRowIndex = 0;
            }
        }

        return aliveCellCount;
    }

    void ProcessCells()
    {
        
        int aliveAdjacentCellCount ;
        

        for(int i=0; i < grid.CellRowCount; i++)
        {
            for(int j=0;j<grid.CellColumnCount; j++)
            {                
                Cell currentCell = grid.Cells[i, j];
                aliveAdjacentCellCount = GetAdjacentCellCount(i, j);

                currentCell.SetDeathStatus(currentCell.GetDeathStatus());//copy previous state value to the current status                               

                if (aliveAdjacentCellCount > 3)
                {
                    currentCell.SetDeathStatus(true);
                }
                else {
                    if (currentCell.GetDeathStatus()) //if current cell is dead
                    {
                        if (aliveAdjacentCellCount == 3)
                        {
                            currentCell.SetDeathStatus(false); //make this cell alive
                        }
                    }
                    else
                    {
                        if(aliveAdjacentCellCount < 2)
                        {
                            currentCell.SetDeathStatus( true); // this cell dies
                        }
                    }
                }
            }
        }
    }
}
