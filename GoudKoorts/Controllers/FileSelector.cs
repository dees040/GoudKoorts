using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GoudKoorts.Controllers
{
    class FileSelector
    {
        public string Get()
        {
            OpenFileDialog file = new OpenFileDialog();

            if (file.ShowDialog() == DialogResult.OK)
            {
                return file.FileName;
            }

            throw new Exception("File not found.");
        }
    }
}
