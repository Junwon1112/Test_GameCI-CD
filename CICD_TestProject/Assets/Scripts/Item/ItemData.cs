using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ������ �����͸� �����ϴ� ������ ������ ����� �ִ� ��ũ���ͺ� ������Ʈ, ���� �޴��� ������ ������ ���� �߰�
/// </summary>
[CreateAssetMenu(fileName = "New Item data" , menuName = "Scriptable Object_Item Data/Item Data", order = 1)]
public class ItemData : ScriptableObject
{
    public string itemName;        //������ �̸�
    public uint ID;                 //������ ID
    public GameObject itemPrefab;  //������ ������
    public Sprite itemIcon;        //������ ������
    public int itemValue;          //������ ��ġ
    public int itemMaxCount;       //������ �ִ� ������
}

public enum ItemIDCode
{
    HP_Potion = 0,
    Basic_Weapon_1,
    Basic_Weapon_2
}
