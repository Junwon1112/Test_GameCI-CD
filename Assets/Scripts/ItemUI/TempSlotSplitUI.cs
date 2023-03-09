using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.UI;

/// <summary>
/// ������ ���Կ��� �������� ���� �� �ӽ÷� ������ ����
/// </summary>
public class TempSlotSplitUI : TempSlotInfoUI
{
    //public bool isSpliting = false;     //SplitUI���� OK��ư ������ true�� �ٲ���
    private TextMeshProUGUI tempSlotItemCountText;
    int takeID = -1;

    void Awake()
    {
        this.itemImage = GetComponentInChildren<Image>();
        tempSlotItemCountText = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        this.gameObject.SetActive(false);
    }

    /// <summary>
    /// ���� ���̶�� update���� ������ ���콺 ��ġ�� ����ٴ�
    /// </summary>
    private void Update()
    {
        //���� ���̶�� �����ϱ�
        transform.position = (Vector3)Mouse.current.position.ReadValue();

    }

    /// <summary>
    /// �ӽý��� ���� �޼���
    /// </summary>
    public void ClearTempSlot()
    {
        itemImage.sprite = null;
        //isSpliting = false;   //splitUI���� ó��
        takeSlotItemData = null;
        takeSlotItemCount = 0;
    }

    /// <summary>
    /// �ӽ� ���� �����͸� �����ϴ� �޼���
    /// </summary>
    /// <param name="itemData"></param>
    /// <param name="count"></param>
    public void SetTempSlotWithData(ItemData itemData, uint count)
    {
        itemImage.sprite = itemData.itemIcon;   //���⼭ �ι��� ���ø��Ҷ� ������(�Ƹ� ��ӹ޾Ƽ� split�ʿ��� ok������ �������°� ����)
        takeSlotItemData = itemData;
        takeSlotItemCount = count;
        tempSlotItemCountText.text = takeSlotItemCount.ToString();
    }

    //--------��ӹ��� TempSlotInfoUI�� ����--------------------------------

    //==================================================================================================================
    //public Image itemImage;                //Image�� ������Ƽ�� ��������Ʈ�� �����Ѵ�. 

    //// ������ ������ �� ���
    //public ItemData tempSlotItemData;   //tempSlot�� �߻���Ų������ �޾ƿ´�.
    //public uint tempSlotItemCount;      //tempSlot�� �߻���Ų������ �޾ƿ´�.

    //private void Awake()
    //{
    //    itemImage = GetComponentInChildren<Image>();
    //}

    //public void SetTempSlotWithData(ItemData itemData, uint count)
    //{
    //    itemImage.sprite = itemData.itemIcon;
    //    tempSlotItemData = itemData;
    //    tempSlotItemCount = count;
    //}
    //==================================================================================================================
}
