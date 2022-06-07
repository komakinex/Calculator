using UniRx;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MVRP.Models
{
	/// <summary>
	/// 
	/// </summary>
	public class Calculator : MonoBehaviour
	{
		// Viewに渡す用のディスプレイに映す数字用変数
		public ReactiveProperty<float> DisplayNum = new ReactiveProperty<float>();
		// 表示する数字用変数
		private float _tempNum = 0;
		// 計算用の1つ前の数字用変数
		private float _preNum = 0;
		// 計算用の1つ前の数字用変数
		private string _preSign = "";
		// 一つ前の入力が数字か記号か
		private bool _isPreTextNum;

		public void DispayProcess(string text)
		{
			float num = 0;
			// textが数字なら
			if(float.TryParse(text, out num))
			{
				// 一つ前の入力が記号なら、数字をリセットする
				if(!_isPreTextNum)
				{
					_tempNum = 0;
				}
				// 桁を増やしていく
				_tempNum = _tempNum * 10 + num;
				// Presenterに変更を通知
				DisplayNum.Value = _tempNum;
				Debug.Log(_tempNum);

				_isPreTextNum = true;
			}
			// textが記号なら
			else
			{
				Debug.Log("preNum: " + _preNum);
				Debug.Log("tempNum: " + _tempNum);
				Debug.Log("preSign: " + _preSign);
				Debug.Log("sign: " + text);
				// 記号別の処理を書く
				switch(text)
				{
					case "on":
						_tempNum = 0;
						_preNum = 0;
						_preSign = "";
						DisplayNum.Value = _tempNum;
						break;

					case "=":
						if(_preSign == "=")
						{
							_tempNum = _preNum;
						}
						_preNum = Calculate(_preNum, _tempNum, _preSign);
						_preSign = text;
						DisplayNum.Value = _preNum;
						break;

					default:
						if(_preSign != "")
						{
							_preNum = Calculate(_preNum, _tempNum, text);
						}
						else
						{
							_preNum = _tempNum;
						}
						_preSign = text;
						DisplayNum.Value = _preNum;
						_tempNum = 0;
						break;
				}
				Debug.Log("preSign: " + _preSign);
				Debug.Log("sign: " + text);

				// 今入力されたのは数字じゃないよ、記号だよ
				_isPreTextNum = false;
			}
		}

		// 計算用関数
		private float Calculate(float preNum, float tempNum, string sign)
		{
			float result = 0;
			switch(sign)
			{
				case "+":
					result = preNum + tempNum;
					Debug.Log(preNum + "+" + tempNum + "=" + result);
					break;
				case "*":
					result = preNum * tempNum;
					Debug.Log(preNum + "*" + tempNum + "=" + result);
					break;
				case "/":
					result = preNum / tempNum;
					Debug.Log(preNum + "/" + tempNum + "=" + result);
					break;
			}
			return result;
		}
	}
}