using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour 
{
	public enum DataType {ClearStage, BGMVolume, SEVolume, NumberOfPlays, BackGroundObjects};
	Dictionary<DataType, string>	data_name = new Dictionary<DataType, string> (){{DataType.ClearStage, "ClearStage"}, {DataType.BGMVolume, "BGMVolume"}, {DataType.SEVolume, "SEVolume"}, {DataType.NumberOfPlays, "NumberOfPlays"}, {DataType.BackGroundObjects, "BackGroundObjects"}};
	Dictionary<DataType, int>		data_value = new Dictionary<DataType, int> ();
	Dictionary<DataType, int>		data_max = new Dictionary<DataType, int>(){{DataType.ClearStage, 30}, {DataType.BGMVolume, 100}, {DataType.SEVolume, 100}, {DataType.NumberOfPlays, int.MaxValue}, {DataType.BackGroundObjects, 1}};
	Dictionary<DataType, int>		data_min = new Dictionary<DataType, int>(){{DataType.ClearStage, 1}, {DataType.BGMVolume, 0}, {DataType.SEVolume, 0}, {DataType.NumberOfPlays, 0}, {DataType.BackGroundObjects, 0}};

	void Awake () 
	{
		data_value.Add (DataType.ClearStage, PlayerPrefs.GetInt (data_name [DataType.ClearStage], 0));
		data_value.Add (DataType.BGMVolume, PlayerPrefs.GetInt (data_name [DataType.BGMVolume], 50));
		data_value.Add (DataType.SEVolume, PlayerPrefs.GetInt (data_name [DataType.SEVolume], 50));
		data_value.Add (DataType.NumberOfPlays, PlayerPrefs.GetInt (data_name [DataType.NumberOfPlays], 0));
		data_value.Add (DataType.BackGroundObjects, PlayerPrefs.GetInt (data_name [DataType.BackGroundObjects], 1));
	}

	/**
	 * データをセットするときはこの関数を使ってね☆(int型)
	 */
	public void DataSetterInt(int new_data, DataType data_type)
	{
		if (new_data >= data_min [data_type] && new_data <= data_max [data_type]) {
			PlayerPrefs.SetInt (data_name [data_type], new_data);
			data_value.Remove (data_type);
			data_value.Add (data_type, new_data);
		}
	}

	/**
	 * データの値が欲しければこの関数を使え(int型)
	 */
	public int DataGetterInt(DataType data_type)
	{
		return data_value [data_type];
	}

	/**
	 * データの最大値が欲しければこれ(int)
	 */
	public int MaxGetterInt(DataType data_type)
	{
		return data_max [data_type];
	}

	/**
	 * データの最小値が欲しければこれ(int)
	 */
	public int MinGetterInt(DataType data_type)
	{
		return data_min [data_type];
	}
}
