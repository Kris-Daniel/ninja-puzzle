using NinjaPuzzle.Code.Unity.Helpers.GenericDictionary;
using NinjaPuzzle.Code.Unity.Systems.Inventory;
using UnityEngine;

namespace NinjaPuzzle.Code.Unity.ScriptableObjects.Inventory
{
	[CreateAssetMenu(fileName = "GlobalDataByEnum", menuName = "GamePlay/GlobalDataByEnum", order = 0)]
	public class GlobalDataByEnum : ScriptableObject
	{
		[SerializeField] private GenericDictionary<EItem, ItemData> items;
		public GenericDictionary<EItem, ItemData> Items => items;
	}
}