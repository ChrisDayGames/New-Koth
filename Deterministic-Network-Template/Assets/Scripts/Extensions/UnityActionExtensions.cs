using UnityEngine;
using Determinism;
using Entitas;
using UnityEngine.Events;

public static class EventExtensions
{
	public static void InvokeSafely ( this UnityAction action )
	{
		if ( action != null )
		{
			action.Invoke();
		}
	}

	public static void InvokeSafely<T0> ( this UnityAction<T0> action, T0 arg0 )
	{
		if ( action != null )
		{
			action.Invoke( arg0 );
		}
	}

	public static void InvokeSafely<T0, T1> ( this UnityAction<T0, T1> action, T0 arg0, T1 arg1 )
	{
		if ( action != null )
		{
			action.Invoke( arg0, arg1 );
		}
	}

	public static void InvokeSafely<T0, T1, T2> ( this UnityAction<T0, T1, T2> action, T0 arg0, T1 arg1, T2 arg2 )
	{
		if ( action != null )
		{
			action.Invoke( arg0, arg1, arg2 );
		}
	}

	public static void DynamicInvokeSafely ( this System.Delegate @delegate, params object[] args )
	{
		if ( @delegate != null )
		{
			@delegate.DynamicInvoke( args );
		}
	}
}
