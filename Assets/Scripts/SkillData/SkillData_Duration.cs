using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���� ����Ÿ���� ��ų�����͸� ������ ��ũ���ͺ� ������Ʈ, SkillData�� ��ӹ���
/// </summary>
[CreateAssetMenu(fileName = ("New Skill Data"), menuName = ("Scriptable Object_Skill Data/Skill Data_Duration"), order = 2)]
public class SkillData_Duration : SkillData     //���ӽð��� �ִ� ���� ��ų
{
    //<�θ� �ִ� ������>

    //public string skillName;
    //public uint skillId;
    //public Sprite skillIcon;

    //public int requireLevel;
    //public float skillCooltime;
    //public float skillDamage;

    //public SkillTypeCode skillType;

    //public string skillStateName;
    //public string skillInformation;
    public float skillDuration;

    public override float SetSkillDamage(float attackDamage)
    {
        float finalSkillDamage = 0;

        finalSkillDamage = this.skillDamage + (attackDamage * 0.2f);


        return finalSkillDamage;
    }
}
