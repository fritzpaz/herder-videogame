using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SheepAliveText : MonoBehaviour
{
    #region DATA
    Text text;                              // Text Reference
    public SheepManager sheepManager;       // Local SheepManager reference
    #endregion

    #region MAIN
    // Start is called before the first frame update
    void Start()
    {
        // Text Reference
        text = GetComponent<Text>();

    }

    // Update is called once per frame
    void Update()
    {
        // Update Score Text
        text.text = "x" + sheepManager.sheepCount.ToString();
    }
    #endregion
}
