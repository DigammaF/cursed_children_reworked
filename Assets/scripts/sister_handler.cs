using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sister_handler : MonoBehaviour
{

	private bar_handler health_bar_handler;
	private entity_handler base_entity_handler;

    // Start is called before the first frame update
    void Start()
    {

    	health_bar_handler = transform.Find("health_bar_canvas").GetComponent<bar_handler>();
    	base_entity_handler = GetComponent<entity_handler>();

    }

    void UpdateHealthBar() {

    	health_bar_handler.ShadowSet(base_entity_handler.health, base_entity_handler.max_health);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
