using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkSpawner : MonoBehaviour
{
    public GameObject chunkPrefab;
    public float respawn = 1.0F;
    private Vector2 boundary;
    // Start is called before the first frame update
    void Start()
    {
        boundary = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width/1/2, Screen.height, Camera.main.transform.position.z));
        StartCoroutine(chunkSpawn());
    }
    private void spawnChunk(){
        GameObject c = Instantiate(chunkPrefab) as GameObject;
        c.transform.position = new Vector2(-boundary.x,boundary.x);
    }
    // Update is called once per frame
    IEnumerator chunkSpawn(){
       while(true)  {
        yield return new WaitForSeconds(respawn);
        spawnChunk();
            }
        }
    }
