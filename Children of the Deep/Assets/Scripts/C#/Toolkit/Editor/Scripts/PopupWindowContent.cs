using UnityEngine;

namespace IUP.Toolkit.Editor
{
    public abstract class PopupWindowContent : UnityEditor.PopupWindowContent
    {
        public Vector2 WindowSize { get; set; }

        public override Vector2 GetWindowSize() => WindowSize;

        public override void OnGUI(Rect rect) { }
    }
}
