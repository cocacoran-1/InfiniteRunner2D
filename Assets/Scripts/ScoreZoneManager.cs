using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreZoneManager : MonoBehaviour
{
    [Header("������ �� Ǯ ����")]
    public GameObject fruitPrefab; // ������ ���� ������
    public int poolSize = 10;

    [Header("���� Ÿ�̹� �� ��ġ")]
    public float spawnInterval = 2.5f;
    public float initialDelay = 1f;
    public float arcHeight = 3f; // ��ġ�� ��� ����
    public float arcWidth = 5f; // ��ġ�� ��� �ʺ�
    public int pointsPerArc = 5; // ��ġ �ϳ��� ���� ����

    private List<GameObject> fruitPool = new List<GameObject>();
    private float currentSpawnX = 26f;

    [Header("��ġ ���� ��ġ ������")]
    public Vector2 arcStartOffset = new Vector2(0f, 0f);

    // Start is called before the first frame update
    void Start()
    {
        InitPool();
        InvokeRepeating(nameof(SpawnFruitArc), initialDelay, spawnInterval);
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
}
