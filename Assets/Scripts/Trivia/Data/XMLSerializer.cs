using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

public static class XMLSerializer
{
	public static object LoadFile (string filePath, Type objectType)
	{
		if (File.Exists (filePath))
		{
			string content = LoadXML (filePath);
			return LoadData (content, objectType);
		}

		return null;
	}

	public static void SaveData (string filePath, object data)
	{
		Type objectType = data.GetType ();

		string content = SerializeObject (data, objectType);

		CreateXML (content, filePath);
	}

	private static string UTF8ByteArrayToString (byte[] characters)
	{
		UTF8Encoding encoding = new ();

		string constructedString = encoding.GetString (characters);

		return constructedString;
	}

	private static byte[] StringToUTF8ByteArray (string pXmlString)
	{
		UTF8Encoding encoding = new ();

		byte[] byteArray = encoding.GetBytes (pXmlString);

		return byteArray;
	}

	private static string SerializeObject (object data, Type objectType)
	{
		MemoryStream memoryStream = new ();

		XmlSerializer serializer = new (objectType);
		XmlTextWriter writer = new (memoryStream, Encoding.UTF8);

		serializer.Serialize (writer, data);

		memoryStream = (MemoryStream)writer.BaseStream;

		return UTF8ByteArrayToString (memoryStream.ToArray ());
	}

	private static object DeserializeObject (string pXmlizedString, Type objectType)
	{
		XmlSerializer serializer = new (objectType);
		MemoryStream memoryStream = new (StringToUTF8ByteArray (pXmlizedString));

		return serializer.Deserialize (memoryStream);
	}

	private static void CreateXML (string content, string filePath)
	{
		StreamWriter sw;

		FileInfo info = new (filePath);

		if (!info.Exists)
			sw = info.CreateText ();
		else
		{
			info.Delete ();
			sw = info.CreateText ();
		}

		sw.Write (content);
		sw.Close ();
	}

	private static string LoadXML (string filePath)
	{
		StreamReader sr = File.OpenText (filePath);
		string xmlData = sr.ReadToEnd ();

		sr.Close ();

		return xmlData;
	}

	private static object LoadData (string content, Type objectType)
	{
		if (content.ToString () != "")
			return DeserializeObject (content, objectType);
		else
			return null;
	}
}
