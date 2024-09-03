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
    float boundary = 0.3f;
    float distance = 0f;

    int objectLayerMask = (1 << 6);

    private void Update()
    {
        headHit();
        BodyHit();
    }

    void headHit()
    {
        RaycastHit2D hit = Physics2D.CircleCast(new Vector2(transform.position.x, transform.position.y + boundary), radius, transform.up, distance, objectLayerMask);

        if (hit.collider != null)
            hit.collider.gameObject.GetComponent<ObjectLayer>().SetLayer(false);
    }

    void BodyHit()
    {
        RaycastHit2D hit = Physics2D.CircleCast(new Vector2(transform.position.x, transform.position.y - boundary), radius, transform.up, distance, objectLayerMask);

        if (hit.collider != null)
            hit.collider.gameObject.GetComponent<ObjectLayer>().SetLayer(true);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(new Vector3(transform.position.x, transform.position.y + boundary, 0), radius);
        Gizmos.DrawWireSphere(new Vector3(transform.position.x, transform.position.y - boundary, 0), radius);
    }
}
