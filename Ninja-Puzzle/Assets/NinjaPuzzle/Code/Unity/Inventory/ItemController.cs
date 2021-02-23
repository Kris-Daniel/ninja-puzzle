using UnityEngine;

namespace NinjaPuzzle.Code.Unity.Inventory
{
	public class ItemController : MonoBehaviour
	{
		public Item Item { get; private set; }

		public void SetItem(Item item)
		{
			Item = item;
		}
	}
}