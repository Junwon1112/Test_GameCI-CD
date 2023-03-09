using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary>
/// ���� TopSideBar�� ���õ� �޼���, �巡�� �� �� ���콺�� ����ٴϴ� ��
/// </summary>
public class TopSideBar : MonoBehaviour,  IBeginDragHandler, IEndDragHandler, IDragHandler
{
    //Button topSideBarButton;
    RectTransform parentRectTransform;

    private void Awake()
    {
        //topSideBarButton = GetComponent<Button>();
        parentRectTransform = transform.parent.GetComponent<RectTransform>();
    }

    /// <summary>
    /// �巡�׸� ���� �ҋ� �ش� ��ġ�� �������� �κ��丮�� ������ �� �ְ��ϴ� �޼���, Ŭ���� �Ǻ����� �ٲ� �ش� ��ġ �������� ������ 
    /// </summary>
    /// <param name="eventData"></param>
    public void OnBeginDrag(PointerEventData eventData)  //������ �� �ش� ��ġ�� pivot������ �����ϴ� �Լ�
    {
        float absoluteMinPosition_x = (parentRectTransform.position.x - parentRectTransform.rect.width * (parentRectTransform.pivot.x));
        float absoluteMinPosition_y = (parentRectTransform.position.y - parentRectTransform.rect.height * (parentRectTransform.pivot.y));

        parentRectTransform.pivot = new Vector2((eventData.position.x - absoluteMinPosition_x) / parentRectTransform.rect.width,
                                                    (eventData.position.y - absoluteMinPosition_y) / parentRectTransform.rect.height);

        parentRectTransform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        
    }

    /// <summary>
    /// ���콺�� ������ ������ ȣ��
    /// </summary>
    /// <param name="eventData"></param>
    public void OnDrag(PointerEventData eventData)  
    {
        parentRectTransform.position = eventData.position;
    }
}
