using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

namespace NinjaPuzzle.Code.UI.Uxml.Mixins
{
	public class XmlDragController : AXmlController
	{
		private Vector2 m_offset;
		private VisualElement m_currentElement;
		private VisualElement m_initialParent;

		public XmlDragController(AXmlController parent, VisualElement xmlElement) : base(parent, xmlElement)
		{
			RegisterCallbacks();
		}

		void RegisterCallbacks()
		{
			XmlElement.RegisterCallback<PointerDownEvent>(OnPointerDown);
			XmlElement.RegisterCallback<PointerMoveEvent>(OnPointerMove);
			XmlElement.RegisterCallback<PointerUpEvent>(OnPointerUp);
		}

		private void OnPointerDown(PointerDownEvent evt)
		{
			m_currentElement = ((VisualElement) evt.target).ClosestParent("item-cell");

			if (m_currentElement != null)
			{
				m_initialParent = m_currentElement.parent;
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
				VisualElement itemCell = (VisualElement) evt.target;

				itemCell = itemCell.ClosestParent("item-cell");

				if (itemCell != null)
				{
					VisualElement inventory = itemCell.ClosestParent("inventory");
					Debug.Log(itemCell.viewDataKey + " = " + inventory.viewDataKey);
				}

				m_initialParent.Add(m_currentElement);
				m_currentElement.style.left = 0;
				m_currentElement.style.top = 0;
				m_currentElement.RemoveFromClassList("item-cell--drag");
				m_currentElement = null;
			}
		}
	}
}