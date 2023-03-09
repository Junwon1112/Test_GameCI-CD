using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * @brief ���尡 ����Ǵ� ������Ʈ�� �ٴ� ��ũ��Ʈ
 * @details 
 * �� ��ũ��Ʈ�� �ٴ� ������Ʈ���� AudioSource ������Ʈ�� �ʼ�
 * ���� ��� ���� ��� ����
 * �ڷ�ƾ�� ����� �����ð� ������ �� ����ϵ��� ����
 * ����� ���� ���� ������Ʈ�� Destroy��
 */

[RequireComponent(typeof(AudioSource))]     //�ش� ������Ʈ�� ���� ��� �ڵ� �߰�
public class SoundObject : MonoBehaviour
{
    [SerializeField]
    private AudioSource audioSource = null;

    private AudioClip audioClip = null;

    private System.Action action_FinishListner = null;

    private IEnumerator obj_currentRoutine = null;

    private bool isStoppable = true;

    private bool isPlaying = false;

    [SerializeField]
    private bool isDestroyWhenPlayEnd = true;

    public AudioSource AudioSource
    {
        get 
        {
            return audioSource;
        }
        set
        {
            audioSource = value;
        }
    }

    public AudioClip AudioClip
    {
        get
        {
            return audioClip;
        }
        set
        {
            audioClip = value;
        }
    }

    public bool IsPlaying
    {
        get
        {
            return isPlaying;
        }
        private set 
        {
            isPlaying = value; 
        }
    }

    private void Awake()
    {
        if(GetComponent<AudioSource>())
        {
            audioSource = GetComponent<AudioSource>();
        }
        
    }

    public void Play(float _volume, float _delaySeconds, bool _isLoop, bool _isStoppable, System.Action _finishListner = null)
    {
        if(audioSource == null)
        {
            Debug.Log("No AudioSource");
            return;
        }
        isStoppable = _isStoppable;

        audioSource.clip = AudioClip;
        audioSource.volume = _volume;
        audioSource.loop = _isLoop;
        action_FinishListner = _finishListner;
        obj_currentRoutine = DelayPlaySound(_delaySeconds);
        StartCoroutine(obj_currentRoutine);
    }

    IEnumerator DelayPlaySound(float delayTime)
    {
        IsPlaying = true;
        yield return new WaitForSeconds(delayTime);
        audioSource.Play();
        yield return new WaitForSeconds(AudioClip.length);
        IsPlaying = false;
        if(action_FinishListner != null)
        {
            action_FinishListner();
        }
        if(!audioSource.loop)
        {
            DestroySound();
        }
    }

    public void Pause()
    {
        audioSource.Pause();
    }

    public void UnPause()
    {
        audioSource.UnPause();
    }

    public void Stop()
    {
        if(isStoppable)
        {
            audioSource.Stop();
            if(obj_currentRoutine != null)
            {
                StopCoroutine(obj_currentRoutine);
            }
            IsPlaying = false;
            DestroySound();
        }
    }

    private void DestroySound()
    {
        try 
        {
            if (isDestroyWhenPlayEnd)
                Destroy(gameObject);
        }
        catch(Exception e)
        {
            Debug.Log(e.ToString());
        }
    }
}
