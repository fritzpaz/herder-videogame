using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DataManager : MonoBehaviour
{
#region DATA
    // GAME
    public int score;
    public int money;
    public int highscore;

    // SOUND
    public float musicVolume;                               // Music refers to the background music
    public float audioVolume;                               // Audio refers to sound effects

    // TERRAIN
    public float startLevelSpeed;
    public float levelSpeed;
    public readonly float maxLevelSpeed;
    List<GameObject> chunkHistory;

    // STORE
    // define how to setup available players, selected players, etc.

    // ORGANIZATION
    public int sceneNumber;
    public bool ads;

#endregion

#region MAIN
    // Runs when DataManager is enabled
    void OnEnable(){
        // Scene Loading Listener
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    // Runs when a different scene is loaded
    void OnSceneLoaded(Scene scene, LoadSceneMode mode){
        Debug.Log("OnSceneLoaded: " + scene.name);
    }
    // Runs when DataManager is disabled
    void OnDisable(){
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    #endregion

    // MUST IMPLEMENT!!!!!!!!!!!!!!!!!!
    #region SAVE

    #endregion
    // MUST IMPLEMENT!!!!!!!!!!!!!!!!!!

    #region METHODS
    // Add integer 's' to score
    public void AddScore(int s){
        score += s;
        if(score > highscore){
            highscore = score;
        }
    }

    // Add integer 'm' to money
    public void AddMoney(int m){
        money += 1;
    }

    // Set levelSpeed to fixed 'speed' value. Bounded by maxLevelSpeed
    public void SetLevelSpeed(float speed){
        if(speed > maxLevelSpeed){
            speed = maxLevelSpeed;
        }
        levelSpeed = speed;
    }

    // Set levelSpeed to 'percentage' based on current levelSpeed. Bounded by maxLevelSpeed
    public void SetLevelSpeedPercentage(float percentage){
        if(levelSpeed * percentage > maxLevelSpeed){
            percentage = maxLevelSpeed/levelSpeed;
        }
        levelSpeed = levelSpeed * percentage;
    }

    // Add a chunk to list of chunks
    public void AddChunk(GameObject chunk){
        chunkHistory.Add(chunk);
    }

    // Get theentire chunkHistory
    public List<GameObject> GetChunkHistory(){
        return chunkHistory;
    }

    // Get the last chunk
    public GameObject GetLastChunk(){
        if(chunkHistory.Count == 0){
            return new GameObject();
        }
        return chunkHistory[chunkHistory.Count - 1];
    }

    // Reset Data
    public void PrepDataForGame()
    {
        score = 0;
        levelSpeed = startLevelSpeed;
    }
#endregion
}

[SerializeField]
public class GameData{
    #region DATA
    // GAME
    public int score;
    public int money;
    public int highscore;
    public float levelSpeed;
    public readonly float maxLevelSpeed;
    List<GameObject> chunkHistory;

    // STORE
    // define how to setup available players, selected players, etc.

    // ORGANIZATION
    public int sceneNumber;
    public bool ads;

#endregion
}