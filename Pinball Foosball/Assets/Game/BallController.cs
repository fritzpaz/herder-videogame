using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
// Components
    [Header("Components")]
    public GameMaster gameMaster;                                           // Declare GameMaster
    private Rigidbody2D rb;                                                 // Declare rigidbody

    [Header("Physics")]
    public float incline;                                                   // Field incline
    public float sideForce;                                                 // Force of Ball to Side
    private float gravity;                                                  // Gravity reference
    private float gravity_start;                                            // Gravity at start of game

    // Start is called before the first frame update
    void Start()
    {   
        // Get Components                             
        rb = GetComponent<Rigidbody2D>();                                   // Initialize rb
        
        // Define Values
        incline *= Mathf.Deg2Rad;                                           // Convert Incline to Rad
        gravity = rb.gravityScale * rb.mass * Mathf.Sin(incline);           // Define world gravity
        gravity_start = gravity;
        rb.gravityScale = gravity;                                          // Set gravity to ball

        // Make Ball Stationary
        rb.isKinematic = true;
    }

    // Update is called every frame
    void Update(){
        if(rb.isKinematic){                                                 // If ball isn't moving
            if(Input.GetKeyDown(KeyCode.S)){                                // Make it start when s is pressed
                StartBall();                                                // Call StartBall()
            }
        }
    }

    // Physics Update
    void FixedUpdate()
    {
        // Update ball's gravity as it moves on field
        if(transform.position.y < 0)
        {
            rb.gravityScale = gravity;                                      // Bottom half positive gravity
        }
        else
        {
            rb.gravityScale = -gravity;                                     // Upper half negative gravity
        }
    }

    // When Colliding with a Trigger
    void OnTriggerEnter2D(Collider2D col){
        // Get Goal Collisions
        if(col.name == "Goal 1"){
            gameMaster.Score(1);                                            // Process goal for Player 1
            ResetBall();                                                    // Reset the Ball
        }else{
            gameMaster.Score(2);                                            // Process goal for Player 2
            ResetBall();                                                    // Reset the Ball
        }
    }

    // When Colliding with a Paddle
    void OnCollisionEnter2D(Collision2D col){
        // Get Paddle Collisions
        if(gravity < gravity_start * 1.5f){
            gravity = gravity * 1.05f;
        }
    }

    // Make Ball Start Moving
    public void StartBall(){
        Debug.Log("Start");
        rb.isKinematic = false;                                             // Make Ball Move again
        int dir = Rand1();
        Debug.Log("Direction: " + dir);
        float z = rb.transform.position.z;
        rb.transform.position = new Vector3(0,-0.001f * dir, z);            // Adjust Ball Position
        rb.gravityScale *= Rand1();                                         // Start Ball with Random direction (-1 or 1)
        rb.AddForce(RandSideForce());                                       // Add Random Sideways force
    }

    // Reset Ball Position
    public void ResetBall(){
        transform.position = new Vector3(0,0,transform.position.z);         // Put Ball in Center
        rb.angularVelocity = 0;                                             
        rb.velocity = new Vector2(0,0);                                     // Make Ball Stationary
        rb.isKinematic = true;                                              // Make Ball Kinematic
    }

    // Update Object's Vertical Acceleration \acceleration.y\
    private void UpdateDirection()
    {
        rb.gravityScale *= -1;                                              // Flip gravityScale
    }

    // Get Random Plus or Minus 1
    private int Rand1(){
        if(Random.Range(0f, 1f) < 0.5){                                     // 50 50 chance
            return -1;                                                      // Return -1
        }
        return 1;                                                           // Return 1
    }

    // Get a Random Sideways Force for the Ball
    private Vector2 RandSideForce(){
        Vector2 direction = new Vector2(1 * Rand1(),0);                     // Random Direction (Left or Right)
        float force = sideForce * (Random.Range(0.5f,1.0f));                // Random Force (50% to 100%)
        //Debug.Log(direction + "," + force + "," + sideForce);
        return direction * force;                                           // Return Force
    }


}
