using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepositionMap : MonoBehaviour
{
    GameObject player;
    [SerializeField] GameObject[] backgrounds;
    int backgroundIndex = 0;

    float nextTriggerX = 25f;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (player.transform.position.x >= nextTriggerX) // 다음 기준 위치에 도달하면
        {
            backgrounds[backgroundIndex].transform.position += new Vector3(26 * 2, 0, 0); // 배경을 오른쪽으로 2칸 이동 
            backgroundIndex = (backgroundIndex + 1) % 2; // 인덱스 번갈아 가기
            nextTriggerX += 26f; // 다음 기준점도 앞으로 이동
        }
    }
}
