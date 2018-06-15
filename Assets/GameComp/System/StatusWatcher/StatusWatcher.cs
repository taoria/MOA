using Code.Item.Data;
using UnityEngine;

public class StatusWatcher : BaseWatcher {
    public GameObject targetObject;

    public CharacterStatus CharacterStatus { get; private set; }


    // Use this for initialization
    private void Start() {
        base.Start();
        if (targetObject == null) Debug.LogError("Warning No Target bind to this StatusWatcher");
        CharacterStatus = targetObject.GetComponent<BaseCharacter>().MeCharacterStatus;
        
    }
}