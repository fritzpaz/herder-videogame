using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
====================== R E S O U R C E S ======================
https://gamedevbeginner.com/singletons-in-unity-the-right-way/#:~:text=Generally%20speaking%2C%20a%20singleton%20in,or%20to%20other%20game%20systems.
*/

public class Singleton : MonoBehaviour
{
#region SINGLETON/MAIN
    public static Singleton instance {get; private set;}                    // Reference for itself
    public DataManager data {get; private set;}                             // Reference for DataManager

    void Awake()
    {
        // Delete any repeated instances
        if(instance != null && instance != this){
            Destroy(this.gameObject);
        }
        else{
            instance = this;
        }
        // Keep this one alive
        DontDestroyOnLoad(this);

        // Reference for DataManager
        data = GetComponentInChildren<DataManager>();
    }
#endregion
}
