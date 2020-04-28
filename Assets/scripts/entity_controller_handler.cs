using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Entity handler interface

public class entity_controller_handler : MonoBehaviour
{

	public GameObject controlled;
	public Camera game_camera;

	public GameObject brother;
	public GameObject sister;

	private entity_handler brother_handler;
	private entity_handler sister_handler;

	private entity_handler controlled_handler;
	private camera_handler game_camera_handler;

	private bool initial_control_set = false;

	// public GameObject brother;
	// public GameObject sister;

    // Start is called before the first frame update
    void Start()
    {

    	controlled_handler = controlled.GetComponent<entity_handler>();
    	game_camera_handler = game_camera.GetComponent<camera_handler>();

    	brother_handler = brother.GetComponent<entity_handler>();
    	sister_handler = sister.GetComponent<entity_handler>();
        
    }

    // Update is called once per frame
    void Update()
    {

    	Debug.Log((int)(1f / Time.unscaledDeltaTime));

    	if (!initial_control_set) {

    		initial_control_set = true;
    		AssumeControl(controlled, controlled_handler);

    	}

    	game_camera_handler.FocusOn(controlled);
        
    }

    public void SetPlayerCommand(Vector2 com) {

    	controlled_handler.SetPlayerCommand(com);

    }

    public void SetAICommand(Vector2 com) {

    	controlled_handler.SetAICommand(com);

    }

    public void AssumeControl(GameObject obj, entity_handler obj_handler) {

    	obj_handler.PlayerAssumeControl();

    	controlled = obj;
    	controlled_handler = obj_handler;

    }

    public void SwitchControl() {

    	if (brother_handler.UnderPlayerCommand()) {

    		brother_handler.PlayerLeaveControl();

    		AssumeControl(sister, sister_handler);

    	} else {

    		sister_handler.PlayerLeaveControl();

    		AssumeControl(brother, brother_handler);

    	}

    }

}
