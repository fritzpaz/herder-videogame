using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighscoreText : MonoBehaviour
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

        // Update Highscore Text
        text.text = data.highscore.ToString("000 000 000");
    }
    #endregion
}
