using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class GameMaster : MonoBehaviour
{
#region References
    [Header("Players")]
    public Player[] players;                                                // Array of Players
    public BallController ball;                                             // Ball

    [Header("Texts")]
    public Text[] texts;                                                    // Array of Texts

    [Header("Rules")]
    public int matchLength;                                                 // Maximum amount of goals

    [Header("Menu UI")]
    public GameObject MenuUI;                                               // Menu UI GameObject

    [Header("Game UI")]
    public GameObject GameUI;                                               // Game UI GameObject

    [Header("End UI")]
    public GameObject EndUI;                                                // End UI GameObject
    public Image Winner;                                                    // EndUI - Winner Display
    public Sprite[] WinnerNum;                                              // EndUI - Winner Sprites
    public Text[] End_PTexts;                                               // EndUI - Player Score Texts
    public GameObject[] End_PCrowns;                                        // EndUI - Player Winner Crowns

    [Header("Data")]
    private int[] pScores;                                                   // Array of Score for each Player

    [SerializeField]
    private int totalMatches;                                               // Match counter
    [SerializeField]
    private List<MatchScore> matches = null;                                // Match list
 
    public void Start(){                                                    // Start is called before the first frame update
        pScores = new int[players.Length];                                  // Initialize Scores
        Load();                                                             // Load Data
        StartMenu();                                                        // Call StartMenu when game starts
    }
#endregion

#region Scene Logic
    
    public void StartMenu(){                                                // Start the Menu Scene
        // UI
        MenuUI.SetActive(true);                                             // Activate Menu
        GameUI.SetActive(false);                                            // Disable  others
        EndUI.SetActive(false);                                             // **
    }
    
    public void StartGame(){                                                // Start the Game by going from Menu to Game
        // Players
        foreach(Player p in players){                                       // Enable Players
            p.gameObject.SetActive(true);                                   // **
        }
        ResetScores();                                                      // Reset Scores

        // Ball
        ball.gameObject.SetActive(true);                                    // Enable Ball
    
        // UI
        MenuUI.SetActive(false);                                            // Activate GameUI
        GameUI.SetActive(true);                                             // Disable others
        EndUI.SetActive(false);                                             // **

        // Start Game
        StartCoroutine(GameStartCountdown());                               // Start Countdown for Game to Start
    }


    public void StartOver(){                                                // Go from End to Game
        // Players
        foreach(Player p in players){
            p.gameObject.SetActive(true);                                   // Enable Players
        }
        ResetScores();                                                      // Reset Scores

        // Ball
        ball.ResetBall();                                                   // Reset Ball
        ball.gameObject.SetActive(true);                                    // Set Ball Active

        // UI
        MenuUI.SetActive(false);                                            // Activate GameUI
        GameUI.SetActive(true);                                             // Disable others
        EndUI.SetActive(false);                                             // **

        // Start Game
        StartCoroutine(GameStartCountdown());                               // Start Countdown for Game to Start
    }

    
    public void BackToMenu(){                                               // Go back to the Menu (from End)
        // Players
        foreach(Player p in players){
            p.gameObject.SetActive(false);                                  // Disable all players
        }
        ResetScores();                                                      // Reset Scores
        
        // Ball
        ball.ResetBall();                                                   // Reset Ball
        ball.gameObject.SetActive(false);                                   // Disable Ball

        // UI
        MenuUI.SetActive(true);                                             // Change UI to Menu
        GameUI.SetActive(false);                                            // Disable others
        EndUI.SetActive(false);                                              // **      
        UpdateEndUI();                                                      // UpdateEndUI
    }

    private void GameOver(){                                                // On GameOver
        // Players
        foreach(Player p in players){
            p.gameObject.SetActive(false);                                  // Disable all players
        }
        
        // Ball
        ball.ResetBall();                                                   // Reset Ball
        ball.gameObject.SetActive(false);                                   // Disable Ball

        // UI
        GameUI.SetActive(false);                                            // Change UI to GameOver
        EndUI.SetActive(true);                                              // **      
        UpdateEndUI();                                                      // UpdateEndUI

        // Data
        totalMatches ++;                                                    // Update totalMatches
        matches.Add(new MatchScore(pScores[0],pScores[1],totalMatches));    // Add match results to List
        Save();                                                             // Save Data
    }

    private void UpdateEndUI(){
        for(int i = 0; i < pScores.Length; i++){                            // Update all Scores
            End_PTexts[i].text = pScores[i].ToString();                     // Update Final Scores
            if(pScores[i] == matchLength){
                End_PCrowns[i].SetActive(true);                             // Give Winner the Crown
                Winner.sprite = WinnerNum[i];                               // Change Winner Number
            }
        }
    }
#endregion
  
#region Game Logic
    public void Score(int playerNumber){                                    // Score a goal
        pScores[playerNumber-1]++;                                          // Give score for PlayerNumber
        Debug.Log("Player " + playerNumber + " scored a Goal!");
        UpdateTexts();                                                      // Update score texts
        CheckEnd();                                                         // Check if game over

        StartCoroutine(GameStartCountdown());                               // Start Countdown for Game to Start
    }

    private void CheckEnd(){                                                // Check if the game is over
        for(int i = 0; i < pScores.Length; i++){                            // For each player
            if(pScores[i] == matchLength){                                  // If max num goals reached
                GameOver();                                                 // GameOver
            }
        }
    }

    private void UpdateTexts(){                                             // Update the Texts
        for(int i = 0; i < texts.Length; i++){                              // For each text
            texts[i].text = pScores[i].ToString();                          // Update it
        }
    }

    private void ResetScores(){                                             // Reset score for game
        for(int i = 0; i < pScores.Length; i++){
            pScores[i] = 0;                                                 // Reset Scores to 0 after GameOver
        }
        UpdateTexts();                                                      // Update the Texts
    }

    private IEnumerator GameStartCountdown(){                               // Do countdown for game to start
        for(int i = 3; i > 0; i--){                                         // 3, 2, 1
            foreach(Text t in texts){                                       // For both player's texts
                t.text = i.ToString();                                      // **
            }
            yield return new WaitForSeconds(1);                             // Wait 1 second, Loop
        }
        foreach(Text t in texts){   
            t.text = "GO";                                                  // Write GO
        }
        yield return new WaitForSeconds(1);                                 // Wait 1 second
        ball.StartBall();                                                   // Launch ball
        UpdateTexts();                                                      // Display scores in text
    }
#endregion

#region Save Logic
    void Load(){                                                            // Load data from File
        string fn = Application.persistentDataPath + "/filename.secret";    // File name
        if(File.Exists(fn)){
            BinaryFormatter bf = new BinaryFormatter();                     // Open bf
            FileStream file = File.Open(fn, FileMode.Open);                 // Open file
            DataMaster dm = (DataMaster)bf.Deserialize(file);               // Load DataMaster
            file.Close();                                                   // Close File

            matches = dm.matches;                                           // Get Match List
            totalMatches = dm.totalMatches;                                 // Get Match Counter
        }   
    }

    void Save(){
        BinaryFormatter bf = new BinaryFormatter();                         // Open bf
        FileStream file = File.Create(Application.persistentDataPath        // Open file
                                        + "/filename.secret");              // file name
        DataMaster dm = new DataMaster();                                   // Load DataMaster

        dm.matches = matches;                                               // Save Match List
        dm.totalMatches = totalMatches;                                     // Save Match Counter

        bf.Serialize(file, dm);                                             // Serialize Data
        file.Close();                                                       // Close File
    }
#endregion
}

[Serializable]
class MatchScore{                                                           // Class Holding Data for each Math
    [SerializeField]    
    int p1Score;                                                            // P1 Score
    [SerializeField]    
    int p2Score;                                                            // P2 Score
    [SerializeField]
    int matchNo;                                                            // Match Number

    public MatchScore(int p1, int p2, int mn){                              // Constructor
        p1Score = p1;                                                       // **
        p2Score = p2;                                                       // **
        matchNo = mn;                                                       // **
    }

}

[Serializable]
class DataMaster{                                                           // Class For Saving
    public int totalMatches;                                                // Match counter
    public List<MatchScore> matches;                                        // Match list
}
