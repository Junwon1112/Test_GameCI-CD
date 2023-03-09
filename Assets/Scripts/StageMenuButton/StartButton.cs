using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// ���Ӿ����� �̵���Ű�� Ŭ����
/// </summary>
public class StartButton : MonoBehaviour
{
    SceneManager sceneManager;
    Button startButton;

    private void Awake()
    {
        startButton = GetComponent<Button>();
    }

    private void Start()
    {
        startButton.onClick.AddListener(StartStage);
    }

    private void StartStage()
    {
        SceneManager.LoadScene("Stage1");
    }
}
