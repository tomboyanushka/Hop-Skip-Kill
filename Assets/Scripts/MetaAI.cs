﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetaAI : MonoBehaviour
{
    public int rabbitCount = 10;
    public int resourceCount;
    public GameObject spawnObject;
    public GameObject spawnPT;
    GameManagment manager;
    int res;
    float generateTimer = 1.5f;
  
    

    public GameObject spawnTop, spawnMiddle, spawnBottom;

    private Bounds bounds;

    bool isRabbitSpawned = false;



    private void Start()
    {
        manager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManagment>();
    }

    private void Update()
    {
        resourceCount = manager.enemyResourceSum;
        res = Random.Range(0, 1);
        generateTimer -= Time.deltaTime;
        if (generateTimer <= 0f && rabbitCount > 0)
        {
            spawnRabbit();
            generateTimer = 1.5f;
        }
        if (resourceCount >= 0)
        {
            if (res == 0)
            {
                manager.GetComponent<GameManagment>().useResources(1, 0);
            }
            else if (res == 1)
            {
                manager.GetComponent<GameManagment>().useResources(1, 1);
            }
        }
        
    }

    public void spawnRabbit()
    {
        // Randomly assign spawning location
        int r = Random.Range(0, 3);
        switch (r)
        {
            case 0:
                spawnPT = spawnTop;
                break;
            case 1:
                spawnPT = spawnMiddle;
                break;
            default:
                spawnPT = spawnBottom;
                break;
        }

        GameObject newRabbit = Instantiate(spawnObject, spawnPT.transform.position, new Quaternion()) as GameObject;
        newRabbit.GetComponent<RabbitProperties>().RabbitType = 1;
        newRabbit.GetComponent<RabbitProperties>().speed = -(newRabbit.GetComponent<RabbitProperties>().speed);
        rabbitCount--;
        isRabbitSpawned = true;

        newRabbit.GetComponentInChildren<SpriteRenderer>().flipX = true;

        manager.enemyRabbits.Add(newRabbit);
    }




}
