using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreZoneManager : MonoBehaviour
{
    public static ScoreZoneManager Instance { get; private set; }

    [Header("������ �� Ǯ ����")]
    public GameObject fruitPrefab; // ������ ���� ������
    public int poolSize = 10;

    [Header("���� Ÿ�̹� �� ��ġ")]

    public float arcHeight = 3f; // ��ġ�� ��� ����
    public float arcWidth = 5f; // ��ġ�� ��� �ʺ�
    public int pointsPerArc = 5; // ��ġ �ϳ��� ���� ����

    private List<GameObject> fruitPool = new List<GameObject>();
    private float currentSpawnX = 26f;

    [Header("��ġ ���� ��ġ ������")]
    public Vector2 arcStartOffset = new Vector2(0f, 0f);

    [Header("���̵� ���� ����")]
    public float initialDelay = 1f;
    public float initialInterval = 2.5f;
    public float spawnAcceleration = 0.2f;
    public float minInterval = 0.8f;

    private float currentInterval;
    void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        InitPool();
        currentInterval = initialInterval;
        StartCoroutine(SpawnRoutine());
    }

    void InitPool()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject fruit = Instantiate(fruitPrefab, new Vector3(1000, 1000, 0), Quaternion.identity);
            fruit.SetActive(false);
            fruit.transform.SetParent(transform);
            fruitPool.Add(fruit);
        }
    }
    GameObject GetPooledFruit()
    {
        foreach (GameObject fruit in fruitPool)
        {
            if (!fruit.activeInHierarchy) return fruit;
        }

        GameObject newFruit = Instantiate(fruitPrefab);
        newFruit.SetActive(false);
        newFruit.transform.SetParent(transform); // Ǯ�� �ڽ����� ����
        fruitPool.Add(newFruit);
        return newFruit;
    }
    IEnumerator SpawnRoutine()
    {

        int spawnCount = 0;
        while (true)
        {
            SpawnFruitArc();
            yield return new WaitForSeconds(currentInterval);
            spawnCount++;
            if (spawnCount % 5 == 0 && currentInterval > minInterval)
            {
                currentInterval -= spawnAcceleration;
                Debug.Log("Current Interval: " + currentInterval);
            }
        }
    }
    void SpawnFruitArc()
    {
        for (int i = 0; i < pointsPerArc; i++)
        {
            float t = (float)i / (pointsPerArc - 1); // 0 ~ 1 ���� ����
            float x = Mathf.Lerp(0, arcWidth, t); // X ���� ����
            float y = Mathf.Sin(t * Mathf.PI) * arcHeight; // ��ġ � (���� � Ȱ��)

            // ���� ������ + ��ġ ���� ��ǥ
            Vector3 spawnPos = new Vector3(
                currentSpawnX + arcStartOffset.x + x,
                arcStartOffset.y + y,
                0f
            );
            GameObject fruit = GetPooledFruit();
            fruit.transform.position = spawnPos;
            fruit.SetActive(true);
        }

        currentSpawnX += 26f; // ���� ��ֹ� ��ġ �������� �̵�
    }
    public void ApplyDifficulty(float difficultyTick)
    {
        float adjustedInterval = initialInterval - (difficultyTick * 0.05f);
        currentInterval = Mathf.Max(adjustedInterval, minInterval);
        // �÷��̾� �ӵ��� �Բ� ���� ����
        PlayerController player = FindObjectOfType<PlayerController>();
        if (player != null)
        {
            player.ApplyDifficulty(difficultyTick);
            Debug.Log($"[�ӵ� �����] ");
        }
    }

}
