using System;

namespace KDTreeDLL
{
    /// <summary>
    /// KeySizeException is thrown when a KDTree method is invoked on a
    /// key whose size (array length) mismatches the one used in the that
    /// KDTree's constructor.
    /// 
    /// @author Simon Levy
    /// Translation by Marco A. Alvarez
    /// </summary> 
    public class KeySizeException : Exception
    {
        public KeySizeException()
        {
            Console.WriteLine("Key size mismatch");
        }
    }
}
