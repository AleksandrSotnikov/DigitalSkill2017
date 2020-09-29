using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace DigitalSkills2017
{
    public static class Manager
    {
        public static Frame MainFrame { get; set; }
        public static dbDigitalSkills2017Entities db;
        public static void fillDg(DataGrid dgView, string[] headers)
        {
            for (int i = 0; i < headers.Count(); i++)
                dgView.Columns[i].Header = headers[i];
        }
    }
}
