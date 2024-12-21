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

    public void Hold(GameObject go)
    {
        go.transform.parent = holdPlace;

        //z 값을 조절 해 들고 있는 오브젝트가 먼저 선택될 수 있게
        go.transform.localPosition = new Vector3(0f, 0f, -0.1f);

        holdingObj = go;
        OnHold?.Invoke(go);
    }    
}
