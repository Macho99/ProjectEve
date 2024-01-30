using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseManager : MonoBehaviour
{
	private void Awake()
	{
		gameObject.AddComponent<PlayerInput>();
	}
}
