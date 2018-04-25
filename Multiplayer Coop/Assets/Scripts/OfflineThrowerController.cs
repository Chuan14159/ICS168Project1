using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OfflineThrowerController : MonoBehaviour {

    // Use this for initialization
    #region Attributes
    private const float Max_Angle = 90.0f;
    private const float Min_Angle = 0.0f;
    public float Angle;
    public Camera PlayerCam;
    public GameObject throwable_Object;
    public GameObject Player;
    public GameObject Arrow;
    public int ThrowForce;
    private int AngleSensitivity = 50;
    public static OfflineThrowerController instance;
    #endregion

    void Start () {
        instance = this;
        Angle = 45;
        ThrowForce = 45;
        Arrow.transform.position = gameObject.transform.position + Vector3.up * 2;
        Arrow.transform.rotation = gameObject.transform.rotation;
	}
	
	// Update is called once per frame
	void Update () {
        SpawnThrowAimer();
        if (Input.GetMouseButtonDown(0) && OfflinePlayerTrigger.instance.isPickingPlayer && throwable_Object != null)
        {
            Throw();
        }  
	}

    private void SpawnThrowAimer()
    {
        //Debug.Log(Input.GetAxis("Mouse ScrollWheel"));
        Angle += -Input.GetAxis("Mouse ScrollWheel") * AngleSensitivity;
        Angle = Mathf.Clamp(Angle, Min_Angle, Max_Angle);
        Arrow.transform.position = gameObject.transform.position + Vector3.up * 2;
        Arrow.transform.rotation = gameObject.transform.rotation * Quaternion.Euler(-Angle, 0, 0);
    }
    private void Throw()
    {
        GameObject tempObject = throwable_Object;
        OfflinePlayerTrigger.instance.DeletePlayerFromList(tempObject);
        tempObject.transform.parent.GetComponent<Rigidbody>().AddForce
        (
                (tempObject.transform.forward * Mathf.Cos(Mathf.Deg2Rad * Angle) +
                tempObject.transform.up * Mathf.Sin(Mathf.Deg2Rad * Angle)) * ThrowForce,
                ForceMode.Impulse
        );
    }
}
