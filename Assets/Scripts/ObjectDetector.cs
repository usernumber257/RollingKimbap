using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEditor;
using UnityEngine;

/// <summary>
/// ObjectLayer 와 상호작용하는 클래스로, Object 를 감지하고 플레이어가 오브젝트의 앞/뒤에 있는 것에 따라 오브젝트의 sortingOrder 를 조정함
/// </summary>
public class ObjectDetector : MonoBehaviour
{
    float radius = 0.2f;
    [SerializeField]float boundary = 0.4f;
    float distance = 0f;

    int objectLayerMask = (1 << 6);
    
    ObjectLayer objectLayer;


    private void Update()
    {
        headHit();
        BodyHit();
    }

    void headHit()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(new Vector2(transform.position.x, transform.position.y + boundary), radius, transform.up, distance, objectLayerMask);

        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i].collider != null)
            {
                objectLayer = hits[i].collider.gameObject.GetComponent<ObjectLayer>();

                if (objectLayer != null)
                    objectLayer.SetLayer(false);
            }
        }
    }

    void BodyHit()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(new Vector2(transform.position.x, transform.position.y - boundary), radius, transform.up, distance, objectLayerMask);

        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i].collider != null)
            {
                objectLayer = hits[i].collider.gameObject.GetComponent<ObjectLayer>();

                if (objectLayer != null)
                    objectLayer.SetLayer(true);
            }
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(new Vector3(transform.position.x, transform.position.y + boundary, 0), radius);
        Gizmos.DrawWireSphere(new Vector3(transform.position.x, transform.position.y - boundary, 0), radius);
    }
}
