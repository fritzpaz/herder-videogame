using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//https://docs.unity3d.com/ScriptReference/Vector3.Lerp.html

public class Dog : MonoBehaviour
{
    #region DATA
    public static Dog instance { get; private set; }        // Self reference

    Vector3 clickPos;                                       // Position of click

    public float speed;                                     // Dog's speed

    public AudioSource Bark;
    #endregion

    #region MAIN
    private void Awake()
    {
        instance = this;                                    // Establish static instance
    }

    void Start(){
        clickPos = transform.position;                      // Initial click position to be dog's spawn position
        Bark = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //Get Click Position
        if(Input.GetMouseButtonDown(0)){
            // Get click position on world. Make sure z-axis isn't changed
            clickPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            clickPos.z = transform.position.z;
            
        }

        // Move towards point if not there
        if(Vector3.Distance(transform.position, clickPos) > 0.2){
            // Get Angle
            float angle = Mathf.Atan2(clickPos.y - transform.position.y, clickPos.x - transform.position.x);

            // Move there
            transform.position = new Vector3(   transform.position.x + (speed * Mathf.Cos(angle) * Time.deltaTime),
                                            transform.position.y + (speed * Mathf.Sin(angle) * Time.deltaTime),
                                            transform.position.z);
            Bark.Play();
            //Angle towards there
            transform.rotation = Quaternion.Euler(0, 0, Mathf.Rad2Deg * angle + 270);
        }

    }
    #endregion
}
