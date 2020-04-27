using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class logo_fade : MonoBehaviour
{


	public CanvasGroup target;

	private bool planned = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    	if (!planned) {

    		planned = true;

    		Invoke("StartFadeIn", 0.5f);
    		Invoke("StartFadeOut", 3.5f);
    		Invoke("GoToMainMenu", 5f);

    	}
        
    }

    void StartFadeIn() {

    	StartCoroutine(FadeEffect.FadeCanvas(target, 0f, 1f, 1f));

    }

    void StartFadeOut() {

    	StartCoroutine(FadeEffect.FadeCanvas(target, 1f, 0f, 1f));

    }

    void GoToMainMenu() {

    	SceneManager.LoadScene("main_menu");

    }
}
