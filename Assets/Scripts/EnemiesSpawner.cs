using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesSpawner : MonoBehaviour
{

    [Header("Constraints Config")]
    [SerializeField] private float maxYSpawn = 4.7f;
    [SerializeField] private float minYSpawn = 2f;
    [SerializeField] private float rightSideSpawnX = 4f;
    [SerializeField] private float leftSideSpawnX = -4f;

    [Header("Bird Enemy")]
    [SerializeField] private float spawnFrequencyMax = 5f;
    [SerializeField] private float spawnFrequencyMin = 1.5f;
    [SerializeField] private GameObject birdEnemyPrefab;

    private List<GameObject> _spawnedEnemies;
    private void Start()
    {
        _spawnedEnemies = new List<GameObject>();
        var spawnFrequency = Random.Range(spawnFrequencyMin, spawnFrequencyMax);
        StartCoroutine(SpawnBirdsRoutine(spawnFrequency));
    }
    
    private IEnumerator SpawnBirdsRoutine(float cooldown = 3f)
    {
        while (true)
        {
            SpawnBirdEnemy();
            yield return new WaitForSeconds(cooldown);
        }
    }

    private void SpawnBirdEnemy()
    {
        var isRandomDirectionRight = Random.value >= 0.5f;
        
        var xValue = isRandomDirectionRight ? leftSideSpawnX : rightSideSpawnX; // Movement direction side, opposite to spawn side
        
        var spawnPosition = new Vector3(
            xValue,
            Random.Range(minYSpawn, maxYSpawn),
            0f);

        var bird = Instantiate(birdEnemyPrefab, spawnPosition, Quaternion.identity, transform);

        bird.GetComponent<HorizontalMover>().isDirectionRight = isRandomDirectionRight;
        
        _spawnedEnemies.Add(bird);
    }

    public void DestroySpawnedEnemies()
    {
        if (_spawnedEnemies.Count == 0) return;
        
        foreach (var enemy in _spawnedEnemies)
        {
            Destroy(enemy);   
        }
        
        _spawnedEnemies = new List<GameObject>();
    }
}
