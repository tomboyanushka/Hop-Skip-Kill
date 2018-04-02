using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabbitAI : MonoBehaviour {

	enum RabbitState { Attack, Mine, Move };		// values 0, 1 and 2 act as priority values with 0 being the highest
	List<int> queuedLayers = new List<int>();		// priority order can be changed as needed
	Rigidbody2D rBody;
	RabbitProperties properties;
	RabbitState currentState;

	// Use this for initialization
	void Start () {
		rBody = GetComponent<Rigidbody2D>();
		properties = GetComponent<RabbitProperties>();
		queuedLayers.Add((int)RabbitState.Move);
		currentState = RabbitState.Move;
	}
	
	// Update is called once per frame
	void Update () {
		try
		{
			if (queuedLayers == null || queuedLayers.Count == 0)
			{
				queuedLayers.Add((int)RabbitState.Move);		// default state
			}
			else
			{
				queuedLayers.Sort();                            // Ascending order sort, Sorts according to highest priority (0 = highest priority)
				if (queuedLayers[0] == (int)RabbitState.Move)	// index 0 will have lowest value after sort
				{
					// Call move function
					currentState = RabbitState.Move;
					Move();
					//print("Move");
				}
				else if (queuedLayers[0] == (int)RabbitState.Mine)
				{
					// Call mine function
					currentState = RabbitState.Mine;
					//Mine();
					print("Mine");
				}
				else if (queuedLayers[0] == (int)RabbitState.Attack)
				{
					// Call attack function
					currentState = RabbitState.Attack;
					//Attack();
					print("Attack");
				}
				queuedLayers.RemoveAt(0);						// Remove the highest priority action after execution
																// may need to change the position of this
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

