using CoordinateConverter.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoordinateConverter
{


    public static class ObservableCollectionExstension
    {
        public static void SortAsc<TSource>(this ObservableCollection<TSource> source, Comparison<TSource> comparer)
        {

            for (int i = 0; i < source.Count; i++)
            {
                for (int j = 0; j < source.Count - 1; j++)
                {
                    
                    if (comparer(source[j], source[j + 1])>0)
                    {
                        var z = source[j];
                        source[j] = source[j + 1];
                        source[j + 1] = z;
                    }
                }
            }

        }

        public static void SortDesc<TSource>(this ObservableCollection<TSource> source, Comparison<TSource> comparer)
        {
            for (int i = 0; i < source.Count; i++)
            {
                for (int j = 0; j < source.Count - 1; j++)
                {
                    
                    if (comparer(source[j], source[j + 1])<0)
                    {
                        var z = source[j];
                        source[j] = source[j + 1];
                        source[j + 1] = z;
                    }
                }
            }
        }

    }
}


