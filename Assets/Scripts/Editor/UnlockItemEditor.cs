using UnityEngine;
using UnityEditor;
using ItemUnlock;


[CustomPropertyDrawer(typeof(UnlockItem))]
public class UnlockItemEditor : PropertyDrawer
{

    SerializedProperty type;
    SerializedProperty id;
    Rect t;
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        //  base.OnGUI(position, property, label);

        type = property.FindPropertyRelative("type");
        id = property.FindPropertyRelative("id");
        t = position;
        t.width = t.width / 2;
        type.enumValueIndex = (int)((UnlockType)EditorGUI.EnumPopup(t, "Type :", (UnlockType)type.enumValueIndex));

        t.x = t.width;

        EditorGUI.PropertyField(t, id);




    }
}

/*[CustomPropertyDrawer(typeof(LevelUnlock))]
public class LevelUnlockEditor : PropertyDrawer
{

    SerializedProperty level;
    SerializedProperty items;
    Rect t;
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        //  base.OnGUI(position, property, label);

        level = property.FindPropertyRelative("level");
        items = property.FindPropertyRelative("items");
        t = position;
        t.width = t.width / 2;
        EditorGUI.PropertyField(t, level);

        t.x = t.width;

        EditorGUI.PropertyField(t, items);




    }
    
     }*/
    

