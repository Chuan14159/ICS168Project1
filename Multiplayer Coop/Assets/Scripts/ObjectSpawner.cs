using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour {


    public List<GameObject> objects;

    private List<GameObject> spawnSet;



	// Use this for initialization
	void Start () {
        spawnSet = new List<GameObject>();
        for (int i = 0; i < 5; i++){
            int j = Random.Range(0, objects.Count);
            spawnSet.Add(objects[j]);
        }
	}
	
    public void spawnItems(){
        StartCoroutine(Spawn());
    }

    IEnumerator Spawn(){
        yield return new WaitForSeconds(1);
        foreach (GameObject o in spawnSet)
        {
            Instantiate(o, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(0.5f);
        }
    }
}
