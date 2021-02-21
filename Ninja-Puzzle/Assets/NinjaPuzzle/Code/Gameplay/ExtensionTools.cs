using System.Collections.Generic;

namespace NinjaPuzzle.Code.Gameplay
{
	public static class ExtensionTools
	{
		public static bool AddUnique<T>(this List<T> list, T element)
		{
			if (!list.Contains(element))
			{
				list.Add(element);
				return true;
			}
			return false;
		}
	}
}