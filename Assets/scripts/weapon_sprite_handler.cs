using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weapon_sprite_handler : MonoBehaviour
{

	private GameObject owner;
	private entity_handler owner_hdl;

	private Color full_opacity = new Color(1f, 1f, 1f, 1f);
	private Color no_opacity = new Color(1f, 1f, 1f, 0f);

	public bool attacking = false;
	private bool prev_attacking = false;

	private SpriteRenderer sprite_renderer;

	void Start() {

		sprite_renderer = GetComponent<SpriteRenderer>();

	}

	void Update() {

		if (attacking != prev_attacking && Owned()) {

			if (attacking) {

				Show();

			} else {

				Hide();

			}

			prev_attacking = attacking;

		}

	}

    public void SetOwner(GameObject obj) {

    	owner = obj;
    	owner_hdl = obj.GetComponent<entity_handler>();

    }

    public void NoOwner() {

    	owner = null;
    	owner_hdl = null;
    	StopAttacking();
    	ShowIndependant();

    }

    public bool Owned() {

    	return owner != null;

    }

    public void StartAttacking() {

    	attacking = true;

    }

    public void StopAttacking() {

    	attacking = false;

    }

    void OnCollisionEnter(Collision collision) {

        Debug.Log("Collision!");

    	if (attacking && Owned()) {

    		if (!(Object.ReferenceEquals(collision.gameObject, owner))) {

                Debug.Log(collision.gameObject);

    			owner_hdl.OnWeaponHit(collision.gameObject);

    		}

    	}

    }

    public void Show() {

    	sprite_renderer.color = full_opacity;

    }

    public void ShowIndependant() {

    	sprite_renderer.color = new Color(1f, 1f, 1f, 1f);

    }

    public void Hide() {

    	sprite_renderer.color = no_opacity;

    }

}
