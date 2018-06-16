using System.Collections.Generic;
using UnityEngine;
//废掉的代码
public interface KeyInputReciver:IComparer<KeyInputReciver> {
    void OnKeyDown(KeyCode keyCode);
    void OnKeyUp(KeyCode keyCode);
    void OnKey(KeyCode keyCode);
    int priority { get; set; }

}
public class InputSystem:MonoBehaviour {
    private List<KeyInputReciver> _list;
        
    public void Start() {
        _list=  new List<KeyInputReciver>();    
    }
    private void Update() {
    }
}