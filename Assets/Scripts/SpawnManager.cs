using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager Instance { get; private set; }
    [Header("장애물 설정")]
    public GameObject[] obstaclePrefabs; // 장애물 프리팹들
    public int poolSize = 5; // 각 장애물당 풀 사이즈

    [Header("스폰 위치 및 타이밍")]
    public float baseSpawnX = 26f;
    public float[] spawnHeights = { -2.5f, -3f };


    private float currentSpawnX;
    private List<GameObject> obstaclePool = new List<GameObject>();

    [Header("난이도 조절 설정")]
    public float initialDelay = 1f;
    public float initialInterval = 2.5f;
    public float spawnAcceleration = 0.2f;
    public float minInterval = 0.8f;

    private float currentInterval;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        currentSpawnX = baseSpawnX;
        InitializePool();
        currentInterval = initialInterval;
        StartCoroutine(SpawnRoutine());
    }

    // 오브젝트 풀 생성
    void InitializePool()
    {
        foreach (GameObject prefab in obstaclePrefabs)
        {
            for (int i = 0; i < poolSize; i++)
            {
                GameObject obj = Instantiate(prefab, new Vector3(1000, 1000, 0), Quaternion.identity);
                obj.transform.SetParent(transform); // 풀의 자식으로 설정
                obj.SetActive(false);
                obstaclePool.Add(obj);
            }
        }
    }

    // 풀에서 비활성화된 오브젝트 가져오기
    GameObject GetPooledObject(int obstacleIndex)
    {
        foreach (GameObject obj in obstaclePool)
        {
            if (!obj.activeInHierarchy && obj.name.Contains(obstaclePrefabs[obstacleIndex].name))
            {
                return obj;
            }
        }
        // 비활성화된 오브젝트가 없으면 새로 생성
        // 없으면 새로 생성하고 풀에 추가
        GameObject newObj = Instantiate(obstaclePrefabs[obstacleIndex], 
            new Vector3(1000, 1000, 0), Quaternion.identity);
        newObj.SetActive(false);
        newObj.transform.SetParent(transform); // 풀의 자식으로 설정

        obstaclePool.Add(newObj);
        return newObj;
    }
    IEnumerator SpawnRoutine()
    {

        int spawnCount = 0;
        while (true)
        {
            SpawnObstacle();

            spawnCount++;
            yield return new WaitForSeconds(currentInterval);

            if (spawnCount % 5 == 0 && currentInterval > minInterval)
            {
                currentInterval -= spawnAcceleration;
            }
        }
    }
    void SpawnObstacle()
    {
        int randomIndex = Random.Range(0, obstaclePrefabs.Length);
        float spawnY = (randomIndex < spawnHeights.Length) ? spawnHeights[randomIndex] : -2.5f;
        Vector3 spawnPosition = new Vector3(currentSpawnX, spawnY, 0f);

        GameObject obstacle = GetPooledObject(randomIndex);
        if (obstacle != null)
        {
            obstacle.transform.position = spawnPosition;
            obstacle.SetActive(true);
        }

        currentSpawnX += 26f;
    }
    public void ApplyDifficulty(float difficultyTick)
    {
        float adjustedInterval = initialInterval - (difficultyTick * 0.05f);
        currentInterval = Mathf.Max(adjustedInterval, minInterval);
    }

}