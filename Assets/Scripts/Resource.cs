using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : MonoBehaviour {

	public enum ResourceType { A, B};
	public ResourceType currentResource;
    public List<GameObject> playerRabbits;
    public List<GameObject> enemyRabbits;

    void Start()
    {
        playerRabbits = new List<GameObject>();
        enemyRabbits = new List<GameObject>();
    }
}
