using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ü�� �� ���� ���� ��ġ�� �ܺο��� ������ �� �ʿ��� �������̽�
/// </summary>
public interface IHealth
{
    public Transform CharacterTransform { get; }
    public float HP { get; set; }
    public float MaxHP { get; }

    public float Defence { get; set; }

}
