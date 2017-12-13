using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour {

    public float ballInitialVelosity = 600f;
    private Rigidbody rb;
    private bool ballInPlay;

	void Awake ()
    {
        rb = GetComponent<Rigidbody>();
	}
	
	void Update ()
    {
		if(Input.GetButtonDown("Fire1") && (ballInPlay == false))
        {
            transform.parent = null;
            ballInPlay = true;
            rb.isKinematic = false;
            rb.AddForce(new Vector3(ballInitialVelosity, ballInitialVelosity, 0));
        }
	}
}
