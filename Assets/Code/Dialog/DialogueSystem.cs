using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Code.Dialog {
    [CreateAssetMenu(menuName = "GameCom/DialogueSystem")]
    public class DialogueSystem :ScriptableObject {
        private DialogPool _dialogPools;
        private Dictionary<string, DialogueNode> _dialogueNodes;
        private Dictionary<string, DialogueNodeTransefer> _dialogueNodeTransefers;
        private static DialogueSystem _instance;

        public static DialogueSystem Instance {
            get {                 
                if (_instance == null) {
                _instance = Resources.Load<DialogueSystem>("System/"+nameof(DialogueSystem));
                }
                return _instance; 
            }
            set { _instance = value; }
        }

        private void OnEnable() {
            _dialogPools = new DialogPool();
            _dialogPools.DialogueNodes = new List<DialogueNode>();
            _dialogueNodeTransefers = new Dictionary<string, DialogueNodeTransefer>();
            _dialogueNodes = new Dictionary<string, DialogueNode>();
            LoadDialogue();
        }

        private void Load() {
            LoadDialogue();
        }

        public object[] GetDefaultTransParams(string transfer) {
            switch (transfer) {
                case "Number1" :{
                    return null;
               
                }
                case "Number2": {
                    return null;
            
                }
                case "Number3": {
                    return null;
                }
                case "Number4": {
                    return null;
                }
            }

            return null;
        }
        public DialogueNode MakeTransfer(DialogueNode dialogueNode,string transferName,params  object[] objects) {
                foreach (var i in dialogueNode.NextDialogueNode) {
                    if (i.TranseferName.Equals(transferName)) {
                        //尝试获取转移条件
                        string varName = _dialogueNodeTransefers[transferName].CondtionName;
                        if (true) {
                            return _dialogueNodes[i.NodeName];
                        }
                    }
                }

            return null;

        }
        public DialogueNode GetNode(string s) {
            if(_dialogueNodes.ContainsKey(s))
                return _dialogueNodes[s];
            return null;
        }
        public void WriteDialogue() {
            StreamWriter streamWriter = new StreamWriter("Dialog.xml");
            string res=XmlUtil.Serialize(typeof(DialogPool), _dialogPools);
            streamWriter.Write(res);
            streamWriter.Close();
        
        }
        public void LoadDialogue() {
            StreamReader sr = new StreamReader("Dialog.xml");
            _dialogPools = (DialogPool) XmlUtil.Deserialize(typeof(DialogPool),sr.BaseStream);
            sr.Close();
            foreach (var  node in _dialogPools.DialogueNodes) {
                _dialogueNodes.Add(node.NodeName,node);
            }

            foreach (var transefer in _dialogPools.DialogueNodeTransefers) {
                _dialogueNodeTransefers.Add(transefer.TranseferName,transefer);
            
            }
        }
    }
}