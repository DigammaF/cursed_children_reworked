using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Entity handler interface

public class entity_controller_handler : MonoBehaviour
{

	public GameObject controlled;
	public Camera game_camera;

	private entity_handler controlled_handler;
	private camera_handler game_camera_handler;

	// public GameObject brother;
	// public GameObject sister;

    // Start is called before the first frame update
    void Start()
    {

    	controlled_handler = controlled.GetComponent<entity_handler>();
    	game_camera_handler = game_camera.GetComponent<camera_handler>();
        
    }

    // Update is called once per frame
    void Update()
    {

    	game_camera_handler.FocusOn(controlled);
        
    }

    public void SetCommand(Vector2 com) {

    	controlled_handler.SetCommand(com);

    }

}
