using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ai_rnd : MonoBehaviour
{

	private Vector2 target;
	private entity_handler handler;
	private bool first_target_set = false;
	private Vector2 original_pos;

	public float range;

    // Start is called before the first frame update
    void Start()
    {

    	handler = GetComponent<entity_handler>();
        
    }

    void NewTarget() {

    	Vector2 pos = handler.Position();

    	target = new Vector2(
    		original_pos.x + Random.Range(-1.0f, 1.0f),
    		original_pos.y + Random.Range(-1.0f, 1.0f)
    	);

    	target *= range;

    }

    // Update is called once per frame
    void Update()
    {

    	if (!first_target_set) {

    		first_target_set = true;
    		original_pos = handler.Position();
    		NewTarget();

    	}

    	if (Vector2.Distance(handler.Position(), target) < 0.01f) {

    		NewTarget();

    	}

    	handler.MoveToward(target);

    	//Debug.Log(target.x + ";" + target.y);
    	Debug.Log(Vector2.Distance(handler.Position(), target));
        
    }
}
