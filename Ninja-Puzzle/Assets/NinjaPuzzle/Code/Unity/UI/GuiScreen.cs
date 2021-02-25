using NinjaPuzzle.Code.Unity.Managers;
using NinjaPuzzle.Code.Unity.Systems.Inventory;
using UnityEngine.UIElements;

namespace NinjaPuzzle.Code.Unity.UI
{
	public class GuiScreen : AUnityMonoManager
	{
		private UIDocument m_uiDocument;
		private VisualElement m_inventory;

		protected override void Awake()
		{
			base.Awake();
			m_uiDocument = GetComponent<UIDocument>();
			m_inventory = m_uiDocument.rootVisualElement.Q<VisualElement>("inventory");
			UnityGameInstance.InputManager.Events[EButtonEvent.OnInventory].Event += ToggleInventory;
		}

		private void ToggleInventory(EEventStage eventStage)
		{
			if (eventStage == EEventStage.Click)
			{
				m_inventory.ToggleInClassList("hide");
			}
		}

		public void DrawInventory(Inventory inventory)
		{
			var cells = m_inventory.Query<VisualElement>("inventory-item").ToList();
			
			for (var i = 0; i < inventory.Stacks.Length; i++)
			{
				if (inventory.Stacks[i] != null)
				{
					var itemText = cells[i].Q<TextElement>("item-text");
					itemText.text = inventory.Stacks[i].ItemData.ItemName;
					
					var itemCount = cells[i].Q<TextElement>("item-count");
					itemCount.text = inventory.Stacks[i].Count.ToString();
				}
			}
			print("Draw");
		}
	}
}