using System;
using System.IO;
using System.Windows.Forms;

namespace FormFinder
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void fileExtension_TextChanged(object sender, EventArgs e)
        {

        }

        private void searchText_TextChanged(object sender, EventArgs e)
        {

        }


        private void filePath_TextChanged(object sender, EventArgs e)
        {

        }

        private void outputBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void searchBtn_Click(object sender, EventArgs e)
        {
            try
            {
                FileLogger("File location method was utilized at ", DateTime.Now);
                string myFilePath = filePath.Text;
                string myFileExt = fileExtension.Text;

                var i = 0;
                for (i = 0; i < myFilePath.Length;)
                {

                    string[] myFiles = Directory.GetFiles(myFilePath, "*" + myFileExt);

                    foreach (string file in myFiles)
                    {
                        outputBox.AppendText(file + Environment.NewLine);

                    }

                    break;
                }
            }

            catch (FileNotFoundException) { FileLogger("File Not Found Error Thrown at", DateTime.Now); }
            finally { }
        }

        private void clearBtn_Click(object sender, EventArgs e)
        {
            try {
                FileLogger("Form fields where cleared at ", DateTime.Now);
                Action<Control.ControlCollection> func = null;

                func = (controls) =>
                {
                    foreach (Control control in controls)
                        if (control is TextBox)
                            (control as TextBox).Clear();
                        else
                            func(control.Controls);
                };

                func(Controls);
            }

            catch (FileNotFoundException) { FileLogger("File Not Found Error Thrown at", DateTime.Now); }
            finally { }
        }
        private void searchTextBtn_Click(object sender, EventArgs e)
        {
            try
            {
                FileLogger("Search string method was utilized at ", DateTime.Now);
                var userFilePathInput = filePath.Text; // text box value for file path
                var userExtensionInput = fileExtension.Text; // combo box value for extension

                // getting files per user input as arguements
                var files = Directory.GetFiles(userFilePathInput, "*" + userExtensionInput);

                foreach (var file in files)
                {
                    var result = searchFile(file.ToString(), searchText.Text);
                    if (result == true)
                    {
                        outputBox.Text = file;
                    }

                    else
                    {
                        outputBox.Text = "No files returned";
                    }
                }
            }

            catch (FileNotFoundException) { FileLogger("File Not Found Error Thrown at", DateTime.Now); }
            finally { }
        }

        private bool searchFile(string fileName, string searchString)
        {
            bool stringFound = false;

            FileStream mystream = new FileStream(fileName, FileMode.Open);
            StreamReader myreader = new StreamReader(mystream);

            while (!myreader.EndOfStream)
            {
                var line = myreader.ReadLine();

                if (line.ToUpper().Contains(searchString.ToUpper()))
                {
                    stringFound = true;
                    break;
                }
            }
           
            return stringFound;           
        }

        public void FileLogger(string myLog, DateTime theDateTime)
        {
            FileStream lFile = new FileStream("log.txt", FileMode.Append, FileAccess.Write);
            StreamWriter logStreamWriter = new StreamWriter(lFile);
            logStreamWriter.WriteLine(myLog + theDateTime);
            logStreamWriter.Close();
        }   
    }
}
