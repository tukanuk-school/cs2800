using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Transform parentToReturnTo = null;
    public Transform placeholdParent = null;

    GameObject placeholder = null;

  public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("OnBeginDrag");

        placeholder = new GameObject();
        placeholder.transform.SetParent(this.transform.parent);
        LayoutElement le = placeholder.AddComponent<LayoutElement>();
        le.preferredWidth = this.GetComponent<LayoutElement>().preferredWidth;
        le.preferredHeight = this.GetComponent<LayoutElement>().preferredHeight;
        le.flexibleWidth = 0;
        le.flexibleHeight = 0;

        placeholder.transform.SetSiblingIndex(this.transform.GetSiblingIndex());

        Debug.Log(placeholder);

        parentToReturnTo = this.transform.parent;
        placeholdParent = parentToReturnTo;
        this.transform.SetParent(this.transform.parent.parent);


        GetComponent<CanvasGroup>().blocksRaycasts = false; 

    }

    public void OnDrag(PointerEventData eventData)
    {
        //Debug.Log("on drag you did it!");

        this.transform.position = eventData.position;

        if(placeholder.transform.parent != placeholdParent)
        {
            placeholder.transform.SetParent(placeholdParent);
        }
        int newSiblingIndex = placeholdParent.childCount;

        for (int i = 0; i < placeholdParent.childCount; i++)
        {
            if(this.transform.position.x < placeholdParent.GetChild(i).position.x)
            {
                newSiblingIndex = i;
                if (placeholder.transform.GetSiblingIndex() < newSiblingIndex)
                    newSiblingIndex--;
                break;
            }
        }

        placeholder.transform.SetSiblingIndex(newSiblingIndex);



    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("end drag you did it!");
        this.transform.SetParent(parentToReturnTo);
        this.transform.SetSiblingIndex(placeholder.transform.GetSiblingIndex());

        GetComponent<CanvasGroup>().blocksRaycasts = true;

        Destroy(placeholder);
    }
}
