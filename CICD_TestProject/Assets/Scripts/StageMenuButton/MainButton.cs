using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// ���θ޴� ������ �̵���Ű�� Ŭ����
/// </summary>
public class MainButton : MonoBehaviour
{
    Button mainButton;

    private void Awake()
    {
        mainButton = GetComponent<Button>();
    }

    private void Start()
    {
        mainButton.onClick.AddListener(BackToMainStage);
    }

    private void BackToMainStage()
    {
        SceneManager.LoadScene("Main");
    }
}
