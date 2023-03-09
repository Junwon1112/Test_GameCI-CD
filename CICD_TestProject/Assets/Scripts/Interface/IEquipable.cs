using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��� �����ϴ� Ŭ������ �־���ϴ� �������̽�
/// </summary>
public interface IEquipable
{
    public void Equip(Player player);
    public void UnEquip(Player player);
}
