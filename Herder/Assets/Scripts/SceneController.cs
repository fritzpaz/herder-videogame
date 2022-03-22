using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{   
#region DATA
    public static SceneController instance {get; private set;}              // Self reference

    DataManager data;                                                       // DataManager reference to Singleton DataManager
#endregion

#region MAIN
    public void Start(){
        data = Singleton.instance.data;                                     // Establishing reference
    }
#endregion

#region METHODS
    // Update currentScene to Active Scene
    public void UpdateSceneNumber(){
        data.sceneNumber = SceneManager.GetActiveScene().buildIndex;    
    }

    // Update currentScene to given number
    public void UpdateSceneNumber(int sceneNumber){
        data.sceneNumber = sceneNumber;
    }

    // Load Scene Synchronously
    // Forces Loading and switches to scene once loading on the next frame
    // Can cause a stutter.
    public void LoadScene(int sceneNumber){
        if(SceneManager.GetSceneByBuildIndex(sceneNumber) != null){
            SceneManager.LoadScene(sceneNumber);
            UpdateSceneNumber(sceneNumber);
        }
        else{
            Debug.Log("Scene Load Failed. sceneNumber:" + sceneNumber + " is not a valid build index.");
        }
    }

    // Load Scene Asynchronously. Coroutine is called to wait for completion.
    // Loads in the background and only switches scene when loading is complete?
    public void LoadSceneAsync(int sceneNumber){
        StartCoroutine(AsyncLoadingCoroutine(sceneNumber));
    }
    IEnumerator AsyncLoadingCoroutine(int sceneNumber){
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneNumber);

        // Wait Until Asynchronous Loading is completed to load scene
        while(!asyncLoad.isDone){
            yield return null;
        }

        UpdateSceneNumber(sceneNumber);
    }

    // Exit Game
    public void ExitGame(){
        #if UNITY_EDITOR
            // Application.Quit() does not work in the editor so
            // UnityEditor.EditorApplication.isPlaying need to be set to false to end the game
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
#endregion
}