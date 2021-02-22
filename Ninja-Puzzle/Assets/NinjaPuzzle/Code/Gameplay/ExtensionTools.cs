using System;
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

		public static void MapEnum<T>(Action<T> callback) where T : Enum
		{
			foreach (T enumValue in (T[]) Enum.GetValues(typeof(T)))
			{
				callback?.Invoke(enumValue);
			}
		}
		
		public static void FastMapEnum<T>(Action<T> callback) where T : Enum
		{
			int bitMask = 1 << (Enum.GetNames(typeof(T)).Length - 1);
			while (bitMask != 0)
			{
				callback?.Invoke((T) (bitMask as object));
				bitMask >>= 1;
			}
		}
	}
}