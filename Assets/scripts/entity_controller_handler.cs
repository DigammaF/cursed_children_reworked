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

	private Vector2 pos_tmp;

	// public GameObject brother;
	// public GameObject sister;

    // Start is called before the first frame update
    void Start()
    {

    	controlled_handler = controlled.GetComponent<entity_handler>();
    	game_camera_handler = game_camera.GetComponent<camera_handler>();

    	brother_handler = brother.GetComponent<entity_handler>();
    	sister_handler = sister.GetComponent<entity_handler>();

    	Application.targetFrameRate = 60;
        
    }

    // Update is called once per frame
    void Update()
    {

    	//Debug.Log((int)(1f / Time.unscaledDeltaTime));

    	if (!initial_control_set) {

    		initial_control_set = true;
    		AssumeControl(controlled, controlled_handler);

    	}

    	if (!controlled_handler.PlayerCanCommand()) {

    		SwitchControl();

    		if (!controlled_handler.PlayerCanCommand()) {

    			// defeat!

    		}

    	}

    	game_camera_handler.FocusOn(controlled);

    	controlled_handler.MakeFlashlightFacePoint(game_camera.ScreenToWorldPoint(Input.mousePosition));

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

    public void ControlSister() {

    	if (brother_handler.UnderPlayerCommand()) {

    		brother_handler.PlayerLeaveControl();
    		AssumeControl(sister, sister_handler);

    	}

    }

    public void ControlBrother() {

    	if (sister_handler.UnderPlayerCommand()) {

    		sister_handler.PlayerLeaveControl();
	    	AssumeControl(brother, brother_handler);

    	}

    }

    public void SwitchControl() {

    	if (brother_handler.UnderPlayerCommand()) {

    		if (sister_handler.PlayerCanCommand()) {

    			brother_handler.PlayerLeaveControl();
    			AssumeControl(sister, sister_handler);

    		}

    	} else {

    		if (brother_handler.PlayerCanCommand()) {

	    		sister_handler.PlayerLeaveControl();
	    		AssumeControl(brother, brother_handler);

	    	}

    	}

    }

    public void AttackControl(Vector3 pos) {

    	Vector3 game_pos = game_camera.ScreenToWorldPoint(pos);
    	pos_tmp.Set(game_pos.x, game_pos.y);
    	controlled_handler.AutoAttackTowardPoint(pos_tmp);

    }

    public void TakeNearWeapon() {

    	controlled_handler.TakeNearWeapon();

    }

    public void DropWeapon() {

    	controlled_handler.DropWeapon();

    }

}
