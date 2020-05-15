using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weapon_placeholder_handler : MonoBehaviour
{

	private GameObject ignore;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DoIgnore(GameObject obj) {

    	ignore = obj;

    }

    public bool HasWeapon() {return false;}

    public void PickupWeapon(GameObject weapon) {}

    public void DropWeapon() {}

}
