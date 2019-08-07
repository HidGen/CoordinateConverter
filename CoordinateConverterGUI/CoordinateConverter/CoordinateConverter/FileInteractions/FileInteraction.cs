using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoordinateConverter.FileInteractions
{
    class FileInteraction : IExcelFileOpen, IXmlFileSave
    {
        public void OpenFile(string path)
        {
            
        }

        public bool Save(string path)
        {
            throw new NotImplementedException();
        }
    }
}
