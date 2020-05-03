using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sister_handler : MonoBehaviour
{

	private bar_handler health_bar_handler;

    // Start is called before the first frame update
    void Start()
    {

    	health_bar_handler = transform.Find("health_bar_canvas").GetComponent<bar_handler>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
