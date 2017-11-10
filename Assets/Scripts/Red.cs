using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Red : MonoBehaviour {


	public AudioSource aud;
	public float Startspeed = 10;
	float speed = 0;
	float friction = 0;
	public Rigidbody rb;
	public float gravity = 9.8f;
	float moment;
	public float mew = 0.2f;
	public float restitution = 0.5f;
	float coeffFriction = 0.0f;
	public Vector3 direction;
	int i = 1;

	// Use this for initialization
	void Start () {
		aud = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
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
		if (col.gameObject.name == "Wall3" || col.gameObject.name == "Wall4")
		{
			ContactPoint contact = col.contacts[0];
			direction = 2 * contact.normal + direction;
			moment = restitution * moment;
			


		}
		else if (col.gameObject.name == "Wall1" || col.gameObject.name == "Wall2")
		{
			ContactPoint contact = col.contacts[0];
			direction = 2 * direction + 3 * contact.normal;
			moment = restitution * moment;
			

		}
		else if (col.gameObject.name == "Cue" || col.gameObject.name == "Blue" ||  col.gameObject.name == "Red")
		{
			if (i == 1)
			{
				rb.velocity = direction * Cue.speed;
				coeffFriction = mew;
				moment = rb.mass * Cue.speed;
			}
			ContactPoint contact = col.contacts[0];
			direction = contact.normal;
			i = i + 1;
			aud.Play();
		}
		else if (col.gameObject.name == "pocket1" || col.gameObject.name == "pocket2" || col.gameObject.name == "pocket3" || col.gameObject.name == "pocket4" || col.gameObject.name == "pocket5" || col.gameObject.name == "pocket6")
		{
			Destroy(this.gameObject);
		}
	}
}
