using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCommand : ICommand
{

	public Animator objectAnimator;
	public void Execute()
	{
		CommandManager.instance.PlayAttack(objectAnimator);
	}

	public float ReturnExecutionTime()
	{
		return 0;
	}

	public void Undo()
	{
		CommandManager.instance.PlayAttack(objectAnimator);
	}
}
