using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabbitProperties : MonoBehaviour {
	[Range(-100,100)]
	public float speed;
	[Range(0, 10)]
	public float gravity;
	public int currentState = 0;
	public bool onFloor = false;
    public int rabbitCount = 0;
    public int RabbitType = 0; // 0 = player
}
