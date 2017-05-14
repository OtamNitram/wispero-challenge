using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wispero.Core;
using Wispero.Entities;

namespace Wispero.Export.Settings
{
    public sealed class QnAMakerSetting : Setting
    {
        public string FileName { get; private set; }
        public string Path { get; private set; }
        public string FullPath { get { return string.Format($"{Path}\\{FileName}"); } }

        public QnAMakerSetting(string path, string fileName) : base("QnAMaker")
        {
            FileName = fileName;
            Path = path;
        }

        public override void Export(List<KnowledgeBaseItem> source)
        {
            string lines = "";
            foreach (KnowledgeBaseItem item in source)
            {
                lines += string.Format($"{item.Query}\\t{item.Answer}") + "\r\n";
            }

            // Write the string to a file.
            System.IO.StreamWriter file = new System.IO.StreamWriter(FullPath);
            file.WriteLine(lines);

            file.Close();
        }
    }
}
