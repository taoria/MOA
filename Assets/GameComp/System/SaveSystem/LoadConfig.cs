using System;
using GameComp.System.MapSystem;
using UnityEngine;

namespace GameComp.System.SaveSystem {
    [CreateAssetMenu(menuName =  "GameInfo/LoadConfig")]
    [Serializable]
    public class LoadConfig:ScriptableObject {
        public string ConfigName;
        public bool NewGame;
        public MapInfo StartingMap;
        public string SaveFilePath;
    }
}