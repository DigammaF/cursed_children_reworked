using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// must be attached to health_bar_canvas

public class bar_handler : MonoBehaviour
{

	public float fade_out_duration = 1f;
	public float display_for_duration = 1f;

	private Slider slider;
	private fade_handler bar_fade_handler;

    private float previous_notify = 1f;

    // Start is called before the first frame update
    void Start()
    {

    	slider = transform.Find("bar").GetComponent<Slider>();
    	bar_fade_handler = GetComponent<fade_handler>();

    	bar_fade_handler.PopOut();
        
    }

    public void DisplayFor(float duration = -1) {

    	if (duration == -1) {
    		duration = display_for_duration;
    	}

    	bar_fade_handler.PopIn();
    	Invoke("FadeOut", duration);

    }

    public void ShadowSet(float value, float max = 1) {

    	slider.value = value/max;

    }

    public void Set(float value, float max = 1, float duration = -1) {

    	if (duration == -1) {
    		duration = display_for_duration;
    	}

    	ShadowSet(value, max);
    	DisplayFor(duration);

    }

    private void FadeOut() {

    	bar_fade_handler.StartFadeOut(fade_out_duration);

    }

    public void Notify(float value, float max = 1) {

        float cvalue = value/max;

        if (previous_notify != cvalue) {

            previous_notify = cvalue;
            Set(cvalue);

        }

    }

}
