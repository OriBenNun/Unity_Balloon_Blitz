using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class ObstaclesSpawner : MonoBehaviour
{

    [SerializeField] private Transform spawnedParent;
    [SerializeField] private float autoKillAfterSeconds = 20f;
    
    [Header("Constraints Config")]
    [SerializeField] private float maxYSpawn = 4.7f;
    [SerializeField] private float minYSpawn = 2f;
    [SerializeField] private float rightSideSpawnX = 4f;
    [SerializeField] private float leftSideSpawnX = -4f;

    [Header("Bird")]
    [SerializeField] private float spawnFrequencyMax = 5f;
    [SerializeField] private float spawnFrequencyMin = 1.5f;
    [SerializeField] private float scaleMax = 0.8f;
    [SerializeField] private float scaleMin = 0.3f;
    [FormerlySerializedAs("birdEnemyPrefab")] [SerializeField] private GameObject birdPrefab;

    private List<IObstacle> _spawnedObstacles;
    private void Start()
    {
        
        _spawnedObstacles = new List<IObstacle>();
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
        // ReSharper disable once IteratorNeverReturns
    }

    private void SpawnBirdObstacle()
    {
        var isRandomDirectionRight = Random.value >= 0.5f;
        
        var xValue = isRandomDirectionRight ? leftSideSpawnX : rightSideSpawnX; // Movement direction side, opposite to spawn side

        var myPosition = transform.position;
        var minY = minYSpawn + myPosition.y;
        var maxY = maxYSpawn + myPosition.y;

        var spawnPosition = new Vector3(
            xValue,
            Random.Range(minY, maxY),
            0f);

        var birdObject = Instantiate(birdPrefab, spawnPosition, Quaternion.identity, spawnedParent);

        birdObject.GetComponent<HorizontalMover>().IsDirectionRight = isRandomDirectionRight;

        var randomScale = Random.Range(scaleMin, scaleMax);
        birdObject.GetComponent<Transform>().localScale = new Vector3(randomScale, randomScale, randomScale);

        birdObject.GetComponent<SpriteRenderer>().flipX = isRandomDirectionRight;
        
        var birdObstacle = birdObject.GetComponent<IObstacle>();
        _spawnedObstacles.Add(birdObstacle);

        DestroyObstacle(birdObstacle ,autoKillAfterSeconds); // To clean the scene of far away obstacles
    }

    private void DestroyObstacle(IObstacle obstacle, float waitSeconds)
    {
        obstacle.Die(waitSeconds);
    }

    public void DestroyAllSpawnedObstacles()
    {
        if (_spawnedObstacles.Count == 0) return;
        
        foreach (var obstacle in _spawnedObstacles)
        {
            DestroyObstacle(obstacle, 0);
        }
        
        _spawnedObstacles.Clear();
    }
}
