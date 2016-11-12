/* Programmed by Ajith Gopinathan
 * ajithdevel@gmail.com
*/

using UnityEngine;
using System.Collections;

public class Grid 
{

   
    int gridSizeX;
    int gridSizeY;
    public int CellRowCount { get; private set; }
    public int CellColumnCount { get; private set; }

    public Cell[,] Cells { get; private set; }
    

    public Grid(Vector3 gridPosition, int sizeX, int sizeY, float cellDiameter
        , GameObject cellPrefab)
    {
      
        this.gridSizeX = sizeX;
        this.gridSizeY = sizeY;        

        CellRowCount = Mathf.RoundToInt(gridSizeY / cellDiameter);
        CellColumnCount = Mathf.RoundToInt(gridSizeX / cellDiameter);
        Cells = new Cell[CellRowCount, CellColumnCount];      

        Vector3 tempCellPosition = Vector3.zero;
        float tempCellPosX, tempCellPosY;

        tempCellPosX = gridPosition.x - CellColumnCount / 2 * cellDiameter + cellDiameter / 2;
        tempCellPosY = gridPosition.y - CellRowCount / 2 * cellDiameter + cellDiameter / 2;
        Vector3 cellZeroPosition = new Vector3(tempCellPosX, tempCellPosY, gridPosition.z); //position of cell[0,0]
        bool isDead;
        for (int i = 0; i < CellRowCount; i++)
        {
            for (int j = 0; j < CellColumnCount; j++)
            {
                tempCellPosX = cellZeroPosition.x + j * cellDiameter;
                tempCellPosY = cellZeroPosition.y + i * cellDiameter;
                tempCellPosition.Set(tempCellPosX, tempCellPosY, gridPosition.z);

                isDead = Random.Range(0f, 99f) < 50; //randomly set death status of the cells

                Cells[i, j] = new Cell(tempCellPosition, isDead, cellDiameter, cellPrefab
                    ,  "Cell " + i + "," + j);

            }
        }
      
    }


    void Update()
    {

    }
}
