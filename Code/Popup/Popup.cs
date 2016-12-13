﻿using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FormsPopup
{
    /// <summary>
    ///     https://github.com/michaeled/FormsPopup
    /// Popup view to be displayed over a <see cref="ContentPage"/>.
    /// </summary>
    /// <remarks>
    /// No default styles have been created for this view.
    /// </remarks>
    public class Popup : ContentView
	{
		private readonly AbsoluteLayout _popupView = new AbsoluteLayout();
        private readonly RelativeLayout _sectionContainer = new RelativeLayout();

		private readonly ContentView _headerSection = new ContentView();
		private readonly ContentView _bodySection = new ContentView();
		private readonly ContentView _footerSection = new ContentView();

		private readonly BoxView _leftBorder = new BoxView();
		private readonly BoxView _rightBorder = new BoxView();
		private readonly BoxView _topBorder = new BoxView();
		private readonly BoxView _bottomBorder = new BoxView();

		private const double BorderWidth = 1;


        /// <summary>
        /// The SecionContainer includes the <see cref="Header"/>, <see cref="Body"/>, and <see cref="Footer"/>
        /// </summary>
        public VisualElement SectionContainer => _sectionContainer;


	    #region Events


		public event EventHandler<PopupTappedEventArgs> Tapped;
		public event EventHandler<EventArgs> Initializing;
		public event EventHandler<PopupShowingEventArgs> Showing;
		public event EventHandler<EventArgs> Shown;

		public event EventHandler<PopupHidingEventArgs> Hiding;
		public event EventHandler<EventArgs> Hidden;


		protected virtual void OnPropertyTapped(PopupTappedEventArgs e)
		{
            Tapped?.Invoke(this, e);
        }


		protected internal virtual void OnInitializing()
		{
		    Initializing?.Invoke(this, EventArgs.Empty);
		}


		protected virtual PopupShowingEventArgs OnShowing()
		{
			var args = new PopupShowingEventArgs();
		    Showing?.Invoke(this, args);
		    return args;
		}


		protected virtual void OnShown()
		{
            Shown?.Invoke(this, EventArgs.Empty);
		}


		protected virtual PopupHidingEventArgs OnHiding()
		{
			var args = new PopupHidingEventArgs();
            Hiding?.Invoke(this, args);
			return args;
		}


		protected virtual void OnHidden()
		{
			Hidden?.Invoke(this, EventArgs.Empty);
		}


		#endregion


		#region Dependency Properties


		public static readonly BindableProperty HeaderProperty = BindableProperty.Create(nameof(Header), typeof(View), typeof(Popup), null, propertyChanged: OnHeaderPropertyChanged);
	    public static readonly BindableProperty BodyProperty = BindableProperty.Create(nameof(Body), typeof (View), typeof (Popup), null, propertyChanged: OnBodyPropertyChanged);
	    public static readonly BindableProperty FooterProperty = BindableProperty.Create(nameof(Footer), typeof (View), typeof (Popup), null, propertyChanged: OnFooterPropertyChanged);

        public static readonly BindableProperty XPositionRequestProperty = BindableProperty.Create(nameof(XPositionRequest), typeof (double), typeof (Popup), default(double), propertyChanged: OnPositionChanged);
	    public static readonly BindableProperty YPositionRequestProperty = BindableProperty.Create(nameof(XPositionRequest), typeof (double), typeof (Popup), default(double), propertyChanged: OnPositionChanged);

	    public static readonly BindableProperty LeftBorderColorProperty = BindableProperty.Create(nameof(LeftBorderColor), typeof (Color), typeof (Popup), Color.Transparent, propertyChanged: OnLeftBorderChanged);
	    public static readonly BindableProperty RightBorderColorProperty = BindableProperty.Create(nameof(RightBorderColor), typeof (Color), typeof (Popup), Color.Transparent, propertyChanged: OnRightBorderChanged);
	    public static readonly BindableProperty TopBorderColorProperty = BindableProperty.Create(nameof(TopBorderColor), typeof (Color), typeof (Popup), Color.Transparent, propertyChanged: OnTopBorderChanged);
	    public static readonly BindableProperty BottomBorderColorProperty = BindableProperty.Create(nameof(BottomBorderColor), typeof (Color), typeof (Popup), Color.Transparent, propertyChanged: OnBottomBorderChanged);


	    public static readonly BindableProperty ContentWidthRequestProperty = BindableProperty.Create(nameof(ContentWidthRequest), typeof (double), typeof (Popup), default(double), propertyChanged: OnPositionChanged);
	    public static readonly BindableProperty ContentHeightRequestProperty = BindableProperty.Create(nameof(ContentHeightRequest), typeof (double), typeof (Popup), default(double), propertyChanged: OnPositionChanged);

	    internal static readonly BindableProperty SectionTypeProperty = BindableProperty.CreateAttached("SectionType", typeof (PopupSectionType), typeof (Popup), PopupSectionType.NotSet);


		public View Header
		{
			get { return (View)GetValue(HeaderProperty); }
			set
			{
				SetValue(HeaderProperty, value);
				_headerSection.Content = value;
			}
		}

		public View Body
		{
			get { return (View)GetValue(BodyProperty); }
			set
			{
				SetValue(BodyProperty, value);
				_bodySection.Content = value;
			}
		}


		public View Footer
		{
			get { return (View)GetValue(FooterProperty); }
			set
			{
				SetValue(FooterProperty, value);
				_footerSection.Content = value;
			}
		}


		public double XPositionRequest
		{
			get { return (double)GetValue(XPositionRequestProperty); }
			set { SetValue(XPositionRequestProperty, value); }
		}


		public double YPositionRequest
		{
			get { return (double)GetValue(YPositionRequestProperty); }
			set { SetValue(YPositionRequestProperty, value); }
		}


		public Color LeftBorderColor
		{
			get { return (Color)GetValue(LeftBorderColorProperty); }
			set { SetValue(LeftBorderColorProperty, value); }
		}


		public Color RightBorderColor
		{
			get { return (Color)GetValue(RightBorderColorProperty); }
			set { SetValue(RightBorderColorProperty, value); }
		}


		public Color TopBorderColor
		{
			get { return (Color)GetValue(TopBorderColorProperty); }
			set { SetValue(TopBorderColorProperty, value); }
		}


		public Color BottomBorderColor
		{
			get { return (Color)GetValue(BottomBorderColorProperty); }
			set { SetValue(BottomBorderColorProperty, value); }
		}


		public double ContentWidthRequest
		{
			get { return (double)GetValue(ContentWidthRequestProperty); }
			set { SetValue(ContentWidthRequestProperty, value); }
		}


		public double ContentHeightRequest
		{
			get { return (double)GetValue(ContentHeightRequestProperty); }
			set { SetValue(ContentHeightRequestProperty, value); }
		}


		#endregion


        #region Dependency Properties Changing


        private static void OnHeaderPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var popup = (Popup)bindable;
            popup.Header = (View)newValue;
        }


        private static void OnBodyPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var popup = (Popup)bindable;
            popup.Body = (View)newValue;
        }


        private static void OnFooterPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var popup = (Popup)bindable;
            popup.Footer = (View)newValue;
        }


        private static void OnPositionChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            var view = (VisualElement)bindable;
            var popup = view.FindParent<Popup>();

            var rect = new Rectangle(popup.XPositionRequest, popup.YPositionRequest, popup.ContentWidthRequest, popup.ContentHeightRequest);
            view.Layout(rect);
            AbsoluteLayout.SetLayoutBounds(view, rect);
        }


        private static void OnLeftBorderChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            var popup = (Popup)bindable;
            if (popup == null) return;

            popup._leftBorder.BackgroundColor = (Color) newvalue;
        }


        private static void OnRightBorderChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            var popup = (Popup)bindable;
            if (popup == null) return;

            popup._rightBorder.BackgroundColor = (Color) newvalue;
        }


        private static void OnTopBorderChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            var popup = (Popup)bindable;
            if (popup == null) return;

            popup._topBorder.BackgroundColor = (Color) newvalue;
        }


        private static void OnBottomBorderChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            var popup = (Popup)bindable;
            if (popup == null) return;

            popup._bottomBorder.BackgroundColor = (Color) newvalue;
        }


        #endregion


		public Popup()
		{
			IsVisible = false;
			_sectionContainer.BindingContext = this;
		    _sectionContainer.Padding = 0;
            _popupView.Padding = 0;
            _bodySection.VerticalOptions = LayoutOptions.FillAndExpand;


            // Used to identify if the user tapped within the header, body, or footer.
            _headerSection.SetValue(SectionTypeProperty, PopupSectionType.Header);
            _bodySection.SetValue(SectionTypeProperty, PopupSectionType.Body);
            _footerSection.SetValue(SectionTypeProperty, PopupSectionType.Footer);
            _popupView.SetValue(SectionTypeProperty, PopupSectionType.Backdrop);


            // Used to assign border colors
			_leftBorder.SetBinding(BackgroundColorProperty, Binding.Create((Popup p) => p.LeftBorderColor));
			_rightBorder.SetBinding(BackgroundColorProperty, Binding.Create((Popup p) => p.RightBorderColor));
			_bottomBorder.SetBinding(BackgroundColorProperty, Binding.Create((Popup p) => p.BottomBorderColor));
			_topBorder.SetBinding(BackgroundColorProperty, Binding.Create((Popup p) => p.TopBorderColor));


            // Adjust the layout bounds (not the overlay)
            _sectionContainer.SetBinding(ContentWidthRequestProperty, Binding.Create((Popup p) => p.ContentWidthRequest));
            _sectionContainer.SetBinding(ContentHeightRequestProperty, Binding.Create((Popup p) => p.ContentHeightRequest));
            _sectionContainer.SetBinding(XPositionRequestProperty, Binding.Create((Popup p) => p.XPositionRequest));
            _sectionContainer.SetBinding(YPositionRequestProperty, Binding.Create((Popup p) => p.YPositionRequest));


            // sizing section container. The overlay is not sized in this class.
            AbsoluteLayout.SetLayoutFlags(_sectionContainer, AbsoluteLayoutFlags.All);
            AbsoluteLayout.SetLayoutBounds(_sectionContainer, new Rectangle(0, 0, 1, 1));

            Initializing += OnPopupInitializing;


            // Create the content
		    var content = new StackLayout
		    {
		        Padding = 0,
		        Spacing = 0,
		        Orientation = StackOrientation.Vertical,

		        Children =
		        {
		            _headerSection,
		            _bodySection,
		            _footerSection
		        }
		    };

            _popupView.Children.Add(_sectionContainer);


            // Position content
            _sectionContainer.Children.Add(content, 
                Constraint.Constant(XPositionRequest), 
                Constraint.Constant(YPositionRequest),
                Constraint.RelativeToParent(p => p.Width),
                Constraint.RelativeToParent(p => p.Height));

            // Position left border
            _sectionContainer.Children.Add(_leftBorder,
                Constraint.Constant(0),
                Constraint.Constant(0),
                Constraint.Constant(BorderWidth),
                Constraint.RelativeToParent(p => p.Height));

            // Position right border
            _sectionContainer.Children.Add(_rightBorder,
                Constraint.RelativeToParent(p => p.Width),
                Constraint.Constant(0),
                Constraint.Constant(BorderWidth),
                Constraint.RelativeToParent(p => p.Height));

            // Position top border
            _sectionContainer.Children.Add(_topBorder,
                Constraint.Constant(0),
                Constraint.Constant(0),
                Constraint.RelativeToParent(p => p.Width),
                Constraint.Constant(BorderWidth));

            // Position bottom border
            _sectionContainer.Children.Add(_bottomBorder,
                Constraint.Constant(0),
                Constraint.RelativeToParent(p => p.Height),
                Constraint.RelativeToParent(p => p.Width),
                Constraint.Constant(BorderWidth));

            Content = _popupView;
		}


		/// <summary>
		/// Show the popup view.
		/// </summary>
        /// <param name="animation">The method is passed the VisualElement that contains the body, header, and footer</param>
		/// <remarks>
		/// This method is not limited adding animations.
		/// </remarks>
		public async Task ShowAsync(Func<Popup, Task> animation)
		{
			if (IsVisible)
			{
				return;
			}

			var parent = Parent.FindParent<Layout>();
		    parent?.RaiseChild(_popupView);

		    var handlerResponse = OnShowing();
			if (handlerResponse.Cancel)
			{
				return;
			}

			IsVisible = true;

			if (animation == null)
			{
				await Task.FromResult(0);
			}
			else
			{
                // the overlay is not passed to the caller
				await animation(this);
			}

			OnShown();
		}


		/// <summary>
		/// Show the popup view.
		/// </summary>
		public void Show()
		{
            #pragma warning disable 4014
			ShowAsync(null);
            #pragma warning restore 4014
		}


		/// <summary>
		/// Hide the popup view.
		/// </summary>
		/// <param name="animation">The method is passed the VisualElement that contains the body, header, and footer</param>
		/// <remarks>
		/// This method is not limited adding animations.
		/// </remarks>
        public async Task HideAsync(Func<Popup, Task> animation)
		{
			if (!IsVisible)
			{
				return;
			}

			var handlerResponse = OnHiding();

			if (handlerResponse.Cancel)
			{
				return;
			}

			if (animation == null)
			{
				await Task.FromResult(0);
			}
			else
			{
                // the overlay is not passed to the caller
				await animation(this);
			}

            IsVisible = false;

			OnHidden();
		}


		/// <summary>
		/// Hide the popup view.
		/// </summary>
		public void Hide()
		{
            #pragma warning disable 4014
			HideAsync(null);
            #pragma warning restore 4014
		}


		private void OnPopupInitializing(object sender, EventArgs e)
		{
			Func<GestureRecognizer> factory = delegate
			{
				var closeOnTap = new TapGestureRecognizer();

				var cmd = new Command(obj =>
				{
					var view = obj as View;

					if (view == null)
					{
						return;
					}

					var evt = PopupTappedEventArgs.Create(this, view);

					OnPropertyTapped(evt);
				});

				closeOnTap.Command = cmd;
				return closeOnTap;
			};

			TapGestureRecognizerVisitor.Visit(_popupView, factory);
		}
    }
}