using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

/// <summary>
/// ��ų ���� UI�� ���� Ŭ����, �ַ� ��ų �������� ���� �� �̵��� ���� �ٷ�
/// </summary>
public class SkillSlotUI : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    //��ų ���� UI���� �����ؾ� �Ұ� 
    //1. �巡���ؼ� ���������� �ű� �� �־����, ��ų ��� �䱸�������� ������ ���� �Ҵ�� ��ų ����Ʈ�� �־�� �巡�� �����ϰ� ����� ����
    //2. ��Ŭ���̳� ���� Ŭ���ؼ� ��ų �ִϸ��̼� �ߵ�?

    /// <summary>
    /// ��ų ���� �� id, skillUI Ŭ�������� �Ҵ��� ����, Ȥ�� �Ҵ� ���� ���ϸ� -1��
    /// </summary>
    int skillSlotUIid = -1;

    /// <summary>
    /// SkillUI�� ����Ʈ�� �迭�� ��ų ��ũ���ͺ� ������Ʈ �ް� ����(skillslotUI)�� �Ҵ�
    /// </summary>
    public SkillData skillData;       
    Image skillIcon;
    TextMeshProUGUI skillInfo;
    TempSlotSkillUI tempSlotSkillUI;

    private void Awake()
    {
        skillIcon = GetComponent<Image>();
        skillInfo = transform.parent.GetComponentInChildren<TextMeshProUGUI>();
        tempSlotSkillUI = GameObject.FindObjectOfType<TempSlotSkillUI>();
    }

    /// <summary>
    /// ��ų ������ ���� �����ð� �÷��ѽ� Info�� ������ �޼���
    /// </summary>
    public void SetSkillUIInfo()    
    {
        if(skillData != null)
        {
            skillIcon.sprite = skillData.skillIcon;
            skillInfo.text = skillData.skillInformation;
        }
        else
        {
            skillIcon.color = Color.clear;
            skillInfo.text = "No Assigned Skill";
        }
    }

    /// <summary>
    /// �巡�� ���۽� ����� �޼���, �ӽ� ���� ����
    /// </summary>
    /// <param name="eventData"></param>
    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
    {
        GameObject.Find("SkillMoveSlotUI").transform.GetChild(0).gameObject.SetActive(true);
        tempSlotSkillUI.SetTempSkillSlotUIData(skillData);
    }

    /// <summary>
    /// �巡�� �Ϸ�� ����� �޼���, ���������� �θ� �ش� �����Կ� ��ų ���
    /// </summary>
    /// <param name="eventData"></param>
    void IEndDragHandler.OnEndDrag(PointerEventData eventData)
    {
        GameObject obj = eventData.pointerCurrentRaycast.gameObject;
        QuickSlotUI quickSlotUI = obj.GetComponent<QuickSlotUI>();

        if(quickSlotUI != null)     //������ ��������� QuickSlotUI������Ʈ�� ������ �������ϱ� �������� ����ٸ� �̶�� ��
        {
            quickSlotUI.QuickSlotSetData(tempSlotSkillUI.tempSkillData);   
        }



        tempSlotSkillUI.transform.gameObject.SetActive(false);
    }

    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        
    }
}
