using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// ItemInfoâ�� ������ �����ϴ� Ŭ����
/// </summary>
public class ItemInfo : MonoBehaviour
{
    /// <summary>
    /// ������ �̸�
    /// </summary>
    public TextMeshProUGUI infoName;           
    public TextMeshProUGUI itemInformation; 
    public CanvasGroup infoCanvasGroup;
    public RectTransform infoTransform;
    public TempSlotInfoUI infoTempSlotUI;

    private void Awake()
    {
        infoName = transform.Find("Name").GetComponent<TextMeshProUGUI>();
        itemInformation = transform.Find("Information").GetComponent<TextMeshProUGUI>();
        infoCanvasGroup = GetComponent<CanvasGroup>();
        infoTransform = GetComponent<RectTransform>();
        infoTempSlotUI = FindObjectOfType<TempSlotInfoUI>();
    }

    /// <summary>
    /// UI�� ������ �� ����� �޼���
    /// </summary>
    public void OpenInfo()
    {
        infoCanvasGroup.alpha = 1.0f;
        infoCanvasGroup.blocksRaycasts = true;
        infoCanvasGroup.interactable = true;
    }

    public void CloseInfo()
    {
        infoCanvasGroup.alpha = 0.0f;
        infoCanvasGroup.blocksRaycasts = false;
        infoCanvasGroup.interactable = false;
    }
}
