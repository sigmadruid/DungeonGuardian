using UnityEngine;

using System;
using System.Collections.Generic;
using System.IO;

using Base;

namespace StaticData
{
    public static class CSVParser
    {
		public static string CONFIG_PATH = Application.streamingAssetsPath + "/Configs/";

        public static List<string[]> LoadFile(string name)
		{
			string path = CONFIG_PATH + name;
            List<string[]> dataStrList = new List<string[]>();

			if (File.Exists(path))
			{
                StreamReader sr = new StreamReader(path, System.Text.UTF8Encoding.UTF8);
				bool isFirstLine = true;
				while (!sr.EndOfStream)
				{
					string line = sr.ReadLine();
					if (isFirstLine)
					{
						isFirstLine = false;
						continue;
					}
					char firstChar = line[0];
					if (firstChar == ',')
					{
						continue;
					}
					string[] strArray = line.Split(',');
					dataStrList.Add(strArray);
				}
				sr.Close();
			}
			else
			{
                BaseLogger.LogError("Can't find config file: {0}", path);
			}
            return dataStrList;
		}

        public static bool ReadBool(string content)
		{
            return content.ToLower() == "true" || content == "1";
		}
        public static int ReadInt(string content)
		{
            return Convert.ToInt32(content);
		}
        public static float ReadFloat(string content)
		{
            return Convert.ToSingle(content);
		}
        public static T ReadEnum<T>(string content)
		{
            if(!Enum.IsDefined(typeof(T), content))
			{
                BaseLogger.LogError("Parse error. content: " + content);
			}
            return ParseKey<T>(content);
		}
        public static List<string> ReadStringList(string content)
		{
			List<string> list = new List<string>();
            if (!string.IsNullOrEmpty(content))
			{
                string[] strList = content.Split('#');
				for (int i = 0; i < strList.Length; ++i)
				{
					string str = strList[i];
					list.Add(str);
				}
			}
			return list;
		}
        public static List<int> ReadIntList(string content)
		{
			List<int> list = new List<int>();
            if (!string.IsNullOrEmpty(content))
			{
                string[] strList = content.Split('#');
				for (int i = 0; i < strList.Length; ++i)
				{
					string str = strList[i];
					list.Add(Convert.ToInt32(str));
				}
			}
			return list;
		}
        public static List<T> ReadEnumList<T>(string content)
		{
			List<T> list = new List<T>();
            if (!string.IsNullOrEmpty(content))
			{
                string[] strList = content.Split('#');
				for (int i = 0; i < strList.Length; ++i)
				{
					string str = strList[i];
					T enumVal = ParseKey<T>(str);
					list.Add(enumVal);
				}
			}
			return list;
		}
        public static Dictionary<K, V> ReadDictionary<K, V>(string content)
        {
            if (string.IsNullOrEmpty(content))
            {
                return new Dictionary<K, V>();
            }

            string[] valList = content.Split('#');
            Dictionary<K,V> dic = new Dictionary<K, V>();
            for(int i = 0; i < valList.Length; ++i)
            {
                string[] pairList = valList[i].Split(':');
                K k = ParseKey<K>(pairList[0].Trim());
                V v = ParseValue<V>(pairList[1].Trim());
                dic.Add(k, v);
            }
            return dic;
        }
        private static T ParseKey<T>(string content)
		{
//            T enumKey = (T)Enum.Parse(typeof(T), str);
            object resultKey = null;
            if (typeof(T) == typeof(int))
            {
                resultKey = Convert.ToInt32(content);
            }
            else
            {
                resultKey = Enum.Parse(typeof(T), content);
            }
            return (T)resultKey;
		}
        private static T ParseValue<T>(string content)
		{
			object resultVal = null;

            if(typeof(T) == typeof(int))
            {
                resultVal = Convert.ToInt32(content);
            }
            else if(typeof(T) == typeof(float))
            {
                resultVal = Convert.ToSingle(content);
            }
            else
            {
                resultVal = content;
            }

			return (T)resultVal;
		}

        public static Color ReadColor(string content)
        {
            if (string.IsNullOrEmpty(content))
            {
                return Color.white;
            }

            string[] colorList = content.Split('#');
            Color color = new Color();
            color.r = Convert.ToSingle(colorList[0]);
            color.g = Convert.ToSingle(colorList[1]);
            color.b = Convert.ToSingle(colorList[2]);
            if(colorList.Length == 4)
            {
                color.a = Convert.ToSingle(colorList[3]);
            }
            else
            {
                color.a = 1f;
            }
            return color;
        }
    }
}

