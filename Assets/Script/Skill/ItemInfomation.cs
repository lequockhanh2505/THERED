using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemInfomation : MonoBehaviour, IPointerClickHandler
{
    
    [SerializeField]
    Image SpriteRenderer;

    [SerializeField]
    Image Hover;

    public void OnPointerClick(PointerEventData eventData)
    {
        SpriteRenderer = gameObject.transform.GetChild(1).GetComponent<Image>();
        //Debug.Log(this.gameObject.transform.GetChild(1).GetComponent<Image>().sprite);
    }

    private void Update()
    {
        if (SpriteRenderer != null)
        {
            if (Input.GetMouseButton(0))
            {
                Hover.sprite = SpriteRenderer.sprite;
            }
        }
    }
}
