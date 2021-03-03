using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weapon_handler : MonoBehaviour
{

	public string animation_type = "weapon_attack_sword";

	public float damage_addition = 0;
	public float damage_multiplier = 1;

	public float cooldown_addition = 0;
	public float cooldown_multiplier = 1;

	public float range_addition = 100; // only usefull for AI

	private weapon_sprite_handler sprite_hdl;
	private Animator animator;

	private Vector2 pos_tmp;

    void Start()
    {

    	sprite_hdl = transform.Find("weapon_sprite").GetComponent<weapon_sprite_handler>();
        animator = GetComponent<Animator>();

        if (sprite_hdl == null) {

        	Debug.Log("A weapon handler could not link to sprite handler (you are missing weapon_sprite_handler.cs script component on weapon_sprite)");

        }

    }

    public Vector2 Position() {

    	pos_tmp.Set(transform.position.x, transform.position.y);
    	return pos_tmp;

    }

    public void SetOwner(GameObject owner) {

    	sprite_hdl.SetOwner(owner);

    }

    public void NoOwner() {

    	sprite_hdl.NoOwner();

    }

    public void DoAttack() {

    	StartAttacking();

        animator.Play(animation_type);

    	Invoke("StopAttacking", 0.5f);
    	Invoke("StopAnimation", 0.5f);

    }

    void StopAnimation() {

    	animator.Play("weapon_idle");

    }

    public bool Owned() {

    	return sprite_hdl.Owned();

    }

    void StartAttacking() {

    	sprite_hdl.StartAttacking();

    }

    void StopAttacking() {

    	sprite_hdl.StopAttacking();

    }

    public void Hide() {

    	sprite_hdl.Hide();

    }

}
