using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.U2D;

public class Holder : MonoBehaviour
{
    [SerializeField] Transform holdPlace;
    public GameObject holdingObj;
    public UnityAction<GameObject> OnHold;

    public bool alreadyHold;

    public void Hold(GameObject go)
    {
        //go.transform.parent = holdPlace;

        //z 값을 조절 해 들고 있는 오브젝트가 먼저 선택될 수 있게
        //go.transform.localPosition = new Vector3(0f, 0f, -0.1f);

        if (go == null)
            return;

        Holder targetHolder = go.GetComponent<Holder>();

        if (targetHolder == null)
        {
            if (go.GetComponent<CompleteFood>() == null)//food stacker 가 holder 가 없는 그냥 김밥 아이템을 건네줌
                return;

            go.transform.parent = holdPlace;
            holdingObj = go;
            holdingObj.transform.localPosition = Vector3.zero;
            holdingObj.GetComponent<Collider2D>().enabled = false;

            OnHold?.Invoke(go);
        }
        else
        {
            if (targetHolder.holdingObj == null)
                return;

            holdingObj = targetHolder.Give();
            holdingObj.transform.parent = holdPlace;
            OnHold?.Invoke(targetHolder.holdingObj);
        }

        alreadyHold = true;
    }    

    public GameObject Give()
    {
        GameObject temp = holdingObj;

        alreadyHold = false;
        holdingObj = null;
        
        return temp;
    }
}
