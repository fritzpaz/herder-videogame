using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleController : MonoBehaviour
{
    [Header("Components")]
    private HingeJoint2D hingeJoint2D;
    private JointMotor2D jointMotor;

    [Header("Variables")]
    public float speed;
    public bool negative;
 
    public void Initialize()
    {
        hingeJoint2D = GetComponent<HingeJoint2D>();
        jointMotor = hingeJoint2D.motor;
        jointMotor.maxMotorTorque = Mathf.Abs(speed);
        if(negative) {  speed *= -1; }
    }
 
    // Move the Paddle Up
    public void MovePaddleUp(){
        jointMotor.motorSpeed = speed;                                      // set motor speed to max
        hingeJoint2D.motor = jointMotor;                                    // update motor
    }

    public void MovePaddleDown(){
        jointMotor.motorSpeed = -speed;                                     // snap motor back
        hingeJoint2D.motor = jointMotor;                                    // update motor
    }
}
