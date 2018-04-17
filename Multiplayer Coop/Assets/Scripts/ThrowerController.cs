using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowerController : MonoBehaviour {

    // Use this for initialization
    public float force;
    private bool canThrow;
    public Camera PlayerCam;

	void Start () {
        force = 10.0f;
        canThrow = false;
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

    }
}
