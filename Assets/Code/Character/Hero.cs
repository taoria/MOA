using UnityEngine;

namespace Code.Character {
    public class Hero :NormalCharacter {
        public GameObject Effect;
        public override object StopFlying(params object[] objects) {
            base.StopFlying();
            return null;
        }

        private new void Start() {
            base.Start();
            Effect.SetActive(false);
            this.castingFire += CastingFire;
            go  = Resources.Load<GameObject>("Entity/" + "BlueFire");
        }

        public override void Fly() {
            MeCharacterStatus.CostPsi = !MeCharacterStatus.CostPsi;
            if (MeCharacterStatus.CostPsi == true) {
                this.gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
                Effect.SetActive(true);
            }
            else {
                this.gameObject.GetComponent<Rigidbody2D>().gravityScale = 1;
                Effect.SetActive(false);
            }
        }

        private GameObject go;
        public object CastingFire() {
            
            GameObject goo=GameObject.Instantiate(go);
            goo.transform.position = this.transform.position;
            goo.GetComponent<BlueFire>().shoot = this;
            goo.GetComponent<Rigidbody2D>().velocity = new Vector2(MeCharacterStatus.Int*0.5f*_facing,0);
            return goo;
        }
    }
}