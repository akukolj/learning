using System;
using System.Collections.Generic;
using System.Text;

namespace Singelton
{
    public sealed class Singleton
    {
        private static readonly Singleton _instance = new Singleton();
        private readonly int _numberOfInstances = 0;

        private Singleton()
        {
            Console.WriteLine("Instatin inside the private constructor");
            _numberOfInstances++;
            Console.WriteLine("number of inst = {0}",_numberOfInstances );
        }

        public static Singleton Instance
        {
            get
            {
                Console.WriteLine("we alreaady have an instance now. Use it");
                return _instance;
            }
            
        }
        public static int MyInt = 25;
    }
}
