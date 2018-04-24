using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour {
    #region Attributes
	#endregion
	
	#region Properties
	
	#endregion

	#region Event Functions
	// Awake is called before Start
	private void Awake ()
	{
	
	}

	// Use this for initialization
	private void Start () 
	{
		
	}
	
	// Update is called once per frame
	private void Update () 
	{
		
	}

    private void OnTriggerEnter (Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //LevelGenerator.Instance.regenLevel();
        }
    }
    #endregion

    #region Methods

    #endregion

    #region Coroutines

    #endregion
}
