using CoordinateConverter.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoordinateConverter.FileInteractions
{
    interface IExcelFileOpen
    {
        IList<CompleteRow> Read(string path);
        Task<IList<CompleteRow>> ReadAsync(string path);
        List<RectCoord> ReadRange(string path, string first, string last);
        Task<List<RectCoord>> ReadRangeAsync(string path, string first, string last);
    }
}
