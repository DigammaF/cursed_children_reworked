using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waypoint_handler : MonoBehaviour
{

	private fade_handler fade_hdl;

    // Start is called before the first frame update
    void Start()
    {

    	fade_hdl = GetComponent<fade_handler>();
    	Invoke("FadeOut", 1f);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FadeOut() {

    	fade_hdl.StartFadeOut();

    }

}
