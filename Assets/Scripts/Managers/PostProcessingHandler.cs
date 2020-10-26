using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PostProcessingHandler : MonoBehaviour
{
	static PostProcessingHandler instance;
	private void Awake()
	{
		instance = this;

		mainCamera = GetComponent<Camera>();
		PostProcessVolume volume = GetComponent<PostProcessVolume>();
		volume.profile.TryGetSettings<LensDistortion>(out lensDistortion);
	}

	static Camera mainCamera;
	public static Camera MainCamera { get { return mainCamera; } }

	LensDistortion lensDistortion;
	public static LensDistortion LensDistort { get { return instance.lensDistortion; } }
}
