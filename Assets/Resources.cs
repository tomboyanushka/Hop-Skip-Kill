using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resources : MonoBehaviour {
    bool collideEnemywall = false;
    bool collideResourcewall = false;
    int resourceA = 0;
    int resourceB = 0;
    int resourceC = 0;
    private GameObject player;
    private GameObject enemy;
    RabbitProperties thisRabbitprop;
    float timer;
    // Use this for initialization
    void Start () {
        //enemy = gameObject.GetComponent<Enemy>();
        thisRabbitprop = gameObject.GetComponent < RabbitProperties > ();
        timer += Time.deltaTime;

	}
	
	// Update is called once per frame
	void Update () {
        //Debug.Log("Started");
        if (Input.GetKeyDown(KeyCode.Q) && resourceA >0)          
        {

                resourceA--;
                Debug.Log("resource A after using: " + resourceA);
            if(timer < 3)
            {
                //function to multiply mining speed by 2.

            }
        }
        
        if(collideResourcewall)
        {
            Debug.Log("resource A : " + resourceA);
            resourceA++;
            collideEnemywall = false;
        }
        
        
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Colliding");
        switch (other.gameObject.tag)
        {
            case "ResourceWall":
                collideResourcewall = true;
                break;
            case "EnemyWallTop":
                collideEnemywall = true;
                break;
            case "PlayerWallTop":
                break;
            case "EnemyWallBottom":
                break;
            
        }
        if (thisRabbitprop.RabbitType == 0)
        {
            if(other.gameObject.GetComponent<RabbitProperties>().RabbitType == 1) // attack enemy
            {
                other.gameObject.GetComponent<RabbitProperties>().rabbitHealth -= 5;
            }
        }
        if(thisRabbitprop.RabbitType == 1)//attack player
        {
            if(other.gameObject.GetComponent<RabbitProperties>().RabbitType == 0)
            {
                other.gameObject.GetComponent<RabbitProperties>().rabbitHealth -= 5;
            }
        }
    }
}
