using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �κ��丮 �� ������ ���� ������
/// </summary>
public class ItemSlot  //: MonoBehaviour
{
    /// <summary>
    /// �ܺο��� ���° �������� �����ϴ� ���� ���̵�. �Ҵ����� -1���� �Ҵ��� ����
    /// </summary>
    public int slotID = -1; 

    /// <summary>
    /// ������ ������ ������
    /// </summary>
    ItemData slotItemData;
    uint itemCount;

    /// <summary>
    /// ������ ���� ������ ������Ƽ
    /// </summary>
    public ItemData SlotItemData
    {
        get
        {
            return slotItemData;
        }
        set
        {
            if(slotItemData != value)
            {
                slotItemData = value;
            }
        }
    }

    /// <summary>
    /// ������ ������ ���� ������Ƽ
    /// </summary>
    public uint ItemCount
    { get; set; }

    /// <summary>
    /// ������
    /// </summary>
    public ItemSlot() { }
    public ItemSlot(ItemData data, uint count)
    {
        slotItemData = data;
        itemCount = count;
    }

    public ItemSlot(ItemSlot newItemSlot)
    {
        slotItemData = newItemSlot.slotItemData;
        itemCount = newItemSlot.itemCount;
    }

    //������ ���Կ� ������ ���� �߰�
    //������ ���Կ� ������ ���� ����
 


    /// <summary>
    /// ������ ���Կ� ������ �Ҵ�(���� ����)
    /// </summary>
    /// <param name="newItemData"></param>
    /// <param name="newItemCount"></param>
    public void AssignSlotItem(ItemData newItemData, uint newItemCount = 1 )
    {
        if(IsEmpty())
        {
            SlotItemData = newItemData;
            ItemCount = newItemCount;
            Debug.Log("�� ���Կ� �Ҵ��Ѵ�");
        }
        
    }

    /// <summary>
    /// ���Կ� �������� ������ �� ���� �߰�
    /// </summary>
    /// <param name="count"></param>
    public void IncreaseSlotItem(uint count = 1)
    {
        if(!IsEmpty())
        {
            if(ItemCount + count <= slotItemData.itemMaxCount)
            {
                ItemCount += count;
                Debug.Log("���� ���Կ� �߰��Ѵ�");
            }
            else
            {
                ItemCount = (uint)slotItemData.itemMaxCount;
                Debug.Log("���� ������ �����ִ�");
            }
        }
    }

    /// <summary>
    /// ������ ���Կ� �ִ� ������ ���� ����
    /// </summary>
    /// <param name="count"></param>
    public void DecreaseSlotItem(uint count = 1)
    {
        if(ItemCount - count > 0)
        {
            ItemCount -= count;
        }
        else if(ItemCount - count <= 0)
        {
            ItemCount = 0;
            SlotItemData = null;
        }
    }

    /// <summary>
    /// ������ ���� ����
    /// </summary>
    public void ClearSlotItem()
    {
        SlotItemData = null;
        ItemCount = 0;
    }



    /// <summary>
    /// ������ ����ִ��� Ȯ��
    /// </summary>
    /// <returns></returns>
    public bool IsEmpty()
    {
        return (slotItemData == null);
    }
}
