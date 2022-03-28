using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeOut : MonoBehaviour
{
    SpriteRenderer fade;                    // Used for Fading Sheep
    void Start()
    {
        // Used for Fading
        fade= GetComponent<SpriteRenderer>();
    }

    IEnumerator Fade(){
       // Fade sheep a bit every 0.05 seconds
       for (float f =1f; f > 0f; f-= 0.05f){
           Color c = fade.material.color;
           c.a = f;                                         // Changing alpha (transparency)
           fade.material.color = c;
           yield return new WaitForSeconds(0.05f);
       }
    }
}
