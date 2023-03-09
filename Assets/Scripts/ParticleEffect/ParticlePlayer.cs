using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * @brief ��ƼŬ �ý����� �����ϴ� ��ũ��Ʈ
 * @details 
 * -�̱����� ���� ����
 * -��ƼŬ�� ������ �� Ʈ�������� ���� ��ü�� ���ӽ�Ų�� vs ���� ��ü�� Ʈ������(��ġ, ȸ��)�� �ް� ���ӽ�Ű�� �ʴ´�.
 * -2������ ������ �ҵ�? ���� ȿ���� ��ü ����, �ܼ� ����Ʈ�� ���� x
 * 
 * -���� vs 1ȸ ��� �� ����
 * 
 */
public class ParticlePlayer : MonoBehaviour
{
    public static ParticlePlayer Instance { get; private set; }

    [SerializeField]
    protected GameObject[] _particles; 

    protected ParticleObject particleBasic;

    protected List<ParticleObject> list_Particles = new List<ParticleObject>();

    protected Dictionary<ParticleType, GameObject> _particlesDict = new Dictionary<ParticleType, GameObject>();




    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            if(Instance != null)
            {
                Destroy(this.gameObject);
            }
        }

        Initialize();
    }

    /// <summary>
    /// Awake�� �� �Լ���
    /// </summary>
    private void Initialize()
    {
        _particlesDict.Clear(); //��ųʸ� �ʱ�ȭ

        list_Particles.Add(particleBasic);  //����Ʈ�� �⺻ ��ƼŬ �߰�

        for(int i = 0; i < _particles.Length; i++)  //��ųʸ��� Enum�� Ű������ ���ӿ�����Ʈ(��ƼŬ������Ʈ)�����ϵ��� ���
        {
            _particlesDict.Add((ParticleType)System.Enum.Parse(typeof(ParticleType), _particles[i].name), _particles[i]);
        }
    }

    private void OnDestroy()
    {
        Instance = null;
    }



    /// <summary>
    /// Ÿ�Ը��� ���� ��ųʸ����� ���ӿ�����Ʈ�� ���Ϲް� �ش� ������Ʈ�� Ʈ������ �Ķ���Ϳ� ���ӽ�ų�� ��ġ ���� ������ ���Ѵ�
    /// </summary>
    /// <param name="particleType">����� ���� ��ƼŬ�� �ش�Ǵ� Ÿ�Ը� </param>
    /// <param name="parentTransform">������� ��ƼŬ�� �θ�</param>
    /// <param name="position">������� ��ƼŬ ��ġ</param>
    /// <param name="rotation">������� ��ƼŬ�� ȸ��</param>
    /// <returns>��ƼŬ�� �÷��� �ϴ� ParticleObject</returns>
    protected ParticleObject CreateParticleObject(ParticleType particleType, Transform parentTransform, Vector3 position, Quaternion rotation)
    {
        _particlesDict.TryGetValue(particleType, out GameObject gameObj);   //�����տ� ���� ������

        GameObject newParticleObj = Instantiate(gameObj, position, rotation, parentTransform);
        ParticleObject particleObj = newParticleObj.AddComponent<ParticleObject>();

        return particleObj;
    }


    protected ParticleObject CreateParticleObject(ParticleType particleType, Vector3 position, Quaternion rotation)
    {
        _particlesDict.TryGetValue(particleType, out GameObject gameObj);   //�����տ� ���� ������

        GameObject newParticleObj = Instantiate(gameObj, position, rotation);
        ParticleObject particleObj = newParticleObj.AddComponent<ParticleObject>();


        return particleObj;
    }

    protected ParticleObject CreateParticleObject(ParticleType particleType, Transform parentTransform)
    {
        _particlesDict.TryGetValue(particleType, out GameObject gameObj);   //�����տ� ���� ������

        GameObject newParticleObj = Instantiate(gameObj, transform, false);
        ParticleObject particleObj = newParticleObj.AddComponent<ParticleObject>();

        return particleObj;
    }


    public void PlayParticle(ParticleType particleType, Transform parentTransform, Vector3 position, Quaternion rotation, float playTime)
    {
        ParticleObject particleObj = CreateParticleObject(particleType, parentTransform, position, rotation);

        particleObj.Play(playTime);
    }
    public void PlayParticle(ParticleType particleType, Vector3 position, Quaternion rotation, float playTime)
    {
        PlayParticle(particleType, null, position, rotation, playTime );
    }

    public void PlayParticle(ParticleType particleType, Transform parentTransform, float playTime)
    {
        ParticleObject particleObj = CreateParticleObject(particleType, parentTransform);

        particleObj.Play(playTime);
    }


    public void PlayParticle(ParticleType particleType, Transform parentTransform,Vector3 position, Quaternion rotation)
    {
        ParticleObject particleObj = CreateParticleObject(particleType, parentTransform, position, rotation);

        particleObj.Play();
    }
    public void PlayParticle(ParticleType particleType, Vector3 position, Quaternion rotation)
    {
        PlayParticle(particleType, null, position, rotation);
    }

    public void PlayParticle(ParticleType particleType, Transform parentTransform)
    {
        ParticleObject particleObj = CreateParticleObject(particleType, parentTransform);

        particleObj.Play();
    }





}
