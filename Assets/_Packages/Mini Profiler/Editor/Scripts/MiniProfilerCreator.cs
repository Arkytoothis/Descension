using UnityEditor;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace kleberswf.tools.miniprofiler.editor {
	public class MiniProfilerCreator {
		private const int DefaultTextHeight = 12;
		private static readonly Vector2 DefaultSize = new Vector2(200, 64);

		[MenuItem("GameObject/UI/Mini Profiler", menuItem = "GameObject/UI/Mini Profiler/Framerate Watcher", priority = 9990)]
		private static void CreateFrameratePanel() {
			var valueProvider = CreatePanel<FramerateValueProvider>("Framerate Watcher", Selection.activeGameObject);
			valueProvider.NumberFormat = "#,##0.0";
			valueProvider.Title = "FPS";
		}

		[MenuItem("GameObject/UI/Mini Profiler", menuItem = "GameObject/UI/Mini Profiler/Memory Watcher", priority = 9991)]
		private static void CreateMemoryPanel() {
			var valueProvider = CreatePanel<MemoryValueProvider>("Memory Watcher", Selection.activeGameObject);
			valueProvider.NumberFormat = "###,##0,K";
			valueProvider.Title = "Memory";
		}

		public static Canvas CreateNewUI() {
			// Root for the UI
			var root = new GameObject("Canvas") { layer = LayerMask.NameToLayer("UI") };
			var canvas = root.AddComponent<Canvas>();
			canvas.renderMode = RenderMode.ScreenSpaceOverlay;
			root.AddComponent<CanvasScaler>();
			root.AddComponent<GraphicRaycaster>();
			Undo.RegisterCreatedObjectUndo(root, "Create " + root.name);
			return canvas;
		}

		private static void CreateEventSystemIfNeeded() {
			if (Object.FindObjectOfType<EventSystem>()) return;
			var eventSystem = new GameObject("EventSystem");
			eventSystem.AddComponent<EventSystem>();
			eventSystem.AddComponent<StandaloneInputModule>();
			Undo.RegisterCreatedObjectUndo(eventSystem, "Create " + eventSystem.name);
		}

		private static T CreatePanel<T>(string name, GameObject selection) where T : AbstractValueProvider {
			Canvas canvas = null;
			if (selection) canvas = selection.GetComponentInParent<Canvas>();
			if (!canvas) canvas = Object.FindObjectOfType<Canvas>();
			if (!canvas) canvas = CreateNewUI();
			CreateEventSystemIfNeeded();

			var root = new GameObject(name);
			GameObjectUtility.SetParentAndAlign(root, selection ?? canvas.gameObject);

			var image = root.AddComponent<Image>();
			image.rectTransform.sizeDelta = MiniProfiler.DefaultSize;

			var t = (RectTransform)root.transform;
			t.anchorMin = new Vector2(0, 1);
			t.anchorMax = new Vector2(0, 1);
			t.pivot = new Vector2(0, 1);
			t.sizeDelta = DefaultSize;
			t.anchoredPosition = Vector2.zero;

			var textGO = new GameObject("Text");
			GameObjectUtility.SetParentAndAlign(textGO, root);
			var text = textGO.AddComponent<Text>();
			text.color = Color.white;
			text.fontSize = 10;
			text.supportRichText = true;
			text.font = Resources.Load<Font>("VisitorTT1Extended");
			text.alignment = TextAnchor.MiddleLeft;
			t = text.rectTransform;
			t.anchorMin = Vector2.zero;
			t.anchorMax = new Vector2(1, 0);
			t.anchoredPosition = new Vector2(0, DefaultTextHeight * 0.5f);
			t.sizeDelta = new Vector2(-8, DefaultTextHeight);

			var provider = root.AddComponent<T>();

			var profiler = root.AddComponent<MiniProfiler>();
			profiler.Text = text;
			profiler.Image = image;
			profiler.ShowText = true;

			// ensure the order
			ComponentUtility.MoveComponentDown(provider);
			Selection.activeGameObject = root;

			return provider;
		}
	}
}