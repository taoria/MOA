using Boo.Lang;
using Code.Item.Data;
using UnityEngine;

namespace GameComp.System.MapSystem {
    [CreateAssetMenu(menuName = "GameInfo/MapInfo")]
    public class MapInfo:ScriptableObject {
        //指向的地图资源
        
        public string MapResourcesName;
        //存储的地图角色的*原始*信息.即ObjectInfo
        public CharacterPreInfo[] CharacterInfoDic;
    }
}