using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    void OnBecameInvisible()
    {
        gameObject.SetActive(false); // ī�޶� ������ ������ ��Ȱ��ȭ
    }
}
