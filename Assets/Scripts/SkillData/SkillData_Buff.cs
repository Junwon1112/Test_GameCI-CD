using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ����Ÿ���� ��ų�����͸� ������ ��ũ���ͺ� ������Ʈ, SkillData�� ��ӹ���, ������ų ���� �̱���
/// </summary>
[CreateAssetMenu(fileName = ("New Skill Data"), menuName = ("Scriptable Object_Skill Data/Skill Data_Buff"), order = 3)]
public class SkillData_Buff : SkillData //���� ��ų
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

    public float buffDuration;

    public override float SetSkillDamage(float attackDamage)    //������ ��ų������ ���� ���� X
    {
        return 0;
    }
}
