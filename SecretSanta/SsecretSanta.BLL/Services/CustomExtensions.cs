using System.Collections.Generic;

namespace SecretSanta.BLL.Services
{
    public static class CustomExtensions
    {
        /// <summary>
        /// Iterator for nodes in a linked list
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static IEnumerable<LinkedListNode<T>> Nodes<T>(this LinkedList<T> list)
        {
            for (var n = list.First; n != null; n = n.Next)
                yield return n;
        }
    }
}
