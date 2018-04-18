using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowerController : MonoBehaviour {

    // Use this for initialization
    #region Attributes
    private const float Max_Angle = 90.0f;
    private const float Min_Angle = 0.0f;
    public float Angle;
    private bool canThrow;
    public Camera PlayerCam;
    public GameObject throwable_Object;
    public GameObject Player;
    public GameObject Arrow;
    public int ThrowForce; 
    #endregion

    void Start () {
        Angle = 45;
        ThrowForce = 10;
        Arrow.transform.position = gameObject.transform.position + Vector3.up * 2;
        Arrow.transform.rotation = gameObject.transform.rotation;
        canThrow = true;
	}
	
	// Update is called once per frame
	void Update () {
        Angle += -Input.GetAxis("Mouse ScrollWheel") * 3;
        Angle = Mathf.Clamp(Angle, Min_Angle, Max_Angle);
        Arrow.transform.position = gameObject.transform.position + Vector3.up * 2;
        Arrow.transform.rotation = gameObject.transform.rotation * Quaternion.Euler(-Angle,0,0);
        if (Input.GetMouseButtonDown(0))
        {
            if (canThrow)
            {
                Throw();
            }
        }
	}

    private void SpawnThrowAngle()
    {

    }

    private void Throw()
    {
        GameObject throwO = Instantiate(throwable_Object, Player.transform.position + Vector3.up * 2, Player.transform.rotation);
        throwO.GetComponent<Rigidbody>().AddForce
        (
            (throwO.transform.forward * Mathf.Cos(Mathf.Deg2Rad * Angle) + 
            throwO.transform.up * Mathf.Sin(Mathf.Deg2Rad * Angle)) * ThrowForce,
            ForceMode.Impulse
        );
    }
}
