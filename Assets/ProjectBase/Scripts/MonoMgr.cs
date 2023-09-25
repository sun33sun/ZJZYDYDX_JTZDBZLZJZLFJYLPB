﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.Events;
using Object = UnityEngine.Object;


namespace ProjectBase
{
    /// <summary>
    /// 1.可以提供给外部添加帧更新事件的方法
    /// 2.可以提供给外部添加 协程的方法
    /// </summary>
    public class MonoMgr : Singleton<MonoMgr>
    {
        private MonoController controller;
        public MonoController Controller => controller;

        public MonoMgr()
        {
            //保证了MonoController对象的唯一性
            GameObject obj = new GameObject("MonoController");
            controller = obj.AddComponent<MonoController>();
        }

        /// <summary>
        /// 给外部提供的 添加帧更新事件的函数
        /// </summary>
        /// <param name="fun"></param>
        public void AddUpdateListener(UnityAction fun)
        {
            controller.AddUpdateListener(fun);
        }

        /// <summary>
        /// 提供给外部 用于移除帧更新事件函数
        /// </summary>
        /// <param name="fun"></param>
        public void RemoveUpdateListener(UnityAction fun)
        {
            controller.RemoveUpdateListener(fun);
        }

        public void AddFixedUpdateListener(UnityAction fun)
        {
            controller.AddFixedUpdateListener(fun);
        }

        public void RemoveFixedUpdateListener(UnityAction fun)
        {
            controller.RemoveFixedUpdateListener(fun);
        }

        public void AddLateUpdateListener(UnityAction fun)
        {
            controller.AddLateUpdateListener(fun);
        }
        
        public void RemoveLatUpdateListener(UnityAction fun)
        {
            controller.RemoveLatUpdateListener(fun);
        }


        public void DoDestroy(Object obj)
		{
            controller.DoDestroy(obj);
		}

        public Coroutine StartCoroutine(IEnumerator routine)
        {
            return controller.StartCoroutine(routine);
        }

        public Coroutine StartCoroutine(string methodName, [DefaultValue("null")] object value)
        {
            return controller.StartCoroutine(methodName, value);
        }

        public Coroutine StartCoroutine(string methodName)
        {
            return controller.StartCoroutine(methodName);
        }

        public void StopCoroutine(IEnumerator routine)
		{
            controller.StopCoroutine(routine);
		}
    }
}

