using System.Collections.Generic;
using NinjaPuzzle.Code.UI.Uxml.Components.ItemCellComponent;
using NinjaPuzzle.Code.UI.Uxml.Mixins;
using NinjaPuzzle.Code.Unity.Managers;
using NinjaPuzzle.Code.Unity.Systems.Inventory;
using UnityEngine;
using UnityEngine.UIElements;

namespace NinjaPuzzle.Code.UI.Uxml.Components.InventoryComponent
{
	public class InventoryXml : AXmlController
	{
		public Inventory Inventory { get; private set; }
		
		public List<ItemCellXml> ItemCells { get; private set; } = new List<ItemCellXml>();
		
		public InventoryXml(AXmlController parent, VisualElement xmlElement) : base(parent, xmlElement)
		{
			XmlElement.AddToClassList("hide");
			UnityGameInstance.InputManager.Events[EButtonEvent.OnInventory].Event += ToggleInventory;
			
			//Init item cells
			var visualItemCells = XmlElement.Query<VisualElement>("item-cell").ToList();
			foreach (var visualItemCell in visualItemCells)
			{
				ItemCells.Add(new ItemCellXml(this, visualItemCell));
			}
			
			RegisterCallbacks();
		}

		private void ToggleInventory(EEventStage eventStage)
		{
			if (eventStage == EEventStage.Down)
			{
				XmlElement.ToggleInClassList("hide");
			}
		}

		public void SetData(Inventory inventory)
		{
			Inventory = inventory;
			Inventory.OnChange += SetItemCellsData;
			SetItemCellsData();
		}

		void SetItemCellsData()
		{
			for (int i = 0; i < Inventory.Stacks.Length; i++)
			{
				if (Inventory.Stacks[i] != null)
				{
					ItemCells[i].SetData(Inventory.Stacks[i].ItemData.ItemName, Inventory.Stacks[i].Count);
				}

				ItemCells[i].Index = i;
			}
		}
		
		void RegisterCallbacks()
		{
			XmlElement.RegisterCallback<PointerDownEvent>(OnPointerDown);
			XmlElement.RegisterCallback<PointerMoveEvent>(OnPointerMove);
			XmlElement.RegisterCallback<PointerUpEvent>(OnPointerUp);
		}

		private VisualElement m_currentItemCell;
		private Vector2 m_dragStartPos;
		
		private void OnPointerDown(PointerDownEvent evt)
		{
			var target = (VisualElement) evt.target;

			m_currentItemCell = target.ClosestParent("item-cell");

			m_currentItemCell?.AddToClassList("item-cell--drag");

			m_dragStartPos = evt.position;
		}

		private void OnPointerMove(PointerMoveEvent evt)
		{
			if (m_currentItemCell != null)
			{
				m_currentItemCell.style.left = evt.position.x - m_dragStartPos.x;
				m_currentItemCell.style.top = evt.position.y - m_dragStartPos.y;
			}
		}

		private void OnPointerUp(PointerUpEvent evt)
		{
			if (m_currentItemCell != null)
			{
				m_currentItemCell.style.left = 0;
				m_currentItemCell.style.top = 0;
				m_currentItemCell?.RemoveFromClassList("item-cell--drag");
				m_currentItemCell = null;
			}
		}
	}
}