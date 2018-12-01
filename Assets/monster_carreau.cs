using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class monster_carreau : MonoBehaviour {

    public Animator anim;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
        float randomSpeed = Random.Range(0.3f, 1.4f);
        anim.SetFloat("speed", randomSpeed);
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
