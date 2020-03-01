using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace DataSaver
{
    public abstract class AbstractTranDataSaver<T>
    {
        public abstract void SaveAction(List<T> data, string key);
        public abstract List<T> LoadAction(string key);
    }

    public class JsonTranDataSaver<T> : AbstractTranDataSaver<T>
    {
        [System.Serializable]
        public class JsonListDataSaveClass<K>
        {
            public List<K> saveList;
        }

        public override void SaveAction(List<T> data, string key)
        {
            JsonListDataSaveClass<T> saver = new JsonListDataSaveClass<T>();
            saver.saveList = data;
            FullSerializSaver.SaveAction(saver, key);
        }

        public override List<T> LoadAction(string key)
        {
            var loder = FullSerializSaver.LoadAction<JsonListDataSaveClass<T>>(key);
            return loder.saveList;
        }

    }
}
