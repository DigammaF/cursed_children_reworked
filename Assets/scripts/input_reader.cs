﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Read input and handle entity_controller

public class input_reader : MonoBehaviour
{

	public entity_controller_handler handler;

	private Vector2 com; // tmp

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    	if (Input.GetButtonDown("SwitchControl")) {

    		handler.SwitchControl();

    	}

        if (Input.GetMouseButtonDown(0)) {

            handler.AttackControl(Input.mousePosition);

        }

        if (Input.GetButtonDown("TakeWeapon")) {

            handler.TakeNearWeapon();

        }

        if (Input.GetButtonDown("DropWeapon")) {

            handler.DropWeapon();

        }

    	com.Set(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

    	handler.SetPlayerCommand(com);

    }
}
