using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
public class NormalCharacter : BaseCharacter {
    protected int _facing { get; set; }
    public int Facing {
        get { return _facing; }
    }
    public bool Chatting = false;
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

    protected void Start() {
        Friends = new Dictionary<string, float>();
        MeCharacterStatus.Con = 8;
        MeCharacterStatus.Wis = 8;
        MeCharacterStatus.Int = 8;
        MeCharacterStatus.Str = 8;
        MeCharacterStatus.Hit = this.MeCharacterStatus.Str*3 + this.MeCharacterStatus.Con*5;
        MeCharacterStatus.ObjectName = gameObject.name;
        MeCharacterStatus.WhenPsiZero += StopFlying;
       
        Friends = new Dictionary<string, float>();
    }

    public virtual object StopFlying(params object[] objects) {
        Fly();
        return null;
    }
    
    public virtual void Fly() {
        MeCharacterStatus.CostPsi = !MeCharacterStatus.CostPsi;
        if (MeCharacterStatus.CostPsi == true) {
            this.gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
        }
        else {
            this.gameObject.GetComponent<Rigidbody2D>().gravityScale = 1;
        }
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

    public bool Landing = true;
    public object Jump(float JumpScale) {
        Rigidbody2D r2 = this.gameObject.GetComponent<Rigidbody2D>();
        if (MeCharacterStatus.CostPsi ) {
            this.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(this.gameObject.GetComponent<Rigidbody2D>().velocity.x,MeCharacterStatus.Jump*JumpScale);

        }
        else {
            if (Landing) {
                r2.AddForce(MeCharacterStatus.Jump*new Vector2(0,25*JumpScale));
            }
        }
        return null;
    }

    public object Flying(float x,float y) {
        Rigidbody2D r2 = this.gameObject.GetComponent<Rigidbody2D>();
        if (MeCharacterStatus.CostPsi) {
            this.gameObject.GetComponent<Rigidbody2D>().velocity =
                new Vector2(r2.velocity.x, y*MeCharacterStatus.Jump);
        }

        return null;
    }

    private bool attacking = false;
    public IEnumerator AttackTrying(float time) {
        var characterAnimator = gameObject.GetComponent<Animator>();
        casting = true;
        characterAnimator.SetInteger("attacking", 1);
        characterAnimator.speed = 0.66f / time; 
        yield return  new WaitForSeconds(time);
        if (attacking == true && MeCharacterStatus.PsiCurrent-MeCharacterStatus.PsiCostRate>=0) {
            CastFire();
            MeCharacterStatus.PsiCurrent -= MeCharacterStatus.PsiCostRate;
        }
        else {
            characterAnimator.speed = 1;
            characterAnimator.SetInteger("attacking", 0);
            attacking = false;
        }

        casting = false;
    }

    public delegate object OnCastingFire();

    public OnCastingFire castingFire;
    private bool dead=false;

    public object CastFire() {
        return castingFire();
    
    }

    private bool casting = false;
    public object Attack() {
        float f = MeCharacterStatus.AttackDura;
        attacking = true;
        if(casting==false)
            StartCoroutine(AttackTrying(f));
        return null;
    }

    public void Update() {
    }

    public void CancelAttack() {
        var characterAnimator = gameObject.GetComponent<Animator>();
        attacking = false;
        characterAnimator.speed = 1;
        characterAnimator.SetInteger("attacking", 0);
    }

    public void Hurt(float f) {
        MeCharacterStatus.Hit -= f;
        if (MeCharacterStatus.Hit < 0) {
            this.dead = true;
        }
        
    }
}