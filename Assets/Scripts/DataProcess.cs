using UnityEngine;
using System.Collections;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Text;
using System.Security.Cryptography;
using System;

/// <summary>
/// 数据处理类，主要用于读取、存储数据
/// </summary>
public class DataProcess
{   #region Const 常量
	const string _KEY = "15678921054268205125896431518456";
	#endregion

	#region Field 字段
    static bool _isDone;
	#endregion

	#region Property 属性
	public static bool _IsDone{get{ return _isDone;}set{ _isDone = value;}}

    /// <summary>
    /// 加解密Xml数据文档
    /// </summary>
    /// <param name="xmlName"></param>
    /// <param name="isEncrypt">true加密 false解密</param>
    /// <returns>文件是否存在</returns>
    public static bool SaveXmlData(string xmlName, bool isEncrypt)
    {
        string path = GetXmlPath(xmlName);
        if (HasFile(path))
        {
            string data = LoadFile(path, !isEncrypt);
            SaveData(path, data, isEncrypt);

            return true;
        }
        else
        {// 提示文件不存在，并在控制台输出
            return false;
        }
    }

    /// <summary>
    /// 获取Xml数据文件
    /// </summary>
    /// <param name="xmlName"></param>
    /// <param name="data"></param>
    /// <param name="isDecrypt">是否解密读取</param>
    /// <returns></returns>
	public static bool GetXmlData(string xmlName, out System.Xml.XmlDocument xmlDoc, bool isDecrypt = false)
    {
        string path = GetXmlPath(xmlName);

        if (Application.platform == RuntimePlatform.Android)
        {
			xmlDoc = new System.Xml.XmlDocument();
            //Debug.Log(Resources.Load("Xml/" + xmlName.Replace(".xml", "")).ToString());
            xmlDoc.LoadXml(Resources.Load("Xml/" + xmlName.Replace(".xml", "")).ToString());
            return true;
        }

        if (HasFile(path))
        {
            string xmlData = LoadFile(path, isDecrypt);
			xmlDoc = new System.Xml.XmlDocument();
            xmlDoc.LoadXml(xmlData);

            //GameManager.GetInstance().tip.text += "读取" + path + "成功！\n";
            return true;
        }
        else
        {// 提示文件不存在，并在控制台输出
            //GameManager.GetInstance().tip.text += path + "文件不存在！\n";
            xmlDoc = null;
            return false;
        }
    }

    //创建XML文件
    public static void CreateXML(string fileName, string thisData)
    {
        string xxx = Encrypt(thisData);
        StreamWriter writer;
        writer = File.CreateText(fileName);
        writer.Write(xxx);
        writer.Close();
    }

	public static  IEnumerator WWWLoadXML(string path, System.Xml.XmlDocument xmlDoc)
    {
        WWW www = new WWW("file:///" + path);
        while (!www.isDone)
        {
            yield return www;
        }
        xmlDoc.LoadXml(www.text);
        _isDone = true;
        //GameManager.GetInstance().tip.text += www.text;
    }

    /// <summary>
    /// 把数据存入路径文件中
    /// </summary>
    /// <param name="path"></param>
    /// <param name="data"></param>
    /// <param name="isEncrypt">false</param>
    public static void SaveData(string path, string data, bool isEncrypt)
    {
        string xxx;
        if (isEncrypt)
            xxx = Encrypt(data);
        else
            xxx = data;
        StreamWriter writer;
        writer = File.CreateText(path);
        writer.Write(xxx);
        writer.Close();
    }

    /// <summary>
    /// 从路径文件中取出数据
    /// </summary>
    /// <param name="path"></param>
    /// <param name="isDecrypt"></param>
    /// <returns></returns>
   static string LoadFile(string path, bool isDecrypt)
    {
        StreamReader sReader = File.OpenText(path);
        string dataString = sReader.ReadToEnd();
        sReader.Close();
        if (isDecrypt)
            return Decrypt(dataString);
        else
            return dataString;
    }

    /// <summary>
    /// 存档文件是否存在
    /// </summary>
    public static bool IsGameDataExist(string xmlName){
        string path = GetXmlPath(xmlName);
        return HasFile(path);
    }

    /// <summary>
    /// 保存存档文件
    /// </summary>
    public static void SaveGameData(object save, System.Type ty, string xmlName, bool isEncrypt){
        string data = DataProcess.SerializeObject(save, ty);
        string path = GetXmlPath(xmlName);
        SaveData(path, data, isEncrypt);
    }


    /// <summary>
    /// 读取存档文件
    /// </summary>
    /// <returns>The game data.</returns>
    public static object LoadGameData(System.Type ty, string name, bool isDecrypt)
    {
        string path = GetXmlPath(name);
		string data = DataProcess.LoadFile(path, isDecrypt);
        object save = DataProcess.DeserializeObject(data, ty);

        return save;
    }

    /// <summary>
    /// 判断是否存在文件
    /// </summary>
    /// <param name="fileName"></param>
    /// <returns>文件是否存在</returns>
    public static bool HasFile(string fileName)
    {
        return File.Exists(fileName);
    }

    /// <summary>
    /// 获取XML文件路径
    /// </summary>
    /// <returns>XML文件路径</returns>
    public static string GetXmlPath(string xmlName)
    {
        string path = "";
        if (Application.platform == RuntimePlatform.Android)
            path = Application.streamingAssetsPath; //在Android中实例化WWW不能在路径前面加"file://"
        else
            path = Application.streamingAssetsPath;//在Windows中实例化WWW必须要在路径前面加"file:///"
        path += "/" + xmlName;
        return path;
    }

    /// <summary>
    /// 获取本地存档XML路径
    /// </summary>
    /// <returns>本地存单XML路径</returns>
    static string GetLocalSavePath()
    {
        string path = Application.persistentDataPath;
        return path;
    }

    /// <summary>
    /// 获取网络存档XML路径
    /// </summary>
    /// <returns>网络XML路径</returns>
    static string GetNetSavePath()
    {
        return GetLocalSavePath();
    }

    /// <summary>
    /// 字符串加密
    /// </summary>
    /// <param name="toE"></param>
    /// <returns>加密后字符串</returns>
    public static string Encrypt(string toE)
    {
        //加密和解密采用相同的key,具体自己填，但是必须为32位//
        byte[] keyArray = UTF8Encoding.UTF8.GetBytes(_KEY);
        RijndaelManaged rDel = new RijndaelManaged();
        rDel.Key = keyArray;
        rDel.Mode = CipherMode.ECB;
        rDel.Padding = PaddingMode.PKCS7;
        ICryptoTransform cTransform = rDel.CreateEncryptor();

        byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(toE);
        byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

        return Convert.ToBase64String(resultArray, 0, resultArray.Length);
    }

    /// <summary>
    /// 字符串解密
    /// </summary>
    /// <param name="toD"></param>
    /// <returns>解密后字符串</returns>
    public static string Decrypt(string toD)
    {
        //加密和解密采用相同的key,具体值自己填，但是必须为32位//
        byte[] keyArray = UTF8Encoding.UTF8.GetBytes(_KEY);

        RijndaelManaged rDel = new RijndaelManaged();
        rDel.Key = keyArray;
        rDel.Mode = CipherMode.ECB;
        rDel.Padding = PaddingMode.PKCS7;
        ICryptoTransform cTransform = rDel.CreateDecryptor();

        byte[] toEncryptArray = Convert.FromBase64String(toD);
        byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

        return UTF8Encoding.UTF8.GetString(resultArray);
    }

    public static string SerializeObject(object pObject, System.Type ty)
    {
        string XmlizedString = null;
        MemoryStream memoryStream = new MemoryStream();
        XmlSerializer xs = new XmlSerializer(ty);
        XmlTextWriter xmlTextWriter = new XmlTextWriter(memoryStream, Encoding.UTF8);
        xs.Serialize(xmlTextWriter, pObject);
        memoryStream = (MemoryStream)xmlTextWriter.BaseStream;
        XmlizedString = UTF8ByteArrayToString(memoryStream.ToArray());
        return XmlizedString;
    }

    public static object DeserializeObject(string pXmlizedString, System.Type ty)
    {
        XmlSerializer xs = new XmlSerializer(ty);
        MemoryStream memoryStream = new MemoryStream(StringToUTF8ByteArray(pXmlizedString));
        XmlTextWriter xmlTextWriter = new XmlTextWriter(memoryStream, Encoding.UTF8);
        return xs.Deserialize(memoryStream);
    }

    public static string UTF8ByteArrayToString(byte[] characters)
    {
        UTF8Encoding encoding = new UTF8Encoding();
        string constructedString = encoding.GetString(characters);
        return (constructedString);
    }

    public static byte[] StringToUTF8ByteArray(String pXmlString)
    {
        UTF8Encoding encoding = new UTF8Encoding();
        byte[] byteArray = encoding.GetBytes(pXmlString);
        return byteArray;
    }
	#endregion
}
