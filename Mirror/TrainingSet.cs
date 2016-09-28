using System.IO;
using Android.OS;
using Android.Net;

namespace Mirror
{
	public class TrainingSet
	{
		Uri[] uriArr;

		public int MediaCount
		{
			get { return uriArr == null ? 0 : uriArr.Length; }
		}

		public TrainingSet()
		{
		}

		public Uri GetMediaUri(int index)
		{
			if (index < 0 || index >= MediaCount)
			{
				return null;
			}
			Uri uri = uriArr[index];
			return uri;
		}

		public void LoadMediaUri(OnSetLoadedDelegate callback)
		{
			uriArr = new Uri[0];
			string storageState = Environment.ExternalStorageState;
			if (storageState == "mounted")
			{
				Java.IO.File picturesDir = Environment.GetExternalStoragePublicDirectory(Environment.DirectoryPictures);
				string pathToTrainingSet = Path.Combine(picturesDir.AbsolutePath, "MirrorAppTrainingSet");
				DirectoryInfo dirInfo = Directory.CreateDirectory(pathToTrainingSet);
				FileInfo[] fileInfoArr = dirInfo.GetFiles();
				uriArr = new Uri[fileInfoArr.Length];
				for (int i = 0; i < fileInfoArr.Length; i++)
				{
					Uri fileUri = Uri.FromFile(new Java.IO.File(fileInfoArr[i].FullName));
					uriArr[i] = fileUri;
				}
			}
				
			callback();
		}
	}
}
