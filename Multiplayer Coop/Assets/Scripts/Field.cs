using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Field : NetworkBehaviour {
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
        /*for (int i = 0; i < length * 3; ++i)
        {
            for (int j = 0; j < width * 3; ++j)
            {
                Vector3 scale = new Vector3(1, heightRange.y, 1);
                Vector3 pos = new Vector3(i, scale.y / 2, j);
                GameObject g = Instantiate(Resources.Load<GameObject>("Prefabs/Cube"), pos, Quaternion.identity, transform);
                g.transform.localScale = scale;
            }
        }
        
        for (int i = 0; i < length * 2; ++i)
        {
            for (int j = 0; j < width * 2; ++j)
            {
                Vector3 scale = new Vector3(1, Random.Range(heightRange.x, heightRange.y) + heightRange.y, 1);
                Vector3 pos = new Vector3(i+length/2, scale.y / 2, j+width/2);
                GameObject g = Instantiate(Resources.Load<GameObject>("Prefabs/Cube2"), pos, Quaternion.identity, transform);
                g.transform.localScale = scale;
            }
        }

        for (int i = 0; i < length; ++i)
        {
            for (int j = 0; j < width; ++j)
            {
                Vector3 scale = new Vector3(1, Random.Range(heightRange.x, heightRange.y) * 2 + heightRange.y * 2, 1);
                Vector3 pos = new Vector3(i + length, scale.y / 2, j + width);
                GameObject g = Instantiate(Resources.Load<GameObject>("Prefabs/Cube3"), pos, Quaternion.identity, transform);
                g.transform.localScale = scale;
            }
        }*/

        for (int i = 0; i < length; ++i)
        {
            for (int j = 0; j < width; ++j)
            {
                Vector3 scale = new Vector3(1, Random.Range(heightRange.x, heightRange.y), 1);
                Vector3 pos = new Vector3(i, scale.y / 2, j);
                GameObject g = Instantiate(Resources.Load<GameObject>("Prefabs/Cube"), transform);
                g.transform.localScale = scale;
                g.transform.localPosition = pos;
                if (j == 4 && i == 4)
                {
                    GameObject e = Instantiate(Resources.Load<GameObject>("Prefabs/Goal"), transform);
                    e.GetComponent<Goal>().MoveToPlatform(g.transform);
                }
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
