using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ������ ������ �� ���ǿ� ���� ������, ��ũ���ͺ� ������Ʈ ��ӹ���
/// </summary>
[CreateAssetMenu(fileName = "New Potion", menuName = "Scriptable Object_Item Data/ItemData_Potion", order = 2)]


public class ItemData_Potion : ItemData, IConsumable
{
    public void Use(Player player)
    {
        if(player.HP + 50.0f <= player.MaxHP)
        {
            player.HP += 50.0f;
        }
        else
        {
            player.HP = player.MaxHP;
        }
        player.SetHP();
        
    }
}
