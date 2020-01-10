using CoordinateConverter.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoordinateConverter.FileInteractions
{
    interface IExcelFileOpen
    {
        IList<CompleteRow> Read(string path);
        Task<IList<CompleteRow>> ReadAsync(string path);
        IList<CompleteRow> ReadRange(string path, string first, string last);
        Task<IList<CompleteRow>> ReadRangeAsync(string path, string first, string last);
    }
}
