using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class JoystickController : MonoBehaviour,IDragHandler,IPointerUpHandler,IPointerDownHandler

{
    private Image backimage;
    private Image JoystickImage;
    private Vector3 inputVector;

    public void Start()
    {
        backimage = GetComponent<Image>();
        JoystickImage = transform.GetChild(0).GetComponent<Image>();
    }
    public virtual void OnPointerDown(PointerEventData ped)
    {
        OnDrag(ped);
    }
    public virtual void OnPointerUp(PointerEventData ped)
    {
        inputVector = Vector3.zero;
        JoystickImage.rectTransform.anchoredPosition = Vector3.zero;
    }

    public virtual void OnDrag(PointerEventData ped)
    {
        Vector2 pos;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(backimage.rectTransform, ped.position, ped.pressEventCamera, out pos)) 

        {
            
            pos.x = (pos.x / backimage.rectTransform.sizeDelta.x);
            pos.y = (pos.y / backimage.rectTransform.sizeDelta.y);
            inputVector = new Vector3(pos.x * 2 + 1, 0, pos.y * 2 - 1);
            inputVector = (inputVector.magnitude > 1.0f) ? inputVector.normalized : inputVector;

            JoystickImage.rectTransform.anchoredPosition = new Vector3(inputVector.x*(backimage.rectTransform.sizeDelta.x/2),inputVector.z*(backimage.rectTransform.sizeDelta.y/2));
            Debug.Log(inputVector);
        }
    }
    public float Horizontal()
    {
        
        
        if (inputVector.x != 0)
        {
            
            return inputVector.x;
        }
        else
        {
            
            return Input.GetAxis("Horizontal");
        }
    }

    public float Vertical()
    {
      
       
        if (inputVector.z != 0)
        {
        
            return inputVector.z;
        }
        else
        {
           
            return Input.GetAxis("Vertical");
        }
    }
}
