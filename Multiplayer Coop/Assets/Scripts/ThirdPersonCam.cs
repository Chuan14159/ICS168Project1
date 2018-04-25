using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCam : MonoBehaviour {

    #region Attributes
    private const float MIN_Y = -20f;
    private const float MAX_Y = 65f;

    public Transform lookAt; //player's Pos.
    private Transform camTransform;

    public float distance = 1.0f;
    private float currentX = 0.0f;
    private float sensitivityX = 3.0f;
    private float currentY = 0.0f;
    private float sensitivityY = 4.7f;
    #endregion

    private void Start()
    {
        camTransform = transform;
    }

    private void Update()
    {
        //currentY += Input.GetAxis("Mouse X") * sensitivityX;
        currentY += -Input.GetAxis("Mouse Y") * sensitivityX;
        currentY= Mathf.Clamp(currentX, MIN_Y, MAX_Y);
    }

    private void LateUpdate()
    {
        Vector3 dir = new Vector3(0,0,-distance);
        Quaternion rotation = Quaternion.Euler(currentY, 0, 0);
        camTransform.position = lookAt.position + rotation * dir;
        camTransform.LookAt(lookAt.position);
    }
}
