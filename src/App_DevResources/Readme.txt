
## Tasks to learn


rhino licencing find source samples and update
MenuItem from class instead of xaml
viss git logo
font awsome icons samples
image zoon and pan, see gestures
csv write
zip folder
Master view
GPS UTM
	https://en.wikipedia.org/wiki/Geocode

canon uwp

localization
	see xamarin-fomms-samples TodoLocalized
	see xamarin-fomms-samples TodoLocalizedRTL
	see xamarin-fomms-samples UsingResxLocalization
	see xamarin-fomms-samples UsingResxLocalization
	C:\_Data\GitHub\Xamarin\docs-archive\Samples\TodoLocalized
	C:\_Data\GitHub\Xamarin\docs-archive\Samples\UsingResxLocalization
	https://www.romaniancoder.com/set-culture-for-all-threads-in-c-apps/
	https://docs.microsoft.com/en-us/xamarin/xamarin-forms/app-fundamentals/localization/

telemetry
	See https://docs.microsoft.com/en-us/appcenter/
	dbsite, navigation, last used,location,errors and many more

## Next
ListView
https://docs.microsoft.com/en-us/xamarin/xamarin-forms/user-interface/
https://docs.microsoft.com/en-us/xamarin/xamarin-forms/user-interface/stepper

review
https://github.com/Daddoon/BlazorMobile
https://github.com/bramborman/UWPHelper 

## Temp

TeamCity
	install
	host name (current is package ID)
	configure
	database extenral sql express
	agentss 3x
	domain user name

get file encoding

Conflict Resolution
	last save always wins
	
https://github.com/kestasjk/DiffSync.NET

     // Write results
		Console.WriteLine("Total changes downloaded: " + result.TotalChangesDownloaded);
		Console.WriteLine("Total changes uploaded: " + result.TotalChangesUploaded);
		Console.WriteLine("Total conflicts: " + result.TotalSyncConflicts);
		Console.WriteLine("Total errors: " + result.TotalSyncErrors);
		Console.WriteLine("Total StartTime: " + result.StartTime);
		Console.WriteLine("Total CompleteTime: " + result.CompleteTime);
		Console.WriteLine("Total duration: " + result.CompleteTime);
		Console.WriteLine("Total message: " + result.Message);
		
		
[Serializable]
	public class Results
	{
		public DateTime StartTime
		{
			get;
			set;
		}

		public string ScopeName
		{
			get;
			set;
		}

		public DateTime CompleteTime
		{
			get;
			set;
		}

		public int TotalChangesDownloaded
		{
			get;
			set;
		}

		public int TotalChangesUploaded
		{
			get;
			set;
		}

		public int TotalSyncConflicts
		{
			get;
			set;
		}

		public int TotalSyncErrors
		{
			get;
			set;
		}

		public string Message
		{
			get;
			set;
		}

		public override string ToString()
		{
			if (CompleteTime != StartTime && CompleteTime > StartTime)
			{
				TimeSpan timeSpan = TimeSpan.FromTicks(CompleteTime.Ticks);
				TimeSpan ts = TimeSpan.FromTicks(StartTime.Ticks);
				TimeSpan timeSpan2 = timeSpan.Subtract(ts);
				string text = $"{timeSpan2.Hours}:{timeSpan2.Minutes}:{timeSpan2.Seconds}.{timeSpan2.Milliseconds}";
				return "Synchronization done. " + Environment.NewLine + $"\tTotal changes downloaded: {TotalChangesDownloaded} " + Environment.NewLine + $"\tTotal changes uploaded: {TotalChangesUploaded}" + Environment.NewLine + $"\tTotal conflicts: {TotalSyncConflicts}" + Environment.NewLine + "\tTotal duration :" + text + " ";
			}
			return base.ToString();
		}
	}
	


## Licencing
	Xml or json for Server, Apps
	Always HTTPS

## Licencing API
	Get server licence
	Get app licence
	
	
public static void ForceFileDelete(string path, bool throwOnFail)
        {
            const int attempts = 10;
            for (var i = 0; i < attempts; i++)
            {
                try
                {
                    if (File.Exists(path) == false)
                    {
                        return;
                    }
                    File.Delete(path);
                }
                catch (FileNotFoundException)
                {
                    return;
                }
                catch (IOException)
                {
                    System.Threading.Thread.Sleep(1000);
                    continue;
                }
                return;
            }

            if (throwOnFail)
            {
                throw new IOException(string.Format("Unable to delete file. {0}", path));
            }
        }
		
		 public static void ForceFileDelete(string path)
        {
            const int attempts = 10;
            for (var i = 0; i < attempts; i++)
            {
                try
                {
                    if (File.Exists(path) == false)
                    {
                        return;
                    }
                    File.Delete(path);
                }
                catch (FileNotFoundException)
                {
                    return;
                }
                catch (IOException)
                {
                    System.Threading.Thread.Sleep(50);
                    continue;
                }
                return;
            }

            throw new IOException(string.Format("Unable to delete file. {0}", path));
        }