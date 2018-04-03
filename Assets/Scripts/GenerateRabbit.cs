using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateRabbit : MonoBehaviour {

    public GameObject RabbitA;
    public GameObject RabbitB;
    public GameObject RabbitNormal;

	GameObject Rabbit;

	GameManagment gmMan;
    public int Rabbittype = 0;

	// Use this for initialization
	void Start () {
		Rabbit = null;
		gmMan = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManagment>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject rabbit = null;
            if (Rabbittype == 0)
            {
				if (Rabbit != null)
				{
					rabbit = Instantiate(Rabbit) as GameObject;
					Vector3 rabbitPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
					rabbitPosition.z = 0f;
					rabbit.transform.position = rabbitPosition;
				}
			}
			//else if (Rabbittype == 1)
			//{
			//    rabbit = Instantiate(RabbitA) as GameObject;
			//}
			//else if (Rabbittype == 2)
			//{
			//    rabbit = Instantiate(RabbitB) as GameObject;
			//}
			

            gmMan.playerRabbits.Add(rabbit);
        }
    }

	public void SelectNormalRabbit()
	{
		Rabbit = RabbitNormal;
	}
	public void SelectRabbitA()
	{
		Debug.Log("Increase Mining");
		Rabbit = RabbitA;
		gmMan.useResources(0, 0);
	}
	public void SelectRabbitB()
	{
		Rabbit = RabbitB;
	}
}
