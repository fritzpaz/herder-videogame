using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondChunkBehavior : MonoBehaviour
{
    public float speed = 10.0f;
    private Rigidbody2D rb;
    private Vector2 boundary;
    // Start is called before the first frame update
    void Start()
    {
         rb=this.GetComponent<Rigidbody2D>();
         rb.velocity = new Vector2(0,-speed);
         boundary = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y < boundary.y){
            Destroy(this.gameObject);
        }
    }          
}
