using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreZoneManager : MonoBehaviour
{
    [Header("프리팹 및 풀 설정")]
    public GameObject fruitPrefab; // 점수용 과일 프리팹
    public int poolSize = 10;

    [Header("스폰 타이밍 및 위치")]
    public float spawnInterval = 2.5f;
    public float initialDelay = 1f;
    public float arcHeight = 3f; // 아치형 곡선의 높이
    public float arcWidth = 5f; // 아치형 곡선의 너비
    public int pointsPerArc = 5; // 아치 하나당 과일 개수

    private List<GameObject> fruitPool = new List<GameObject>();
    private float currentSpawnX = 26f;

    [Header("아치 시작 위치 오프셋")]
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
        newFruit.transform.SetParent(transform); // 풀의 자식으로 설정
        fruitPool.Add(newFruit);
        return newFruit;
    }
    void SpawnFruitArc()
    {
        for (int i = 0; i < pointsPerArc; i++)
        {
            float t = (float)i / (pointsPerArc - 1); // 0 ~ 1 사이 분포
            float x = Mathf.Lerp(0, arcWidth, t); // X 간격 분포
            float y = Mathf.Sin(t * Mathf.PI) * arcHeight; // 아치 곡선 (사인 곡선 활용)

            // 시작 오프셋 + 아치 형태 좌표
            Vector3 spawnPos = new Vector3(
                currentSpawnX + arcStartOffset.x + x,
                arcStartOffset.y + y,
                0f
            );
            GameObject fruit = GetPooledFruit();
            fruit.transform.position = spawnPos;
            fruit.SetActive(true);
        }

        currentSpawnX += 26f; // 다음 장애물 위치 기준으로 이동
    }
}
