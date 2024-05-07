using UnityEditor;

namespace IUP.Toolkit.Editor
{
    public static class ExtensionSerializedProperty
    {
        public static SerializedProperty InsertArrayElementAt(this SerializedProperty @this, int index)
        {
            @this.InsertArrayElementAtIndex(index);
            return @this.GetArrayElementAtIndex(index);
        }

        public static SerializedProperty InsertArrayElemenetAtEnd(this SerializedProperty @this)
        {
            int index = @this.arraySize;
            @this.InsertArrayElementAtIndex(index);
            return @this.GetArrayElementAtIndex(index);
        }
    }
}
