using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinBehavior : MonoBehaviour
{
    public AudioSource coinChing;

    // Start is called before the first frame update
    void Start()
    {
        coinChing = GetComponent<AudioSource>();
    }


    public void OnTriggerEnter2D(Collider2D collision){
        if(collision.gameObject.tag == "Sheep"){

            coinChing.Play();        
            Destroy(gameObject,.1f);   
            
            //Add Coins to Score - Fritz
        }
    }
}
