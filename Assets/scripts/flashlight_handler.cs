using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.LWRP;

public class flashlight_handler : MonoBehaviour
{

	public bool is_on = false;

	private Light2D light_2d;

    // Start is called before the first frame update
    void Start()
    {

    	light_2d = GetComponent<Light2D>();

    	if (!is_on) {

    		TurnOff();

    	}
        
    }

    public void TurnOn() {

    	is_on = true; 
    	light_2d.intensity = 1f;

    }

    public void AutoTurnOn() {

    	if (!is_on) {

    		TurnOn();

    	}

    }

    public void TurnOff() {

    	is_on = false;
    	light_2d.intensity = 0f;

    }

    public void AutoTurnOff() {

    	if (is_on) {

    		TurnOff();

    	}

    }

    public void FacePoint(Vector3 pos) {

    	Vector3 relative_pos = pos - transform.position;
    	float angle = Mathf.Atan2(relative_pos.y, relative_pos.x)*Mathf.Rad2Deg - 90;
    	transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

    }

}
