using System;
using Android.App;
using Android.Widget;
using Android.OS;
using Android.Media;
using Android.Net;

namespace Mirror
{
	[Activity(Label = "Mirror", MainLauncher = true, Icon = "@mipmap/icon")]
	public class MainActivity : Activity
	{
		TrainingSet trainingSet;

		TestSet testSet;

		int trainingSetIndex;

		int testSetIndex;

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			SetContentView(Resource.Layout.Main);

			int[] buttonResourceIds = new int[] {
				Resource.Id.loadTrngSet,
				Resource.Id.loadTrngSetPrev,
				Resource.Id.loadTrngSetNext,
				Resource.Id.loadTestSet,
				Resource.Id.loadTestSetPrev,
				Resource.Id.loadTestSetNext
			};

			foreach (int resourceId in buttonResourceIds)
			{
				Button button = FindViewById<Button>(resourceId);
				button.Click += OnButtonClick;
			}

			int[] videoResourceIds = new int[] {
				Resource.Id.loadTrngSetVideo
			};

			foreach (int resourceId in videoResourceIds)
			{
				VideoView videoView = FindViewById<VideoView>(resourceId);
				videoView.Click += OnVideoViewClick;
			}

			int[] imageResourceIds = new int[] {
				Resource.Id.loadTestSetImage
			};

			foreach (int resourceId in imageResourceIds)
			{
				ImageView imageView = FindViewById<ImageView>(resourceId);
				imageView.Click += OnImageViewClick;
			}

			int[] textViewResourceIds = new int[] {
				Resource.Id.loadTrngSetId,
				Resource.Id.loadTestSetId,
				Resource.Id.loadTestSetActualLbl
			};

			foreach (int resourceId in textViewResourceIds)
			{
				TextView textView = FindViewById<TextView>(resourceId);
				textView.Click += OnTextViewClick;
			}

			int[] editTextResourceIds = new int[] {
				Resource.Id.loadTrngSetLabel,
				Resource.Id.loadTestSetLabel
			};

			foreach (int resourceId in editTextResourceIds)
			{
				EditText editText = FindViewById<EditText>(resourceId);
				editText.Click += OnEditTextClick;
			}

		}

		void OnButtonClick(object sender, EventArgs e)
		{
			Button buttonSender = sender as Button;
			if (buttonSender != null)
			{
				if (buttonSender.Id == Resource.Id.loadTrngSet)
				{
					trainingSet = trainingSet ?? new TrainingSet();
					trainingSet.LoadMediaUri(OnTrainingSetLoaded);
				}
				else if (buttonSender.Id == Resource.Id.loadTestSet)
				{
					testSet = testSet ?? new TestSet();
					testSet.LoadMediaUri(OnTestSetLoaded);
				}
				else if (buttonSender.Id == Resource.Id.loadTrngSetPrev)
				{
					trainingSetIndex = Math.Max(0, --trainingSetIndex);
					LoadTrainingMedia();
				}
				else if (buttonSender.Id == Resource.Id.loadTrngSetNext)
				{
					if (trainingSet != null)
					{
						int trainingMediaCount = trainingSet.MediaCount;
						trainingSetIndex = Math.Min(trainingMediaCount - 1, ++trainingSetIndex);
						LoadTrainingMedia();
					}
				}
				else if (buttonSender.Id == Resource.Id.loadTestSetPrev)
				{
					testSetIndex = Math.Max(0, --testSetIndex);
					LoadTestMedia();
				}
				else if (buttonSender.Id == Resource.Id.loadTestSetNext)
				{
					if (testSet != null)
					{
						int testMediaCount = testSet.MediaCount;
						testSetIndex = Math.Min(testMediaCount - 1, ++testSetIndex);
						LoadTestMedia();
					}
				}
			}
		}

		void OnVideoViewClick(object sender, EventArgs e)
		{
		}

		void OnImageViewClick(object sender, EventArgs e)
		{
		}

		void OnTextViewClick(object sender, EventArgs e)
		{
		}

		void OnEditTextClick(object sender, EventArgs e)
		{
		}

		void OnTrainingSetLoaded()
		{
			LoadTrainingMedia();
		}

		void OnTestSetLoaded()
		{
			LoadTestMedia();
		}

		void LoadTrainingMedia()
		{
			Android.Net.Uri mediaUri = trainingSet.GetMediaUri(trainingSetIndex);
			if (mediaUri != null)
			{
				TextView textView = FindViewById<TextView>(Resource.Id.loadTrngSetId);
				textView.Text = mediaUri.LastPathSegment;

				VideoView videoView = FindViewById<VideoView>(Resource.Id.loadTrngSetVideo);
				videoView.SetVideoURI(mediaUri);
				videoView.RequestFocus();
				videoView.Start();
			}
			else
			{
				AlertDialog.Builder alertBuilder = new AlertDialog.Builder(this);
				alertBuilder.SetTitle("No Training Data");

				AlertDialog alert = alertBuilder.Create();
				alert.Show();
			}
		}

		void LoadTestMedia()
		{
			Android.Net.Uri mediaUri = testSet.GetMediaUri(testSetIndex);
			if (mediaUri != null)
			{
				TextView textView = FindViewById<TextView>(Resource.Id.loadTestSetId);
				textView.Text = mediaUri.LastPathSegment;

				ImageView imageView = FindViewById<ImageView>(Resource.Id.loadTestSetImage);
				imageView.SetImageURI(mediaUri);
				imageView.RequestFocus();
			}
			else
			{
				AlertDialog.Builder alertBuilder = new AlertDialog.Builder(this);
				alertBuilder.SetTitle("No Test Data");

				AlertDialog alert = alertBuilder.Create();
				alert.Show();
			}
		}
			
	}
}

