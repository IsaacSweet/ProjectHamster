using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettings : MonoBehaviour {
    public static GameSettings Instance;

    public bool Paused;

	// Use this for initialization
	void Start () {
        Instance = this;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
