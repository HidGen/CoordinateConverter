using CoordinateConverter.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoordinateConverter.FileInteractions
{
    interface IExcelFileOpen
    {
        List<RectCoord> Read(string path);
        Task<List<RectCoord>> ReadAsync(string path);
    }
}
