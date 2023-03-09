using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ������ ����� �޼ҵ��� ����(MakeItem)
/// </summary>
public class ItemFactory : MonoBehaviour
{
    /// <summary>
    /// �������� �ϳ� ���鶧 ���� �ø� ������ ����
    /// </summary>
    static int itemCount = 0;

    /// <summary>
    /// �������� �������� �����ϴ� �޼���
    /// </summary>
    /// <param name="itemIDCode">� �������� ������ ItemIdCode�� ����, �ٸ� ����� �����ε��� ���� ����</param>
    /// <param name="position">������ ��ġ</param>
    /// <param name="rotation">������ ȸ���� ����</param>
    /// <returns></returns>
    public static GameObject MakeItem(ItemIDCode itemIDCode, Vector3 position, Quaternion rotation)
    {
        GameObject obj = Instantiate(GameManager.Instance.ItemManager[itemIDCode].itemPrefab, position, rotation);
        Item item = obj.AddComponent<Item>();   //������Ʈ�� ������ ������Ʈ �߰�, �̸� ���� �������� ��
        if (itemIDCode == ItemIDCode.Basic_Weapon_1 || itemIDCode == ItemIDCode.Basic_Weapon_2)
        {
            obj.AddComponent<PlayerWeapon>();
            CapsuleCollider capsuleCollider = obj.AddComponent<CapsuleCollider>();
            capsuleCollider.radius = 0.1f;
            capsuleCollider.height = 1.4f;
            capsuleCollider.isTrigger = true;
        }
        else if(itemIDCode == ItemIDCode.HP_Potion)
        {
            SphereCollider sphereCollider = obj.AddComponent<SphereCollider>();
            sphereCollider.radius = 0.5f;
            sphereCollider.isTrigger = true;
        }
        //GameObject obj = new GameObject();      //���ο� ������Ʈ �����
        

        item.data = GameManager.Instance.ItemManager[itemIDCode];   //�߰��� ������Ʈ�� �����ʹ� ItemIdCode�� �����Ϳ� ���󰣴�.
        obj.name = $"{item.data.name}_{itemCount}";                 //�̸� ����
        obj.layer = LayerMask.NameToLayer("Item");                  //���̾� ����
        
        itemCount++;    //���� ������ ���� �Ѱ� �߰�

        //������� ���⿡ �̴ϸ�ǥ�� �߰��� ��, ���߿� �̴ϸ� ����� �߰� ���

        return obj;
    }

    /// <summary>
    /// id�� ���� ���� ���������� enumŸ������ ����ȯ�ؼ� ������ִ� �Լ�
    /// </summary>
    /// <param name="id"></param>
    /// <param name="position"></param>
    /// <param name="rotation"></param>
    /// <returns></returns>
    public static GameObject MakeItem(uint id, Vector3 position, Quaternion rotation)
    {
        GameObject obj = MakeItem((ItemIDCode)id, position, rotation);  
        return obj;
    }

    //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

    /// <summary>
    /// ������ ����� ��ġ���� �̼��ϰ� �ٸ��� �������� ����� �޼ҵ�
    /// </summary>
    /// <param name="itemIDCode"></param>
    /// <param name="itemPosition"></param>
    /// <param name="rotation"></param>
    /// <param name="randomNoise">bool randomNoise = false �� ���࿡ �Ķ���ͷ� randomNoise�� ���� �������� false�� ����Ѵٴ� ���̴�</param>
    /// <returns></returns>

    public static GameObject MakeItem(ItemIDCode itemIDCode, Vector3 itemPosition, Quaternion rotation, bool randomNoise = false)   
    {
        GameObject obj = MakeItem(itemIDCode, itemPosition, rotation);

        if(randomNoise)
        {
            Vector2 noise = Random.insideUnitCircle * 0.5f;
            itemPosition.x += noise.x;
            itemPosition.y += noise.y;
        }

        obj.transform.position = itemPosition;

        return obj;
    }
    /// <summary>
    /// ��¦�������� ��ġ�� ����µ� id�� ����
    /// </summary>
    /// <param name="id"></param>
    /// <param name="itemPosition"></param>
    /// <param name="rotation"></param>
    /// <param name="randomNoise"></param>
    /// <returns></returns>
    public static GameObject MakeItem(uint id, Vector3 itemPosition, Quaternion rotation, bool randomNoise = false)
    {
        return (MakeItem((ItemIDCode)id, itemPosition, rotation, randomNoise));
    }


    //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------


    /// <summary>
    /// ��� �ѹ��� �������� �������� ����� �ڵ�� ���ϰ��� void�̴�
    /// </summary>
    /// <param name="itemIDCode"></param>
    /// <param name="itemPosition"></param>
    /// <param name="rotation"></param>
    /// <param name="count"></param>
    public static void MakeItem(ItemIDCode itemIDCode, Vector3 itemPosition, Quaternion rotation,uint count)
    {
        //GameObject obj = MakeItem(itemIDCode);

        for(int i=0; i < count; i++)
        {
            MakeItem(itemIDCode, itemPosition, rotation, true);
        } 
    }

    /// <summary>
    /// ��� �ѹ��� �������� �������� ����� �ڵ�� id�� ����(�����ε�)
    /// </summary>
    /// <param name="id"></param>
    /// <param name="itemPosition"></param>
    /// <param name="rotation"></param>
    /// <param name="count"></param>
    public static void MakeItem(uint id, Vector3 itemPosition, Quaternion rotation, uint count)
    {
        MakeItem((ItemIDCode)id, itemPosition, rotation, count);
    }







}
