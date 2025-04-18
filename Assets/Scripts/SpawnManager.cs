using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] obstacles;
    float spawnPosX = 26f; // 장애물 생성 위치
    float spawnPosY = -2.5f; // 장애물 생성 높이
    float spawnSpeed = 2.5f; // 장애물 생성 속도
    float spawnDelay = 1f; // 장애물 생성 지연 시간
    void Start()
    {
        InvokeRepeating("SpawnObstacle", spawnDelay, spawnSpeed); // 1초 후에 SpawnObstacle 메서드를 호출하고, 이후 매 2초마다 호출
    }

    void Update()
    {

    }
    void SpawnObstacle()
    {
        int randomIndex = Random.Range(0, obstacles.Length);
        if (randomIndex == 0)
        {
            spawnPosY = -2.5f; // 장애물의 Y 좌표를 -2.5로 설정
        }
        else if (randomIndex == 1)
        {
            spawnPosY = -3f; // 장애물의 Y 좌표를 -1.5로 설정
        }
       
        Vector3 spawnPosition = new Vector3(spawnPosX, spawnPosY, 0f);
        Instantiate(obstacles[randomIndex], spawnPosition, Quaternion.identity);
        spawnPosX += 26f; // 장애물 생성 위치를 오른쪽으로 이동
    }
}