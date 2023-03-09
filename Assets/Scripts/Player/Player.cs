using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Animations;
using UnityEngine.UI;

public class Player : MonoBehaviour, IHealth
{
    Player player;



    /// <summary>
    /// �������� ���� ��ǲ �ý��ۿ�
    /// </summary>
    public PlayerInput input;

    /// <summary>
    /// �̵� ���� �ް� ���Ͽ�
    /// </summary>
    Vector3 dir = Vector3.zero;

    float walkSoundVolume = 1.0f;
    float AttackSoundVolume = 0.7f;

    /// <summary>
    /// �ִϸ��̼� �� 
    /// </summary>
    Animator anim;

    /// <summary>
    /// �ٸ� �ൿ�� �������� �����ϱ� ����
    /// </summary>
    bool canMove = true;

    /// <summary>
    /// ü�� ���� ������
    /// </summary>
    float hp;
    float maxHp = 100;
    Slider hpBar;

    /// <summary>
    /// ����ġ ���� ������
    /// </summary>
    float exp = 0.0f;
    float maxExp = 100;
    Slider expBar;

    [SerializeField]
    int level = 1;

    /// <summary>
    /// ȸ�� ���� ������
    /// </summary>
    float turnToX;
    float turnToY;
    float turnToZ;

    float turnSpeed = 30.0f;

    /// <summary>
    /// ������ ���� ����
    /// </summary>
    Item item;
    ItemFactory itemFactory;
    ItemIDCode itemID;
    ItemData_Potion potion;
    public ItemData_Weapon myWeapon;

    float findItemRange = 3.0f;
    Inventory playerInventory;
    InventoryUI playerInventoryUI;

    /// <summary>
    /// ���� �ٲܶ� ����ϱ� ���� ������
    /// </summary>
    GameObject weaponPrefab;
    CapsuleCollider weaponCollider;
    public Transform weaponHandTransform;
    public bool isFindWeapon = false;

    /// <summary>
    /// ��ų ��� ������ üũ�ϱ� ���� ���
    /// </summary>
    public bool isSkillUsing = false;

    SkillUse[] skillUses;

    public Transform CharacterTransform
    {
        get { return this.transform; }
    }

    public float HP
    {
        get { return hp; }
        set 
        { 
            hp = value; 

        }
    }

    public float MaxHP
    {
        get { return maxHp; }
    }

    public float Exp
    {
        get { return exp; }
        set
        {
            exp = value;

        }
    }

    public float MaxExp
    {
        get { return maxExp; }
        set
        {
            maxExp = value;
        }
    }

    /// <summary>
    /// private���� ����Ƽ���� ��ġ�ٲܼ� �ְ� ���ִ� ��
    /// </summary>
    [SerializeField]    
    float attackDamage = 10;

    [SerializeField]
    float defence = 5;

    public float AttackDamage
    {
        get { return attackDamage; }
        set { attackDamage = value; }
    }
    public float Defence
    {
        get { return defence; }
        set { defence = value; }
    }


    private void Awake()
    {
        input = new PlayerInput();
        anim = GetComponent<Animator>();
        hpBar = GameObject.Find("HpSlider").GetComponent<Slider>();
        player = GetComponent<Player>();
        playerInventory = GetComponentInChildren<Inventory>();
        playerInventoryUI = FindObjectOfType<InventoryUI>();
        weaponHandTransform = FindObjectOfType<FindWeaponHand>().transform;
        expBar = GameObject.Find("ExpSlider").GetComponent<Slider>();
        skillUses = FindObjectsOfType<SkillUse>();
    }

    /// <summary>
    /// InputSystem�� ����� ����Ű�鿡 �ش��ϴ� �Լ� ���
    /// </summary>
    private void OnEnable()
    {
        input.Player.Enable();
        input.Player.Move.performed += OnMoveInput;
        input.Player.Attack.performed += OnAttackInput;
        input.Player.Look.performed += OnLookInput;
        input.Player.TempItemUse.performed += OnTempItemUse;
        input.Player.TakeItem.performed += OnTakeItem;
        input.Player.TestMakeItem.performed += OnTestMakeItem;
    }


    /// <summary>
    /// InputSystem�� ����� ����Ű�鿡 �ش��ϴ� �Լ� ����
    /// </summary>
    private void OnDisable()
    {
        input.Player.TestMakeItem.performed -= OnTestMakeItem;
        input.Player.TempItemUse.performed -= OnTempItemUse;
        input.Player.Attack.performed -= OnAttackInput;
        input.Player.Move.performed -= OnMoveInput;
        input.Player.Look.performed -= OnLookInput;
        input.Player.Disable();
        input.Player.TakeItem.performed -= OnTakeItem;
    }

    

    private void Start()
    {
        hp = maxHp;
        SetHP();
        potion = new ItemData_Potion();
        SetExp();
        myWeapon = new ItemData_Weapon();
    }

    private void Update()
    {
        transform.Translate(dir * Time.deltaTime * 10, Space.Self);
        if (dir == Vector3.zero)
        {
            anim.SetBool("IsMove", false);
        }
    }

    private void OnMoveInput(InputAction.CallbackContext obj)
    {
        if(canMove)
        {
            //2���� �ุ �ʿ��� 2d vector�� ����� readvalue���� 2d�� �޾ƾ߸� �Ѵ�.
            //���� 3d�� ��ȯ�ϴ� ������ ��ģ��.
            Vector3 tempDir;
            tempDir = obj.ReadValue<Vector2>();
            dir.x = tempDir.x;
            dir.z = tempDir.y;

            anim.SetFloat("DirSignal_Front", dir.z);
            anim.SetFloat("DirSignal_Side", dir.x);
            anim.SetBool("IsMove", true);
        }
        
    }


    private void OnAttackInput(InputAction.CallbackContext obj)
    {
        anim.SetBool("IsMove", false);
        anim.SetTrigger("AttackOn");
    }

    //private void OnTriggrEnter(Collider other)
    //{
    //    //�÷��̾� Į���ִ� �ö��̴��� Ʈ����
    //    if(other.CompareTag("Monster"))
    //    {
    //        Monster monster;
    //        monster = other.GetComponent<Monster>();
    //        if(monster.playerTriggerOff == false)
    //        {
    //            Attack(monster);
    //            monster.SetHP();
                
    //        }
    //        monster.playerTriggerOff = false;

    //    }
    //}

    private void OnLookInput(InputAction.CallbackContext obj)
    {
        float moveX = obj.ReadValue<Vector2>().x;
        float moveY = obj.ReadValue<Vector2>().y;

        //�¿� ȸ��
        turnToY = turnToY + moveX * turnSpeed * Time.deltaTime; 

        //���Ʒ� �Ĵٺ���, ī�޶� ��ũ��Ʈ ���� �� ī�޶� �����̰� �� ����
        turnToX = turnToX + moveY * turnSpeed * Time.deltaTime; 
        
        //turnToY = Mathf.Clamp(turnToY, -80, 80);    //�ִ밪 ����
        turnToX = Mathf.Clamp(turnToX, -20, 20);

        transform.eulerAngles = new Vector3(0, turnToY, 0);


    }

    /// <summary>
    /// Keyboard Q
    /// </summary>
    private void OnTempItemUse(InputAction.CallbackContext obj)     
    {

        //������ ���� ==> ����
        //GameObject itemObj = ItemFactory.MakeItem((uint)ItemIDCode.HP_Potion, transform.position, Quaternion.identity);
        //������ ���
        //if(playerInventory.FindSameItemSlotForUseItem(potion). != null);
        if(playerInventory.FindSameItemSlotForUseItem(potion).SlotItemData != null)
        {
            int tempID;
            potion.Use(player);
            if(playerInventory.FindSameItemSlotForUseItem(potion).ItemCount == 1)
            {
                tempID = playerInventory.FindSameItemSlotForUseItem(potion).slotID;
                playerInventory.FindSameItemSlotForUseItem(potion).ClearSlotItem();
                playerInventoryUI.slotUIs[tempID].slotUIData = null;
                playerInventoryUI.slotUIs[tempID].slotUICount = 0;
                playerInventoryUI.SetAllSlotWithData();
            }
            else
            {
                tempID = playerInventory.FindSameItemSlotForUseItem(potion).slotID;
                playerInventory.FindSameItemSlotForUseItem(potion).ItemCount--;
                playerInventoryUI.slotUIs[tempID].slotUICount--;
                playerInventoryUI.SetAllSlotWithData();
            }
            
        }
        
        
    }

    /// <summary>
    /// Keyboard F�� ���� ����
    /// </summary>
    private void OnTakeItem(InputAction.CallbackContext obj)    
    {
        Collider[] findItem = Physics.OverlapSphere(transform.position, findItemRange, LayerMask.GetMask("Item"));
        if(findItem.Length > 0)
        {
            GameObject tempObj = findItem[0].gameObject;
            Item tempItem = tempObj.GetComponent<Item>();

            playerInventory.TakeItem(tempItem.data, 1);
            playerInventoryUI.SetAllSlotWithData();
            Destroy(tempObj);

        }
    }

    /// <summary>
    /// Mouse Right Click (UI�������� ��)
    /// </summary>
    /// <param name="obj"></param>
    private void OnTestMakeItem(InputAction.CallbackContext obj)    
    {
        ItemFactory.MakeItem(ItemIDCode.Basic_Weapon_1, transform.position, Quaternion.identity);
    }

    public void SetHP()
    {
        hpBar.value = HP / MaxHP;
    }

    public void SetExp()
    {
        expBar.value = Exp / MaxExp;
    }

    public void LevelUp()
    {
        level++;
        Exp -= MaxExp;
        MaxExp *= 1.3f;
        SetExp();
    }

    /// <summary>
    /// �ٷ� �Ʒ� ��ġ�� �ִϸ��̼����� attackTrigger�����ϴ� �Լ��� collider�� �����ֱ� ���� �Լ�
    /// </summary>
    public void TakeWeapon()     
    {
        PlayerWeapon tempPlayerWeapon = FindObjectOfType<PlayerWeapon>();
        for(int i = 0; i < skillUses.Length; i++)   //���� ������ SkillUseŬ���������� ���⸦ �޾ƿ����� ��(���Ⱑ ������ �� �����Ǿ����� �ʾ� SkillUse Awake���� ���Ѵ�)
        {
            skillUses[i].TakeWeapon();
        }
        if (tempPlayerWeapon != null)
        {
            weaponPrefab = tempPlayerWeapon.gameObject;
            weaponCollider = this.weaponPrefab.GetComponent<CapsuleCollider>();
            weaponCollider.enabled = false;
            isFindWeapon = true;
            Debug.Log("����ã��");
        }
        else
        {
            isFindWeapon = false;
            Debug.Log("�����ã��");
        }
        
    }

    public void EquipWeaponAbility()
    {
        AttackDamage += myWeapon.attackDamage;
    }

    public void UnEquipWeaponAbility()
    {
        attackDamage -= myWeapon.attackDamage;
        myWeapon = null;
    }

    /// <summary>
    /// ����Ƽ �ִϸ��̼ǿ��� �̺�Ʈ�� Ȱ��ȭ �� �Լ�
    /// </summary>
    public void AttackTriggerOn()
    {
        if(isFindWeapon)
        weaponCollider.enabled = true;
    }

    /// <summary>
    /// ����Ƽ �ִϸ��̼ǿ��� �̺�Ʈ�� Ȱ��ȭ �� �Լ�
    /// </summary>
    public void AttackTriggerOff()
    {
        if(isFindWeapon)
        weaponCollider.enabled = false;
    }

    /// <summary>
    /// ����Ƽ �ִϸ��̼ǿ��� �̺�Ʈ�� Ȱ��ȭ �� �Լ�
    /// </summary>
    public void IsSkillUseOn()
    {
        isSkillUsing = true;
    }

    /// <summary>
    /// ����Ƽ �ִϸ��̼ǿ��� �̺�Ʈ�� Ȱ��ȭ �� �Լ�
    /// </summary>
    public void IsSkillUseOff()
    {
        isSkillUsing = false;
    }

    /// <summary>
    /// ����Ƽ �ִϸ��̼ǿ��� �̺�Ʈ�� Ȱ��ȭ �� �Լ�
    /// </summary>
    public void AttackSoundStart()
    {
        SoundPlayer.Instance?.PlaySound(SoundType.Sound_Attack, AttackSoundVolume);
    }

    /// <summary>
    /// ����Ƽ �ִϸ��̼ǿ��� �̺�Ʈ�� Ȱ��ȭ �� �Լ�
    /// </summary>
    public void WalkSoundAndEffectStart()
    {
        SoundPlayer.Instance?.PlaySound(SoundType.Sound_Walk, walkSoundVolume);
        ParticlePlayer.Instance?.PlayParticle(ParticleType.ParticleSystem_Walk, transform.position, transform.rotation);
    }

}
