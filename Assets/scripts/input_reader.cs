using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Read input and handle entity_controller

public class input_reader : MonoBehaviour
{

	public entity_controller_handler handler;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    	Vector2 com = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

    	handler.SetCommand(com);

    }
}
