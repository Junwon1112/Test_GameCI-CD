using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

/// <summary>
/// ��ų ���� ���õ� Ŭ����, �����Կ��� ȣ��
/// </summary>
public class SkillUse : MonoBehaviour
{
    public bool isSkillUsed = false;    //��Ÿ�� üũ�� bool�Լ�
    public float timer = 0.0f;
    Animator anim;
    Player player;
    PlayerWeapon weapon;


    private void Awake()
    {
        player = FindObjectOfType<Player>();
        
        anim = player.transform.GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        if(timer > 0)
        {
            timer -= Time.fixedDeltaTime;
            isSkillUsed = true;
        }
        else
        {
            isSkillUsed = false;
        }
        
    }

    /// <summary>
    /// ��ų �����Ϳ� ���� �ٸ������� ��ų�� �����
    /// </summary>
    /// <param name="skillData"></param>
    public void UsingSkill(SkillData skillData)
    {
        if(!isSkillUsed)
        {
            timer = skillData.skillCooltime;
            weapon.SkillDamage = skillData.SetSkillDamage(player.AttackDamage);

            anim.SetBool("IsSkillUse", true);
            if(skillData.skillType == SkillTypeCode.Skill_Duration)
            {
                SkillData_Duration tempSkill_Duration = GameManager.Instance.SkillDataManager.FindSkill_Duration(skillData.skillId);
                float skillUsingTime = tempSkill_Duration.skillDuration;

                float compensateTime = 0.5f;
                Quaternion compensateRotaion = Quaternion.Euler(-90.0f, 0.0f, 0.0f);
                Vector3 compensatePosition = new Vector3(0, -1.5f, 0);

                ParticlePlayer.Instance?.PlayParticle(ParticleType.ParticleSystem_WheelWind, player.transform, 
                    player.transform.position + compensatePosition, player.transform.rotation * compensateRotaion, skillUsingTime+ compensateTime);

                StartCoroutine(SkillDurationTime(skillUsingTime));
            }
        }
    }

    /// <summary>
    /// ���������� ��ų ���ݽ� �ִϸ��̼� ���ӽð��� ����
    /// </summary>
    /// <param name="skillDuration"></param>
    /// <returns></returns>
    IEnumerator SkillDurationTime(float skillDuration) //��ų ���ӽð�
    {
        yield return new WaitForSeconds(skillDuration);
        anim.SetBool("IsSkillUse", false);
    }

    /// <summary>
    /// �÷��̾ ���⸦ ������ �ش� ���⸦ �ڵ����� ã���� �ϴ� �޼���
    /// </summary>
    public void TakeWeapon()    //�÷��̾�� ���� ������ �� ������
    {
        weapon = FindObjectOfType<PlayerWeapon>();
    }

}
