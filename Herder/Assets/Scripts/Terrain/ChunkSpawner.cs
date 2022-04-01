using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkSpawner : MonoBehaviour
{
    #region DATA
    public Chunk[] chunks;                      // Array of possible spawnable prefab chunks
    public float chunkSpawnPos;                 // Chunk Spawning Position
    public float chunkDeathPos;                 // Chunk Destroy Position

    public List<Chunk> activeChunks;
    #endregion

    // Start is called before the first frame update
    void Start(){
        SpawnChunk();   // Spawn First Chunk on Start
    }

    // Update
    private void Update()
    {
        if(Singleton.instance.data.levelSpeed < Singleton.instance.data.startLevelSpeed)
        {
            Singleton.instance.data.levelSpeed += 0.5f * Time.deltaTime;
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
