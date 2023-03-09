using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// SplitUI�� ��ӹ��� DropUI, �������� �ۿ� ������ ���Ǵ� Ŭ����
/// </summary>
public class DropUI : SplitUI
{
    private Transform playerTransform;

    protected override void Awake()
    {
        okButton = transform.Find("OKButton").GetComponent<Button>();
        cancelButton = transform.Find("CancelButton").GetComponent<Button>();
        inputField = GetComponentInChildren<TMP_InputField>();
        splitUICanvasGroup = GetComponent<CanvasGroup>();
        inventory = FindObjectOfType<Inventory>();
        inventoryUI = FindObjectOfType<InventoryUI>();
        splitTempSlotSplitUI = GameObject.Find("ItemMoveSlotUI").transform.GetChild(0).GetComponent<TempSlotSplitUI>();   //Ȱ��ȭ�� ������Ʈ ã���� ������ �����ϰ�
        playerTransform = FindObjectOfType<Player>().transform;
    }

    protected override void Start()
    {
        //inputField.
        inputField.onEndEdit.AddListener(this.CheckRightCount); //��Ʈ��Ÿ�� ���Ϲ޴� �Լ� ����  => �Էµ� ���ڰ� ������ itemCount���� ũ�� itemCount��, ������ 0�� ����

        okButton.onClick.AddListener(this.ClickOKButton);
        cancelButton.onClick.AddListener(ClickCancelButton);

    }

    protected override void ClickOKButton()
    {
        splitPossibleCount -= (uint)splitCount;

        for(int i = 0; i < splitCount; i++)
        {
            ItemFactory.MakeItem(splitItemData.ID, playerTransform.position, playerTransform.rotation);
        }

        if(splitPossibleCount > 0)  //���� ������ ���� �� ������ 1�� �̻��̸� ���� ���Կ� �������� �ٽ� ����� �ش�.
        {
            inventory.itemSlots[takeID].AssignSlotItem(splitItemData, splitPossibleCount);             //UI�� ���� �����Ϳ����� ��
            inventoryUI.slotUIs[takeID].SetSlotWithData(splitItemData, splitPossibleCount);
        }

        SplitUIClose();
    }

    /// <summary>
    /// �ؽ�Ʈ�� ���� ���� �Է½� �´� ���ڸ� �������� Ȯ���ϴ� �Լ� 
    /// </summary>
    /// <param name="inputText"></param>
    protected override void CheckRightCount(string inputText) //�ؽ�Ʈ�� ���� ���� �Է� �� ����
    {

        //uint tempNum;
        //bool isParsing = uint.TryParse(splitUI.inputCount.text, out tempNum);
        bool isParsing = int.TryParse(inputText, out splitCount);
        if (splitCount > (int)splitPossibleCount)
        {
            splitCount = (int)splitPossibleCount;
        }
        else if (splitCount < 1)
        {
            splitCount = 1;
        }

        inputField.text = splitCount.ToString();
        //inputText = splitCount.ToString();
        //textCount = splitCount.ToString();
        //return textCount;
    }

    public override void ClickCancelButton()
    {
        inventory.itemSlots[takeID].AssignSlotItem(splitItemData, splitPossibleCount);
        inventoryUI.slotUIs[takeID].SetSlotWithData(splitItemData, splitPossibleCount);
        SplitUIClose();
    }
}
