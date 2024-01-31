using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAction : MonoBehaviour
{
	[SerializeField] GameObject battleFieldObj;

	Coroutine battleFieldOnCoroutine;

	private void OnControl(InputValue value)
	{
		bool pressed = value.Get<float>() > 0.9f ? true : false;

		if(pressed == true)
		{
			battleFieldObj.SetActive(true);
			battleFieldOnCoroutine = StartCoroutine(CoBattleFieldOn());
		}
		else
		{
			battleFieldObj.SetActive(false);
			if (battleFieldOnCoroutine != null)
				StopCoroutine(battleFieldOnCoroutine);
			battleFieldObj.transform.up = transform.up;
		}
	}

	IEnumerator CoBattleFieldOn()
	{
		GameObject target;
		while (true)
		{
			target = GameManager.Pointer.CurPointerOnObj;

			Vector3 lerpTarget;
			if(target == null)
			{
				lerpTarget = transform.up;
			}
			else
			{
				lerpTarget = Vector3.Cross(transform.forward, (target.transform.position - transform.position)).normalized;
			}
			battleFieldObj.transform.up = Vector3.Lerp(battleFieldObj.transform.up, lerpTarget, Time.deltaTime * 10f);

			yield return null;
		}
	}
}
