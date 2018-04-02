using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetaAI : MonoBehaviour
{
    public int rabbitCount = 10;
    public int resourceCount = 10;
    public GameObject spawnObject;
    public GameObject spawnPT;

    private Bounds bounds;

    bool isRabbitSpawned = false;



    private void Start()
    {

    }

    private void Update()
    {

        if (rabbitCount > 0 && isRabbitSpawned == false)
        {
            GameObject newRabbit = Instantiate(spawnObject, spawnPT.transform.position, new Quaternion()) as GameObject;
            newRabbit.GetComponent<RabbitProperties>().RabbitType = 1;
            newRabbit.GetComponent<RabbitProperties>().speed = -(newRabbit.GetComponent<RabbitProperties>().speed);
            rabbitCount--;
            isRabbitSpawned = true;      
            
        }
        
    }

    
}
