using CoordinateConverter.Model;
using System;
using System.Collections.ObjectModel;

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

        public static void MoveUp<TSource>(this ObservableCollection<TSource> source, ObservableCollection<TSource> selection)
            where TSource : CompleteRow
        {
            for (int i = 0; i < source.Count; i++)
            {
                for (int j = 0; j < selection.Count; j++)
                {
                    if (selection[j] == source[i])
                    {
                        source.Move(i, i - 1);
                        break;
                    }
                }
            }          
        }


        public static void MoveDown<TSource>(this ObservableCollection<TSource> source, ObservableCollection<TSource> selection)
              where TSource : CompleteRow

        {
            for (int i = source.Count - 1; i >= 0; i--)
            {
                for (int j = 0; j < selection.Count; j++)
                {
                    if (selection[j] == source[i])
                    {
                        source.Move(i, i + 1);
                        break;
                    }
                }
            }
        }
    }
}


