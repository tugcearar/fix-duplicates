using AarLibsFix;
using System;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            AndroidAarFixups.FixupAarClass(@"..\library1.aar", "lib-1");
            AndroidAarFixups.FixupAarClass(@"..\library2.aar", "lib-2");
        }
    }
}
