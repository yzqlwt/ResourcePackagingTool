using System;
using System.Collections.Generic;

namespace QF.Master
{
	public class TypeEventSystem
	{
		/// <summary>
		/// 接口 只负责存储在字典中
		/// </summary>
		interface IRegisterations
		{
			
		}
		
		/// <summary>
		/// 多个注册
		/// </summary>
		class Registerations<T> : IRegisterations
		{
			/// <summary>
			/// 不需要 List<Action<T>> 了
			/// 因为委托本身就可以一对多注册
			/// </summary>
			public Action<T> OnReceives = obj => { };
		}

		/// <summary>
		/// 
		/// </summary>
		private static Dictionary<Type, IRegisterations> mTypeEventDict = new Dictionary<Type, IRegisterations>();
		
		/// <summary>
		/// 注册事件
		/// </summary>
		/// <param name="onReceive"></param>
		/// <typeparam name="T"></typeparam>
		public static void Register<T>(System.Action<T> onReceive)
		{
			var type = typeof(T);

			IRegisterations registerations = null;

			if (mTypeEventDict.TryGetValue(type, out registerations))
			{
				var reg = registerations as Registerations<T>;
				reg.OnReceives += onReceive;
			}
			else
			{
				var reg = new Registerations<T>();
				reg.OnReceives += onReceive;
				mTypeEventDict.Add(type, reg);
			}
		}

		/// <summary>
		/// 注销事件
		/// </summary>
		/// <param name="onReceive"></param>
		/// <typeparam name="T"></typeparam>
		public static void UnRegister<T>(System.Action<T> onReceive)
		{
			var type = typeof(T);
			
			IRegisterations registerations = null;
			
			if (mTypeEventDict.TryGetValue(type, out registerations))
			{
				var reg = registerations as Registerations<T>;
				reg.OnReceives -= onReceive;
			}
		}
		
		/// <summary>
		/// 发送事件
		/// </summary>
		/// <param name="t"></param>
		/// <typeparam name="T"></typeparam>
		public static void Send<T>(T t)
		{
			var type = typeof(T);

			IRegisterations registerations = null;
			
			if (mTypeEventDict.TryGetValue(type, out registerations))
			{
				var reg = registerations as Registerations<T>;
				reg.OnReceives(t);
			}
		}
	}
}