using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Camera interface

public class camera_handler : MonoBehaviour
{

	private Rigidbody2D rigid_body;
	private Vector2 target_point = new Vector2(0, 0);

	public GameObject initial_focus;
	public float speed = 100f;

    // Start is called before the first frame update
    void Start()
    {

    	rigid_body = GetComponent<Rigidbody2D>();
    	FocusOn(initial_focus);
        
    }

    // Update is called once per frame
    void Update()
    {

    	rigid_body.velocity = (target_point - rigid_body.position)*speed;
        
    }

    public void FocusOn(GameObject target) {

    	target_point = target.transform.position;

    }

}
