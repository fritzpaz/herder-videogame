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

    public AudioClip dogBarkSound;                          // Dog's Barking Sound 
    public Vector2 dogBarkSoundInterval;                    // Interval between barks (random in range)
    AudioSource audioSource;                                // AudioSource Component
    #endregion

    #region MAIN
    private void Awake()
    {
        instance = this;                                    // Establish static instance
    }

    void Start(){
        clickPos = transform.position;                      // Initial click position to be dog's spawn position
        audioSource = GetComponent<AudioSource>();            // Get AudioSource Reference

        StartCoroutine("PlayBarkSound");                    // Start coroutine to bark every 1 to 3 seconds.
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

            
            //Angle towards there
            transform.rotation = Quaternion.Euler(0, 0, Mathf.Rad2Deg * angle + 270);

        }

    }

    // Bark Every Few Seconds
    IEnumerator PlayBarkSound()
    {
        audioSource.PlayOneShot(dogBarkSound);
        yield return new WaitForSeconds(Random.Range(dogBarkSoundInterval.x, dogBarkSoundInterval.y));

        StartCoroutine("PlayBarkSound");
    }
    #endregion
}
