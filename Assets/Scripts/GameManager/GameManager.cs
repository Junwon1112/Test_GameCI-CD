using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �̱��� ������ �̿��� �÷��̾�, ������, ��ų ���� �����͸� ������ �� �ִ� GameManager Ŭ����
/// </summary>
public class GameManager : MonoBehaviour
{
    Player player;
    ItemDataManager itemDataManager;
    SkillDataManager skillDataManager;

    /// <summary>
    /// player�� ���� ������Ƽ
    /// </summary>
    public Player MainPlayer
    {
        get
        {
            return player;
        }
    }

    /// <summary>
    /// ItemManager�� ���� ������Ƽ
    /// </summary>
    public ItemDataManager ItemManager
    {
        get
        {
            return itemDataManager;
        }
    }

    /// <summary>
    /// SkillDataManager�� ���� ������Ƽ
    /// </summary>
    public SkillDataManager SkillDataManager
    {
        get
        {
            return skillDataManager;
        }
    }
    /// <summary>
    /// static�� ����� �ܺο����� ���ο� Ŭ������ ������ �ʰ� �ٷ� ������ �� ����, �ϳ��� �ν��Ͻ��� ����
    /// </summary>
    public static GameManager Instance;

    /// <summary>
    /// Instance�� ���� ������Ƽ
    /// </summary>
    public GameManager Inst
    {
        get
        {
            return Instance;
        }
    }

    /// <summary>
    /// �̱������Ͽ��� �� 1���� GameManager�� �����ϰ� �ϱ����� ���� �ٲ� ������ 1���� �ν��Ͻ��� �����ϴ��� Ȯ���ϴ� ����
    /// 1�� �̻� ���� �� �ڱ��ڽ��� �ı�
    /// </summary>
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            if(Instance != this)
            {
                Destroy(this.gameObject);
            }
        }
        Initialize();
    }


    private void Initialize()   //�����ϸ� ������Ʈ ��������
    {
        player = FindObjectOfType<Player>();
        itemDataManager = FindObjectOfType<ItemDataManager>();
        skillDataManager = FindObjectOfType<SkillDataManager>();
    }
}
