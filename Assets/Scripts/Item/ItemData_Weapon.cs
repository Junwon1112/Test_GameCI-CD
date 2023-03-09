using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ������ ������ �� ���ǿ� ���� ������, ��ũ���ͺ� ������Ʈ ��ӹ���
/// </summary>
[CreateAssetMenu(fileName = "New Weapon", menuName = "Scriptable Object_Item Data/ItemData_Weapon", order = 3)]
public class ItemData_Weapon : ItemData, IEquipable
{
    public float attackDamage;
    GameObject makedItem = null;

    /// <summary>
    /// �����ϴ� �ڵ�(�����ϸ� prefab�� �÷��̾��� ��� �����Ǵ� ��ġ�� ������Ű��, �÷��̾� �ɷ�ġ�� ��� �ɷ�ġ��ŭ�� �����͸� �߰� ��Ŵ)
    /// </summary>
    /// <param name="player"></param>
    public void Equip(Player player)
    {
        
        makedItem = Instantiate(itemPrefab, player.transform.position, player.transform.rotation);
        player.AttackDamage += attackDamage;
    }

    /// <summary>
    /// �����ϴ� �Լ�(�����ϸ� prefab�� �÷��̾��� ��� ����, �÷��̾� �ɷ�ġ�� ��� �ɷ�ġ��ŭ�� �����͸� ���� ��Ŵ)
    /// </summary>
    /// <param name="player"></param>
    public void UnEquip(Player player)
    {
        Destroy(makedItem);
        player.AttackDamage -= attackDamage;
    }
}
