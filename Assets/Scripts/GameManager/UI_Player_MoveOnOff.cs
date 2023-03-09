using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// � UI�� 1�� �̻� ������ �÷��̾� �������� ���߰� �ϱ� ���� ��ü UI�� �����ϵ��� ���� Ŭ����
/// </summary>
public class UI_Player_MoveOnOff : MonoBehaviour
{
    /// <summary>
    /// playerInputSystem�� �ִ� Inventroy�� UI���� ��Ŭ�� �Է��� �־ ��ü UI�� ����Ϸ��� ����;� �ߴ�. 
    /// �������� ��ü UI�� �����ϴ� InputSystem�׸��� ����� �ű⼭ ��Ŭ�� �Է��� �����ؾ� �� (Ȯ�强�� ���� ��� �ʿ�)
    /// </summary>
    InventoryUI inventoryUI;    

    bool isOnInventoryItemUseConnect = false;

    public CanvasGroup[] canvasGroups; 

    private void Awake()
    {
        inventoryUI = GetComponentInChildren<InventoryUI>();
    }


    /// <summary>
    /// UI�� �����ų� ������ �÷��̾ �������� Ȯ��
    /// </summary>
    public void IsUIOnOff()    
    {
        uint count = 0;
        for (int i = 0; i < canvasGroups.Length; i++)
        {
            
            if (canvasGroups[i].interactable)
            {
                GameManager.Instance.MainPlayer.input.Disable();
                if(!isOnInventoryItemUseConnect)
                {
                    isOnInventoryItemUseConnect = true;
                    inventoryUI.inventoryControl.Inventory.InventoryItemUse.performed += inventoryUI.OnInventoryItemUse;
                    Debug.Log("OnInventoryItemUseConnect");
                }
                break;
            }
            else
            {
                count++;
                if(count >= canvasGroups.Length)
                {
                    GameManager.Instance.MainPlayer.input.Enable();
                    if(isOnInventoryItemUseConnect)
                    {
                        isOnInventoryItemUseConnect = false;
                        inventoryUI.inventoryControl.Inventory.InventoryItemUse.performed -= inventoryUI.OnInventoryItemUse;
                        Debug.Log("NO OnInventoryItemUseConnect");
                    }
                    
                    
                } 
            }
        }
    }

}
