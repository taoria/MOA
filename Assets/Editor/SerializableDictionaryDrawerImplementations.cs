using Code.Item.Data;
using UnityEngine;
using UnityEngine.UI;
 
using UnityEditor;
/// <summary>
///
/// copy from wiki
/// 
/// </summary>
/// <see cref="http://wiki.unity3d.com/index.php/SerializableDictionary"/>
/// <typeparam name="K"></typeparam>
/// <typeparam name="V"></typeparam>
// ---------------
//  String => Int
// ---------------
[UnityEditor.CustomPropertyDrawer(typeof(StringIntDictionary))]
public class StringIntDictionaryDrawer : SerializableDictionaryDrawer<string, int> {
    protected override SerializableKeyValueTemplate<string, int> GetTemplate() {
        return GetGenericTemplate<SerializableStringIntTemplate>();
    }
}
internal class SerializableStringIntTemplate : SerializableKeyValueTemplate<string, int> {}
 
// ---------------
//  GameObject => Float
// ---------------
[UnityEditor.CustomPropertyDrawer(typeof(GameObjectFloatDictionary))]
public class GameObjectFloatDictionaryDrawer : SerializableDictionaryDrawer<GameObject, float> {
    protected override SerializableKeyValueTemplate<GameObject, float> GetTemplate() {
        return GetGenericTemplate<SerializableGameObjectFloatTemplate>();
    }
}
internal class SerializableGameObjectFloatTemplate : SerializableKeyValueTemplate<GameObject, float> {}

[UnityEditor.CustomPropertyDrawer(typeof(CharacterDic))]
public class CharacterDicDrawer : SerializableDictionaryDrawer<string, string> {
    protected override SerializableKeyValueTemplate<string,string> GetTemplate() {
        return GetGenericTemplate<SerializableCharacterDicTemplate>();
    }
}
internal class SerializableCharacterDicTemplate : SerializableKeyValueTemplate<string, string> {}