﻿using System.Collections.Concurrent;

namespace PokemonApi.PokemonDetailsProvider.ApiCache
{
    public class LeastRecentlyUsedCache<TKey, TValue>
    {
        private readonly Dictionary<TKey, TValue> _cache;
        private List<TKey> _keys;
        private readonly object _lock = new();
        private readonly int _capacity;
        private int _size = 0;

        public LeastRecentlyUsedCache(int capacity)
        {
            _capacity = capacity;
            _keys = new List<TKey>();
            _cache = new Dictionary<TKey, TValue>();
        }

        public bool TryGet(TKey key, out TValue value)
        {
            value = default;
            lock (_lock)
            {
                if (!_keys.Contains(key)) return false;

                _keys.Add(key);
                _keys.Remove(key);
                value = _cache[key];
            }
            return true;
        }

        public void Put(TKey key, TValue value)
        {
            lock (_lock)
            {
                if (_keys.Contains(key))
                {
                    _cache[key] = value;
                    _keys.Remove(key);
                }
                else if (_size < _capacity)
                {
                    _size++;
                    _cache.Add(key, value);
                }
                else
                {
                    _cache.Remove(_keys.First());
                    _keys.RemoveAt(0);
                    _cache.Add(key, value);
                }
                _keys.Add(key); 
            }
        }
    }
}
