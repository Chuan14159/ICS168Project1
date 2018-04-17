using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCam : MonoBehaviour {

    #region Attributes
    private const float MIN_Y = 0.0f;
    private const float MAX_Y = 50.0f;

    public Transform lookAt; //player's Pos.
    private Transform camTransform;

    public float distance = 10.0f;
    private float currentX = 0.0f;
    private float sensitivityX = 4.6f;
    private float currentY = 0.0f;
    private float sensitivityY = 1.0f;
    #endregion

    private void Start()
    {
        camTransform = transform;
    }
    private void Update()
    {
        currentX += Input.GetAxis("Horizontal");
        currentY += Input.GetAxis("Mouse Y");
        Mathf.Clamp(currentY, MIN_Y, MAX_Y);
    }
    private void LateUpdate()
    {
        Vector3 dir = new Vector3(0,0,-distance);
        Quaternion rotation = Quaternion.Euler(-currentY, currentX * sensitivityX, 0);
        camTransform.position = lookAt.position + rotation * dir;
        camTransform.LookAt(lookAt.position);
    }
}
