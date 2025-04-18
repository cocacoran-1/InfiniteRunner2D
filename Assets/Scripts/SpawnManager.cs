using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [Header("��ֹ� ����")]
    public GameObject[] obstaclePrefabs; // ��ֹ� �����յ�
    public int poolSize = 5; // �� ��ֹ��� Ǯ ������

    [Header("���� ��ġ �� Ÿ�̹�")]
    public float baseSpawnX = 26f;
    public float[] spawnHeights = { -2.5f, -3f };
    public float spawnInterval = 2.5f;
    public float initialDelay = 1f;

    private float currentSpawnX;
    private List<GameObject> obstaclePool = new List<GameObject>();
    void Start()
    {
        currentSpawnX = baseSpawnX;
        InitializePool();
        InvokeRepeating(nameof(SpawnObstacle), initialDelay, spawnInterval);
    }

    // ������Ʈ Ǯ ����
    void InitializePool()
    {
        foreach (GameObject prefab in obstaclePrefabs)
        {
            for (int i = 0; i < poolSize; i++)
            {
                GameObject obj = Instantiate(prefab, new Vector3(1000, 1000, 0), Quaternion.identity);
                obj.transform.SetParent(transform); // Ǯ�� �ڽ����� ����
                obj.SetActive(false);
                obstaclePool.Add(obj);
            }
        }
    }

    // Ǯ���� ��Ȱ��ȭ�� ������Ʈ ��������
    GameObject GetPooledObject(int obstacleIndex)
    {
        foreach (GameObject obj in obstaclePool)
        {
            if (!obj.activeInHierarchy && obj.name.Contains(obstaclePrefabs[obstacleIndex].name))
            {
                return obj;
            }
        }
        // ��Ȱ��ȭ�� ������Ʈ�� ������ ���� ����
        // ������ ���� �����ϰ� Ǯ�� �߰�
        GameObject newObj = Instantiate(obstaclePrefabs[obstacleIndex], 
            new Vector3(1000, 1000, 0), Quaternion.identity);
        newObj.SetActive(false);
        newObj.transform.SetParent(transform); // Ǯ�� �ڽ����� ����

        obstaclePool.Add(newObj);
        return newObj;
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
}