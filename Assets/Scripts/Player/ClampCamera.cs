using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace User257
{
    public class ClampCamera : MonoBehaviour
    {
        [SerializeField] Transform player;

        [SerializeField] float startPosX;
        [SerializeField] float endPosX;
        
        [SerializeField] float startPosY;
        [SerializeField] float endPosY;

        private void LateUpdate()
        {
            transform.position = new Vector3(Mathf.Clamp(player.position.x, startPosX, endPosX), Mathf.Clamp(player.position.y, startPosY, endPosY), transform.position.z);
        }
    }
}
