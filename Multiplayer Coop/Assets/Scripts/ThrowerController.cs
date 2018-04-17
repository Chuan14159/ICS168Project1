using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowerController : MonoBehaviour {

    // Use this for initialization
    public float force;
    private bool canThrow;
    public Camera PlayerCam;
    public GameObject throwable_Object;
    public GameObject Player;
    public int constant;
	void Start () {
        force = 1f;
        canThrow = true;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            if (canThrow)
            {
                Throw();
            }
        }
	}

    protected void Throw()
    {
        GameObject throwO = Instantiate(throwable_Object, Player.transform.position + Vector3.up, Player.transform.rotation);
        throwO.GetComponent<Rigidbody>().AddForce
        (
            (throwO.transform.forward * Mathf.Cos(Mathf.Deg2Rad * force) + 
            throwO.transform.up * Mathf.Sin(Mathf.Deg2Rad * force)) * constant,
            ForceMode.Impulse
        );
    }
}
