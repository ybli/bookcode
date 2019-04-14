·这个类库修改自github上的一个开源项目，用于程序中搜索最近点

 <summary>
 This is an adaptation of the Java KDTree library implemented by Levy 
 and Heckel. This simplified version is written by Marco A. Alvarez
 
 KDTree is a class supporting KD-tree insertion, deletion, equality
 search, range search, and nearest neighbor(s) using double-precision
 floating-point keys.  Splitting dimension is chosen naively, by
 depth modulo K.  Semantics are as follows:
 <UL>
 <LI> Two different keys containing identical numbers should retrieve the 
      same value from a given KD-tree.  Therefore keys are cloned when a 
      node is inserted.
 <BR><BR>
 <LI> As with Hashtables, values inserted into a KD-tree are <I>not</I>
      cloned.  Modifying a value between insertion and retrieval will
      therefore modify the value stored in the tree.
 </UL>
 
 @author Simon Levy, Bjoern Heckel
 Translation by Marco A. Alvarez
 </summary>