using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LakeBehavior : MonoBehaviour
{
    public AudioSource Bubble;
    void Start () {
        Bubble = GetComponent<AudioSource>();
    }
   private void OnCollisionEnter2D(Collision2D collison){
        if (collison.gameObject.tag == "Sheep"){
            Bubble.Play();
            Destroy(collison.gameObject,1);
        }
    }
}
