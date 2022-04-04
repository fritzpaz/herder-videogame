using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OntriggerEnter(Collider2D collision){
        if(collision.gameObject.tag == "Sheep"){
            Destroy(this.gameObject);
            //Add Coins to Score - Fritz
        }
    }
}
