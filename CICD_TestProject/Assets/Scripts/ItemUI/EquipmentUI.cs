using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

/// <summary>
/// �κ��丮 UI�� ��ӹ��� ��� Ŭ����, �ϼ� �� UIŬ������ ���� ����� �� ��..�̶�� �� ������.
/// </summary>
public class EquipmentUI : InventoryUI     
{
    /// <summary>
    /// uŰ�� ����Ű������ ��ǲ�ý��ۿ� ����
    /// </summary>
    public PlayerInput equipmentControl;   

    /// <summary>
    /// ���� Ű�°� canvasGroup�� �̿��� ����
    /// </summary>
    protected CanvasGroup canvasGroupOnOff;   

    protected Button equipCloseButton;

    /// <summary>
    /// �κ��丮�� �����ִ��� �����ִ��� Ȯ���ϱ� ���� ����
    /// </summary>
    public bool isEquipCanvasGroupOff = true;   

    public EquipSlotUI[] equipSlotUIs;

    UI_Player_MoveOnOff ui_OnOff_E;

    protected override void Awake()
    {
        inventoryControl = new PlayerInput();   //������ ��Ŭ���� inven���� �����ؼ� �ʿ��� �� �������� ���ؼ�
        equipmentControl = new PlayerInput();
        canvasGroupOnOff = GetComponent<CanvasGroup>();
        equipCloseButton = transform.Find("CloseButton").GetComponent<Button>();
        equipSlotUIs = GetComponentsInChildren<EquipSlotUI>();

        graphicRaycaster = GameObject.Find("Canvas").gameObject.GetComponent<GraphicRaycaster>();
        player = FindObjectOfType<Player>();
        ui_OnOff_E = GetComponentInParent<UI_Player_MoveOnOff>();
    }

    private void Start()
    {
        equipCloseButton.onClick.AddListener(EquipmentOnOffSetting);
        isEquipCanvasGroupOff = true;
        for(int i = 0; i < equipSlotUIs.Length; i++)
        {
            equipSlotUIs[i].equipSlotID = 1001 + i; //1000���� ������ ��񽽷����� �����ϱ� ���� �߰�
        }
    }

    private void OnEnable()
    {
        equipmentControl.Equipment.Enable();
        equipmentControl.Equipment.EquipmentOnOff.performed += OnEquipmentOnOff;
    }

    private void OnDisable()
    {
        equipmentControl.Equipment.EquipmentOnOff.performed -= OnEquipmentOnOff;
        equipmentControl.Equipment.Disable();
    }

    /// <summary>
    /// UŰ�� ���� ���â onoff�� ����
    /// </summary>
    /// <param name="obj"></param>
    private void OnEquipmentOnOff(InputAction.CallbackContext obj)
    {
        EquipmentOnOffSetting();
    }

    /// <summary>
    /// ���â UI�� Ű�ų� ���� �� �����ؾ� �� �޼���
    /// </summary>
    private void EquipmentOnOffSetting()
    {
        if (isEquipCanvasGroupOff)
        {
            isEquipCanvasGroupOff = false;

            canvasGroupOnOff.alpha = 1;
            canvasGroupOnOff.interactable = true;
            canvasGroupOnOff.blocksRaycasts = true;

            ui_OnOff_E.IsUIOnOff();  //��ü UI ONOFF���¸� Ȯ���ϰ� InputSystem�� Enable�� Disable���ִ� �Լ�

            //if(inventoryUI.isInvenCanvasGroupOff)
            //{
            //    GameManager.Instance.MainPlayer.input.Disable();
            //    inventoryUI.inventoryControl.Inventory.InventoryItemUse.performed += inventoryUI.OnInventoryItemUse;
            //}
        }
        else
        {
            isEquipCanvasGroupOff = true;


            canvasGroupOnOff.alpha = 0;
            canvasGroupOnOff.interactable = false;
            canvasGroupOnOff.blocksRaycasts = false;
            
            ui_OnOff_E.IsUIOnOff();

            //if (inventoryUI.isInvenCanvasGroupOff)
            //{
            //    GameManager.Instance.MainPlayer.input.Enable();
            //    inventoryUI.inventoryControl.Inventory.InventoryItemUse.performed -= inventoryUI.OnInventoryItemUse;
            //}
        }
    }


}

