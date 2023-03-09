using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

/// <summary>
/// �巡�� �� ���� �ӽ� ��ų����
/// </summary>
public class TempSlotSkillUI : MonoBehaviour
{
    Image skillImage;
    public SkillData tempSkillData;

    private void Awake()
    {
        skillImage = GetComponent<Image>();
    }

    private void Start()
    {
        this.gameObject.SetActive(false);   //canvasGroup���� OnOff�ϸ� �Ⱥ��̴� ���� update���� Image�� ���콺�� ��� ����ٴϹǷ� Ȱ��ȭ�� ����. 
    }

    private void Update()
    {
        transform.position = (Vector3)Mouse.current.position.ReadValue();
    }

    public void SetTempSkillSlotUIData(SkillData skillData)
    {
        tempSkillData = skillData;
        skillImage.sprite = tempSkillData.skillIcon;
    }
}
