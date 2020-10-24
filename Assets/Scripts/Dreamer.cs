using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Dreamer : MonoBehaviour, IInteractable
{
	[Header("Dream")]
	[SerializeField] string dreamSceneTitle;

	public void OnInteract(ObjectInteractor interactor)
	{
		LoadDream();
	}

	[ContextMenu("Load Dream")]
	public void LoadDream()
	{
		Debug.Log("Dream Time");
		if (Application.CanStreamedLevelBeLoaded(dreamSceneTitle))
		{
			// Handles Post Processing and loads scene whene effects are complete
			LeanTween.value(0, -100, 2.0f).setOnUpdate((float val) => PostProcessingHandler.LensDistort.intensity.value = val);
			LeanTween.value(1, 0.01f, 2.5f).setOnUpdate((float val) => PostProcessingHandler.LensDistort.scale.value = val);
			LeanTween.alphaCanvas(PostProcessCanvas.canvasGroup, 1, 3.0f).setFrom(0).setOnComplete(() => LoadScene());
		}
	}

	void LoadScene()
	{
		SceneManager.LoadScene(dreamSceneTitle);
	}

	[ContextMenu("Return From Dream")]
	public void ReturnFromDream()
	{
		LeanTween.value(-100, 0, 2.0f).setOnUpdate((float val) => PostProcessingHandler.LensDistort.intensity.value = val);
		LeanTween.value(0.01f, 1.0f, 2.0f).setOnUpdate((float val) => PostProcessingHandler.LensDistort.scale.value = val);
		LeanTween.alphaCanvas(PostProcessCanvas.canvasGroup, 0, 2.0f).setFrom(1).setOnComplete(() => LoadMainScene());
	}

	void LoadMainScene()
	{
		SceneManager.LoadScene(0);
		SceneManager.sceneLoaded += SetDreamVariables;
	}

	void SetDreamVariables(Scene scene, LoadSceneMode loadMode)
	{
		if (scene == SceneManager.GetSceneByName(dreamSceneTitle))
			DreamManager.dreamOwner = this;
	}
}
