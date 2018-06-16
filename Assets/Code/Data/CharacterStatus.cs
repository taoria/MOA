using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Object = System.Object;

namespace Code.Item.Data {
    public class CharacterStatus : MonoBehaviour {
        //真实数据的数据实体
        public CharacterStatusData _characterStatusData = new CharacterStatusData();
        //保存人物数据给存档文件
        public void SaveToSaveData(SaveData saveData) {
            if (ObjectName == null || ObjectName.Equals("")) {
                ObjectName = this.GetHashCode().ToString();
            }
            saveData._objectStatuseDataDic[ObjectName] = this._characterStatusData;
        }
        //从人物信息中生成人物属性.
        public void LoadFromCharacterInfo(CharacterInfo characterInfo) {
            foreach (var i in characterInfo.CharacterDic.dictionary) {
                SetValue(i.Key,i.Value);
            }
        }
        //从存档中生成人物属性
        public void LoadFromSaveData(SaveData saveData) {
            if (ObjectName == null || ObjectName.Equals("")) {
                ObjectName = this.GetHashCode().ToString();
            }
            if (saveData._objectStatuseDataDic.ContainsKey(ObjectName)) {
                this._characterStatusData = saveData._objectStatuseDataDic[ObjectName];
            }
        }
        //对人物属性的代理
        public float Cha {
            get { return _characterStatusData.Cha; }
            set { this._characterStatusData.Cha = value; }
        }
        public float Con {
            get { return _characterStatusData.Con; }
            set { this._characterStatusData.Con = value; }
        }
        public float Dex {
            get { return _characterStatusData.Dex; }
            set { this._characterStatusData.Dex = value; }
        }
        public float Int {
            get { return _characterStatusData.Int; }
            set { this._characterStatusData.Int = value; }
        }
        public string ObjectName {
            get { return _characterStatusData.ObjectName; }
            set { this._characterStatusData.ObjectName = value; }
        }
        public float Str {
            get { return _characterStatusData.Str; }
            set { this._characterStatusData.Str = value; }
        }
        public float Wis  {
            get { return _characterStatusData.Wis; }
            set { this._characterStatusData.Wis = value; }
        }
        public float Hit  {
            get { return _characterStatusData.Hit; }
            set { this._characterStatusData.Hit = value; }
        }
        public float FreePoint {
            get { return _characterStatusData.FreePoint; }
            set { this._characterStatusData.FreePoint = value; }
        }
        
        public float Psi {
            get { return _characterStatusData.Wis +_characterStatusData.Int; }
        }
        public float BaseHitPoint {
            get { return Str + Con; }
        }
        //the Speed of A Character is decided to be 2 * log10(dex+1)
        public float Speed {
            get { return 1.5f * UnityMath.Sigmoid(Dex-8); }
        }

        public float HitMax{
            get { return Con*5 + Str*3; }
        }
        public float Jump {
            get { return Mathf.Log10(Con * Str+100); }
        }

        //设置某个属性的值,目前仅支持对浮点值的修改
        public void SetValue(string valueName,object value) {
            try {
                float f = float.Parse(value.ToString());
                Debug.Log(valueName);
                if (valueName != null) GetType().GetProperty(name: valueName).SetValue(this, f, null);
            }
            catch (Exception e) {
                Debug.Log("Can't find property:" + valueName);
                throw;
            }
        }
        //获取某个属性的值
        public object GetValue(string valueName) {
            return GetType().GetProperty(valueName).GetValue(this, null);
        }
        //增加某个属性的值
        public void Add(string valueName, float amount) {
            SetValue(valueName,(float)GetValue(valueName)+amount);
        }
        //加点用
        public void AddPoint(string valueName) {
            if ((float)GetValue("FreePoint") > 0) {
                Add(valueName,1);
                Add("FreePoint",-1);
            }
        }
        // Use this for initialization
        private void Start() {
            FreePoint = 10;
            Hit = HitMax;
        }

        // Update is called once per frame
        private void Update() {
        }
    }
    [Serializable]
    public class CharacterStatusData {
        public float Cha { get; set; } = 8;
        public float Con { get; set; } = 8;
        public float Dex { get; set; } = 8;
        public float Int { get; set; } = 8;
        public string ObjectName { get; set; } = "default";
        public float Str { get; set; } = 8;
        public float Wis { get; set; } = 8;
        public float Hit { get; set; }
        public float FreePoint { get; set; } = 0;
        public string CharacterScriptType { get; set; } = "BaseCharacter";
        public string CharacterOutlook = "BlackMan";
    }
}