using System;
using System.Collections.Generic;
using System.Text;

namespace KDTreeDLL
{
    /// <summary>
    /// This is an adaptation of the Java KDTree library implemented by Levy 
    /// and Heckel. This simplified version is written by Marco A. Alvarez
    /// 
    /// KDTree is a class supporting KD-tree insertion, deletion, equality
    /// search, range search, and nearest neighbor(s) using double-precision
    /// floating-point keys.  Splitting dimension is chosen naively, by
    /// depth modulo K.  Semantics are as follows:
    /// <UL>
    /// <LI> Two different keys containing identical numbers should retrieve the 
    ///      same value from a given KD-tree.  Therefore keys are cloned when a 
    ///      node is inserted.
    /// <BR><BR>
    /// <LI> As with Hashtables, values inserted into a KD-tree are <I>not</I>
    ///      cloned.  Modifying a value between insertion and retrieval will
    ///      therefore modify the value stored in the tree.
    /// </UL>
    /// 
    /// @author Simon Levy, Bjoern Heckel
    /// Translation by Marco A. Alvarez
    /// </summary>
    public class KDTree
    {
        // K = number of dimensions
        private int m_K;

        // root of KD-tree
        private KDNode m_root;

        // count of nodes
        private int m_count;

        /**
         * Creates a KD-tree with specified number of dimensions.
         *
         * @param k number of dimensions
         */
        public KDTree(int k)
        {

            m_K = k;
            m_root = null;
        }


        /** 
         * Insert a node in a KD-tree.  Uses algorithm translated from 352.ins.c of
         *
         *   <PRE>
         *   &#064;Book{GonnetBaezaYates1991,                                   
         *     author =    {G.H. Gonnet and R. Baeza-Yates},
         *     title =     {Handbook of Algorithms and Data Structures},
         *     publisher = {Addison-Wesley},
         *     year =      {1991}
         *   }
         *   </PRE>
         *
         * @param key key for KD-tree node
         * @param value value at that key
         *
         * @throws KeySizeException if key.length mismatches K
         * @throws KeyDuplicateException if key already in tree
         */
        public void insert(double[] key, Object value)
        {

            if (key.Length != m_K)
            {
                throw new KeySizeException();
            }

            else try
                {
                    m_root = KDNode.ins(new HPoint(key), value, m_root, 0, m_K);
                }

                catch (KeyDuplicateException e)
                {
                    throw e;
                }

            m_count++;
        }

        /** 
         * Find  KD-tree node whose key is identical to key.  Uses algorithm 
         * translated from 352.srch.c of Gonnet & Baeza-Yates.
         *
         * @param key key for KD-tree node
         *
         * @return object at key, or null if not found
         *
         * @throws KeySizeException if key.length mismatches K
         */
        public Object search(double[] key)
        {

            if (key.Length != m_K)
            {
                throw new KeySizeException();
            }

            KDNode kd = KDNode.srch(new HPoint(key), m_root, m_K);

            return (kd == null ? null : kd.v);
        }


        /** 
         * Delete a node from a KD-tree.  Instead of actually deleting node and
         * rebuilding tree, marks node as deleted.  Hence, it is up to the caller
         * to rebuild the tree as needed for efficiency.
         *
         * @param key key for KD-tree node
         *
         * @throws KeySizeException if key.length mismatches K
         * @throws KeyMissingException if no node in tree has key
         */
        public void delete(double[] key)
        {

            if (key.Length != m_K)
            {
                throw new KeySizeException();
            }

            else
            {
                bool deleted = false;
                m_root = KDNode.delete(new HPoint(key), m_root, 0, m_K, ref deleted);
                if (deleted == false) {
                    throw new KeyMissingException();
                }
                m_count--;
            }
        }

        /**
        * Find KD-tree node whose key is nearest neighbor to
        * key. Implements the Nearest Neighbor algorithm (Table 6.4) of
        *
        * <PRE>
        * &#064;techreport{AndrewMooreNearestNeighbor,
        *   author  = {Andrew Moore},
        *   title   = {An introductory tutorial on kd-trees},
        *   institution = {Robotics Institute, Carnegie Mellon University},
        *   year    = {1991},
        *   number  = {Technical Report No. 209, Computer Laboratory, 
        *              University of Cambridge},
        *   address = {Pittsburgh, PA}
        * }
        * </PRE>
        *
        * @param key key for KD-tree node
        *
        * @return object at node nearest to key, or null on failure
        *
        * @throws KeySizeException if key.length mismatches K

        */
        public Object nearest(double[] key)
        {

            Object[] nbrs = nearest(key, 1);
            return nbrs[0];
        }

        /**
        * Find KD-tree nodes whose keys are <I>n</I> nearest neighbors to
        * key. Uses algorithm above.  Neighbors are returned in ascending
        * order of distance to key. 
        *
        * @param key key for KD-tree node
        * @param n how many neighbors to find
        *
        * @return objects at node nearest to key, or null on failure
        *
        * @throws KeySizeException if key.length mismatches K
        * @throws IllegalArgumentException if <I>n</I> is negative or
        * exceeds tree size 
        */
        public Object[] nearest(double[] key, int n)
        {

            if (n < 0 || n > m_count)
            {
                throw new ArgumentException("Number of neighbors cannot be negative or greater than number of nodes");
            }

            if (key.Length != m_K)
            {
                throw new KeySizeException();
            }

            Object[] nbrs = new Object[n];
            NearestNeighborList nnl = new NearestNeighborList(n);

            // initial call is with infinite hyper-rectangle and max distance
            HRect hr = HRect.infiniteHRect(key.Length);
            double max_dist_sqd = Double.MaxValue;
            HPoint keyp = new HPoint(key);

            KDNode.nnbr(m_root, keyp, hr, max_dist_sqd, 0, m_K, nnl);

            for (int i = 0; i < n; ++i)
            {
                KDNode kd = (KDNode)nnl.removeHighest();
                nbrs[n - i - 1] = kd.v;
            }

            return nbrs;
        }


        /** 
         * Range search in a KD-tree.  Uses algorithm translated from
         * 352.range.c of Gonnet & Baeza-Yates.
         *
         * @param lowk lower-bounds for key
         * @param uppk upper-bounds for key
         *
         * @return array of Objects whose keys fall in range [lowk,uppk]
         *
         * @throws KeySizeException on mismatch among lowk.length, uppk.length, or K
         */
        public Object[] range(double[] lowk, double[] uppk)
        {

            if (lowk.Length != uppk.Length)
            {
                throw new KeySizeException();
            }

            else if (lowk.Length != m_K)
            {
                throw new KeySizeException();
            }

            else
            {
                List<KDNode> v = new List<KDNode>();
                KDNode.rsearch(new HPoint(lowk), new HPoint(uppk),
                       m_root, 0, m_K, v);
                Object[] o = new Object[v.Count];
                for (int i = 0; i < v.Count; ++i)
                {
                    KDNode n = (KDNode)v[i];
                    o[i] = n.v;
                }
                return o;
            }
        }

        public String toString()
        {
            return m_root.toString(0);
        }



        /// <summary>
        /// K-D Tree node class
        /// </summary>
        class KDNode
        {
            // these are seen by KDTree
            protected HPoint k;
            public Object v;
            protected KDNode left, right;
            public bool deleted;

            // Method ins translated from 352.ins.c of Gonnet & Baeza-Yates
            public static KDNode ins(HPoint key, Object val, KDNode t, int lev, int K)
            {
                if (t == null)
                {
                    t = new KDNode(key, val);
                }

                else if (key.equals(t.k))
                {

                    // "re-insert"
                    if (t.deleted)
                    {
                        t.deleted = false;
                        t.v = val;
                    }

                    else
                    {
                        throw (new KeyDuplicateException());
                    }
                }

                else if (key.coord[lev] > t.k.coord[lev])
                {
                    t.right = ins(key, val, t.right, (lev + 1) % K, K);
                }
                else
                {
                    t.left = ins(key, val, t.left, (lev + 1) % K, K);
                }

                return t;
            }


            // Method srch translated from 352.srch.c of Gonnet & Baeza-Yates
            public static KDNode srch(HPoint key, KDNode t, int K)
            {

                for (int lev = 0; t != null; lev = (lev + 1) % K)
                {

                    if (!t.deleted && key.equals(t.k))
                    {
                        return t;
                    }
                    else if (key.coord[lev] > t.k.coord[lev])
                    {
                        t = t.right;
                    }
                    else
                    {
                        t = t.left;
                    }
                }

                return null;
            }

            public static KDNode delete(HPoint key, KDNode t, int lev, int K, ref bool deleted) {
                if (t == null) return null;
                if (!t.deleted && key.equals(t.k)) 
                {
                    t.deleted = true;
                    deleted = true;
                } 
                else if (key.coord[lev] > t.k.coord[lev]) 
                {
                    t.right = delete(key, t.right, (lev + 1) % K, K, ref deleted);
                } 
                else 
                {
                    t.left = delete(key, t.left, (lev + 1) % K, K, ref deleted);
                }

                if (!t.deleted || t.left != null || t.right != null) 
                {
                    return t;
                } 
                else {
                    return null;
                }
            }

            // Method rsearch translated from 352.range.c of Gonnet & Baeza-Yates
            public static void rsearch(HPoint lowk, HPoint uppk, KDNode t, int lev,
                      int K, List<KDNode> v)
            {

                if (t == null) return;
                if (lowk.coord[lev] <= t.k.coord[lev])
                {
                    rsearch(lowk, uppk, t.left, (lev + 1) % K, K, v);
                }
                int j;
                for (j = 0; j < K && lowk.coord[j] <= t.k.coord[j] &&
                     uppk.coord[j] >= t.k.coord[j]; j++)
                    ;
                if (j == K && !t.deleted) v.Add(t);
                if (uppk.coord[lev] > t.k.coord[lev])
                {
                    rsearch(lowk, uppk, t.right, (lev + 1) % K, K, v);
                }
            }

            // Method Nearest Neighbor from Andrew Moore's thesis. Numbered
            // comments are direct quotes from there. Step "SDL" is added to
            // make the algorithm work correctly.  NearestNeighborList solution
            // courtesy of Bjoern Heckel.
            public static void nnbr(KDNode kd, HPoint target, HRect hr,
                                  double max_dist_sqd, int lev, int K,
                                  NearestNeighborList nnl)
            {

                // 1. if kd is empty then set dist-sqd to infinity and exit.
                if (kd == null)
                {
                    return;
                }

                // 2. s := split field of kd
                int s = lev % K;

                // 3. pivot := dom-elt field of kd
                HPoint pivot = kd.k;
                double pivot_to_target = HPoint.sqrdist(pivot, target);

                // 4. Cut hr into to sub-hyperrectangles left-hr and right-hr.
                //    The cut plane is through pivot and perpendicular to the s
                //    dimension.
                HRect left_hr = hr; // optimize by not cloning
                HRect right_hr = (HRect)hr.clone();
                left_hr.max.coord[s] = pivot.coord[s];
                right_hr.min.coord[s] = pivot.coord[s];

                // 5. target-in-left := target_s <= pivot_s
                bool target_in_left = target.coord[s] < pivot.coord[s];

                KDNode nearer_kd;
                HRect nearer_hr;
                KDNode further_kd;
                HRect further_hr;

                // 6. if target-in-left then
                //    6.1. nearer-kd := left field of kd and nearer-hr := left-hr
                //    6.2. further-kd := right field of kd and further-hr := right-hr
                if (target_in_left)
                {
                    nearer_kd = kd.left;
                    nearer_hr = left_hr;
                    further_kd = kd.right;
                    further_hr = right_hr;
                }
                //
                // 7. if not target-in-left then
                //    7.1. nearer-kd := right field of kd and nearer-hr := right-hr
                //    7.2. further-kd := left field of kd and further-hr := left-hr
                else
                {
                    nearer_kd = kd.right;
                    nearer_hr = right_hr;
                    further_kd = kd.left;
                    further_hr = left_hr;
                }

                // 8. Recursively call Nearest Neighbor with paramters
                //    (nearer-kd, target, nearer-hr, max-dist-sqd), storing the
                //    results in nearest and dist-sqd
                nnbr(nearer_kd, target, nearer_hr, max_dist_sqd, lev + 1, K, nnl);

                KDNode nearest = (KDNode)nnl.getHighest();
                double dist_sqd;

                if (!nnl.isCapacityReached())
                {
                    dist_sqd = Double.MaxValue;
                }
                else
                {
                    dist_sqd = nnl.getMaxPriority();
                }

                // 9. max-dist-sqd := minimum of max-dist-sqd and dist-sqd
                max_dist_sqd = Math.Min(max_dist_sqd, dist_sqd);

                // 10. A nearer point could only lie in further-kd if there were some
                //     part of further-hr within distance sqrt(max-dist-sqd) of
                //     target.  If this is the case then
                HPoint closest = further_hr.closest(target);
                if (HPoint.eucdist(closest, target) < Math.Sqrt(max_dist_sqd))
                {

                    // 10.1 if (pivot-target)^2 < dist-sqd then
                    if (pivot_to_target < dist_sqd)
                    {

                        // 10.1.1 nearest := (pivot, range-elt field of kd)
                        nearest = kd;

                        // 10.1.2 dist-sqd = (pivot-target)^2
                        dist_sqd = pivot_to_target;

                        // add to nnl
                        if (!kd.deleted)
                        {
                            nnl.insert(kd, dist_sqd);
                        }

                        // 10.1.3 max-dist-sqd = dist-sqd
                        // max_dist_sqd = dist_sqd;
                        if (nnl.isCapacityReached())
                        {
                            max_dist_sqd = nnl.getMaxPriority();
                        }
                        else
                        {
                            max_dist_sqd = Double.MaxValue;
                        }
                    }

                    // 10.2 Recursively call Nearest Neighbor with parameters
                    //      (further-kd, target, further-hr, max-dist_sqd),
                    //      storing results in temp-nearest and temp-dist-sqd
                    nnbr(further_kd, target, further_hr, max_dist_sqd, lev + 1, K, nnl);
                    KDNode temp_nearest = (KDNode)nnl.getHighest();
                    double temp_dist_sqd = nnl.getMaxPriority();

                    // 10.3 If tmp-dist-sqd < dist-sqd then
                    if (temp_dist_sqd < dist_sqd)
                    {

                        // 10.3.1 nearest := temp_nearest and dist_sqd := temp_dist_sqd
                        nearest = temp_nearest;
                        dist_sqd = temp_dist_sqd;
                    }
                }

                // SDL: otherwise, current point is nearest
                else if (pivot_to_target < max_dist_sqd)
                {
                    nearest = kd;
                    dist_sqd = pivot_to_target;
                }
            }


            // constructor is used only by class; other methods are static
            private KDNode(HPoint key, Object val)
            {

                k = key;
                v = val;
                left = null;
                right = null;
                deleted = false;
            }

            public String toString(int depth)
            {
                String s = k + "  " + v + (deleted ? "*" : "");
                if (left != null)
                {
                    s = s + "\n" + pad(depth) + "L " + left.toString(depth + 1);
                }
                if (right != null)
                {
                    s = s + "\n" + pad(depth) + "R " + right.toString(depth + 1);
                }
                return s;
            }

            private static String pad(int n)
            {
                String s = "";
                for (int i = 0; i < n; ++i)
                {
                    s += " ";
                }
                return s;
            }

            private static void hrcopy(HRect hr_src, HRect hr_dst)
            {
                hpcopy(hr_src.min, hr_dst.min);
                hpcopy(hr_src.max, hr_dst.max);
            }

            private static void hpcopy(HPoint hp_src, HPoint hp_dst)
            {
                for (int i = 0; i < hp_dst.coord.Length; ++i)
                {
                    hp_dst.coord[i] = hp_src.coord[i];
                }
            }
        }
    }

}