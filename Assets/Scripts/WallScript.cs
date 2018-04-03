using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallScript : MonoBehaviour
{
    float timeDelay = 2f;
    bool startTimer = false;
    public GameObject spawnObject;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("Collided");
        if (collision.gameObject.GetComponent<RabbitProperties>().RabbitType == 0)
        {
            startTimer = true;
            //spawnRabbit();


        }
    }
    public void spawnRabbit()
    {
        GameObject newRabbit = Instantiate(spawnObject, gameObject.transform.position, new Quaternion()) as GameObject;
        newRabbit.GetComponent<RabbitProperties>().RabbitType = 1;
        newRabbit.GetComponent<RabbitProperties>().speed = -(newRabbit.GetComponent<RabbitProperties>().speed);
    }

    private void Update()
    {


        if (startTimer)
        {
            timeDelay -= Time.deltaTime;
            if (timeDelay <= 0f)
            {
                spawnRabbit();
                startTimer = false;
                timeDelay = 2f;
            }
        }
    }



}
