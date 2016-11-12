/* Programmed by Ajith Gopinathan
 * ajithdevel@gmail.com
*/

using UnityEngine;
using System.Collections;

public class Cell {

    private bool[] dead = new bool[2]; //variable that stores the dead/alive status of the cell
                                       //for the previous frame and current frame  
        
    static int currentStatusIndex;
    static int prevStatusIndex;

    public static void OnFrameEnd() //swap the status indices
    {
        currentStatusIndex = prevStatusIndex;
        prevStatusIndex = prevStatusIndex == 0 ? 1 : 0;
    }

    public bool GetDeathStatus() 
    {
        return dead[prevStatusIndex]; //return the cell status at the end of previous frame
    }
        
    public void SetDeathStatus(bool dead) //set the cell status for the current frame
    {
        this.dead[currentStatusIndex] = dead;
        if (dead)
        {
            
            material.color = Color.grey;

        }
        else
        {
            
            material.color = Color.green;
        }

    }
    
        
    public Vector3 position;
    private GameObject gameObject;
    private Material material;
    
	
    public Cell(Vector3 position, bool dead, float cellDiameter, GameObject cellPrefab, string name
        )
    {
        gameObject = Object.Instantiate(cellPrefab); //create a cell based on the prefab
        gameObject.name = name;      
        material = gameObject.GetComponent<Renderer>().material;
        currentStatusIndex = 1;
        prevStatusIndex = 0;
        SetDeathStatus(dead);
        this.dead[0] = dead;
                
        this.position = position;        
        float cellBoundsX = cellPrefab.GetComponent<Renderer>().bounds.size.x;
        cellDiameter = cellDiameter - cellDiameter * .1f; //add a small gap
        float scaleFactor = cellDiameter / cellBoundsX; //get a scale factor depending on the 
                                                        //current size of the prefab
        gameObject.transform.localScale *= scaleFactor; //Make the size of the gameobject uniform
                                                        //for all prefab sizes;
        gameObject.transform.position = position;
        
    }
}
