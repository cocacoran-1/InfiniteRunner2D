using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyManager : MonoBehaviour
{
    public static DifficultyManager Instance { get; private set; }

    [Header("난이도 설정")]
    public float tickInterval = 1f;           // 몇 초마다 난이도 상승할지
    public float difficultyTick = 0f;         // 누적 틱 수
    PlayerController player;
    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
        player = FindObjectOfType<PlayerController>();
    }

    void Start()
    {
        StartCoroutine(DifficultyRoutine());
    }

    IEnumerator DifficultyRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(tickInterval);

            difficultyTick++;

            // 난이도 적용 대상 호출
            ScoreZoneManager.Instance?.ApplyDifficulty(difficultyTick);
            SpawnManager.Instance?.ApplyDifficulty(difficultyTick);
        }
    }
}
