using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cue : MonoBehaviour {


	public AudioSource aud;
	public static float Startspeed = 10.0f;
	public static float speed=0;
	float friction =0;
	public Rigidbody rb;
	public float gravity;
	float moment=0;
	public float mew = 0.2f;
	float coeffFriction = 0.0f;
	public Vector3 direction;
	public float restiution = 0.5f;
	// Use this for initialization

	void Start () {
		aud = GetComponent<AudioSource>();
		moment = rb.mass * speed;
	}
	
	void Update () {
		if(Input.GetKeyDown(KeyCode.Space))
		{
			rb.velocity = direction * Startspeed;
			coeffFriction = mew;
			moment = rb.mass * Startspeed;
			aud.Play();
		}

		friction = coeffFriction * rb.mass * gravity * Time.deltaTime;
		moment = (moment - friction);

		speed = (moment / rb.mass);
		
		if (speed <= 0.0f)
		{
			speed = 0.0f;
		}
		rb.velocity = (direction * speed);

	}


	void OnCollisionEnter(Collision col)
	{
		if(col.gameObject.name == "Wall3" || col.gameObject.name == "Wall4")
		{
			ContactPoint contact = col.contacts[0];
			direction = (2*contact.normal+direction).normalized;
			moment = restiution * moment;
			
					
		}
		else if(col.gameObject.name == "Wall1" || col.gameObject.name == "Wall2")
		{
			ContactPoint contact = col.contacts[0];
			direction =  (2*direction+3*contact.normal).normalized;
			moment = restiution * moment;
			
		}
		else if(col.gameObject.name == "Red" || col.gameObject.name == "Blue")
		{	

			ContactPoint contact = col.contacts[0];
			var angle = Vector3.Angle(direction, contact.normal);
			direction = direction*Mathf.Cos(angle);
			moment = 0.98f * moment;
			aud.Play();
		}
		else if (col.gameObject.name == "pocket1" || col.gameObject.name == "pocket2" || col.gameObject.name == "pocket3" || col.gameObject.name == "pocket4" || col.gameObject.name == "pocket5" || col.gameObject.name == "pocket6")
		{
			Destroy(this.gameObject);
		}
	}
}
