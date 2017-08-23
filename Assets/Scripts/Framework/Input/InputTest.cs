using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputTest : MobileInput {

	// Use this for initialization
	void Start () {
		base.Start ();
	}
	
	// Update is called once per frame
	void Update () {
		base.Update ();
	}

	public override void RightR(float dist){
		Debug.Log (dist);
		transform.eulerAngles += new Vector3 (0,0,dist / 10);
	}

	public override void LeftR (float dist){
		Debug.Log (dist);
		transform.eulerAngles += new Vector3 (0,0,dist / 10);
	}

}
