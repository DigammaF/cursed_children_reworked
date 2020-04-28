using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class auto_y_sorting : MonoBehaviour
{

	private SpriteRenderer sprite_renderer;

    // Start is called before the first frame update
    void Start()
    {

    	sprite_renderer = GetComponent<SpriteRenderer>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void LateUpdate() {

    	sprite_renderer.sortingOrder = (int)Camera.main.WorldToScreenPoint (sprite_renderer.bounds.min).y * -1;

    }
}
