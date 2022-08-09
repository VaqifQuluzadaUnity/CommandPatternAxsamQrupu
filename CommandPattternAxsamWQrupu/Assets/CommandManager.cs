using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandManager : MonoBehaviour
{
	public static CommandManager instance;

	private IEnumerator moveCoroutine;

	private List<ICommand> executedCommands=new List<ICommand>();

	private bool isUndoing = false;

	private void Awake()
	{
		if(instance!=null && instance != this)
		{
			Destroy(instance.gameObject);
		}
		instance = this;
	}

	public void MoveObject(GameObject _movedObject,Vector3 _destinationPoint,float _speed)
	{
		if (moveCoroutine != null)
		{
			StopCoroutine(moveCoroutine);
		}

		moveCoroutine = MoveObjectCoroutine(_movedObject, _destinationPoint, _speed);

		StartCoroutine(moveCoroutine);
	}


	public void PlayAttack(Animator animator)
	{
		animator.Play("Attack");
	}
	public void AddCommandToList(ICommand command)
	{
		executedCommands.Add(command);
	}
	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space)&& !isUndoing)
		{
			isUndoing = true;
			StartCoroutine(UndoAllCommands());
		}

		if(Input.GetKeyDown(KeyCode.LeftShift)&& !isUndoing)
		{
			UndoLastCommand();
		}
	}

	IEnumerator UndoAllCommands()
	{
		for(int i = executedCommands.Count - 1; i >= 0; i--)
		{
			executedCommands[i].Undo();

			yield return new WaitForSeconds(executedCommands[i].ReturnExecutionTime());
		}

		executedCommands.Clear();

		isUndoing = false;
	}

	private void UndoLastCommand()
	{
		if (executedCommands.Count > 0)
		{
			executedCommands[executedCommands.Count - 1].Undo();

			executedCommands.RemoveAt(executedCommands.Count - 1);
		}
		
	}
	IEnumerator MoveObjectCoroutine(GameObject _movedObject, Vector3 _destinationPoint, float _speed)
	{
		while (_movedObject.transform.position != _destinationPoint)
		{
			_movedObject.transform.position =
				Vector3.MoveTowards(_movedObject.transform.position,_destinationPoint,_speed*Time.deltaTime);
			yield return new WaitForEndOfFrame();
		}
	}
}

public interface ICommand
{
	public void Execute();

	public void Undo();

	public float ReturnExecutionTime();
}
