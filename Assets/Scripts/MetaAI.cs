using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetaAI : MonoBehaviour
{
    public int rabbitCount = 10;
    public int resourceCount = 10;
    public GameObject spawnObject;
    public GameObject spawnPT;
    GameManagment manager;
    int res;
    //int type = 1;
    

    public GameObject spawnTop, spawnMiddle, spawnBottom;

    private Bounds bounds;

    bool isRabbitSpawned = false;



    private void Start()
    {

    }

    private void Update()
    {
        res = Random.Range(0, 1);
        spawnPT = spawnMiddle;
        if (rabbitCount > 0 && isRabbitSpawned == false)
        {
            spawnRabbit();
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
        GameObject newRabbit = Instantiate(spawnObject, spawnPT.transform.position, new Quaternion()) as GameObject;
        newRabbit.GetComponent<RabbitProperties>().RabbitType = 1;
        newRabbit.GetComponent<RabbitProperties>().speed = -(newRabbit.GetComponent<RabbitProperties>().speed);
        rabbitCount--;
        isRabbitSpawned = true;

    }




}
