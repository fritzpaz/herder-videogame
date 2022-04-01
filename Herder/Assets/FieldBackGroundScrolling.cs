using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldBackGroundScrolling : MonoBehaviour
{
    #region DATA
    public float height;
    #endregion

    #region MAIN

    // Update is called once per frame
    void Update()
    {
        // Make chunk scroll down
        transform.position = new Vector3(transform.position.x,
                                            transform.position.y - (Singleton.instance.data.levelSpeed * Time.deltaTime),
                                            transform.position.z);

        // Reset Position
        if (transform.position.y <= -height)
        {
            transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        }
    }
    #endregion
}
