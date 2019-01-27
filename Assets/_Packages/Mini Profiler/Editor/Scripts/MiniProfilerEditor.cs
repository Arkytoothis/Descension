using System.Collections;
using System.Reflection;
using kleberswf.tools.util;
using UnityEditor;
using UnityEngine;

namespace kleberswf.tools.miniprofiler {
	[CustomEditor(typeof(MiniProfiler))]
	[CanEditMultipleObjects]
	public class MiniProfilerEditor : Editor {
		private SerializedProperty _readIntervalProperty;
		private SerializedProperty _imageProperty;
		private SerializedProperty _textProperty;

		private SerializedProperty _collapsedProperty;
		private SerializedProperty _touchToggleCollapsedProperty;
		private SerializedProperty _showTextProperty;
		private SerializedProperty _colorizeTextProperty;

		private SerializedProperty _backgroundColorProperty;
		private SerializedProperty _avgValueColorProperty;
		private SerializedProperty _maxValueColorProperty;
		private SerializedProperty _minValueColorProperty;
		private SerializedProperty _titleColorProperty;

		private void OnEnable() {
			_readIntervalProperty = serializedObject.FindProperty("ReadInterval");
			_imageProperty = serializedObject.FindProperty("Image");
			_textProperty = serializedObject.FindProperty("Text");

			_collapsedProperty = serializedObject.FindProperty("_collapsed");
			_touchToggleCollapsedProperty = serializedObject.FindProperty("TouchToggleCollapsed");
			_showTextProperty = serializedObject.FindProperty("_showText");
			_colorizeTextProperty = serializedObject.FindProperty("_colorizeText");

			_backgroundColorProperty = serializedObject.FindProperty("BackgroundColor");
			_avgValueColorProperty = serializedObject.FindProperty("_averageValueColor");
			_maxValueColorProperty = serializedObject.FindProperty("_maxValueColor");
			_minValueColorProperty = serializedObject.FindProperty("_minValueColor");
			_titleColorProperty = serializedObject.FindProperty("_titleColor");
		}

		public override void OnInspectorGUI() {
			serializedObject.Update();

			EditorGUILayout.PropertyField(_readIntervalProperty);
			if (serializedObject.isEditingMultipleObjects) GUI.enabled = false;
			EditorGUILayout.PropertyField(_imageProperty);
			EditorGUILayout.PropertyField(_textProperty);
			GUI.enabled = true;

			GUILayout.Space(5);
			var updateText = false;
			var updateTexture = false;

			string updatePropertyName = null;
			SerializedProperty updateProperty = null;

			EditorGUI.BeginChangeCheck();
			EditorGUILayout.PropertyField(_collapsedProperty);
			if (EditorGUI.EndChangeCheck()) {
				updatePropertyName = "Collapsed";
				updateProperty = _collapsedProperty;
			}

			EditorGUILayout.PropertyField(_touchToggleCollapsedProperty);

			EditorGUI.BeginChangeCheck();
			EditorGUILayout.PropertyField(_showTextProperty);
			if (EditorGUI.EndChangeCheck()) {
				updatePropertyName = "ShowText";
				updateProperty = _showTextProperty;
			}

			EditorGUI.BeginChangeCheck();
			EditorGUILayout.PropertyField(_colorizeTextProperty);
			if (EditorGUI.EndChangeCheck()) updateText = true;

			GUILayout.Space(5);

#if UNITY_5
			if (CheckDrop()) {
				updateText = true;
				updateTexture = true;
			}
#endif

			EditorGUI.BeginChangeCheck();
			EditorGUI.BeginChangeCheck();
			EditorGUILayout.PropertyField(_backgroundColorProperty);
			if (EditorGUI.EndChangeCheck()) updateTexture = true;

			EditorGUILayout.PropertyField(_titleColorProperty);
			EditorGUILayout.PropertyField(_minValueColorProperty);
			EditorGUILayout.PropertyField(_avgValueColorProperty);
			EditorGUILayout.PropertyField(_maxValueColorProperty);

			if (EditorGUI.EndChangeCheck()) updateText = true;

			serializedObject.ApplyModifiedProperties();

			if (updateProperty != null) UpdateBoolProperty(updatePropertyName, updateProperty.boolValue);
			if (updateTexture) UpdateTexture(_backgroundColorProperty.colorValue);
			if (updateText) UpdateText();
		}

		private void UpdateBoolProperty(string propertyName, bool value) {
			foreach (var o in targets) {
				var property = o.GetType()
					.GetProperty(propertyName, BindingFlags.Public | BindingFlags.Instance | BindingFlags.SetProperty);
				if (property == null) return;
				property.SetValue(o, value, null);
			}
		}

		private void UpdateTexture(Color32 value) {
			foreach (var o in targets) {
				var p = o as MiniProfiler;
				if (!p) continue;
				Texture2DUtil.Clear((Texture2D)p.Image.mainTexture, value);
				EditorUtility.SetDirty(p.Image.mainTexture);
			}
		}

		private void UpdateText() {
			foreach (var o in targets) {
				var p = o as MiniProfiler;
				if (!p) continue;
				p.SetColorDirty();
				p.UpdateTextLine();
				if (p.Text != null) EditorUtility.SetDirty(p.Text);
			}
		}


#if UNITY_5
		private Color32[] _colors;
		private bool _dragExited;

		private bool CheckDrop() {
			var t = Event.current.type;
			if (t == EventType.DragUpdated) {
				DragAndDrop.visualMode = DragAndDropVisualMode.Move;
				return false;
			}

			if (_dragExited) {
				if (t == EventType.Repaint) UpdateColors();
				if (t == EventType.ValidateCommand) {
					UpdateColors();
					_colors = null;
					_dragExited = false;
				}
				return true;
			}

			if (t != EventType.DragExited) return false;
			var drops = DragAndDrop.objectReferences;
			if (drops.Length == 0) return false;
			_dragExited = true;

			foreach (var drop in drops) {
				if (!string.Equals(drop.GetType().FullName, "UnityEditor.ColorPresetLibrary"))
					continue;
				var field = drop.GetType().GetField("m_Presets",
					BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.NonPublic);
				if (field == null) continue;
				var presets = field.GetValue(drop) as IList;
				if (presets == null) continue;
				if (presets.Count < 5) return false;

				_colors = new Color32[5];
				for (var i = 0; i < 5; i++) {
					var preset = presets[i];
					var f = preset.GetType()
						.GetField("m_Color", BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.NonPublic);
					if (f == null) break;
					_colors[i] = (Color)f.GetValue(preset);
				}
				return true;
			}

			return false;
		}

		private void UpdateColors() {
			if (_colors == null || _colors.Length == 0) return;
			_backgroundColorProperty.colorValue = _colors[0];
			_titleColorProperty.colorValue = _colors[1];
			_minValueColorProperty.colorValue = _colors[2];
			_avgValueColorProperty.colorValue = _colors[3];
			_maxValueColorProperty.colorValue = _colors[4];
		}

#endif
	}
}