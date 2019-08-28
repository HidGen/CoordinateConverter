using CoordinateConverter.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoordinateConverter.FileInteractions
{
    interface IExcelFileOpen
    {
        List<RectCoord> Read(string path);
        Task<List<RectCoord>> ReadAsync(string path);
        List<RectCoord> ReadRange(string path, string first, string last);
        Task<List<RectCoord>> ReadRangeAsync(string path, string first, string last);
    }
}
