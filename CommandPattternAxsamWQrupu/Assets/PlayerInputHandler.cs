using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputHandler : MonoBehaviour
{
	[SerializeField] private float playerSpeed = 5;
	private void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);

			RaycastHit hit = new RaycastHit();

			if(Physics.Raycast(mouseRay, out hit)) 
			{
				MoveCommand command = new MoveCommand(gameObject,transform.position,hit.point,playerSpeed);

				command.Execute();

				CommandManager.instance.AddCommandToList(command);
			}
		}
	}
}
