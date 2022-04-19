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
    public float targetLevelSpeed;
    public float levelSpeed;
    public float maxLevelSpeed;
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
        Load();
    }
    // Runs when a different scene is loaded
    void OnSceneLoaded(Scene scene, LoadSceneMode mode){
        Debug.Log("OnSceneLoaded: " + scene.name);
    }
    // Runs when DataManager is disabled
    void OnDisable(){
        SceneManager.sceneLoaded -= OnSceneLoaded;
        Save();
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
        levelSpeed = 0;

    }

    // Save and Load
    public void Load()
    {
        money = PlayerPrefs.GetInt("money", 0);
        highscore = PlayerPrefs.GetInt("highscore", 0);
        musicVolume = PlayerPrefs.GetFloat("music", 1);
        audioVolume = PlayerPrefs.GetFloat("audio", 1);
        if (PlayerPrefs.GetString("ads", "true").Equals("false")) { ads = false; } else { ads = true; }
    }
    public void Save()
    {
        PlayerPrefs.SetInt("money", money);
        PlayerPrefs.SetInt("highscore", highscore);
        PlayerPrefs.SetFloat("music", musicVolume);
        PlayerPrefs.SetFloat("audio", audioVolume);
        if (ads.Equals("false")) { PlayerPrefs.SetString("ads", "false"); }

    }
#endregion
}