using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabbitAI : MonoBehaviour {

	enum RabbitState { Attack, Mine, Move };		// values 0, 1 and 2 act as priority values with 0 being the highest
	List<int> queuedLayers = new List<int>();		// priority order can be changed as needed
	Rigidbody2D rBody;
	RabbitProperties properties;
	RabbitState currentState;
    GameObject target;
    float cooldown;
    public int miningSpeed = 1;
    GameManagment gm;

	// Use this for initialization
	void Start () {
		rBody = GetComponent<Rigidbody2D>();
		properties = GetComponent<RabbitProperties>();
		queuedLayers.Add((int)RabbitState.Move);
		currentState = RabbitState.Move;
        cooldown = 1f;
        gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManagment>();
        //Debug.Log(gm);
        Debug.Log("Reminder: 0 = Attack, 1 = Mine, 2 = Move");
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
					print("Move");
				}
				else if (queuedLayers[0] == (int)RabbitState.Mine)
				{
					// Call mine function
					currentState = RabbitState.Mine;
					Mine();
					print("Mine");
				}
				else if (queuedLayers[0] == (int)RabbitState.Attack)
				{
					// Call attack function
					currentState = RabbitState.Attack;
					Attack();
					print("Attack");
                }
                queuedLayers.RemoveAt(0);                       // Remove the highest priority action after execution
                                                                // may need to change the position of this
            }
        }
		catch (System.IndexOutOfRangeException e)
		{
			throw new System.ArgumentOutOfRangeException("index parameter is out of range.", e);
		}
        if(GetComponent<RabbitProperties>().health <= 0)
        {
            Destroy(this.gameObject);
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

    public void Mine()
    {
        cooldown -= Time.deltaTime * miningSpeed;
        if(cooldown <= 0)
        {
            // If we're increasing player resources
            if (GetComponent<RabbitProperties>().RabbitType == 0)
            {
                // If Resource A
                if(target.GetComponent<Resource>().currentResource == Resource.ResourceType.A)
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
        cooldown -= Time.deltaTime;
        if(cooldown <= 0)
        {
            if(target.tag == "Rabbit")
            {
                target.GetComponent<RabbitProperties>().health--;
            }
            else
            {
                // Decrement enemy resources
            }
            cooldown = 1;
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
            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionY;
		}


        // Attacking wall
        if (collision.tag == "Wall")
        {
            queuedLayers.Add((int)RabbitState.Attack);
            target = collision.gameObject;
        }
        // Attacking rabbit
        else if (collision.tag == "Rabbit")
        {
            queuedLayers.Clear();
            // If it's not a rabbit from the same side, attack it
            if (collision.gameObject.GetComponent<RabbitProperties>().RabbitType != GetComponent<RabbitProperties>().RabbitType)
            {
                queuedLayers.Add((int)RabbitState.Attack);
                target = collision.gameObject;
            }
        }
        // Mining
        else if (collision.tag == "Resource")
        {
            queuedLayers.Add((int)RabbitState.Mine);
            target = collision.gameObject;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Floor")
        {
            properties.onFloor = true;
            rBody.gravityScale = 0;
        }
        else
        {
            properties.onFloor = false;
            rBody.gravityScale = 1;
            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionY;
        }


        // Attacking wall
        if (collision.tag == "Wall")
        {
            queuedLayers.Add((int)RabbitState.Attack);
            target = collision.gameObject;
        }
        // Attacking rabbit
        else if (collision.tag == "Rabbit")
        {
            queuedLayers.Clear();
            // If it's not a rabbit from the same side, attack it
            if (collision.gameObject.GetComponent<RabbitProperties>().RabbitType != GetComponent<RabbitProperties>().RabbitType)
            {
                queuedLayers.Add((int)RabbitState.Attack);
                target = collision.gameObject;
            }
        }
        // Mining
        else if (collision.tag == "Resource")
        {
            queuedLayers.Add((int)RabbitState.Mine);
            target = collision.gameObject;
        }
    }
}