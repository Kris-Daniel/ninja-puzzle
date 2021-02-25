using NinjaPuzzle.Code.Unity.Managers;
using NinjaPuzzle.Code.Unity.Systems.Inventory;
using UnityEngine.UIElements;

namespace NinjaPuzzle.Code.UI.Uxml.Pages.GuiPage
{
	public class GuiPageManager : AUnityMonoManager
	{
		private UIDocument m_uiDocument;
		private GuiPageXml m_guiPageXml;

		protected override void Awake()
		{
			base.Awake();
			m_uiDocument = GetComponent<UIDocument>();
			/*UnityGameInstance.InputManager.Events[EButtonEvent.OnInventory].Event += ToggleInventory;*/
		}

		private void Start()
		{
			m_guiPageXml = new GuiPageXml(null, m_uiDocument.rootVisualElement.Q("gui-page"), this);
		}

		public void DrawInventory(Inventory inventory)
		{
			/*var cells = m_inventory.Query<VisualElement>("inventory-item").ToList();

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

			print("Draw");*/
		}
	}
}