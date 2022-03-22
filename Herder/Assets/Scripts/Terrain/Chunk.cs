using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk : MonoBehaviour
{
    #region DATA
    // Components
    public ChunkSpawner dad;                // ChunkSpawner (parent)
    SheepManager sm;                        // Sheep Manager reference

    // Chunk Data
    public float size;                      // Size of this chunk (determine when next chunk is spawned)
    bool spawnedNextChunk = false;          // Check if it has spawned the next chunk
    #endregion

    #region MAIN

    // Update is called once per frame
    void Update()
    {
        // Make chunk scroll down
        transform.position = new Vector3(   transform.position.x,
                                            transform.position.y - (Singleton.instance.data.levelSpeed * Time.deltaTime),
                                            transform.position.z );
        
        // Spawn next chunk after it has scroller enough
        if(transform.position.y < dad.chunkSpawnPos - size & !spawnedNextChunk){
            dad.SpawnChunk();
            spawnedNextChunk = true;
        }

        // Destroy chunk when it reaches death position
        if(transform.position.y < dad.chunkDeathPos){
            Destroy(this.gameObject);
        }
    }
    #endregion
}
