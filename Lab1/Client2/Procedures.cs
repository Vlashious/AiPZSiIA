using System;

namespace Client2
{
    public class Procedures
    {
        public Func<int, int, int> AddFunc()
        {
            return (a, b) => a + b;
        }
    }
}