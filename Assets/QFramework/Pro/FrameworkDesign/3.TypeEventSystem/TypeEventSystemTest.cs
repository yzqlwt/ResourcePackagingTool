using System;
using UnityEngine;

namespace QF.Master
{
    public class TypeEventSystemTest : MonoBehaviour
    {
        class A
        {
        }

        class B
        {
            public int    Age;
            public string Name;
        }

        private void Start()
        {
            TypeEventSystem.Register<A>(ReceiveA);

            TypeEventSystem.Register<B>(ReceiveB);
        }

        void ReceiveA(A a)
        {
            Debug.Log("received A");
        }

        void ReceiveB(B b)
        {
            Debug.LogFormat("received B:{0} {1}", b.Name, b.Age);
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                TypeEventSystem.Send(new A());
            }

            if (Input.GetMouseButtonDown(1))
            {
                TypeEventSystem.Send(new B()
                {
                    Age = 10,
                    Name = "凉鞋"
                });
            }

            if (Input.GetKeyDown(KeyCode.U))
            {
                TypeEventSystem.UnRegister<A>(ReceiveA);
                TypeEventSystem.UnRegister<B>(ReceiveB);
            }
        }
    }
}