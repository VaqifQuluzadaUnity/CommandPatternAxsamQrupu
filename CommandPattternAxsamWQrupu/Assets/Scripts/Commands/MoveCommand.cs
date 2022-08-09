using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCommand : ICommand
{
	public GameObject movedObject;

	public Vector3 initialPosition;

	public Vector3 destinationPosition;

	public float speed;

	public MoveCommand(GameObject _movedObject, Vector3 _initialPos, Vector3 _destinationPos,float _speed) 
	{
		movedObject = _movedObject;

		initialPosition = _initialPos;

		destinationPosition = _destinationPos;

		speed = _speed;
	}

	public void Execute()
	{
		CommandManager.instance.MoveObject(movedObject,destinationPosition,speed);
	}

	public float ReturnExecutionTime()
	{
		float distance = Vector3.Distance(initialPosition, destinationPosition);

		return distance / speed;
	}

	public void Undo()
	{
		CommandManager.instance.MoveObject(movedObject, initialPosition, speed);
	}
}
