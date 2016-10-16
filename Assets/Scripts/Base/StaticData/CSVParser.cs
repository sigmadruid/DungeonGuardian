using UnityEngine;

using System;
using System.Collections.Generic;
using System.IO;

using Base;

namespace Base
{
    public static class CSVParser
    {
        private static readonly string CONFIG_PATH = Application.streamingAssetsPath + "/Configs/";

        private static string CSVName;
		private static int rowIndex;
        private static int colIndex;
        private static List<string[]> dataStrList = new List<string[]>();

        #region Flow Controller
        public static void Init(string csvName)
        {
            CSVName = csvName;
            rowIndex = 0;
            colIndex = 0;
            dataStrList.Clear();
            LoadFile(csvName);
        }
        public static bool IsEndOfRow()
		{
			return rowIndex == dataStrList.Count;
		}
        public static void NextLine()
		{
			rowIndex++;
		}

        #endregion

        private static void LoadFile(string csvName)
        {
            string path = CONFIG_PATH + csvName;

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

                rowIndex = 0;
            }
            else
            {
                BaseLogger.LogError("Can't find config file: {0}", path);
            }
        }

        #region Read Values

        public static bool ReadBool()
		{
            string str = ReadNextStr();
            if(str.ToLower() == "true" || str == "1")
			{
				return true;
			}
			else
			{
				return false;
			}
		}
        public static int ReadInt()
		{
            string str = ReadNextStr();
			return Convert.ToInt32(str);
		}
        public static string ReadString()
		{
            string str = ReadNextStr();
			return str;
		}
        public static float ReadFloat()
		{
            string value = ReadNextStr();
			return Convert.ToSingle(value);
		}
        public static T ReadEnum<T>()
		{
            string value = ReadNextStr();
            if(!Enum.IsDefined(typeof(T), value))
			{
                BaseLogger.LogError("Parse error. CSV: {0}, Col:{1}, Row:{2}", CSVName, colIndex - 1, rowIndex);
			}
            return ParseKey<T>(value);
		}
        public static List<string> ReadStringList()
		{
            string value = ReadNextStr();
			List<string> list = new List<string>();
			if (!string.IsNullOrEmpty(value))
			{
				string[] strList = value.Split('#');
				for (int i = 0; i < strList.Length; ++i)
				{
					string str = strList[i];
					list.Add(str);
				}
			}
			return list;
		}
        public static List<int> ReadIntList()
		{
            string value = ReadNextStr();
			List<int> list = new List<int>();
			if (!string.IsNullOrEmpty(value))
			{
				string[] strList = value.Split('#');
				for (int i = 0; i < strList.Length; ++i)
				{
					string str = strList[i];
					list.Add(Convert.ToInt32(str));
				}
			}
			return list;
		}
        public static List<T> ReadEnumList<T>()
		{
            string value = ReadNextStr();
			List<T> list = new List<T>();
			if (!string.IsNullOrEmpty(value))
			{
				string[] strList = value.Split('#');
				for (int i = 0; i < strList.Length; ++i)
				{
					string str = strList[i];
					T enumVal = ParseKey<T>(str);
					list.Add(enumVal);
				}
			}
			return list;
		}
        public static Dictionary<K, V> ReadDictionary<K, V>()
        {
            string value = ReadNextStr();
            if (string.IsNullOrEmpty(value))
            {
                return new Dictionary<K, V>();
            }

            string[] valList = value.Split('#');
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
        private static T ParseKey<T>(string str)
		{
//            T enumKey = (T)Enum.Parse(typeof(T), str);
            object resultKey = null;
            if (typeof(T) == typeof(int))
            {
                resultKey = Convert.ToInt32(str);
            }
            else
            {
                resultKey = Enum.Parse(typeof(T), str);
            }
            return (T)resultKey;
		}
        private static T ParseValue<T>(string str)
		{
			object resultVal = null;

            if(typeof(T) == typeof(int))
            {
                resultVal = Convert.ToInt32(str);
            }
            else if(typeof(T) == typeof(float))
            {
                resultVal = Convert.ToSingle(str);
            }
            else
            {
                resultVal = str;
            }

			return (T)resultVal;
		}

        public static Color ReadColor()
        {
            string value = ReadNextStr();
            if (string.IsNullOrEmpty(value))
            {
                return Color.white;
            }

            string[] colorList = value.Split('#');
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

        private static string ReadNextStr()
        {
            string[] strArray = dataStrList[rowIndex];
            string str = strArray[colIndex++];
            return str;
        }

        #endregion
    }
}

