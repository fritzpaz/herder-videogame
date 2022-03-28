using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LakeBehavior : MonoBehaviour
{
   
    private void OnCollisionEnter2D(Collision2D collison){
        if (collison.gameObject.tag == "Sheep"){
            Destroy(collison.gameObject);
        }
            
        
    }

}
