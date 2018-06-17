using UnityEngine;

public class PlayerKeyboardController : MonoBehaviour {
    public delegate void MoveAction(GameObject gameObject, string[] args);


    private int _direction = -1;

    private MoveAction _moveAction;

    private NormalCharacter _thisNormalCharacter;

    // Use this for initialization
    private void Start() {
        _thisNormalCharacter = GetComponent<NormalCharacter>();
    }

    private void WhenMove() {
    }

    // Update is called once per frame
    private void FixedUpdate() {
        //Get Player Input

        var hSpeed = Input.GetAxis("Horizontal");
        if (hSpeed.Equals(0) == false) {
            if (_thisNormalCharacter.Facing != (hSpeed > 0f ? 1 : -1)) _thisNormalCharacter.Turn();
            
            _thisNormalCharacter.Walk();
        }
        else {
            _thisNormalCharacter.Stop();
        }

        var jump = Input.GetAxis("Jump");
        if (Input.GetKeyDown(KeyCode.K)) {
            _thisNormalCharacter.Fly();
        }
        if (jump != 0) {
            _thisNormalCharacter.Jump(jump);
        }
        else {
            _thisNormalCharacter.Flying(Input.GetAxis("Horizontal"),Input.GetAxis("Vertical"));
        }
        if (Input.GetAxis("Fire1")>0) {
            _thisNormalCharacter.Attack();
        }
        else {
            _thisNormalCharacter.CancelAttack();
        }
    }
}