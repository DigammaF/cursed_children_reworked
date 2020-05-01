using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class fade_handler : MonoBehaviour
{


	public CanvasGroup target;


	public void PopIn() {

		target.alpha = 1f;

	}

	public void PopOut() {

		target.alpha = 0f;

	}

    public void StartFadeIn(float duration = 1f) {

    	StartCoroutine(FadeEffect.FadeCanvas(target, 0f, 1f, duration));

    }

    public void StartFadeOut(float duration = 1f) {

    	StartCoroutine(FadeEffect.FadeCanvas(target, 1f, 0f, duration));

    }

}
