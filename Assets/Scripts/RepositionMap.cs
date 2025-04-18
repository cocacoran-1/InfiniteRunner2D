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
        if (player.transform.position.x >= nextTriggerX) // ���� ���� ��ġ�� �����ϸ�
        {
            backgrounds[backgroundIndex].transform.position += new Vector3(26 * 2, 0, 0); // ����� ���������� 2ĭ �̵� 
            backgroundIndex = (backgroundIndex + 1) % 2; // �ε��� ������ ����
            nextTriggerX += 26f; // ���� �������� ������ �̵�
        }
    }
}
