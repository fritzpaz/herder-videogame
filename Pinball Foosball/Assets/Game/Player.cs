using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Paddles")]
    public PaddleController[] paddles;                                      // Array of paddles
    public float paddleSpeed;                                               // Paddle speed

    [Header("Controls")]
    public KeyCode[] controls;                                              // Controls

    [Header("Player Field")]
    public GameObject[] fieldTiles;                                         // Tiles
    private GameObject[][] tilePos = new GameObject[3][];                   // Jagged 2D array

    // Start is called before the first frame update
    void Start()
    {
        // Initialize Paddles
        foreach(PaddleController p in paddles){
            p.speed = paddleSpeed;                                          // Make sure paddle speed is right
            p.Initialize();                                                 // Initialize Paddle
        }

        // Initialize tile array
        tilePos[0] = new GameObject[4];
        tilePos[1] = new GameObject[5];
        tilePos[2] = new GameObject[5];
    }


    // Update happens each frame
    void Update(){
        for(int i = 0; i < paddles.Length; i++){                            // For each Paddle
            if(Input.GetKey(controls[i])){                                  // If corresponding input is pressed
                paddles[i].MovePaddleUp();                                  // Move paddle up
                Debug.Log(this.transform.name + " paddle: " + i + " up");
            }
            else{                                                           // If not pressed
                paddles[i].MovePaddleDown();                                // Snap it back down
            }
        }
    }

}
