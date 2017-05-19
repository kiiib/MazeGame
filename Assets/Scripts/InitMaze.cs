using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitMaze : MonoBehaviour {

    [Header("Maze Settings")]
    [Tooltip("Cell's GameObject")]
    public GameObject Cell;
    [Tooltip("Maze's Width and Height")]
    public int MazeWidth = 10;  // Maze's Width and Height
    [Tooltip("Maze start point")]
    public string startCellName = "Cell00";

    private List<GameObject> Grid = new List<GameObject>(); // record the maze
    
    // Use this for initialization
    void Start () {
        int cellNumber = 0;
        CellParameter newCellParameters = new CellParameter();
        // Init the maze
        for (int i = 0; i < MazeWidth; i++) {
            for (int j = 0; j < MazeWidth; j++) {
                GameObject newCell;
                newCell = Instantiate(Cell, new Vector3(i, 0, j), Quaternion.identity);
                // setting cell's parameters
                newCellParameters = newCell.GetComponent<CellParameter>();
                newCellParameters.cellNumber = cellNumber;
                string newCellName = "Cell" + i + j;
                newCell.name = newCellName;
                // push each cell into Grid
                Grid.Add(GameObject.Find(newCellName));
                cellNumber++;
            }
        }
        // Check Neighbors and remove walls
        //GameObject currentCell = GameObject.Find(startCellName);
        //currentCell.GetComponent<CellParameter>().isVisited = true;
        //do {
        //    GameObject Cell = checkNeighbors(currentCell);
        //} while (currentCell.GetComponent<CellParameter>().cellNumber != 0);
    }
	
    private GameObject checkNeighbors(GameObject currentCell) {
        List<GameObject> neighbors = new List<GameObject>(); // record neighbors
        CellParameter currentCellParameter = currentCell.GetComponent<CellParameter>();
        float x = currentCell.transform.position.x;
        float z = currentCell.transform.position.z;
        
        // figure out the edge problem
        if(z - 1 >= 0) {
            GameObject celltop = Grid[getIndex(x, z - 1)];
            if (!celltop.GetComponent<CellParameter>().isVisited) {
                neighbors.Add(celltop);
            }
        }
        if (z - 1 >= 0) {
            GameObject celltop = Grid[getIndex(x, z - 1)];
            if (!celltop.GetComponent<CellParameter>().isVisited) {
                neighbors.Add(celltop);
            }
        }
        if (z - 1 >= 0) {
            GameObject celltop = Grid[getIndex(x, z - 1)];
            if (!celltop.GetComponent<CellParameter>().isVisited) {
                neighbors.Add(celltop);
            }
        }
        if (z - 1 >= 0) {
            GameObject celltop = Grid[getIndex(x, z - 1)];
            if (!celltop.GetComponent<CellParameter>().isVisited) {
                neighbors.Add(celltop);
            }
        }
        return null;
    }
    private int getIndex(float x, float z) {
        //figure out the invalid index
        if (x < 0 || z < 0 || x > MazeWidth - 1 || z > MazeWidth - 1)
            return -1;

        return (int)(x + z * MazeWidth);   // depends on MazeWidth, grid(MazeWidth x MazeWidth)
    }

    // Update is called once per frame
    void Update () {
		
	}
}
