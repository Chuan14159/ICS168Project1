using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Field : MonoBehaviour {
    #region Attributes
    [SerializeField]
    private int length;             // The length of the field
    [SerializeField]
    private int width;              // The width of the field
    [SerializeField]
    private Vector2 heightRange;    // The range of heights of the cube
	#endregion
	
	#region Properties
	
	#endregion

	#region Event Functions
	// Awake is called before Start
	private void Awake ()
	{
		for (int i = 0; i < length; ++i)
        {
            for (int j = 0; j < length; ++j)
            {
                Vector3 scale = new Vector3(1, Random.Range(heightRange.x, heightRange.y), 1);
                Vector3 pos = new Vector3(i, scale.y / 2, j);
                GameObject g = Instantiate(Resources.Load<GameObject>("Prefabs/Cube"), pos, Quaternion.identity, transform);
                g.transform.localScale = scale;
            }
        }
	}

	// Use this for initialization
	private void Start () 
	{
		
	}
	
	// Update is called once per frame
	private void Update () 
	{
		
	}
	#endregion
	
	#region Methods
	
	#endregion
	
	#region Coroutines
	
	#endregion
}
