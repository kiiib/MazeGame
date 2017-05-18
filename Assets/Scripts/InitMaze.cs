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

    private Stack<GameObject> Grid; // record the maze

	// Use this for initialization
	void Start () {
        // Init the maze
        for (int i = 0; i < MazeHeight; i++) {
            for (int j = 0; j < MazeWidth; j++) {
                GameObject newCell;
                newCell = Instantiate(Cell, new Vector3(i, 0, j), Quaternion.identity);
                Grid.Push(newCell);
                newCell.name = "Cell" + i + j;
                
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
