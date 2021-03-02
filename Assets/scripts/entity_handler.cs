using System.Collections;
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

	public bool can_hold_weapon = true;

	private bool under_player_command = false;
	private bool player_can_command = true;

	private Vector2 eye_direction = new Vector2(0, 0);
	public float vision_distance;
	public float vision_angle; // degree
	public LayerMask vision_block_layer; // this may not work with complex mask manipulation, maybe bitwise ops will be needed

	private flashlight_handler flashlight;
	private weapon_placeholder_handler weapon_holder;

	public float interaction_distance = 100f;

	private Collider2D[] colliders_tmp;
	private Collider2D hitbox;
	private ContactFilter2D no_filter;

    public GameObject attack_cooldown_bar_canvas;
    private bar_handler attack_cooldown_bar_handler;

	public bool UnderPlayerCommand() {
		return under_player_command;
	}

	public bool PlayerCanCommand() {
		return player_can_command;
	}

	public void PreventPlayerCommand() {
		player_can_command = false;
	}

	public void AllowPlayerCommand() {
		player_can_command = true;
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

	public void AutoAttackTowardPoint(Vector2 pos) {

		if (CanAttack() && HasWeapon()) {

			weapon_holder.AttackTowardPoint(pos);
			attack = ComputedCooldown();

		}

	}

	public bool CanAttackPoint(Vector2 pos) 
	// made to ease AI decision
	{

		return Vector2.Distance(Position(), pos) <= ComputedRange();

	}

	public bool CanAttackEntity(entity_handler hdl) {

		return CanAttackPoint(hdl.Position());

	}

    void Start()
    {

    	rigid_body = GetComponent<Rigidbody2D>();
    	animation_handler = GetComponent<entity_animation_handler>();
    	flashlight = transform.Find("flashlight").GetComponent<flashlight_handler>();
    	weapon_holder = transform.Find("weapon_placeholder").GetComponent<weapon_placeholder_handler>();

    	weapon_holder.LinkOwner(gameObject);

    	colliders_tmp = new Collider2D[6];
    	hitbox = GetComponent<Collider2D>();

    	ContactFilter2D filter = new ContactFilter2D();
    	no_filter = filter.NoFilter();

        if (attack_cooldown_bar_canvas) {

            attack_cooldown_bar_handler = attack_cooldown_bar_canvas.GetComponent<bar_handler>();

        }

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

        if (attack_cooldown_bar_handler) {

            float max = ComputedCooldown();
            attack_cooldown_bar_handler.Notify(max - attack, max);

        }

    }

    bool CanAttack() {

    	return attack == 0;

    }

    bool CanShot() {

    	return reload == 0;

    }

    float ComputedDamage() {

    	return weapon_holder.AutoWeaponDamage(attack_damage);

    }

    float ComputedCooldown() {

    	return weapon_holder.AutoWeaponCooldown(attack_cooldown);

    }

    float ComputedRange() {

    	return weapon_holder.AutoWeaponRange(interaction_distance);

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

    public void OnWeaponHit(GameObject obj) {

    	entity_handler hdl = obj.GetComponent<entity_handler>();

    	if (hdl != null) {

    		OnWeaponHitEntity(hdl);

    	}

    }

    public void OnWeaponHitEntity(entity_handler hdl) {

    	Debug.Log("Entity hit !");

    }

    public bool CanGrabWeapon(GameObject weapon) {

    	weapon_handler weapon_hdl = weapon.GetComponent<weapon_handler>();
    	return (!HasWeapon()) && Vector2.Distance(weapon_hdl.Position(), Position()) < interaction_distance;

    }

    public void GrabWeapon(GameObject weapon) {

    	weapon_holder.PickupWeapon(weapon);

    }

    public void AutoGrabWeapon(GameObject weapon) {

    	if (can_hold_weapon && !(weapon.GetComponent<weapon_handler>().Owned()) && CanGrabWeapon(weapon)) {

    		GrabWeapon(weapon);

    	}

    }

    public void DropWeapon() {

    	weapon_holder.DropWeapon();

    }

    public bool HasWeapon() {

    	return weapon_holder.HasWeapon();

    }

    public void TakeNearWeapon() {

    	int amount = hitbox.OverlapCollider(no_filter, colliders_tmp);

    	for (int i = 0; i < amount; i++) {

    		GameObject gameobject = colliders_tmp[i].gameObject;
    		weapon_handler weapon_hdl = gameobject.GetComponent<weapon_handler>();

    		if (weapon_hdl != null) {

    			GrabWeapon(gameobject);
    			break;

    		}

    	}

    }

}
