using System;
using System.Collections.Generic;
using UnityEngine;

namespace IUP.Toolkit.SerializableCollections
{
    public class SRK_SRV_Dictionary<TKey, TValue> : ISerializationCallbackReceiver
    {
        [Serializable]
        public struct SKeyValuePair
        {
            public SKeyValuePair(TKey key, TValue value)
            {
                Key = key;
                Value = value;
            }

            public static readonly string KeyPropertyPath = nameof(Key);
            public static readonly string ValuePropertyPath = nameof(Value);

            [SerializeReference] public TKey Key;
            [SerializeReference] public TValue Value;
        }

        public static readonly string SKeyValuePairsPropertyPath = nameof(_sKeyValuePairs);

        public Dictionary<TKey, TValue> Dictionary { get; set; } = new();

        [SerializeField] private List<SKeyValuePair> _sKeyValuePairs;

        public void OnAfterDeserialize()
        {
            Dictionary = new Dictionary<TKey, TValue>(_sKeyValuePairs.Count);
            foreach (SKeyValuePair sKeyValuePair in _sKeyValuePairs)
            {
                Dictionary.Add(sKeyValuePair.Key, sKeyValuePair.Value);
            }
        }

        public void OnBeforeSerialize()
        {
            _sKeyValuePairs = new List<SKeyValuePair>(Dictionary.Count);
            foreach (KeyValuePair<TKey, TValue> keyValuePair in Dictionary)
            {
                SKeyValuePair sKeyValuePair = new(keyValuePair.Key, keyValuePair.Value);
                _sKeyValuePairs.Add(sKeyValuePair);
            }
        }
    }
}
