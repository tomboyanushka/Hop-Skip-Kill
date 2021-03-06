﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlternativeAI : MonoBehaviour {

	enum RabbitState { Attack, Mine, Move };        // values 0, 1 and 2 act as priority values with 0 being the highest
	List<int> queuedLayers = new List<int>();       // priority order can be changed as needed
	Rigidbody2D rBody;
	RabbitProperties properties;
    [SerializeField]
	RabbitState currentState;
	GameObject target;
	float cooldown;
	public int miningSpeed = 1;
	bool attacking = false;
	GameManagment gm;
    float yPos = float.PositiveInfinity;

	// Use this for initialization
	void Start()
	{
		rBody = GetComponent<Rigidbody2D>();
		properties = GetComponent<RabbitProperties>();
		queuedLayers.Add((int)RabbitState.Move);
		currentState = RabbitState.Move;
		cooldown = 1f;
		gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManagment>();
	}

	// Update is called once per frame
	void Update()
	{
        if(yPos != float.PositiveInfinity)
        {
            transform.position = new Vector3(transform.position.x, yPos, 0);
        }
		if (properties.health <= 0)
		{
			Destroy(gameObject);
		}

        if (currentState == RabbitState.Move)
		{
			Move();
		}
		else if (currentState == RabbitState.Attack)
		{
			Attack();
		}
		else if (currentState == RabbitState.Mine)
		{
			Mine();
		}
	}

	public void Move()
	{
		//if (!attacking)
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
	}

	public void Mine()
	{
		cooldown -= Time.deltaTime * miningSpeed;
		if (cooldown <= 0)
		{
			// If we're increasing player resources
			if (GetComponent<RabbitProperties>().RabbitType == 0)
			{
				// If Resource A
				if (target.GetComponent<Resource>().currentResource == Resource.ResourceType.A)
				{
					gm.PlayerResourceA++;
				}
				// If Resource B
				else
				{
					gm.PlayerResourceB++;
				}
			}
			// If we're increasing AI resources
			else
			{
				// If Resource A
				if (target.GetComponent<Resource>().currentResource == Resource.ResourceType.A)
				{
					gm.EnemyResourceA++;
				}
				// If Resource B
				else
				{
					gm.EnemyResourceB++;
				}
			}
			cooldown = 3;
		}
	}

	public void Attack()
	{
		attacking = true;
		cooldown -= Time.deltaTime;
		if (cooldown <= 0)
		{
			if (target != null)
			{
				if (target.tag == "Rabbit")
				{
					target.GetComponent<RabbitProperties>().health--;
					if (target.GetComponent<RabbitProperties>().health <= 0)
					{
						currentState = RabbitState.Move;
						attacking = false;
					}
				}
				else
				{
					// Decrement enemy resources
				}
				cooldown = Random.Range(1, 2);
			}
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		StateChangeLogic(collision);
        if(collision.tag == "Floor")
        {
            yPos = transform.position.y;
        }
	}

	private void OnTriggerStay2D(Collider2D collision)
	{
		StateChangeLogic(collision);
	}

    private void OnTriggerExit2D(Collider2D collision)
    {
        StateChangeLogic(collision);
    }

	void SetState(bool move,bool mine,bool attack)
	{
		if (move)
		{
			currentState = RabbitState.Move;
		}
		else if (mine)
		{
			currentState = RabbitState.Mine;
		}
		else if (attack)
		{
			currentState = RabbitState.Attack;
		}
    }

	void StateChangeLogic(Collider2D collision)
	{
        bool stateSet = false;
		// Attacking wall
		if (collision.tag == "Wall")
		{
			SetState(false, false, true);
			queuedLayers.Add((int)RabbitState.Attack);
			target = collision.gameObject;
            stateSet = true;
		}
		// Attacking rabbit
		else if (collision.tag == "Rabbit")
		{
			// If it's not a rabbit from the same side, attack it
			if (collision.gameObject.GetComponent<RabbitProperties>().RabbitType != GetComponent<RabbitProperties>().RabbitType)
            {
                SetState(false, false, true);
                attacking = true;
				queuedLayers.Add((int)RabbitState.Attack);
				target = collision.gameObject;
                stateSet = true;
            }
        }
		// Mining
		else if (collision.tag == "Resource")
		{
            int numRabbits = collision.gameObject.GetComponent<Resource>().playerRabbits.Count + collision.gameObject.GetComponent<Resource>().enemyRabbits.Count;
            int randNum = Random.Range(2, 10);
            if (numRabbits < randNum)
            {
                SetState(false, true, false);
                queuedLayers.Add((int)RabbitState.Mine);
                target = collision.gameObject;
                stateSet = true;
            }
        }

        if (!stateSet)
        {
            if (collision.tag == "Floor")
            {
                SetState(true, false, false);
                properties.onFloor = true;
                rBody.gravityScale = 0;
            }
            else
            {
                //SetState(false, false, true);
                properties.onFloor = false;
                rBody.gravityScale = 1;
                GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionY;
            }
        }
    }
}
