using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using ZJZYDYDX_JTZDBZLZJZLFJYLPB;

[Serializable]
public class StringStringDictionary : SerializableDictionary<string, string>
{
}

[Serializable]
public class ObjectColorDictionary : SerializableDictionary<UnityEngine.Object, Color>
{
}

[Serializable]
public class ColorArrayStorage : SerializableDictionary.Storage<Color[]>
{
}

[Serializable]
public class StringColorArrayDictionary : SerializableDictionary<string, Color[], ColorArrayStorage>
{
}

[Serializable]
public class MyClass
{
	public int i;
	public string str;
}

[Serializable]
public class QuaternionMyClassDictionary : SerializableDictionary<Quaternion, MyClass>
{
}

[Serializable]
public class IntStringArrayDictionary : SerializableDictionary<int, List<string>, StringArrayStorage>
{
}

[Serializable]
public class StringArrayStorage : SerializableDictionary.Storage<List<string>>
{
}

[Serializable]
public class IntListIntDictionary : SerializableDictionary<int, List<int>, IntListStorage>
{

}

[Serializable]
public class IntListStorage : SerializableDictionary.Storage<List<int>>
{

}



[Serializable]
public class CaseToIntInts : SerializableDictionary<Case, IntInts>
{

}

[Serializable]
public class IntInts : SerializableDictionary.Storage<List<IntInt>>
{
}

[Serializable]
public class IntInt
{
	public DrugName drug;
	public int weight;
	public Sprite sprite;
}

