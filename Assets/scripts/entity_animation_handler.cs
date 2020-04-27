using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;

public class entity_animation_handler : MonoBehaviour
{

	public int entity_type;
	// 0 brother
	// 1 sister
	// 2 s demon
	// 3 demon
	// 4 soldier

	private Animator animator;

    // Start is called before the first frame update
    void Start()
    {

    	animator = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FaceRight() {

    	Vector3 current_scale = transform.localScale;
    	transform.localScale = new Vector3(
    		Math.Abs(current_scale.x),
    		current_scale.y,
    		current_scale.z
    	);

    }

    public void FaceLeft() {

    	Vector3 current_scale = transform.localScale;
    	transform.localScale = new Vector3(
    		-Math.Abs(current_scale.x),
    		current_scale.y,
    		current_scale.z
    	);

    }

    public void Walk() {

    	if (entity_type == 0) {

    		animator.Play("brother_walk");

    	} else if (entity_type == 1) {

    		animator.Play("sister_walk");

    	} else if (entity_type == 2) {

    		animator.Play("brother_s_demon_walk");

    	} else if (entity_type == 3) {

    		animator.Play("brother_demon_walk");

    	} else if (entity_type == 4) {

    		animator.Play("soldier_walk");

    	} else {

    		Debug.Log("Unknown entity type : " + entity_type);

    	}

    }

    public void Idle() {

    	if (entity_type == 0) {

    		animator.Play("brother_idle");

    	} else if (entity_type == 1) {

    		animator.Play("sister_idle");

    	} else if (entity_type == 2) {

    		animator.Play("brother_s_demon_walk");

    	} else if (entity_type == 3) {

    		animator.Play("brother_demon_walk");

    	} else if (entity_type == 4) {

    		animator.Play("soldier_idle");

    	} else {

    		Debug.Log("Unknown entity type : " + entity_type);

    	}

    }

    public void Die() {

    	if (entity_type == 0) {

    		animator.Play("brother_die");

    	} else if (entity_type == 1) {

    		animator.Play("sister_dir");

    	} else if (entity_type == 2) {

    		animator.Play("brother_die");

    	} else if (entity_type == 3) {

    		animator.Play("brother_die");

    	} else if (entity_type == 4) {

    		// animator.Play("soldier_walk");

    	} else {

    		Debug.Log("Unknown entity type : " + entity_type);

    	}

    }

}
