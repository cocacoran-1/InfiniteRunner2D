using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] obstacles;
    float spawnPosX = 26f; // ��ֹ� ���� ��ġ
    float spawnPosY = -2.5f; // ��ֹ� ���� ����
    float spawnSpeed = 2.5f; // ��ֹ� ���� �ӵ�
    float spawnDelay = 1f; // ��ֹ� ���� ���� �ð�
    void Start()
    {
        InvokeRepeating("SpawnObstacle", spawnDelay, spawnSpeed); // 1�� �Ŀ� SpawnObstacle �޼��带 ȣ���ϰ�, ���� �� 2�ʸ��� ȣ��
    }

    void Update()
    {

    }
    void SpawnObstacle()
    {
        int randomIndex = Random.Range(0, obstacles.Length);
        if (randomIndex == 0)
        {
            spawnPosY = -2.5f; // ��ֹ��� Y ��ǥ�� -2.5�� ����
        }
        else if (randomIndex == 1)
        {
            spawnPosY = -3f; // ��ֹ��� Y ��ǥ�� -1.5�� ����
        }
       
        Vector3 spawnPosition = new Vector3(spawnPosX, spawnPosY, 0f);
        Instantiate(obstacles[randomIndex], spawnPosition, Quaternion.identity);
        spawnPosX += 26f; // ��ֹ� ���� ��ġ�� ���������� �̵�
    }
}