using System.Linq;
using NinjaPuzzle.Code.UI.Uxml.Components.InventoryComponent;
using NinjaPuzzle.Code.UI.Uxml.Components.ItemCellComponent;
using NinjaPuzzle.Code.UI.Uxml.Mixins;
using NinjaPuzzle.Code.Unity.Player;
using UnityEngine;
using UnityEngine.UIElements;

namespace NinjaPuzzle.Code.UI.Uxml.Pages.GuiPage
{
	public class GuiPageXml : PageXml
	{
		public GuiPageManager GuiPageManager { get; private set; }
		public InventoryXml InventoryXml { get; private set; }
		
		public GuiPageXml(AXmlController parent, VisualElement xmlElement, GuiPageManager guiPageManager) : base(parent, xmlElement)
		{
			GuiPageManager = guiPageManager;
			
			InventoryXml = new InventoryXml(this, xmlElement.Q("inventory"));
			InventoryXml.SetData(UnityGameInstance.GetUnityMonoManager<PlayerController>().Inventory);
			RegisterCallbacks();
		}
		
		
		void RegisterCallbacks()
		{
			XmlElement.RegisterCallback<PointerDownEvent>(OnPointerDown);
			XmlElement.RegisterCallback<PointerMoveEvent>(OnPointerMove);
			XmlElement.RegisterCallback<PointerUpEvent>(OnPointerUp);
		}

		private Vector2 startPos;
		private Vector2 m_offset;
		private VisualElement m_currentElement;
		private VisualElement m_initialParent;
		
		private void OnPointerDown(PointerDownEvent evt)
		{
			m_currentElement = ((VisualElement) evt.target).ClosestParent("item-cell");
			
			if (m_currentElement != null)
			{
				m_initialParent = m_currentElement.parent;
				startPos = evt.position;
				m_offset = new Vector2(m_currentElement.layout.width * 0.25f, m_currentElement.layout.height * 0.75f);
				
				m_currentElement.style.width = m_currentElement.layout.width;
				m_currentElement.style.height = m_currentElement.layout.height;
				
				m_currentElement.AddToClassList("item-cell--drag");
				m_currentElement.GetRootElement().Insert(0, m_currentElement);
				
				m_currentElement.style.left = evt.position.x - m_offset.x;
				m_currentElement.style.top = evt.position.y - m_offset.y;
			}
		}

		private void OnPointerMove(PointerMoveEvent evt)
		{
			if (m_currentElement != null)
			{
				m_currentElement.style.left = evt.position.x - m_offset.x;
				m_currentElement.style.top = evt.position.y - m_offset.y;
			}
		}

		private void OnPointerUp(PointerUpEvent evt)
		{
			if (m_currentElement != null)
			{
				var currentTarget = (VisualElement) evt.target;

				currentTarget = currentTarget.ClosestParent("item-cell");

				var text = "NotFound ";

				if (currentTarget != null)
				{
					text = "";
					var cl = currentTarget.GetClasses().ToList();
				
					foreach (string s in cl)
					{
						text += s + " ";
					}
				}

				m_initialParent.Add(m_currentElement);
				
				m_currentElement.style.left = 0;
				m_currentElement.style.top = 0;
				m_currentElement.RemoveFromClassList("item-cell--drag");

				Debug.Log(currentTarget.parent.viewDataKey);
				
				m_currentElement = null;
			}
		}
	}
}