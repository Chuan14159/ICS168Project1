using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils {
	#region Attributes
	
	#endregion
	
	#region Properties
	
	#endregion
	
	#region Methods
	// Returns a random color
    public static Color RandomColor (bool useAlpha)
    {
        float r = Random.Range(0f, 1f);
        float g = Random.Range(0f, 1f);
        float b = Random.Range(0f, 1f);
        float a = 1f;
        if (useAlpha)
        {
            a = Random.Range(0f, 1f);
        }
        return new Color(r, g, b, a);
    }

    // Changes the color of a material to a new one
    public static void ChangeColor (this MeshRenderer mr, Color newColor)
    {
        mr.material = new Material(mr.material);
        mr.material.color = newColor;
    }
	#endregion
	
	#region Coroutines
	
	#endregion
}
