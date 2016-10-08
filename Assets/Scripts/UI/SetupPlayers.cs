using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SetupPlayers : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
	public static GameObject itemBeingDragged;
	Vector3 startPosition;
	Transform startParent;

	[SerializeField]
	public GlobalVariables.ControlType type { get; private set; }

	public void OnBeginDrag(PointerEventData eventData)
	{
		itemBeingDragged = gameObject;
		startPosition = transform.position;
		startParent = transform.parent;
		GetComponent<CanvasGroup>().blocksRaycasts = false;
	}

	public void OnDrag(PointerEventData eventData)
	{
		transform.position = Input.mousePosition;
	}

	public void OnEndDrag(PointerEventData eventData)
	{
		itemBeingDragged = null;
		GetComponent<CanvasGroup>().blocksRaycasts = true;
		if (transform.parent == startParent)
		{
			transform.position = startPosition;
		}
	}	

	

}
