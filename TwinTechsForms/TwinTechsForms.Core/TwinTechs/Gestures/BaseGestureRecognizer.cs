﻿using System;
using Xamarin.Forms;
using System.Windows.Input;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.ComponentModel;
using System.Threading.Tasks;

[assembly: 
	InternalsVisibleTo ("TwinTechsLib.iOS"),
	InternalsVisibleTo ("TwinTechsLib.Droid")]
namespace TwinTechs.Gestures
{
	public enum GestureRecognizerState
	{
		Possible,
		Began,
		Changed,
		Ended,
		Cancelled,
		Failed,
		Recognized = 3
	}

	public class BaseGestureRecognizer : IGestureRecognizer
	{
		#region IGestureRecognizer impl

		public event PropertyChangedEventHandler PropertyChanged;

		#endregion

		public View View { get; set; }

		/// <summary>
		/// Gets or sets the command.
		/// </summary>
		/// <value>The command.</value>
		public ICommand Command {
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the command parameter.
		/// </summary>
		/// <value>The command parameter.</value>
		public object CommandParameter {
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the OnAction callback. Made available in case your views need access to the gesture responses
		/// </summary>
		/// <value>The tapped callback.</value>
		public Action<BaseGestureRecognizer> OnAction {
			get;
			set;
		}

		public bool DelaysTouchesEnded { get; set; } = false;

		public bool DelaysTouchesBegan { get; set; } = false;

		public bool CancelsTouchesInView { get; set; } = false;


		public GestureRecognizerState State { get { return NativeGestureRecognizer == null ? GestureRecognizerState.Failed : NativeGestureRecognizer.State; } }

		public int NumberOfTouches { get { return NativeGestureRecognizer == null ? 0 : NumberOfTouches; } }

		#region internal impl

		internal void SendAction ()
		{
			Command?.Execute (CommandParameter);
			OnAction?.Invoke (this);
		}

		/// <summary>
		/// Sets the underlying gesture recognzier - used by the factory for adding/removal
		/// </summary>
		/// <value>The native gesture recognizer.</value>
		internal INativeGestureRecognizer NativeGestureRecognizer { get; set; }


		#endregion

		public override string ToString ()
		{
			return string.Format ("[BaseGestureRecognizer: View={0}, State={1}]", View, State);
		}

		public Point LocationInView (VisualElement view)
		{
			return NativeGestureRecognizer.LocationInView (view);
		}

		public Point LocationOfTouch (int touchIndex, VisualElement view)
		{
			return NativeGestureRecognizer.LocationOfTouch (touchIndex, view);
		}
	}
}

