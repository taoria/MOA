using System.Collections.Generic;
using UnityEngine;
public class NormalCharacter : BaseCharacter {
    private int _facing { get; set; }
    public int Facing {
        get { return _facing; }
    }
    public Dictionary<string, float> Friends;
    public object Turn() {
        if (_facing == -1) return _facing = 1;
        return _facing = -1;
    }
    public object Walk() {
        var characterAnimator = gameObject.GetComponent<Animator>();
        characterAnimator.SetInteger("move_state", 1);
        characterAnimator.speed = MeCharacterStatus.Speed;
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(MeCharacterStatus.Speed * _facing,
            gameObject.GetComponent<Rigidbody2D>().velocity.y);
        if (characterAnimator.GetInteger("direction") != _facing)
            characterAnimator.SetInteger("direction", _facing > 0f ? 1 : -1);

        return null;
    }
    public object Stop() {
        var characterAnimator = gameObject.GetComponent<Animator>();
        characterAnimator.speed = 1;
        characterAnimator.SetInteger("move_state", 0);
        return null;
    }
    public object Run() {
        return null;
    }
    private void Start() {
        Friends = new Dictionary<string, float>();
        Debug.Log(this.MeCharacterStatus);
        MeCharacterStatus.Con = 8;
        MeCharacterStatus.Wis = 8;
        MeCharacterStatus.Int = 8;
        MeCharacterStatus.Str = 8;
        MeCharacterStatus.Hit = this.MeCharacterStatus.Str*3 + this.MeCharacterStatus.Con*5;
        MeCharacterStatus.ObjectName = gameObject.name;
    }

    public void MakeFriendWith(BaseCharacter baseCharacter) {
        if (Friends.ContainsKey(baseCharacter.name)) {
            Debug.LogError("You already know :" + baseCharacter.name);
        }
    }

    public bool KnowSomeone(BaseCharacter baseCharacter) {
        return Friends.ContainsKey(baseCharacter.name);
    }
    private void SaveCharacter() {
        
    }
    public object Jump() {
        this.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(this.gameObject.GetComponent<Rigidbody2D>().velocity.x,MeCharacterStatus.Jump);
        Debug.Log(MeCharacterStatus.Jump);
        return null;
    }
    
    public object Attack() {
        return null;
    }
}