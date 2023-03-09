using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

/// <summary>
/// ������ ���� UI�� ���õ� �޼���
/// </summary>
public class ItemSlotUI : MonoBehaviour, IPointerClickHandler, IDragHandler, IBeginDragHandler, IEndDragHandler , IPointerEnterHandler, IPointerExitHandler, IPointerMoveHandler
{
    /// <summary>
    /// ��ü ���Կ��� ���° �������� �����ִ� �����ִ� ��, UI�� �����͸� �޴� ������ ID�� ����. �Ҵ����� ���� -1�� �Ҵ�
    /// </summary>
    public int slotUIID = -1;

    /// <summary>
    /// Infoâ�� ������ �� slotUI���� �̸��̳� ���� ���� ������ ������ ����ؼ� ����
    /// </summary>
    public ItemData slotUIData;

    /// <summary>
    /// tempslot�� ���������� ���� ����
    /// </summary>
    public uint slotUICount = 0;       
    uint splitCount;        //splitUI���� �����ް� ������Ƽ���� �������ִ� ������ ��ȯ

    //�� �̹����� ����� ������ �����͸� �޾��� �� �ش� �������� Icon�� �̹����� ��ȯ
    Image itemImage;                //Image�� ������Ƽ�� ��������Ʈ�� �����Ѵ�. 
    TextMeshProUGUI itemCountText;  //UI�� �ؽ�Ʈ�� ǥ���ϸ� UGUI�� ���
    Inventory playerInven;
    InventoryUI playerInvenUI;

    /// <summary>
    /// ����â�̳� tempslotUI�� ���콺 ��ġ���� �������� ���콺 ������ ����
    /// </summary>
    Vector2 mousePos = Vector2.zero;

    /// <summary>
    /// ���콺�� �������� �÷����� �� ������ ���� ������ ����
    /// </summary>
    float infoOpenTime = 1.0f;          

    bool isDrag = false;
    bool isOnPointer = false;   //���콺�� �������� �ö� �ִ���
    bool isInfoOpen = false;

    ItemInfo itemInfo;  //�������� ���콺 �÷����� �� Ȱ����ų ������ ����â ��������
    SplitUI splitUI;
    DropUI dropUI;
    
    TempSlotSplitUI tempSlotSplitUI;
   
    private void Awake()
    {
        itemImage = GetComponentInChildren<Image>();
        itemCountText = GetComponentInChildren<TextMeshProUGUI>();
        playerInven = FindObjectOfType<Inventory>();
        playerInvenUI = FindObjectOfType<InventoryUI>();
        itemInfo = FindObjectOfType<ItemInfo>();
        splitUI = GameObject.Find("SplitUI").GetComponent<SplitUI>();  //dropUI�� splitUI�� ��ӹ޾Ҵµ� findobjectoftype���� �������� dropUI�� �޾ƿü����ִ�.
        dropUI = GameObject.Find("DropUI").GetComponent<DropUI>();
        //tempSlotSplitUI = FindObjectOfType<TempSlotSplitUI>();    //��Ȱ��ȭ üũ�س����� Awake���� ã�Ƶ� ��ã�´�. ��Ȱ��ȭ Ÿ�̹��� Awake���� ������ ����. �׷��� �Ʒ�ó�� ã�´�. 
        tempSlotSplitUI = GameObject.Find("ItemMoveSlotUI").transform.GetChild(0).GetComponent<TempSlotSplitUI>();   //Ȱ��ȭ�� ������Ʈ ã���� ������ �����ϰ�
    }


    private void Update()
    {
        InfoInUpdate();
    }

    /// <summary>
    /// ������ �����ͷ� ����UI ���� 
    /// </summary>
    /// <param name="itemData">������ ������ ������</param>
    /// <param name="count">������ ����</param>
    public void SetSlotWithData(ItemData itemData, uint count)  
    {
        if(itemData != null && count > 0)    //������ �����Ͱ� �����Ѵٸ�
        {
            slotUIData = itemData;
            slotUICount = count;

            itemImage.color = Color.white;
            itemImage.sprite = itemData.itemIcon;

            itemImage.raycastTarget = true;
            itemCountText.alpha = 1.0f;
            itemCountText.text = count.ToString();
        }
        else
        {
            slotUIData = null;
            slotUICount = count;
            itemImage.color = Color.clear;
            itemCountText.alpha = 0;
        }
        
    }
    /// <summary>
    /// ���콺�� ���Կ� �� ���� �ľ�
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerEnter(PointerEventData eventData) 
    {
        isOnPointer = true;
    }

    /// <summary>
    /// ���콺�� ���Կ��� ���� ���� �ľ�
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerExit(PointerEventData eventData)    
    {
        isOnPointer = false;
    }

    /// <summary>
    /// ������ ���� 1���̻� ������ �� ������ ���� â ǥ��, ����â�� ǥ�õ� ���¿��� �����̸� ����â �ݱ�
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerMove(PointerEventData eventData)  
    {
        if (!isDrag && isOnPointer && slotUIData != null) //�巡�� ���� ����(�Ϲ����� ���¿���) �������� ���� �� �����̸� �����ð� �� ����â ���� 
        {
            infoOpenTime = 1.0f;
            
            if(isInfoOpen)
            {
                itemInfo.CloseInfo();
                isInfoOpen = false;
            }
            mousePos = eventData.position;

            //GameObject objOfFindSlot = eventData.pointerCurrentRaycast.gameObject;
            //objOfFindSlot.GetComponent<ItemData>() 



            /*
             * �ڷ�ƾ�� ���콺�� �����϶����� �����ؾ� �ɶ����� �����ؾߵǼ� �δ㽺����Ƿ� update���� Ÿ��üũ
             * ���콺�� ������ ������ infoOpenTime�� 1�ʷ� �ٽ� �ʱ�ȭ
             * 0�ʰ� �Ǹ� ���� ����
             */
        }
    }

    /// <summary>
    /// ����â ǥ���� �� ������ �ֵ�
    /// </summary>
    private void SetInfo()  
    {
        itemInfo.infoTempSlotUI.itemImage.sprite = slotUIData.itemIcon;
        itemInfo.infoTransform.position = mousePos;
        itemInfo.OpenInfo();
        itemInfo.infoName.text = slotUIData.itemName;
        itemInfo.itemInformation.text = "No Information";
        isInfoOpen = true;
    }

    /// <summary>
    /// ������Ʈ���� ������ ����â ���°��� �Լ�
    /// </summary>
    private void InfoInUpdate() 
    {
        if(!isDrag)
        {
            if (isOnPointer)
            {
                infoOpenTime -= Time.deltaTime;
            }
            if (isOnPointer && !isInfoOpen && infoOpenTime < 0.0f)
            {
                if (slotUIData != null)  //�����Ͱ� �־�� ǥ���Ѵ�.
                {
                    SetInfo();
                }
            }
        }
        
    }

    /// <summary>
    /// Ŭ���� ������ �������� Ŭ���̴�, ������ Ŭ���� �����ų �޼���
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerClick(PointerEventData eventData)  
    {
        //shift�� ���� ������ ������ ����
        //shift�� �Բ� ������ �� => splitUI����, Keyboard�Լ��� inputsystem�� �־�߸� Ȱ�밡��, ������ ���߿� shiftŬ���� �Ѱ� �ƴ��� Ȯ��
        if (Keyboard.current.leftShiftKey.ReadValue() > 0 && !splitUI.isSplitting) 
        {
            if (slotUICount < slotUIData.itemMaxCount +1 && slotUICount > 1)    //�����͸� ���������� Ȯ��
            {
                splitUI.splitItemData = slotUIData;
                splitUI.splitPossibleCount = slotUICount;   //���� ������ �������ִ� ������ ������, splitPossibleCount�� splitUI������ ���� ������ splitCount�� ��ȯ
                splitUI.takeID = slotUIID;                  //���� �ش� ������ ID�� ������ � �������� ����
                splitUI.SplitUIOpen();
                //�������

            }

        }
        //------------------Split�� OK�� ���� ���콺�� TempSlot�� Ȱ��ȭ�� ����----------------------------------------------------
        else if(splitUI.isSplitting)    //�׳� ������ �� => tempSlot�� Ȱ��ȭ �� �������� Ȯ�� && �ùٸ� ��ġ���� Ȯ��
        {
            if (slotUIData == null)
            {
                slotUIData = splitUI.splitTempSlotSplitUI.takeSlotItemData;     //tempslot�� �����͸� �Ѱܹޱ�
                slotUICount = splitUI.splitTempSlotSplitUI.takeSlotItemCount;   //tempslot�� �������� ������ �Ѱܹޱ�
                playerInven.itemSlots[this.slotUIID].AssignSlotItem(slotUIData, slotUICount);    //���� ���Կ��� �����Ϳ� ���� ����
                splitUI.splitTempSlotSplitUI.ClearTempSlot();                   //tempSlot�� ������ �������� �ʱ�ȭ
                splitUI.isSplitting = false;
                splitUI.splitTempSlotSplitUI.gameObject.SetActive(false);       //ó�� �������� tempslotUI����

                playerInvenUI.SetAllSlotWithData(); //���ιٲﰪ ���ΰ�ħ
            }
            else if(slotUIData == splitUI.splitTempSlotSplitUI.takeSlotItemData)  //tempslot�� �����Ϳ� �������� Ȯ��
            {
                //������ ���������Űܿ��� �� ��ģ�� �ִ� �������� ���� ���
                if (splitUI.splitTempSlotSplitUI.takeSlotItemCount + slotUICount < splitUI.splitTempSlotSplitUI.takeSlotItemData.itemMaxCount)   
                {
                    slotUICount += splitUI.splitTempSlotSplitUI.takeSlotItemCount;   //tempslot�� �������� ������ ��ġ��
                    playerInven.itemSlots[this.slotUIID].IncreaseSlotItem(splitUI.splitTempSlotSplitUI.takeSlotItemCount);    //���� ���Կ��� ���� ��ġ��
                    splitUI.splitTempSlotSplitUI.ClearTempSlot();                   //tempSlot�� ������ �������� �ʱ�ȭ
                    splitUI.isSplitting = false;
                    splitUI.splitTempSlotSplitUI.gameObject.SetActive(false);       //ó�� �������� tempslotUI����

                    playerInvenUI.SetAllSlotWithData(); //���ιٲﰪ ���ΰ�ħ
                }
                else //������ ���������Űܿ��� �� ��ģ�� �ִ� �������� ���� ���
                {
                    uint remainCount;   //remainCount = �ִ밪���� �ʿ��� ����
                    uint newTempSlotCount;
                    remainCount = (uint)splitUI.splitTempSlotSplitUI.takeSlotItemData.itemMaxCount - slotUICount;   //�ִ밪���� �ʿ��� ���� ����
                    newTempSlotCount = splitUI.splitTempSlotSplitUI.takeSlotItemCount - remainCount;  //�����ִ� �縸ŭ ���ְ� ������ �ش� �� ����
                    slotUICount = (uint)slotUIData.itemMaxCount;       //=> �ִ밪��ŭ ���޹޾ұ� ������ �ִ밪��ŭ ����
                    playerInven.itemSlots[this.slotUIID].IncreaseSlotItem(remainCount); //������ ���Ե� �ִ밪��ŭ ����

                    //�ִ밹���� �ְ� ���� �ִ� ���Կ� ���� ���� �����ִ� �۾�
                    playerInvenUI.slotUIs[splitUI.takeID].slotUICount += newTempSlotCount;      //�����ִ� ����UI�� ���� ���� ������
                    playerInven.itemSlots[splitUI.takeID].IncreaseSlotItem(newTempSlotCount);   //�����ִ� ���Կ� ���� ���� ������
                    splitUI.splitTempSlotSplitUI.ClearTempSlot();                   //tempSlot�� ������ �������� �ʱ�ȭ
                    splitUI.isSplitting = false;
                    splitUI.splitTempSlotSplitUI.gameObject.SetActive(false);       //ó�� �������� tempslotUI����

                    playerInvenUI.SetAllSlotWithData(); //���ιٲﰪ ���ΰ�ħ

                }


            }


            //1. �󽽷� �ϰ�� �׳� �ֱ�
            //2. ���� ������ Ÿ���̰� ������ �� maxcount�� �ȳѴ� ���
            //3. ���� ������ Ÿ���̰� ������ �� maxcount�� �Ѵ� ��� -> ���� �� ���� �������� ������.
            //4. �ٸ� ��� �ƹ��ϵ� �Ͼ�� �ʴ´�.

            //���� ���� : �������� �ѹ� ������ �Ҵ��ϴ°� �����ѵ� �������ŷ� �ٽ� ������ ���� ���ø� â����(������� �ߵ�) ok������ ������
            //�ذ�, �ʱ�ȭ �Ҷ� Image�� null�� ���� ���信������
        }


        /*
         * -���� �۾��� ���Ͽ�-
         * splitUI�� ų���� Shift�� �Բ� �������Ѵ�.
         * �׳� Ŭ���� ���� SplitUI���� OK�� ���� �� �ٸ� ������ �����ؾ��ϴ� ��Ȳ���� Ȯ���ϰ� 
         * �ƴ϶�� �ƹ��ϵ� �Ͼ�� �ʰ� 
         * �´ٸ� Ŭ���� ��ġ�� ������ �������� Ȯ���ϰ� �󽽷��̰ų� ���� �������� Ȯ���Ѵ�. 
         * ���� �߸��� �����̰ų� �߸��� ��ġ�� ������ ���� ���Կ� ��������.
         * ������ ������ ������ �������� ������ ���� �������� ���� �Ҵ��ϰų� �߰��Ѵ�.   
         * 
         * -SplitUI�� ����-
         * Ŭ���ϸ� �ش� ������ ������ ������ 2�̻� ���� Ȯ���ϰ� �´ٸ� SplitUI�� Ŀ����ġ�� �����Ų��.
         * ��ǲ �ʵ�� 1�̻� ������ ���� ������ ������ ������ �ȴ�.
         * ok��ư�� ������ SplitUI�� �ݰ� ���� �����Ϳ��� �ش� �ϴ� ����ŭ�� ������ ���� �ش��ϴ� ������ �����͸� ���� tempSlotUI�� �����.
         * cancelButton�� ������ SplitUI�� �ݴ´�.
         */
    }


    //------------------OnDrag�� ������� �ʴ��� �������̽� ����� �ȹ����� BiginDrag�� �۵����� �ʴ´�.-----------------------------

    /// <summary>
    /// ������ �̵��̳� ��� ���� �ϱ����� �巡�� ���� ���� 
    /// </summary>
    /// <param name="eventData"></param>
    public void OnBeginDrag(PointerEventData eventData)     
    {
        if(slotUIData != null)
        {
            isDrag = true;

            //FindObjectOfType<TempSlotSplitUI>().gameObject.SetActive(true);   //��Ȱ��ȭ�ż� �ٷ� ã�°� �ȵ�, �Ʒ�ó�� �θ� ã���� �� �ڽ��� ã���������� ã�ƾ���

            GameObject.Find("ItemMoveSlotUI").transform.GetChild(0).gameObject.SetActive(true); //tempSlot�� ��Ȱ��ȭ ���״� �θ������Ʈ�� ���� ã�Ƽ� Ȱ��ȭ ��ų���̴�.

            tempSlotSplitUI.SetTempSlotWithData(slotUIData, slotUICount);       //�̵��� ������ tempslot�� �����ϰ�

            playerInven.itemSlots[slotUIID].ClearSlotItem();             //UI�� ���� �����Ϳ����� �����Ϳ� ������ ��
            slotUICount = 0;

            playerInvenUI.SetAllSlotWithData();

        }

        /*
         * �巡�׸� �����ϸ� tempslotUI�� �巡�׸� �����Ѱ��� �̹����� �����ͼ� ���콺 ��ġ�� update�ȴ�.
         */
    }

    /// <summary>
    /// �̶� ȣ��Ǵ� OnEndDrag�Լ��� ó���� OnBiginDrag�� �ִ� ������ �Լ���.
    /// </summary>
    /// <param name="eventData"></param>
    public void OnEndDrag(PointerEventData eventData)       //������ �̵��̳� ��� ���� �ϱ� ���� �巡�� ���� ����
    {
        GameObject obj = eventData.pointerCurrentRaycast.gameObject;
        if (obj != null)    //�ϴ� ���콺�� �� ���� ����ĳ��Ʈ�� �ִ°� = �κ��丮 ������ ���
        {
            //Debug.Log($"{obj.name}");
            ItemSlotUI targetItemSlotUI = obj.GetComponent<ItemSlotUI>();

            if (targetItemSlotUI != null) //������ ������Ʈ�� ������ ����UI�� �ִ°��̶�� = ItemSlotUI���
            {
                if(targetItemSlotUI.slotUIData == null)    //���� ������ �󽽷��̶��
                {
                    targetItemSlotUI.SetSlotWithData(tempSlotSplitUI.takeSlotItemData, tempSlotSplitUI.takeSlotItemCount);
                    playerInven.itemSlots[targetItemSlotUI.slotUIID].AssignSlotItem(tempSlotSplitUI.takeSlotItemData, tempSlotSplitUI.takeSlotItemCount);//���� ���Կ��� �����Ϳ� ���� ����
                    splitUI.splitTempSlotSplitUI.ClearTempSlot();                   //tempSlot�� ������ �������� �ʱ�ȭ
                    splitUI.splitTempSlotSplitUI.gameObject.SetActive(false);       //ó�� �������� tempslotUI����

                    playerInvenUI.SetAllSlotWithData(); //��ġ�� ���� ���� �����Ͱ� ����� ������ Ȯ���ϱ� ���� �����ͷ� ������ �ٲ��ش�
                }
                else    //�󽽷��� �ƴϰ� �������� �����ϴ� �����̶��
                {
                    if(targetItemSlotUI.slotUIData == tempSlotSplitUI.takeSlotItemData &&   //�ű�� ������ ������ ���� //2���� ���� maxCount���� �۰ų� ������쿣 ��ģ��
                        targetItemSlotUI.slotUICount + tempSlotSplitUI.takeSlotItemCount < targetItemSlotUI.slotUIData.itemMaxCount +1 ) 
                    {
                        targetItemSlotUI.slotUICount += tempSlotSplitUI.takeSlotItemCount;
                        playerInven.itemSlots[targetItemSlotUI.slotUIID].IncreaseSlotItem(tempSlotSplitUI.takeSlotItemCount);    //���� ���Կ��� �����Ϳ� ���� ����
                        targetItemSlotUI.SetSlotWithData(targetItemSlotUI.slotUIData, targetItemSlotUI.slotUICount);    //�ڱ� �ڽ��� ������ �ٲ������ �ش絥���ͷ� UIǥ�� �ֽ�ȭ

                        splitUI.splitTempSlotSplitUI.ClearTempSlot();                   //tempSlot�� ������ �������� �ʱ�ȭ
                        splitUI.splitTempSlotSplitUI.gameObject.SetActive(false);       //ó�� �������� tempslotUI����

                        playerInvenUI.SetAllSlotWithData(); //��ġ�� ���� ���� �����Ͱ� ����� ������ Ȯ���ϱ� ����  �����ͷ� ������ �ٲ��ش�
                    }
                    else    //�ű�� ������ ������ �ٸ� �����̰ų� ������ ���ľ��ϴµ� 2���� ���� maxCount���� ���� ��� => �����͸� ���� �ٲ۴�
                    {
                        //�� �������� �ڸ� �ű��
                        playerInven.itemSlots[this.slotUIID].AssignSlotItem(targetItemSlotUI.slotUIData, targetItemSlotUI.slotUICount);    //���� ���Կ� �����Ϳ� ���� ����
                        SetSlotWithData(targetItemSlotUI.slotUIData, targetItemSlotUI.slotUICount);  //temp�������� �����͸� ���� ����ִ� �ڱ� �ڽſ� ��� ������ �� ���� �Ҵ�

                        //���� ���Կ� �����Ϳ� ���� ����
                        playerInven.itemSlots[targetItemSlotUI.slotUIID].AssignSlotItem(tempSlotSplitUI.takeSlotItemData, tempSlotSplitUI.takeSlotItemCount);    
                        targetItemSlotUI.SetSlotWithData(tempSlotSplitUI.takeSlotItemData, tempSlotSplitUI.takeSlotItemCount);
                        
                        splitUI.splitTempSlotSplitUI.ClearTempSlot();                   //tempSlot�� ������ �������� �ʱ�ȭ
                        splitUI.splitTempSlotSplitUI.gameObject.SetActive(false);       //ó�� �������� tempslotUI����

                        playerInvenUI.SetAllSlotWithData(); //��ġ�� ���� ���� �����Ͱ� ����� ������ Ȯ���ϱ� ���� �����ͷ� ������ �ٲ��ش�
                    }
                }

            }
            else    //������ ����UI�� �ƴ϶��
            {
                SetSlotWithData(tempSlotSplitUI.takeSlotItemData, tempSlotSplitUI.takeSlotItemCount);
                playerInven.itemSlots[this.slotUIID].AssignSlotItem(slotUIData, slotUICount);    //���� ���Կ��� �����Ϳ� ���� ����
                splitUI.splitTempSlotSplitUI.ClearTempSlot();                   //tempSlot�� ������ �������� �ʱ�ȭ
                splitUI.splitTempSlotSplitUI.gameObject.SetActive(false);       //ó�� �������� tempslotUI����

                playerInvenUI.SetAllSlotWithData(); //��ġ�� ���� ���� �����Ͱ� ����� ������ Ȯ���ϱ� ����
            }
        }
        else //raycast�޴� ���� ������Ʈ�� �ƿ����� �� => Inventory�ۿ� ������ �� = ������ ���
        {

            dropUI.splitItemData = tempSlotSplitUI.takeSlotItemData;
            dropUI.splitPossibleCount = tempSlotSplitUI.takeSlotItemCount;   //���� ������ �������ִ� ������ ������, splitPossibleCount�� splitUI������ ���� ������ splitCount�� ��ȯ
            dropUI.takeID = slotUIID;                                      //���� �ش� ������ ID�� ������ � �������� ����
            splitUI.splitTempSlotSplitUI.ClearTempSlot();                   //tempSlot�� ������ �������� �ʱ�ȭ
            splitUI.splitTempSlotSplitUI.gameObject.SetActive(false);       //ó�� �������� tempslotUI����
            
            dropUI.SplitUIOpen();
            //�������

            
        }




        //Debug.Log($"{slotUIID}");



        isDrag = false;

        /*
         * 1.
         * ���� tempslotUI�� �̹����� �巡�װ� ������ ���� �̹����� ���ٸ� ������ ������ Ȯ���ؼ� �������� �̵���Ų��.
         * ���� �̵���Ų ������ ������ ���� �ִ� �������� ���ٸ� ��� �̵���ų�� �����ϴ� â�� ����� ���������� �ִ밹������ ���� �����ϰ� �����.
         * �̵���Ų �����۰����� ���� �ִ밹������ ���ٸ� ����� �ʰ� �ٷ� �̵��Ѵ�
         * ������ ������ �����ִٸ� ��ġ�� �ٲ۴�.
         * 
         * 2.���� tempslotUI�� �̹����� �巡�װ� ������ ���� �̹����� �ٸ��� NULL�� �ƴ϶�� �������� ��ġ�� �ٲ۴�.
         *3.���� ������ ���� �̹����� NULL�̶�� �������� �ش� ��ġ�� �̵��Ѵ�.
         *4.���� ������ ���� ������ �ƴϰ� �κ��丮�� ���� �׵θ���� �ƹ��ϵ� �Ͼ�� �ʴ´�.(���� ������ġ�� ���ư���.)
         *5.���� ������ ���� �����̳� �κ��丮�� ���� �׵θ��� �ƴ϶��(�ܺζ��) ������ ���ø� â�� ��������� �������� ����Ѵ�.
         */
    }

    /// <summary>
    /// onbigindrag�� onenddrag�� �۵��Ǳ� ���ؼ��� ondrag�� �ʿ��ϴ�
    /// </summary>
    /// <param name="eventData"></param>
    public void OnDrag(PointerEventData eventData)
    {
        
    }


}
