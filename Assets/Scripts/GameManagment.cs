using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagment : MonoBehaviour {

    public int PlayerResourceA;
    public int EnemyResourceA;
    public int PlayerResourceB;
    public int EnemyResourceB;
    public int PlayerResourceC; //C for stolen resources.
    public int EnemyResourceC; 

    int playerResourceSum;
    int enemyResourceSum;

    int goal;

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
    }
	
	// Update is called once per frame
	void Update () {
        playerResourceSum = PlayerResourceA + PlayerResourceB + PlayerResourceC;
        enemyResourceSum = EnemyResourceA + EnemyResourceB + EnemyResourceC;

        if(playerResourceSum >= goal)
        {
            Debug.Log("Player Win!");
        }

        if(enemyResourceSum >= goal)
        {
            Debug.Log("Enenmy Win!");
        }
	}
}
