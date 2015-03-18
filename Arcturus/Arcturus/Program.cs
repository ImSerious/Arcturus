using System;

namespace Arcturus
{
#if WINDOWS || XBOX
    static class Program
    {
        static void Main(string[] args)
        {
            using (Arcturus arcturus = new Arcturus())
            {
                arcturus.Run();
            }
        }
    }
#endif
}

