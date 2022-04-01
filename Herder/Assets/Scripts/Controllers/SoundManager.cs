using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;

/// HOWTO USE SOUNDMANAGER
/// 
/// SoundManager is designed to not crash even if something is wrong, and will just do nothing instead.
/// If sound is not playing, code won't crash. Be aware when debugging.
/// If it crashes, let Fritz know please
/// 
/// 1. Declare SoundManager Reference in Code
/// #   SoundManager sm;
/// 
/// 2. In Start, Set Reference
/// #   public void Start(){
/// #       sm = Singleton.instance.sound;
/// #   }
/// 
/// 3. To Play an AudioClip Once:
/// #   sm.PlayAudioClip(~ clip name ~);
/// 
/// 4. To Play Audio Clip without Overriding Last Audio Clip:
/// #   sm.PlayAudioClipAfterLastPlayer(~ clip name ~);
/// 
/// 5. To Stop Current Audio Clip from Playing:
/// #   sm.CutAudioClip();
/// 
/// 6. To Change Music Volume (between 0 and 1):
/// #   sm.ChangeMusicVolume(~ volume ~);
/// 
/// 7. To Change Audio Volume (between 0 and 1):
/// #   sm.ChangeAudioVolume(~ volume ~);
/// 
/// If more than one audio clip needs to be played simultaneously, we need to define how many maximum, 
/// and it can be implemented. Let Fritz know if this is something worth doing.
/// I - Fritz - didn't implement this because I feel like many simultaneous audio clips would be hectic,
/// but don't mind adding a few. Just tell me how many. An endless amount might be doable (not sure), but
/// could be terribly inefficient. 
/// 
/// HOWTO USE SOUNDMANAGER


public class SoundManager : MonoBehaviour
{
    #region DATA
    DataManager data;                           // DataManager Reference
    AudioSource musicSource;                    // AudioSource for music (background music) Reference

    public AudioClip musicClip;                 // Music clip for looping background music

    #endregion


    #region MAIN
    private void Start()
    {
        data = Singleton.instance.data;                                         // DataManager Reference
        musicSource = GetComponent<AudioSource>();                              // Assign Music Player Audio Source

        StartMusic();                                                           // Starts background music
    }

    // Start Playing the Background Music
    public void StartMusic()
    {
        if (musicSource && musicClip)
        {
            musicSource.clip = musicClip;                                       // Set music source's clip
            musicSource.Play();                                                 // Play on repeat
            musicSource.loop = true;                                            // Repeat
        }
    }

    public void ChangeMusicVolume(float volume)
    {
        // Volume should be passed as between 0 and 1
        if((volume >= 0 || volume <= 1 ) && musicSource)
        {
            data.musicVolume = volume;          // Save in DataManager for settings persistence
            musicSource.volume = volume;
        }
        else
        {
            Debug.LogError("ChangeMusicVolume: Volume not between 0 and 1.");
        }
    }

    public void ChangeMasterVolume(float volume)
    {
        // Volume should be passed as between 0 and 1
        if ((volume >= 0 || volume <= 1 ))
        {
            AudioListener.volume = volume;
        }
        else
        {
            Debug.LogError("ChangeAudioVolume: Volume not between 0 and 1.");
        }
    }

    #endregion
}