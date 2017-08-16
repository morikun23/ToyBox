using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearRotate : MonoBehaviour {

    [Range(-3, 3)]
    public float xx, yy, zz;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(xx, yy, zz);
	}
}
