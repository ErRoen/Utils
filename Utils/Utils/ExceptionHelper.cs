using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

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

			//public static class Collection
			//{
			//	public static IEnumerable<T> IsEmpty<T>(IEnumerable<T> collection)
			//	{
			//		if (collection != null)
			//			if (collection.Count() >= 0)
			//				return collection;

			//		throw new 
			//	}
			//}

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