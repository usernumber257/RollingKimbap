using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class Server : MonoBehaviour
{
    [SerializeField] Transform holdPlace;
    public GameObject holdingObj;

    public void Hold(GameObject go)
    {
        go.transform.parent = holdPlace;
        go.transform.localPosition = Vector3.zero;

        holdingObj = go;
    }    

    public GameObject Serve()
    {
        return holdingObj;
    }
}
