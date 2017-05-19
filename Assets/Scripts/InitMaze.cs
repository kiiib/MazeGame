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
        List<GameObject> cellPath = new List<GameObject>(); // record the maze created path
        GameObject currentCell = GameObject.Find(startCellName);
        currentCell.GetComponent<CellParameter>().isVisited = true;
        do {
            
            GameObject nextCell = checkNeighbors(currentCell);

            // if exist neighbor which haven't been visited
            if (nextCell) {
                // mark the next cell is visited
                Grid[nextCell.GetComponent<CellParameter>().cellNumber].GetComponent<CellParameter>().isVisited = true;
                // push the current cell into list
                cellPath.Add(currentCell);
                // remove the wall between the current cell and the chosen cell
                removeWalls(currentCell, nextCell);
                // set it to be the next one
                currentCell = nextCell;
                //Debug.Log("next cell number is " + nextCell.GetComponent<CellParameter>().cellNumber);
            } else {
                int lastElementIndex = cellPath.Count - 1;
                cellPath.RemoveAt(lastElementIndex);
                currentCell = cellPath[lastElementIndex - 1];
            }
        } while (currentCell.GetComponent<CellParameter>().cellNumber != 0);
    }
	
    private GameObject checkNeighbors(GameObject currentCell) {
        List<GameObject> neighbors = new List<GameObject>(); // record neighbors
        float x = currentCell.transform.position.x;
        float z = currentCell.transform.position.z;

        // figure out the edge problem
        if (z - 1 >= 0) {
            GameObject cellLeft = Grid[getIndex(x, z - 1)];
            if (!cellLeft.GetComponent<CellParameter>().isVisited) {
                neighbors.Add(cellLeft);
            }
        }
        if (x + 1 < MazeWidth) {
            GameObject cellBottom = Grid[getIndex(x + 1, z)];
            if (!cellBottom.GetComponent<CellParameter>().isVisited) {
                neighbors.Add(cellBottom);
            }
        }
        if (z + 1 < MazeWidth) {
            GameObject cellRight = Grid[getIndex(x, z + 1)];
            if (!cellRight.GetComponent<CellParameter>().isVisited) {
                neighbors.Add(cellRight);
            }
        }
        if (x - 1 >= 0) {
            GameObject cellTop = Grid[getIndex(x - 1, z)];
            if (!cellTop.GetComponent<CellParameter>().isVisited) {
                neighbors.Add(cellTop);
            }
        }
        // if neighors are exist, pick up a random neigbor
        if (neighbors.Count > 0) {
            
            int randomNeighborIndex = Random.Range(0, neighbors.Count - 1);
            //Debug.Log("randomNeighborIndex " + randomNeighborIndex);
            return neighbors[randomNeighborIndex];
        }else {
            return null;
        }
    }

    private int getIndex(float x, float z) {
        // figure out the invalid index
        if (x < 0 || z < 0 || x > MazeWidth - 1 || z > MazeWidth - 1)
            return -1;
        return (int)(x * MazeWidth + z);   // depends on MazeWidth, grid(MazeWidth x MazeWidth)
    }

    private void removeWalls(GameObject currentCell, GameObject neighborCell) {
        int currentCellNumber = currentCell.GetComponent<CellParameter>().cellNumber;
        int neighborCellNumber = neighborCell.GetComponent<CellParameter>().cellNumber;
        //Debug.Log("current cell number is " + currentCellNumber + " neighbor cell number is " + neighborCellNumber);
        int x = (int) (currentCell.transform.position.x - neighborCell.transform.position.x);
        int z = (int) (currentCell.transform.position.z - neighborCell.transform.position.z);

        // childs' index: 0 top wall, 1 right wall, 2 bottom wall, 3 left wall
        // current cell's left wall and neighbor cell's right wall
        if (z == 1) {
            Grid[currentCellNumber].transform.GetChild(3).gameObject.SetActive(false);
            Grid[neighborCellNumber].transform.GetChild(1).gameObject.SetActive(false);
        }
        // current cell's right wall and neighbor cell's left wall
        if (z == -1) {
            Grid[currentCellNumber].transform.GetChild(1).gameObject.SetActive(false);
            Grid[neighborCellNumber].transform.GetChild(3).gameObject.SetActive(false);
        }
        // current cell's top wall and neighbor cell's bottom wall
        if (x == 1) {
            Grid[currentCellNumber].transform.GetChild(0).gameObject.SetActive(false);
            Grid[neighborCellNumber].transform.GetChild(2).gameObject.SetActive(false);
        }
        // current cell's bottom wall and neighbor cell's top wall
        if (x == -1) {
            Grid[currentCellNumber].transform.GetChild(2).gameObject.SetActive(false);
            Grid[neighborCellNumber].transform.GetChild(0).gameObject.SetActive(false);
        }

    }

    // Update is called once per frame
    void Update () {
        
    }
}
