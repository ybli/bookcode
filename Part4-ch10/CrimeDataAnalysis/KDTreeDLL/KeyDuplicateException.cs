using System;

namespace KDTreeDLL
{
    /// <summary>
    /// KeyDuplicateException is thrown when the <TT>KDTree.insert</TT> method
    /// is invoked on a key already in the KDTree.
    /// 
    /// @author Simon Levy
    /// Translation by Marco A. Alvarez
    /// </summary> 
    public class KeyDuplicateException : Exception
    {
        public KeyDuplicateException()
        {
            Console.WriteLine("Key already in tree");
        }
    }
}
