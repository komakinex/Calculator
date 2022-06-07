using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Test.Views
{
	/// <summary>
	/// 
	/// </summary>
	public class DisplayTest : MonoBehaviour
	{
		// Button
		// Number
		[SerializeField] private Button _btnZero, _btnOne, _btnTwo, _btnThree, _btnFour;
		// Sign
		[SerializeField] private Button _btnPlus, _btnMinus, _btnMulti, _btnDivide, _btnEqual, _btnON;

		public Subject<string> OnButtonClicked = new Subject<string>();

		// Displayに表示される文字
		[SerializeField] private Text _text;

		public void DisplayText(string text)
		{
			_text.text = text;
		}
		public void ClearText()
		{
			_text.text = "";
		}
		public void InitText()
		{
			_text.text = "0";
		}

		void Start()
		{
			InitText();
			SubscribeButtons();
		}

		// 各ボタンを押したときのイベント
		private void SubscribeButtons()
		{
			// Number
			_btnZero.OnClickAsObservable()
				.Subscribe(_ => {
					OnButtonClicked.OnNext("0"); // このボタンを押したときに購読側へ伝えることをOnNextに書く
				})
				.AddTo(this);
			_btnOne.OnClickAsObservable()
				.Subscribe(_ => {OnButtonClicked.OnNext("1");})
				.AddTo(this);
			_btnTwo.OnClickAsObservable()
				.Subscribe(_ => {OnButtonClicked.OnNext("2");})
				.AddTo(this);
			_btnThree.OnClickAsObservable()
				.Subscribe(_ => {OnButtonClicked.OnNext("3");})
				.AddTo(this);
			_btnFour.OnClickAsObservable()
				.Subscribe(_ => {OnButtonClicked.OnNext("4");})
				.AddTo(this);
			// Sign
			_btnPlus.OnClickAsObservable()
				.Subscribe(_ => {OnButtonClicked.OnNext("+");})
				.AddTo(this);
			_btnMinus.OnClickAsObservable()
				.Subscribe(_ => {OnButtonClicked.OnNext("-");})
				.AddTo(this);
			_btnMulti.OnClickAsObservable()
				.Subscribe(_ => {OnButtonClicked.OnNext("*");})
				.AddTo(this);
			_btnDivide.OnClickAsObservable()
				.Subscribe(_ => {OnButtonClicked.OnNext("/");})
				.AddTo(this);
			_btnEqual.OnClickAsObservable()
				.Subscribe(_ => {OnButtonClicked.OnNext("=");})
				.AddTo(this);
			_btnON.OnClickAsObservable()
				.Subscribe(_ => {OnButtonClicked.OnNext("on");})
				.AddTo(this);
		}

	}
}