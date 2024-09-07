using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class Holder : MonoBehaviour
{
    [SerializeField] Transform holdPlace;
    public GameObject holdingObj;

    public void Hold(GameObject go)
    {
        go.transform.parent = holdPlace;
        go.transform.localPosition = Vector3.zero;

        holdingObj = go;
    }    
}
