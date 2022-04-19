using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkSpawner : MonoBehaviour
{
    #region DATA
    public DataManager data;                    // Data Manager reference

    public Chunk[] chunks;                      // Array of possible spawnable prefab chunks
    public float chunkSpawnPos;                 // Chunk Spawning Position
    public float chunkDeathPos;                 // Chunk Destroy Position

    public List<Chunk> activeChunks;
    #endregion

    // Start is called before the first frame update
    void Start(){
        SpawnChunk();   // Spawn First Chunk on Start

        data = Singleton.instance.data;                 // Set Reference
        data.targetLevelSpeed = data.startLevelSpeed;   // Set target Level Speed
    }

    // Update
    private void Update()
    {
        // Adjust target speed based on score
        if(data.targetLevelSpeed < data.maxLevelSpeed)
        {
            if(((data.maxLevelSpeed) * (data.score / 99999f)) > data.startLevelSpeed){
                data.targetLevelSpeed = (data.maxLevelSpeed) * (data.score / 99999f);
            }
        }

        // Change Actual level scrolling speed
        if(data.levelSpeed < data.targetLevelSpeed)
        {
            data.levelSpeed += 0.5f * Time.deltaTime;
        }

    }

    // Spawns the Chunk and Gives it necessary information/organization to keep project neat
    public void SpawnChunk(){
        // Spawning of next chunk is handled within the chunk
        Chunk c = Instantiate(chunks[Random.Range(0, chunks.Length)]);
        activeChunks.Add(c);
        c.transform.position = new Vector3(0, chunkSpawnPos, 0);
        c.dad = this;
        c.transform.parent = this.transform;        
    }
}
