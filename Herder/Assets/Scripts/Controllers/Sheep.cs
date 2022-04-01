using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sheep : MonoBehaviour
{
    #region DATA
    SheepManager sm;                        // SheepManager instance
    Dog dog;                                // Dog instance
    DataManager data;                       // DataManager instance
    SpriteRenderer fade;                    // SpriteManager Reference rUsed for Fading Sheep
    AudioSource audioSource;                // AudioSource Reference
    public AudioClip sheepSpawnSound;       // Sheep Spawn Sound
    public float sheepSpawnSoundVolume;     // Sheep Spawn Sound Volume
    public AudioClip sheepDrownSound;       // Sheep Drown Sound
    public float sheepDrownSoundVolume;     // Sheep Drown Sound Volume

    public Vector2 sheepRepulsion;          // Sheep Repulsion Speed
    public Vector2 sheepMvmtX;              // Sheep Random Movement X
    float randomMvmtX;                      // Local Random Movement Variable
    public Vector2 sheepMvmtY;              // Sheep Random Movement Y
    float randomMvmtY;                      // Local Random Movement Variable
    public float sheepSpeed;                // Minimum Vertical Sheep Speed
    float distance;                         // Distance from Dog
    float angle;                            // Angle from Dog 
    public float sheepRadius;               // Radius of Effect                  
    #endregion

    #region MAIN
    // Start is called before the first frame update
    void Start()
    {
        print("sheep");

        // Sheep Manager Reference
        sm = SheepManager.instance;

        // Dog Reference
        dog = Dog.instance;

        // Data Reference
        data = Singleton.instance.data;

        // Update Count
        sm.sheepCount += 1;

        // SoundManager Reference
        audioSource = GetComponent<AudioSource>();

        // Used for Fading
        fade= GetComponent<SpriteRenderer>();

        // Play Sheep Spawn Sound
        audioSource.volume = sheepSpawnSoundVolume;
        audioSource.PlayOneShot(sheepSpawnSound);

        // Set Sheep Random Movement Direction
        StartCoroutine("RandomMovement");
    }

    // Update is called once per frame
    void Update()
    {
        // Get distance & angle from dog
        distance = Vector2.Distance(transform.position, dog.transform.position);

        // Make Sheep React to Dog Movement
        if (Vector2.Distance(dog.transform.position, transform.position) < sheepRadius)
        {
            angle = Mathf.Atan2(dog.transform.position.y - transform.position.y,
                    dog.transform.position.x - transform.position.x);

            // React to dog if close
            transform.position = new Vector3(   transform.position.x - (sheepSpeed * (sheepRepulsion.x * (1 / Mathf.Pow(distance, 1.2f) * Mathf.Cos(angle) * Time.deltaTime))),
                                                transform.position.y - (sheepSpeed * (sheepRepulsion.y * (1 / Mathf.Pow(distance, 1.2f) * Mathf.Sin(angle) * Time.deltaTime))),
                                                transform.position.z);

            // Rotate Sheep towards where it moves
            transform.rotation = Quaternion.Euler(0, 0, Mathf.Rad2Deg * angle + 90);
        }
        else
        {
            //Only change movement if far enough to avoid jitterness
            if(Vector2.Distance(dog.transform.position, transform.position) > sheepRadius + 0.5)
            {
                angle = Mathf.Atan2((transform.position.y + 2 + randomMvmtY) - transform.position.y,
                        (transform.position.x + randomMvmtX) - transform.position.x);

                // Rotate Sheep towards where it moves
                transform.rotation = Quaternion.Euler(0, 0, Mathf.Rad2Deg * angle + 270);
            }
            else
            {
                angle = Mathf.Atan2(dog.transform.position.y - transform.position.y,
                    dog.transform.position.x - transform.position.x);

                // Rotate Sheep towards where it moves
                transform.rotation = Quaternion.Euler(0, 0, Mathf.Rad2Deg * angle + 90);
            }

            // Move randomly when far from dog
            transform.position = new Vector3(   transform.position.x + (randomMvmtX * sheepSpeed * Time.deltaTime),
                                                transform.position.y + (randomMvmtY * sheepSpeed * Time.deltaTime),
                                                transform.position.z);

        }

        // Check for death
        if (transform.position.y < sm.sheepDeathPos)
        {
            Destroy(this.gameObject);
        }
    }

    IEnumerator RandomMovement()
    {
        randomMvmtX = Random.Range(sheepMvmtX.x, sheepMvmtX.y);
        randomMvmtY = Random.Range(sheepMvmtY.x, sheepMvmtY.y);
        yield return new WaitForSeconds(Random.Range(1f, 3));
        StartCoroutine("RandomMovement");
    }

    // When sheep is destroyed, reduce sheepCount
    private void OnDestroy()
    {
        sm.sheepCount -= 1;
    }

    // Handle Sheep's collision
    private void OnTriggerEnter2D(Collider2D collision){
        //If Sheep Collides with Lake, Make the Sheep Fade then Destroy itself
        if (collision.gameObject.tag == "Lake"){
            sheepSpeed = 0;                                     // Reduce sheep speed
            audioSource.volume = sheepDrownSoundVolume;         // Set Volume
            audioSource.PlayOneShot(sheepDrownSound);           // Play Drowning Sound
            StartCoroutine("FadeSheepSpriteInLake");            // Call IEnumerator
        }
    }
       
    //Allows for timing the fading/destruction of sheep.
   IEnumerator FadeSheepSpriteInLake(){
       // Fade sheep a bit every 0.05 seconds
       for (float f =1f; f > 0f; f-= 0.05f){
           Color c = fade.material.color;
           c.a = f;                                         // Changing alpha (transparency)
           fade.material.color = c;
           yield return new WaitForSeconds(0.05f);
       }
       
        Destroy(gameObject, 1);

    }
    #endregion
}


