using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FullSerializer;
using System;
using System.IO;

namespace DataSaver
{
    public static class FullSerializSaver
    {
        static string GetFilePath(string fileName)
        {
            return Application.dataPath + "/SaveData/" + fileName + ".json";
        }

        public static void SaveAction<T>(T data, string fileName)
        {
            var json = StringSerializationAPI.Serialize(typeof(T), data);


            StreamWriter streamWriter = new StreamWriter(GetFilePath(fileName));
            streamWriter.Write(json);
            streamWriter.Flush();
            streamWriter.Close();
        }

        public static T LoadAction<T>(string fileName)
        {
            StreamReader streamReader = new StreamReader(GetFilePath(fileName));
            string json = streamReader.ReadToEnd();
            streamReader.Close();
            T data = (T)StringSerializationAPI.Deserialize(typeof(T), json);
            return data;
        }


        public static bool ExsistFile(string fileName)
        {
            string filePath = GetFilePath(fileName);
            return File.Exists(filePath);
        }
    }

    public static class StringSerializationAPI
    {
        private static readonly fsSerializer _serializer = new fsSerializer();

        public static string Serialize(Type type, object value)
        {
            // serialize the data
            fsData data;
            _serializer.TrySerialize(type, value, out data).AssertSuccessWithoutWarnings();

            // emit the data via JSON
            return fsJsonPrinter.CompressedJson(data);
        }

        public static object Deserialize(Type type, string serializedState)
        {
            // step 1: parse the JSON data
            fsData data = fsJsonParser.Parse(serializedState);

            // step 2: deserialize the data
            object deserialized = null;
            _serializer.TryDeserialize(data, type, ref deserialized).AssertSuccessWithoutWarnings();

            return deserialized;
        }
    }
}