using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateRabbit : MonoBehaviour {

    public GameObject RabbitA;
    public GameObject RabbitB;
    public GameObject Rabbit;

    public int Rabbittype = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject rabbit = null;
            if (Rabbittype == 0)
            {
                rabbit = Instantiate(Rabbit) as GameObject;
            }
            else if (Rabbittype == 1)
            {
                rabbit = Instantiate(RabbitA) as GameObject;
            }
            else if (Rabbittype == 2)
            {
                rabbit = Instantiate(RabbitB) as GameObject;
            }
            Vector3 rabbitPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            rabbitPosition.z = 0f;
            rabbit.transform.position = rabbitPosition;
        }
    }
}
