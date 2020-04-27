using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;

// Entity interface

public class entity_handler : MonoBehaviour
{

	private Rigidbody2D rigid_body;
	private Vector2 command;
	private entity_animation_handler animation_handler;

	public float move_speed = 100f;

	public bool alive = true;
	public float health = 100f;
	public float reload_time = 3f;
	public float attack_time = 3f;
	public float attack_damage = 20f;

	private float reload = 0f;
	private float attack = 0f;

    // Start is called before the first frame update
    void Start()
    {

    	rigid_body = GetComponent<Rigidbody2D>();
    	animation_handler = GetComponent<entity_animation_handler>();
        
    }

    void UpdateVelocity() {

    	rigid_body.velocity = command*move_speed*Time.deltaTime;

    }

    void UpdateWalkIdle() {

    	if (rigid_body.velocity.magnitude == 0) {

    		animation_handler.Idle();

    	} else {

    		animation_handler.Walk();

    	}

    }

    void UpdateFacing() {

    	if (rigid_body.velocity.x > 0) {

    		animation_handler.FaceRight();

    	} else if (rigid_body.velocity.x < 0) {

    		animation_handler.FaceLeft();

    	}

    }

    void UpdateTimes() {

    	reload = Math.Max(0, reload - Time.deltaTime);
    	attack = Math.Max(0, attack - Time.deltaTime);

    }

    bool CanAttack() {

    	return attack == 0;

    }

    bool CanShot() {

    	return reload == 0;

    }

    float ComputedDamage() {

    	return attack_damage;

    }

    // Update is called once per frame
    void Update()
    {

    	UpdateVelocity();
    	UpdateWalkIdle();
    	UpdateFacing();
    	UpdateTimes();
        
    }

    public void SetCommand(Vector2 com) {

    	command = com.normalized;

    }

    public void MoveToward(Vector2 point) {

    	SetCommand(point - Position());

    }

    public Vector2 Position() {

    	return rigid_body.position;

    }

}
