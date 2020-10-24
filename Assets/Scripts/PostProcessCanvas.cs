using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PostProcessCanvas : MonoBehaviour
{
    public static CanvasGroup canvasGroup;
    public static Image image;

	private void Awake()
	{
		canvasGroup = GetComponent<CanvasGroup>();
		image = GetComponent<Image>();
	}
}
