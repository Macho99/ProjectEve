using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EnemyAction : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{
	public void OnPointerDown(PointerEventData eventData)
	{
		//print(eventData.button);
		//PointerEventData.InputButton.Left;
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		GameManager.Pointer.CurPointerOnObj = gameObject;
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		GameManager.Pointer.CurPointerOnObj = null;
	}
}
