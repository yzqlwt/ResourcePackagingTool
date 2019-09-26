/****************************************************************************
 * Copyright (c) 2017 imagicbell
 * Copyright (c) 2017 snowcold
 * Copyright (c) 2017 ~ 7 liangxie
 *
 * TODO: 这个应该写成扩展关键字方式的
 * 
 * http://qframework.io
 * https://github.com/liangxiegame/QFramework
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
 ****************************************************************************/

using UnityEngine;

namespace QF
{
	using System.IO;
	
	// 为了防止进行 clrbidning
    using Newtonsoft.Json;

    public static partial class SerializeHelper
	{

		// 为了防止进行 clrbinding
		public static string ToJson<T>(this T obj) where T : class
		{
            return JsonConvert.SerializeObject(obj, Formatting.Indented);
		}

		public static T FromJson<T>(this string json) where T : class
		{
            return JsonConvert.DeserializeObject<T>(json);
		}

		public static string SaveJson<T>(this T obj, string path) where T : class
		{
			var jsonContent = obj.ToJson();
			File.WriteAllText(path, jsonContent);
			return jsonContent;
		}

		public static T LoadJson<T>(string path) where T : class
		{
			if (path.Contains(Application.streamingAssetsPath))
			{
				using (var streamReader = new StreamReader(path))
				{
					return FromJson<T>(streamReader.ReadToEnd());
				}
			}
			
			return File.ReadAllText(path).FromJson<T>();
		}
		

//		public static byte[] ToProtoBuff<T>(this T obj) where T : class
//		{
//			using (MemoryStream ms = new MemoryStream())
//			{
//				ProtoBuf.Serializer.Serialize<T>(ms, obj);
//				return ms.ToArray();
//			}
//		}
//
//		public static T FromProtoBuff<T>(this byte[] bytes) where T : class
//		{
//			if (bytes == null || bytes.Length == 0)
//			{
//				throw new System.ArgumentNullException("bytes");
//			}
//			T t = ProtoBuf.Serializer.Deserialize<T>(new MemoryStream(bytes));
//			return t;
//		}

//		public static void SaveProtoBuff<T>(this T obj, string path) where T : class
//		{
//			File.WriteAllBytes(path, obj.ToProtoBuff<T>());
//		}
//
//		public static T LoadProtoBuff<T>(string path) where T : class
//		{
//			return File.ReadAllBytes(path).FromProtoBuff<T>();
//		}
	}
}