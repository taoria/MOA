﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;
using UnityEngine.Networking;

//对话节点通过对话转移器开始转移.使用xml文件能比
 
/// <summary>
/// Xml序列化与反序列化
/// </summary>
///
/// 
public class XmlUtil
{
    #region 反序列化
    /// <summary>
    /// 反序列化
    /// </summary>
    /// <param name="type">类型</param>
    /// <param name="xml">XML字符串</param>
    /// <returns></returns>
    public static object Deserialize(Type type, string xml)
    {
        try
        {
            using (StringReader sr = new StringReader(xml))
            {
                XmlSerializer xmldes = new XmlSerializer(type);
                return xmldes.Deserialize(sr);
            }
        }
        catch (Exception e)
        {
 
            return null;
        }
    }
    /// <summary>
    /// 反序列化
    /// </summary>
    /// <param name="type"></param>
    /// <param name="xml"></param>
    /// <returns></returns>
    public static object Deserialize(Type type, Stream stream)
    {
        XmlSerializer xmldes = new XmlSerializer(type);
        return xmldes.Deserialize(stream);
    }
    #endregion
 
    #region 序列化
    /// <summary>
    /// 序列化
    /// </summary>
    /// <param name="type">类型</param>
    /// <param name="obj">对象</param>
    /// <returns></returns>
    public static string Serialize(Type type, object obj)
    {
        MemoryStream Stream = new MemoryStream();
        XmlSerializer xml = new XmlSerializer(type);
        try
        {
            //序列化对象
            xml.Serialize(Stream, obj);
        }
        catch (InvalidOperationException)
        {
            throw;
        }
        Stream.Position = 0;
        StreamReader sr = new StreamReader(Stream);
        string str = sr.ReadToEnd();
         
        sr.Dispose();
        Stream.Dispose();
 
        return str;
    }
 
    #endregion
}
[Serializable]
public  class DialogueNodeTransefer {
    [XmlElement]public string CondtionName;
    [XmlAttribute]public string TranseferName;
   
    [XmlElement]public string paramsMaker;
}
[Serializable]
public class DialogueNode {
    public delegate object[] ParamsMaker();
    [XmlArrayItem] public List<string> Words { get; set; }
    [XmlArrayItem] public List<DialogPair> NextDialogueNode { get; set; }
    [XmlAttribute] public string DialogType = "Default";
    [XmlAttribute] public string NodeName;

}

[Serializable]
public class DialogPair {
    [XmlAttribute]public string TranseferName;
    [XmlAttribute]public string NodeName;
    [XmlAttribute] public int Priority;
    public DialogPair(string transeferName, string nodeName) {
        TranseferName = transeferName;
        NodeName = nodeName;
    }

    public DialogPair() {
        Priority = 0;
    }
}
[XmlRoot("DialogPool")]
public class DialogPool {
    [XmlArrayItem]public List<DialogueNode> DialogueNodes { get; set; }
    [XmlArrayItem]public List<DialogueNodeTransefer> DialogueNodeTransefers { get; set; }
}
