using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Infoâ ������ �̹����� �̿�
/// </summary>
public class TempSlotInfoUI : MonoBehaviour
{
    public Image itemImage;                //Image�� ������Ƽ�� ��������Ʈ�� �����Ѵ�. 

    // ������ ������ �� ���
    public ItemData takeSlotItemData;   //tempSlot�� �߻���Ų������ �޾ƿ´�.
    public uint takeSlotItemCount;      //tempSlot�� �߻���Ų������ �޾ƿ´�.

    

    void Awake()
    {
        itemImage = GetComponentInChildren<Image>();
    }

}
