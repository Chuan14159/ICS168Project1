using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCam : MonoBehaviour {

    #region Attributes
    private const float MIN_X = 0.0f;
    private const float MAX_X = 50.0f;

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
        if (Input.GetMouseButton(1))
        {
            currentY += Input.GetAxis("Mouse X") * sensitivityX;
            currentX += -Input.GetAxis("Mouse Y") * sensitivityX;
            currentX = Mathf.Clamp(currentX, MIN_X, MAX_X);
        }
    }

    private void LateUpdate()
    {
        Vector3 dir = new Vector3(0,0,-distance);
        Quaternion rotation = Quaternion.Euler(currentX, currentY, 0);
        camTransform.position = lookAt.position + rotation * dir;
        camTransform.LookAt(lookAt.position);
    }
}
