using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PointerManager : MonoBehaviour
{
	private GameObject curPointerOnObj;
	public GameObject CurPointerOnObj
	{
		get	{ return curPointerOnObj; }
		set
		{
			curPointerOnObj = value;
			OnCurPointerOnObjChange?.Invoke(value);
		}
	}

	[HideInInspector] UnityEvent<GameObject> OnCurPointerOnObjChange;

	private void Awake()
	{
		OnCurPointerOnObjChange = new UnityEvent<GameObject>();
	}
}
