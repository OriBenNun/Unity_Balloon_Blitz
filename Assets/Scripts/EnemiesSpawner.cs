using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesSpawner : MonoBehaviour
{
    [SerializeField] private GameObject birdEnemyPrefab;

    [SerializeField] private float maxYSpawn = 4.7f;
    [SerializeField] private float minYSpawn = 2f;
    [SerializeField] private float rightSideSpawnX = 4f;
    [SerializeField] private float leftSideSpawnX = -4f;

    private List<GameObject> _spawnedEnemies;
    private void Start()
    {
        _spawnedEnemies = new List<GameObject>();
        StartCoroutine(SpawnBirdsRoutine(1));
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

        bird.GetComponent<IEnemy>().isDirectionRight = isRandomDirectionRight;
        
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
