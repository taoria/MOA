using Code.Item.Data;
using UnityEngine;

public class BaseCharacter : MonoBehaviour {
    public CharacterStatus MeCharacterStatus { get; private set; }

    // Use this for initialization
    void OnEnable() {
        MeCharacterStatus = GetComponent<CharacterStatus>();
    }
    // Update is called once per frame
    private void Update() {
    }
}