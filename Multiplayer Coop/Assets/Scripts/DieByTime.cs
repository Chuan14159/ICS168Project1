using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieByTime : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Destroy(gameObject, 4.0f);
	}
}
