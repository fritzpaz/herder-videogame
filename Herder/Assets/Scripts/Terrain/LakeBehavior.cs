using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LakeBehavior : MonoBehaviour
{
    SoundManager soundManager;                              // SoundManager Reference
    public AudioClip breakSound;                            // Breaking Barn Sound

    void Start () {
        soundManager = Singleton.instance.sound;            // SoundManager Reference
    }
   private void OnCollisionEnter2D(Collision2D collison){
        if (collison.gameObject.tag == "Sheep"){
            soundManager.PlayAudioClip(breakSound);         // Play audio clip when sheep dies   
            Destroy(collison.gameObject,1);
        }
    }
}
