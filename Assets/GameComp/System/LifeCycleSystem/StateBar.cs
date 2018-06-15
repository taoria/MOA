using UnityEngine;
using UnityEngine.UI;

namespace GameComp.System.LifeCycleSystem {
    public class StateBar :MonoBehaviour{
        public ScriptableObject stateMachine;
        public string WatchName;
        public void Update() {
            float value = (float) stateMachine.GetType().GetProperty(WatchName).GetValue(stateMachine, null) ;
            this.GetComponent<Image>().fillAmount = value;
    
            GameObject.Find(WatchName+"State").GetComponent<Text>().text = (string) stateMachine.GetType().GetProperty(WatchName + "State").GetValue(stateMachine, null);
        }
    }
}