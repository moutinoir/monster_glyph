using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class extend : MonoBehaviour
{
    Animator animator;
    float time = 0;
    float random;
    bool bad;
	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
        random = Random.Range(3.0f, 5.0f);
	}
	
	// Update is called once per frame
	void Update () {

		if(Time.time - time > random)
        {
            bad = !bad;
            animator.SetBool("is_badguy", bad);
            time = Time.time;
        }
	}
}
