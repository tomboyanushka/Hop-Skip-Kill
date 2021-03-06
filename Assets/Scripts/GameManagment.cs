﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagment : MonoBehaviour
{

    public int PlayerResourceA;
    public int EnemyResourceA;
    public int PlayerResourceB;
    public int EnemyResourceB;
    public int PlayerResourceC; //C for stolen resources.
    public int EnemyResourceC;
    bool collideResourcewall = false;
    public int playerResourceSum;
    public int enemyResourceSum;
    float targetTime= 5;

    public List<GameObject> playerRabbits;
    public List<GameObject> enemyRabbits;

    RabbitProperties thisRabbitprop; //accessing the Rabbitproperties script.

    int goal, type, res;

    bool playerStartTimer = false;
    bool enemyStartTimer = false;
    // Use this for initialization
    void Start () {
        PlayerResourceA = 0;
        PlayerResourceB = 0;
        PlayerResourceC = 0;
        playerResourceSum = 0;
        EnemyResourceA = 0;
        EnemyResourceB = 0;
        EnemyResourceC = 0;
        enemyResourceSum = 0;
       
       
        goal = 50;
        thisRabbitprop = gameObject.GetComponent<RabbitProperties>();

        playerRabbits = new List<GameObject>();
        enemyRabbits = new List<GameObject>();
    }

    // Update is called once per frame
    void Update() {
        
        playerResourceSum = PlayerResourceA + PlayerResourceB + PlayerResourceC;
        enemyResourceSum = EnemyResourceA + EnemyResourceB + EnemyResourceC;


        //useResources(type,res);
        if (playerResourceSum >= goal)
        {
            Debug.Log("Player Win!");
        }

        if (enemyResourceSum >= goal)
        {
            Debug.Log("Enenmy Win!");
        }
        if (collideResourcewall)
        {
            
            PlayerResourceA++;
            collideResourcewall = false;
        }

        if (playerStartTimer)
        {
            targetTime -= Time.deltaTime;
            if (targetTime <= 0.0f)
            {
                foreach(GameObject g in playerRabbits)
                {
                    g.GetComponent<AlternativeAI>().miningSpeed = 1;
                }
                playerStartTimer = false;
            }
        }

        if (enemyStartTimer)
        {
            targetTime -= Time.deltaTime;
            if (targetTime <= 0.0f)
            {
                foreach (GameObject g in enemyRabbits)
                {
                    g.GetComponent<AlternativeAI>().miningSpeed = 1;
                }
                enemyStartTimer = false;
            }
        }
    }
 
   void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.gameObject.tag)
        {
            case "ResourceWall":
                collideResourcewall = true;
                break;
        }
        if (thisRabbitprop.RabbitType == 0)
        {
            if (other.gameObject.GetComponent<RabbitProperties>().RabbitType == 1) // attack enemy
            {
                other.gameObject.GetComponent<RabbitProperties>().health -= 5;

            }
        }
        if (thisRabbitprop.RabbitType == 1)//attack player
        {
            if (other.gameObject.GetComponent<RabbitProperties>().RabbitType == 0)
            {
                other.gameObject.GetComponent<RabbitProperties>().health -= 5;
            }
        }
    }
   public void useResources(int type, int res)    //type : player or AI?
    {                                              //res is what resource they are using [0 = player, 1 = AI]
        if(type == 0 )
        {
            if(res == 0)
            {
				if (PlayerResourceA >= 1)
				{
					PlayerResourceA--;
					foreach (GameObject g in playerRabbits)
					{
						g.GetComponent<AlternativeAI>().miningSpeed = 2;
					}
					//rAI.miningSpeed = 2;
					playerStartTimer = true;
				}
            }
            else
            {
				if(PlayerResourceB >=1)
					PlayerResourceB--;
            }
        }
        else if (type == 1)
        {
            if (res==0)
            {
				if (EnemyResourceA >= 1)
				{
					EnemyResourceA--;
					foreach (GameObject g in enemyRabbits)
					{
						g.GetComponent<AlternativeAI>().miningSpeed = 2;
					}
					//rAI.miningSpeed = 2;
					enemyStartTimer = true;
				}
            }
            else
            {
				if (EnemyResourceB >= 1)
				{
					EnemyResourceB--;
				}
            }
            
        }
    }
    
}
