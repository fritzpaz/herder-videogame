using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepManager : MonoBehaviour
{

    #region DATA
    public static SheepManager instance { get; private set; }               // Self reference
    DataManager data;                                                       // DataManager Reference
    
    public int sheepCount;                                                  // Sheep Count
    public float sheepDeathPos;                                             // Sheep Death Position
    #endregion

    #region MAIN
    public void Awake()
    {  
        instance = this;                                                    // Static Self Reference
        data = Singleton.instance.data;                                     // Set Reference
    }

    // Update is called once per frame
    public void Update()
    {
        // If sheep exist, keep score increasing
        if(sheepCount > 0){
            data.AddScore(sheepCount);
        }
        // Else GameOver
        else if (data.score > 300)
        {
            GameOver();
        }
    }
    #endregion

    #region METHODS
    // End Game
    void GameOver()
    {
        // Load Scene #2
        SceneController.instance.LoadScene(2);
    }
    #endregion
}
