using UniRx;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Test.Models
{
	/// <summary>
	/// 
	/// </summary>
	public class CalculatorTest : MonoBehaviour
	{
		// Viewに渡す用のディスプレイに映す数字用変数
		public ReactiveProperty<float> DisplayNum = new ReactiveProperty<float>();
		// 入力した数字用変数
		private float _tempNum = 0;
		// 計算結果用変数
		private float _resultNum = 0;
		// 計算用の1つ前の数字用変数
		private float _preNum = 0;
		// 計算用の1つ前の数字用変数
		private string _preSign;
		// 一つ前の入力が数字か記号か
		private bool _isPreTextNum;

		public void DispayProcess(string text)
		{
			float num = 0;
			// textが数字なら
			if(float.TryParse(text, out num))
			{
				// 一つ前の入力が記号なら、入力用の数字をリセットする
				if(!_isPreTextNum)
				{
					_preNum = _tempNum;
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
				// 記号別の処理を書く
				switch(text)
				{
					case "=":
						_resultNum = Calculate(_tempNum, _preNum, _preSign);
						_tempNum = _resultNum;
						_preSign = "=";
						break;
					case "+":
						_tempNum = Calculate(_tempNum, _preNum, text);
						_preSign = "+";
						break;
					case "-":
						_tempNum = Calculate(_tempNum, _preNum, text);
						_preSign = "-";
						break;
					case "on":
						_tempNum = 0;
						_preNum = 0;
						_resultNum = 0;
						_preSign = "";
						break;


				}
				DisplayNum.Value = _tempNum;

				// 今入力されたのは数字じゃないよ、記号だよ
				_isPreTextNum = false;
			}
		}

		// 計算用関数
		private float Calculate(float tempNum, float preNum, string sign)
		{
			float result = 0;
			// Debug.Log("tempNum:" + tempNum);
			// Debug.Log("preNum:" + preNum);
			switch(sign)
			{
				case "+":
					result = preNum + tempNum;
					Debug.Log(result + "=" + tempNum + "+" + preNum);
					break;
				case "-":
					result = preNum - tempNum;
					Debug.Log(result + "=" + tempNum + "-" + preNum);
					break;
				case "*":
					result = preNum * tempNum;
					break;
				case "/":
					result = preNum / tempNum;
					break;
			}
			return result;
		}
	}
}