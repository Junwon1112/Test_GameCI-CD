using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���ϴ� ��ų�����͸� ã�� Ŭ����
/// </summary>
public class SkillDataManager : MonoBehaviour
{
    public SkillData_Normal[] skillDatas_Normal;
    public SkillData_Buff[] skillDatas_Buff;
    public SkillData_Duration[] skillDatas_Duration;


    //----------------------------------------------------------------------
    /// <summary>
    /// ID�� ���ϴ� ��ų������ ã�� �Լ�, �����ε�
    /// </summary>
    /// <param name="skillID"></param>
    /// <returns></returns>
    public SkillData_Normal FindSkill_Normal(int skillID)
    {
        for(int i = 0; i < skillDatas_Normal.Length; i++)
        {
            if(skillDatas_Normal[i].skillId == skillID)
            {
                return skillDatas_Normal[i];
            }            
        }

        return null;
    }

    /// <summary>
    /// skillIDCode(������)�� ���ϴ� ��ų������ ã�� �Լ�, �����ε�
    /// </summary>
    /// <param name="skillID"></param>
    /// <returns></returns>
    public SkillData_Normal FindSkill_Normal(SkillIDCode skillIDCode)
    {
        for (int i = 0; i < skillDatas_Normal.Length; i++)
        {
            if (skillDatas_Normal[i].skillIDCode == skillIDCode)
            {
                return skillDatas_Normal[i];
            }
        }

        return null;
    }

    /// <summary>
    /// �̸����� ���ϴ� ��ų������ ã�� �Լ�, �����ε�
    /// </summary>
    /// <param name="skillID"></param>
    /// <returns></returns>
    public SkillData_Normal FindSkill_Normal(string skillName)
    {
        for (int i = 0; i < skillDatas_Normal.Length; i++)
        {
            if (skillDatas_Normal[i].skillName == skillName)
            {
                return skillDatas_Normal[i];
            }
        }

        return null;
    }

    /////////-------------------------------------------------------------------------

    /// <summary>
    /// ID�� ���ϴ� ��ų������ ã�� �Լ�, �����ε�
    /// </summary>
    /// <param name="skillID"></param>
    /// <returns></returns>
    public SkillData_Duration FindSkill_Duration(int skillID)
    {
        for (int i = 0; i < skillDatas_Duration.Length; i++)
        {
            if (skillDatas_Duration[i].skillId == skillID)
            {
                return skillDatas_Duration[i];
            }
        }

        return null;
    }

    /// <summary>
    /// skillIDCode(������)�� ���ϴ� ��ų������ ã�� �Լ�, �����ε�
    /// </summary>
    /// <param name="skillID"></param>
    /// <returns></returns>
    public SkillData_Duration FindSkill_Duration(SkillIDCode skillIDCode)
    {
        for (int i = 0; i < skillDatas_Duration.Length; i++)
        {
            if (skillDatas_Duration[i].skillIDCode == skillIDCode)
            {
                return skillDatas_Duration[i];
            }
        }

        return null;
    }

    /// <summary>
    /// �̸����� ���ϴ� ��ų������ ã�� �Լ�, �����ε�
    /// </summary>
    /// <param name="skillID"></param>
    /// <returns></returns>
    public SkillData_Duration FindSkill_Duration(string skillName)
    {
        for (int i = 0; i < skillDatas_Duration.Length; i++)
        {
            if (skillDatas_Duration[i].skillName == skillName)
            {
                return skillDatas_Duration[i];
            }
        }

        return null;
    }
    /////////-------------------------------------------------------------------------
    /// <summary>
    /// ID�� ���ϴ� ��ų������ ã�� �Լ�, �����ε�
    /// </summary>
    /// <param name="skillID"></param>
    /// <returns></returns>
    public SkillData_Buff FindSkill_Buff(int skillID)
    {
        for (int i = 0; i < skillDatas_Buff.Length; i++)
        {
            if (skillDatas_Buff[i].skillId == skillID)
            {
                return skillDatas_Buff[i];
            }
        }

        return null;
    }

    /// <summary>
    /// skillIDCode(������)�� ���ϴ� ��ų������ ã�� �Լ�, �����ε�
    /// </summary>
    /// <param name="skillID"></param>
    /// <returns></returns>
    public SkillData_Buff FindSkill_Buff(SkillIDCode skillIDCode)
    {
        for (int i = 0; i < skillDatas_Buff.Length; i++)
        {
            if (skillDatas_Buff[i].skillIDCode == skillIDCode)
            {
                return skillDatas_Buff[i];
            }
        }

        return null;
    }

    /// <summary>
    /// �̸����� ���ϴ� ��ų������ ã�� �Լ�, �����ε�
    /// </summary>
    /// <param name="skillID"></param>
    /// <returns></returns>
    public SkillData_Buff FindSkill_Buff(string skillName)
    {
        for (int i = 0; i < skillDatas_Buff.Length; i++)
        {
            if (skillDatas_Buff[i].skillName == skillName)
            {
                return skillDatas_Buff[i];
            }
        }

        return null;
    }

}
