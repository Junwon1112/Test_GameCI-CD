using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.InputSystem;

/// <summary>
/// ��� ���Կ� �� Ŭ����, TempSlotInfoUI�� ��ӹ���, �̰͵� SlotŬ������ ���� ����� ����� �߾�� �ߴ�
/// </summary>
public class EquipSlotUI : TempSlotInfoUI
{
    private TextMeshProUGUI takeSlotItemCountText;
    int takeID = -1;
    /// <summary>
    /// ���â ���� ���� ���̵� = 1001 ��
    /// </summary>
    public int equipSlotID = 1001;     

    void Awake()
    {
        this.itemImage = GetComponentInChildren<Image>();
        takeSlotItemCountText = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        ClearTempSlot();
    }

    /// <summary>
    /// �ش� ������ ������ ���� �Լ�
    /// </summary>
    public void ClearTempSlot()
    {
        itemImage.color = Color.clear;
        takeSlotItemCountText.alpha = 0;
        takeSlotItemData = null;
        takeSlotItemCount = 0;
    }

    /// <summary>
    /// �ش� ������ �Է¹��� ���������� �����ϴ� �Լ�
    /// </summary>
    /// <param name="itemData">� ���������� �����ͷ� ����</param>
    /// <param name="count">��� ������ ����</param>
    public void SetTempSlotWithData(ItemData itemData, uint count)
    {
        itemImage.sprite = itemData.itemIcon;   
        itemImage.color = Color.white;

        takeSlotItemData = itemData;
        takeSlotItemCount = count;

        takeSlotItemCountText.text = takeSlotItemCount.ToString();
        takeSlotItemCountText.alpha = 1;
    }
}
