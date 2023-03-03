using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ObstaclesSpawner : MonoBehaviour
{

    [Header("Constraints Config")]
    [SerializeField] private float maxYSpawn = 4.7f;
    [SerializeField] private float minYSpawn = 2f;
    [SerializeField] private float rightSideSpawnX = 4f;
    [SerializeField] private float leftSideSpawnX = -4f;

    [Header("Bird")]
    [SerializeField] private float spawnFrequencyMax = 5f;
    [SerializeField] private float spawnFrequencyMin = 1.5f;
    [FormerlySerializedAs("birdEnemyPrefab")] [SerializeField] private GameObject birdPrefab;

    private List<GameObject> _spawnedObstacles;
    private void Start()
    {
        _spawnedObstacles = new List<GameObject>();
        var spawnFrequency = Random.Range(spawnFrequencyMin, spawnFrequencyMax);
        StartCoroutine(SpawnBirdsRoutine(spawnFrequency));
    }
    
    private IEnumerator SpawnBirdsRoutine(float cooldown = 3f)
    {
        while (true)
        {
            SpawnBirdObstacle();
            yield return new WaitForSeconds(cooldown);
        }
    }

    private void SpawnBirdObstacle()
    {
        var isRandomDirectionRight = Random.value >= 0.5f;
        
        var xValue = isRandomDirectionRight ? leftSideSpawnX : rightSideSpawnX; // Movement direction side, opposite to spawn side
        
        var spawnPosition = new Vector3(
            xValue,
            Random.Range(minYSpawn, maxYSpawn),
            0f);

        var bird = Instantiate(birdPrefab, spawnPosition, Quaternion.identity, transform);

        bird.GetComponent<HorizontalMover>().isDirectionRight = isRandomDirectionRight;
        
        _spawnedObstacles.Add(bird);
    }

    public void DestroySpawnedObstacles()
    {
        if (_spawnedObstacles.Count == 0) return;
        
        foreach (var obstacle in _spawnedObstacles)
        {
            Destroy(obstacle);   
        }
        
        _spawnedObstacles = new List<GameObject>();
    }
}
