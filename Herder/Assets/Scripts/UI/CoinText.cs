using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinText : MonoBehaviour
{
    #region DATA
    Text text;              // Text Reference
    DataManager data;       // Local datamanger reference
    #endregion

    #region MAIN
    // Start is called before the first frame update
    void Start()
    {
        // Text Reference
        text = GetComponent<Text>();

        // Data Reference
        data = Singleton.instance.data;
    }

    // Update is called once per frame
    void Update()
    {
        // Update Score Text
        text.text = "x" + data.money.ToString();
    }
    #endregion
}
