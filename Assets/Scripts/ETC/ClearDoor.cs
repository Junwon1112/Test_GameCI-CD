using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// ���� ����ϸ� Ŭ���� ���������� �̵��ϴ� �Ϳ� ���� �Լ�
/// </summary>
public class ClearDoor : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            SceneManager.LoadScene("Clear");
        }
    }
}
