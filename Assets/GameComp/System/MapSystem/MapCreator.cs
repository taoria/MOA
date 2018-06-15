using UnityEngine;
using UnityEngine.Tilemaps;

namespace GameComp.System.MapSystem {
	public class MapCreator : MonoBehaviour {
	
		private Tilemap _tilemap;
		public void SaveMap() {
			_tilemap  = this.GetComponent<Tilemap>();
		}
		// Use this for initialization
		void Start () {
		
		}
	
		// Update is called once per frame
		void Update () {
		
		}
	}
}
