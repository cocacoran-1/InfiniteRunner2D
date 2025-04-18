using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    void OnBecameInvisible()
    {
        gameObject.SetActive(false); // 카메라 밖으로 나가면 비활성화
    }
}
