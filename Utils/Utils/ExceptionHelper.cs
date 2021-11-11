using System;

namespace Utils
{
	public class ExceptionHelper
	{
		internal static class ThrowIf
		{
			public static class Argument
			{
				public static T IsNull<T>(T argument, string argumentName)
				{
					// ToDo: Fix "possible compare of value type with null"
					if (argument == null)
						throw new ArgumentNullException(argumentName);

					return argument;
				}
			}

			/*
			 * ThrowIf.Collection.IsEmpty
			 * ThrowIf.Value.IsZero
			 * ThrowIf.Value.IsGreaterThan
			 * ThrowIf.Value.IsNegative
			 * ThrowIf.ArrayIndex.IsOutOfBounds
			*/
		} 
	}
}