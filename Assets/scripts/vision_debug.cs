using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class vision_debug : MonoBehaviour
{

	public GameObject target;
	public entity_handler obs_handler;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    	obs_handler.LookForward();

    	if (obs_handler.CanSee(target.GetComponent<Rigidbody2D>().position)) {

    		Debug.Log("Can see !");

    	} else {

    		Debug.Log("Not see !");

    	}
        
    }
}
