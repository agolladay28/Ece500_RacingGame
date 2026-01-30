using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class hud_ui : EditorWindow
{
    public float speed = 0;
    [SerializeField]
    private VisualTreeAsset m_VisualTreeAsset = default;

    [MenuItem("Window/UI Toolkit/hud_ui")]
    public static void ShowExample()
    {
        hud_ui wnd = GetWindow<hud_ui>();
        wnd.titleContent = new GUIContent("hud_ui");
    }

    public void CreateGUI()
    {
        // Each editor window contains a root VisualElement object
        VisualElement root = rootVisualElement;

        // VisualElements objects can contain other VisualElement following a tree hierarchy.
        VisualElement label = new Label("Hello World! From C#");
        root.Add(label);

        // Instantiate UXML
        VisualElement labelFromUXML = m_VisualTreeAsset.Instantiate();
        root.Add(labelFromUXML);
    }
}
