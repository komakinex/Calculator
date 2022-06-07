using Test.Models;
using Test.Views;
using UniRx;
using UnityEngine;

namespace Test.Presenters
{
	/// <summary>
	/// 
	/// </summary>
	public class ResultPresenterTest : MonoBehaviour
	{
		[SerializeField] CalculatorTest _calculator;
		[SerializeField] DisplayTest _display;

		void Start()
		{
			// 押されたボタン：Viewからの通知
			_display.OnButtonClicked
				.Subscribe(text => 
				{
					// 何が押されたかModelに伝える：Modelへ連絡
					_calculator.DispayProcess(text);
				})
				.AddTo(this);
			// 表示するもの：Modelからの通知
			// DispllayNumに変更があったときだけ通知が来る
			_calculator.DisplayNum.SkipLatestValueOnSubscribe()
				.Subscribe(value =>
				{
					_display.DisplayText(value.ToString());
				})
				.AddTo(this);
			
		}

		void Update()
		{

		}
	}
}