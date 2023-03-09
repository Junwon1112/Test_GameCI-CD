using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �����۰� ���õ� �����͸� �����ϴ� Ŭ����
/// </summary>
public class ItemDataManager : MonoBehaviour
{
    public ItemData[] itemDatas;

    /// <summary>
    /// �ε���, ������Ƽ�� �迭ó�� ���, ������Ƽ �̸��� this�� �ؼ� Ŭ�����̸����� ������Ƽ�� 
    /// ȣ�� ��, �迭�� �Ἥ �ش� �迭�� ���� �ε����� ���� ������Ƽ ���� �������� 
    /// </summary>
    /// <param name="i"></param>
    /// <returns></returns>
    public ItemData this[int i] 
    {
        get
        {
            return itemDatas[i];
        }
    }
    //�迭ó�� ���� ������Ƽ

    public ItemData this[ItemIDCode ID ]  //�ε���
    {
        get
        {
            return itemDatas[(int)ID];
        }
    }
    //�迭ó�� ���� ������Ƽ

    
}
