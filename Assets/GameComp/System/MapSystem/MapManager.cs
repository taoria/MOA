using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

namespace GameComp.System.MapSystem {
    [CreateAssetMenu(menuName = "GameCom/MapManager")]
    public class MapManager:ScriptableObject {
        public GameObject currentTerrain;
        private static MapManager _instance;
        public static MapManager Instance { 		get {
                if (_instance == null) {
                    _instance = Resources.Load<MapManager>("System/"+nameof(MapManager));
                }
                return _instance;
            }
            set { _instance = value; } }
    
        private void OnEnable() {
            if(Instance==null)
             Instance = this;
        }

        private void Awake() {
            if(Instance==null)
                Instance = this;
        }
        
        public IEnumerator LoadMap(MapInfo mapInfo) {
            var terrain = Resources.LoadAsync("Maps/" + mapInfo.MapResourcesName+"/Map");
    
            terrain.completed += (x) => {
                Debug.Log("test");
                currentTerrain = (x as ResourceRequest)?.asset as GameObject; DontDestroyOnLoad(currentTerrain);
            };
            yield return terrain;
        }
        public void ShowMap() {
            Debug.Log(SceneManager.GetActiveScene().name);
            GameObject go = GameObject.Instantiate(currentTerrain);
            GameObject map  = GameObject.Find("Map");
            GameObject.Destroy(map);
            go.name = "Map";
            go.transform.parent = GameObject.Find("Terrain").transform;
   
        }
    }
}