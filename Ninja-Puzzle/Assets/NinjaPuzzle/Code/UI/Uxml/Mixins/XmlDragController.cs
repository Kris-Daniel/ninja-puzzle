using NinjaPuzzle.Code.UI.Uxml.Components.ItemCellComponent;
using NinjaPuzzle.Code.Unity.Enums;
using NinjaPuzzle.Code.Unity.Systems.Inventory;
using UnityEngine;
using UnityEngine.UIElements;

namespace NinjaPuzzle.Code.UI.Uxml.Mixins
{
	public class XmlDragController : AXmlController
	{
		private Vector2 m_offset;
		private VisualElement m_currentItemCell;
		private ItemStack m_currentItemStack;
		private Inventory m_currentInventory;

		public XmlDragController(AXmlController parent, VisualElement xmlElement) : base(parent, xmlElement)
		{
			RegisterCallbacks();
			EventManager.OnChangeGameState += ResetIfHasCurrentDragData;
		}

		void RegisterCallbacks()
		{
			XmlElement.RegisterCallback<PointerDownEvent>(OnPointerDown);
			XmlElement.RegisterCallback<PointerMoveEvent>(OnPointerMove);
			XmlElement.RegisterCallback<PointerUpEvent>(OnPointerUp);
		}

		private void OnPointerDown(PointerDownEvent evt)
		{
			if(m_currentItemStack.ItemData) return;
			
			m_currentItemCell = ((VisualElement) evt.target).ClosestParent("item-cell");

			if (m_currentItemCell != null)
			{
				var inventoryXml = m_currentItemCell.ClosestParent("inventory");
				if (inventoryXml != null)
				{
					var inventory = UnityGameInstance.InventoryManager.Inventories[int.Parse(inventoryXml.viewDataKey)];
					m_currentInventory = inventory;
					
					m_offset = new Vector2(m_currentItemCell.layout.width * 0.25f, m_currentItemCell.layout.height * 0.75f);

					m_currentItemCell.style.width = m_currentItemCell.layout.width;
					m_currentItemCell.style.height = m_currentItemCell.layout.height;

					m_currentItemCell.AddToClassList("item-cell--drag");
					XmlElement.Insert(0, m_currentItemCell);

					m_currentItemCell.style.left = evt.position.x - m_offset.x;
					m_currentItemCell.style.top = evt.position.y - m_offset.y;
					
					m_currentItemStack = inventory.SafeUseFromIndex(int.Parse(m_currentItemCell.viewDataKey));
				}
			}
		}

		private void OnPointerMove(PointerMoveEvent evt)
		{
			if (m_currentItemCell != null)
			{
				m_currentItemCell.style.left = evt.position.x - m_offset.x;
				m_currentItemCell.style.top = evt.position.y - m_offset.y;
			}
		}

		private void OnPointerUp(PointerUpEvent evt)
		{
			if (m_currentItemCell != null)
			{
				VisualElement itemCellLanding = (VisualElement) evt.target;

				itemCellLanding = itemCellLanding.ClosestParent("item-cell-landing");
				
				if (itemCellLanding != null)
				{
					VisualElement inventoryXml = itemCellLanding.ClosestParent("inventory");

					var inventory = UnityGameInstance.InventoryManager.Inventories[int.Parse(inventoryXml.viewDataKey)];

					var remains = inventory.SafeAddToIndex(m_currentItemStack, int.Parse(itemCellLanding.viewDataKey));
					
					if (remains.Count > 0)
					{
						m_currentItemStack = remains;
						ItemCellXml.Render(m_currentItemCell, m_currentItemStack);
					}
					else
					{
						ResetDragData();
					}
				}
				else
				{
					m_currentInventory.SafeAdd(m_currentItemStack.ItemData, (uint) m_currentItemStack.Count);
					ResetDragData();
				}
			}
		}

		public void ResetIfHasCurrentDragData(EGameState gameState, EGameState prevGameState)
		{
			if (prevGameState == EGameState.GamePlayInventory)
			{
				if (m_currentItemCell != null)
				{
					m_currentInventory.SafeAdd(m_currentItemStack.ItemData, (uint) m_currentItemStack.Count);
					ResetDragData();
				}
			}
		}

		private void ResetDragData()
		{
			m_currentItemStack = new ItemStack();
			m_currentItemCell.parent.Remove(m_currentItemCell);
			m_currentItemCell = null;
			m_currentInventory = null;
		}
	}
}