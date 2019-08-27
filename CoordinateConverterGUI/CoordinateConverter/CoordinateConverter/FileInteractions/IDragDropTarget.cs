using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoordinateConverter.FileInteractions
{
    interface IDragDropTarget
    {
        void OnFileDrop(string[] filepaths);
        void OnTextDrop(string str);
    }
}
