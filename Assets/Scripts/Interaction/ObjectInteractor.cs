using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectInteractor : MonoBehaviour
{
	[Header("Interaction Raycast")]
	[SerializeField] float interactionRange = 4.0f;
	[SerializeField] LayerMask interactableLayers;
	IInteractable hoveredInteractable;

	// Update is called once per frame
	void Update()
	{
		DetectionRaycast();
		DisplayTooltip();

		if (Input.GetKeyUp(KeyCode.E))
		{
			TrySelectHoveredInteractable();
		}
	}

	void DetectionRaycast()
	{
		if (Physics.Raycast(PostProcessingHandler.MainCamera.transform.position, PostProcessingHandler.MainCamera.transform.forward, out RaycastHit hit, interactionRange, interactableLayers))
		{
			IInteractable interactable = hit.transform.GetComponent<IInteractable>();
			if (interactable != null)
				hoveredInteractable = interactable;
		}
	}

	void DisplayTooltip()
	{
		if (hoveredInteractable != null)
			Debug.Log("Hovered");
	}

	bool TrySelectHoveredInteractable()
	{
		if (hoveredInteractable != null)
		{
			hoveredInteractable.OnInteract(this);
			return true;
		}

		return false;
	}
}
