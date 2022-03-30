using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarnBehavior : MonoBehaviour
{
    public GameObject sheepPrefab;              // Prefab for Spawning Sheep
    SpriteRenderer fade;                        // Used for Fading Sheep
    SoundManager soundManager;                  // SoundManager Reference
    public AudioClip breakSound;                // Breaking Barn Sound

    void Start()
    {
        // Used for Fading
        fade= GetComponent<SpriteRenderer>();

        soundManager = Singleton.instance.sound;        // SoundManager Reference
    }

    private void OnTriggerEnter2D(Collider2D collision){
        //If Sheep Collides with Lake, Make the Sheep Fade then Destroy itself
        if (collision.gameObject.tag == "Dog"){
            StartCoroutine("Fade");  // Call IEnumerator
            soundManager.PlayAudioClip(breakSound);

            // Spawn a Random Number of Sheep
            for (int i = 0; i < Random.Range(1,6); i++){
                Instantiate(sheepPrefab, transform.position, Quaternion.identity);
            }

            // Destroy Barn
            Destroy(gameObject);
        }
    }

    IEnumerator Fade()
    {
        // Fade barn a bit every 0.05 seconds
        for (float f = 1f; f > 0f; f -= 0.25f)
        {
            Color c = fade.material.color;
            c.a = f;                                         // Changing alpha (transparency)
            fade.material.color = c;
            yield return new WaitForSeconds(0.05f);
        }
    }
}
    
