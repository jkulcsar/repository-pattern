﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Repository
{
    public abstract class Repository<T> : IDisposable where T : class
    {
        //===============================================================
        protected Repository(Func<T, Object[]> keySelector)
        {
            KeySelector = keySelector;
        }
        //===============================================================
        protected Func<T, Object[]> KeySelector { get; private set; }
        //===============================================================
        public abstract void Store(T value);
        //===============================================================
        public virtual void Store(IEnumerable<T> values)
        {
            foreach (var value in values)
                Store(value);
        }
        //===============================================================
        public void Remove(T obj)
        {
            RemoveByKey(KeySelector(obj));
        }
        //===============================================================
        public void RemoveAll(IEnumerable<T> objects)
        {
            RemoveAllByKey(objects.Select(x => KeySelector(x)));
        }
        //===============================================================
        public abstract void RemoveByKey(params Object[] keys);
        //===============================================================
        public virtual void RemoveAllByKey(IEnumerable<Object[]> keys)
        {
            foreach (var keySet in keys)
                RemoveByKey(keySet);
        }
        //===============================================================
        public abstract bool Exists(params Object[] keys);
        //===============================================================
        public abstract void Update<TValue>(TValue value, params Object[] keys);
        //===============================================================
        public abstract void Update<TValue, TProperty>(TValue value, Func<T, TProperty> getter, params Object[] keys);
        //===============================================================
        public abstract void Update(String json, UpdateType updateType, params Object[] keys);
        //===============================================================
        public abstract void Update(String pathToProperty, String json, UpdateType updateType, params Object[] keys);
        //===============================================================
        public abstract ObjectContext<T> Find(params Object[] keys);
        //===============================================================
        public abstract EnumerableObjectContext<T> Items { get; }
        //===============================================================
        public abstract void Dispose();
        //===============================================================
    }
}