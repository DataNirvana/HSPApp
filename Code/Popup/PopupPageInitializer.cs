﻿using System;
using System.Collections;
using System.Collections.Generic;
using Xamarin.Forms;

namespace FormsPopup
{
	/// <summary>
	/// Initialize the <see cref="Popup"/> views that are shown from a <seealso cref="ContentPage"/>
	/// </summary>
	public sealed class PopupPageInitializer : IEnumerable<Popup>
	{
		private readonly List<Popup> _popups = new List<Popup>();
		private readonly AbsoluteLayout _absContent = new AbsoluteLayout();
		private bool _initialized;

		public ContentPage ParentPage { get; set; }
		public IEnumerable<Popup> Popups => _popups;


	    /// <summary>
		/// Instantiate <see cref="PopupPageInitializer"/>
		/// </summary>
		/// <param name="parentPage">The page that contains the <see cref="Popup"/> views</param>
		public PopupPageInitializer(ContentPage parentPage)
		{
			if (parentPage == null) throw new ArgumentNullException(nameof(parentPage));

			ParentPage = parentPage;
			parentPage.ChildAdded += ParentPage_ChildAdded;
			parentPage.Appearing += ParentPage_Appearing;
		}


		/// <summary>
		/// This method must be called before the <seealso cref="ContentPage.Content"/> property is set.
		/// </summary>
		/// <param name="popup">The popup to be initialized</param>
		public void Add(Popup popup)
		{
			if (popup == null) throw new ArgumentNullException(nameof(popup));
			_popups.Add(popup);
		}


		public IEnumerator<Popup> GetEnumerator()
		{
			return _popups.GetEnumerator();
		}


		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}


		private void Initialize()
		{
			if (_initialized) return;
			_initialized = true;

			var oldContent = ParentPage.Content;

			Device.OnPlatform(() => ParentPage.Content = null);

			_absContent.Children.Add(oldContent);

			AbsoluteLayout.SetLayoutFlags(oldContent, AbsoluteLayoutFlags.All);
			AbsoluteLayout.SetLayoutBounds(oldContent, new Rectangle(0, 0, 1, 1));

			ParentPage.Content = _absContent;
		}


		private void ParentPage_ChildAdded(object sender, ElementEventArgs e)
		{
			if (ParentPage is PopupPage) return;

			if (ParentPage.Content == e.Element && e.Element != _absContent)
			{
				Initialize();
			}
		}


		private void ParentPage_Appearing(object sender, EventArgs e)
		{
			Initialize();

			foreach (var popup in Popups)
			{
				_absContent.Children.Add(popup, new Rectangle(0, 0, 1, 1), AbsoluteLayoutFlags.All);
				popup.OnInitializing();
			}
		}
	}
}