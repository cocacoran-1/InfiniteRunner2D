using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyManager : MonoBehaviour
{
    public static DifficultyManager Instance { get; private set; }

    [Header("���̵� ����")]
    public float tickInterval = 1f;           // �� �ʸ��� ���̵� �������
    public float difficultyTick = 0f;         // ���� ƽ ��
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

            // ���̵� ���� ��� ȣ��
            ScoreZoneManager.Instance?.ApplyDifficulty(difficultyTick);
            SpawnManager.Instance?.ApplyDifficulty(difficultyTick);
        }
    }
}
