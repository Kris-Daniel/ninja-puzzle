using UnityEngine;

namespace NinjaPuzzle.Code.Unity.Systems.Inventory
{
	public class ItemController : MonoBehaviour
	{
		[SerializeField] private Rigidbody rigidbody;
		public Rigidbody Rigidbody => rigidbody;

		public Item Item { get; private set; }

		public void SetItem(Item item)
		{
			Item = item;
		}
	}
}