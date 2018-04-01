using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabbitAI : MonoBehaviour {

	enum RabbitState { Move, Mine, Attack};
	static int priorityValue = 0;				// default value 0 is highest priority
	List<int> queuedLayers = new List<int>();
	Rigidbody2D rBody;
	RabbitProperties properties;
	// Use this for initialization
	void Start () {
		rBody = GetComponent<Rigidbody2D>();
		properties = GetComponent<RabbitProperties>();
		queuedLayers.Add((int)RabbitState.Move);
	}
	
	// Update is called once per frame
	void Update () {
		try
		{
			if (queuedLayers == null || queuedLayers.Count == 0)
			{
				queuedLayers.Add(0);
			}
			else
			{
				queuedLayers.Sort();
				if (queuedLayers[0] == (int)RabbitState.Move)
				{
					// Call move function
					Move();
				}
				else if (queuedLayers[0] == (int)RabbitState.Mine)
				{
					// Call mine function
					//Mine();
				}
				else if (queuedLayers[0] == (int)RabbitState.Attack)
				{
					// Call attack function
					//Attack();
				}
			}
		}
		catch (System.IndexOutOfRangeException e)
		{
			throw new System.ArgumentOutOfRangeException("index parameter is out of range.", e);
		}
	}

	public void Move()
	{
		if (properties.onFloor)
		{
			transform.position += new Vector3(properties.speed * Time.deltaTime, 0, 0);
		}
		else if (!properties.onFloor)
		{
			transform.position += new Vector3(0, -properties.gravity * Time.deltaTime, 0);
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.tag == "Floor")
		{
			properties.onFloor = true;
			rBody.gravityScale = 0;
		}
		else
		{
			properties.onFloor = false;
			rBody.gravityScale = 1;
		}
	}
}

