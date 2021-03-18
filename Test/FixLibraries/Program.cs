using AarLibsFix;
using System;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            AndroidAarFixups.FixupAarClass(@"D:\source\Github\fix-duplicates\Test\Aars\library1.aar", "lib-1");
            AndroidAarFixups.FixupAarClass(@"D:\source\Github\fix-duplicates\Test\Aars\library2.aar", "lib-2");
        }
    }
}
