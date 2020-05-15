﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;

// Entity interface

public class entity_handler : MonoBehaviour
{

	private Rigidbody2D rigid_body;
	private Vector2 player_command;
	private Vector2 ai_command;
	private entity_animation_handler animation_handler;

	public float move_speed = 100f;

	public bool alive = true;
	public float health = 100f;
	public float max_health = 100f;
	public float reload_time = 3f;
	public float attack_cooldown = 3f;
	public float attack_damage = 20f;

	private float reload = 0f;
	private float attack = 0f;

	private bool under_player_command = false;
	private bool player_can_command = true;

	private Vector2 eye_direction = new Vector2(0, 0);
	public float vision_distance;
	public float vision_angle; // degree
	public LayerMask vision_block_layer; // this may not work with complex mask manipulation, maybe bitwise ops will be needed

	private flashlight_handler flashlight;
	private weapon_placeholder_handler weapon;

	public bool UnderPlayerCommand() {
		return under_player_command;
	}

	public bool PlayerCanCommand() {
		return player_can_command;
	}

	public void LookForward() {

		if (rigid_body.velocity.magnitude != 0f) {

			eye_direction = rigid_body.velocity;

		}

	}

	public bool CanSee(Vector2 pos) {

		if (Vector2.Angle(eye_direction, pos - rigid_body.position) < vision_angle) {

			RaycastHit2D hit = Physics2D.Raycast(rigid_body.position, pos - rigid_body.position, vision_distance, vision_block_layer);

			return hit.collider == null;

		}

		return false;

	}

	public bool CanSeeEntity(entity_handler hdl) {

		return CanSee(hdl.Position());

	}

	public void AttackTowardPoint(Vector2 pos) {}

	public bool CanAttackPoint(Vector2 pos) {return false;}

	public bool CanAttackEntity(entity_handler hdl) {

		return CanAttackPoint(hdl.Position());

	}

    // Start is called before the first frame update
    void Start()
    {

    	rigid_body = GetComponent<Rigidbody2D>();
    	animation_handler = GetComponent<entity_animation_handler>();
    	flashlight = transform.Find("flashlight").GetComponent<flashlight_handler>();
    	weapon = transform.Find("weapon_placeholder").GetComponent<weapon_placeholder_handler>();

    	weapon.DoIgnore(gameObject);

    }

    public void MakeFlashlightFacePoint(Vector3 pos) {

    	flashlight.FacePoint(pos);

    }

    void UpdateVelocity() {

    	if (under_player_command) {

    		rigid_body.velocity = player_command*move_speed*Time.deltaTime;

    	} else {

    		rigid_body.velocity = ai_command*move_speed*Time.deltaTime;

    	}

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

    	LookForward();
        
    }

    public void SetPlayerCommand(Vector2 com) {

    	player_command = com.normalized;

    }

    public void StopPlayerCommand() {

    	player_command.Set(0, 0);

    }

    public void SetAICommand(Vector2 com) {

    	ai_command = com.normalized;

    }

    public void AIMoveToward(Vector2 point) {

    	SetAICommand(point - Position());

    }

    public Vector2 Position() {

    	return rigid_body.position;

    }

    public void PlayerLeaveControl() {

    	under_player_command = false;
    	StopPlayerCommand();

    	flashlight.AutoTurnOff();

    }

    public void PlayerAssumeControl() {

    	under_player_command = true;

    	flashlight.AutoTurnOn();

    }

}
