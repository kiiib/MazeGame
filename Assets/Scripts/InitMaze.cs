using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitMaze : MonoBehaviour {

    [Header("Maze Settings")]
    [Tooltip("Cell's GameObject")]
    public GameObject Cell;
    [Tooltip("Maze's Width")]
    public int MazeWidth = 10;  // Maze's Width
    [Tooltip("Maze's Height")]
    public int MazeHeight = 10; // Maze's Height



    private Stack<GameObject> Grid = new Stack<GameObject>(); // record the maze

	// Use this for initialization
	void Start () {
        // Init the maze
        for (int i = 0; i < MazeHeight; i++) {
            for (int j = 0; j < MazeWidth; j++) {
                GameObject newCell;
                newCell = Instantiate(Cell, new Vector3(i, 0, j), Quaternion.identity);
                
                CellParameter newCellParameters = newCell.GetComponent<CellParameter>();
                //CellParameter.
                string newCellName = "Cell" + i + j;
                newCell.name = newCellName;
                // push each cell into Grid
                Grid.Push(GameObject.Find(newCellName));
            }
        }
        
    }
	
    private void checkNeighbors() {
        string startCellName = "Cell00";
        GameObject currentCell = GameObject.Find(startCellName);

    }

    // Update is called once per frame
    void Update () {
		
	}
}
