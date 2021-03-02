using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weapon_placeholder_handler : MonoBehaviour
{

	private GameObject weapon_gameobject = null;
	private weapon_handler weapon_hdl;

	private GameObject owner;

	private Vector3 pos_tmp;

	public Camera gcamera;

    private Transform starting_weapon;

    public void LinkOwner(GameObject obj) {

    	owner = obj;

    }

    public bool HasWeapon() {

    	return (weapon_gameobject != null);

    }

    public void PickupWeapon(GameObject weapon) {

    	if (HasWeapon()) {

    		DropWeapon();

    	}

    	weapon_gameobject = weapon;
    	weapon_hdl = weapon.GetComponent<weapon_handler>();

    	weapon_hdl.SetOwner(owner);

    	// pick

    	weapon.transform.parent = gameObject.transform;
    	weapon.transform.position.Set(0f, 0f, 0f);

    	// end pick

    	weapon_hdl.Hide();

    }

    public void DropWeapon() {

    	if (HasWeapon()) {

    		weapon_hdl.NoOwner();

	    	// drop

	    	weapon_gameobject.transform.parent = null;

	    	// end drop

	    	weapon_gameobject = null;
	    }

    }

    public void WeaponFacePoint(Vector2 pos) {

    	pos_tmp.Set(pos.x, pos.y, 0f);
    	Vector3 relative_pos = pos_tmp - transform.position;
    	float angle = Mathf.Atan2(relative_pos.y, relative_pos.x)*Mathf.Rad2Deg - 90;
    	transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

    }

    void Start() {

	    starting_weapon = transform.Find("weapon");

    }

    void Update() {

        if (starting_weapon) {

            PickupWeapon(starting_weapon.gameObject);
            starting_weapon = null;

        }

    	if (HasWeapon()) {

    		Vector3 v = gcamera.ScreenToWorldPoint(Input.mousePosition);
    		Vector2 v2 = new Vector2(v.x, v.y);
    		WeaponFacePoint(v2);

    	}

    }

    public void AttackTowardPoint(Vector2 pos) {

    	// rotate this toward pos

    	WeaponFacePoint(pos);

    	// play attack animation

    	weapon_hdl.DoAttack();

    }

    public float AutoWeaponDamage(float base_damage) {

    	if (HasWeapon()) {

    		return base_damage*weapon_hdl.damage_multiplier + weapon_hdl.damage_addition;

    	} else {

    		return base_damage;

    	}

    }

    public float AutoWeaponCooldown(float base_cooldown) {

    	if (HasWeapon()) {

    		return base_cooldown*weapon_hdl.cooldown_multiplier + weapon_hdl.cooldown_addition;

    	} else {

    		return base_cooldown;

    	}

    }

    public float AutoWeaponRange(float base_range) {

    	if (HasWeapon()) {

    		return base_range + weapon_hdl.range_addition;

    	} else {

    		return base_range;
    	}

    }

}
