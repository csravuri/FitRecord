using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using Xamarin.Forms.Internals;

namespace FitRecord.Common
{
    public static class Utils
    {
        public static void MakeSureDirectoryExists(string folderPath)
        {
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
        }

        public static void AddRange<T>(this ObservableCollection<T> observableCollection, IEnumerable<T> list)
        {
            if (list == null) { throw new ArgumentNullException(nameof(list)); }

            list.ForEach(x => observableCollection.Add(x));
        }
    }
}
