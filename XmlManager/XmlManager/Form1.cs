using System;
using System.IO;
using System.Windows.Forms;
using log4net;
using XmlManager.Controller;
using XmlManager.Model;
using XmlManager.Serializer;

namespace XmlManager
{
    public partial class Form1 : Form
    {
        private readonly IFileController _fileController = new FileController();
        private readonly ILog _logger = LogManager.GetLogger(typeof(Form1));
        private readonly IXmlSerializer _serializer = new XmlSerializer();
        private int _rowIndex;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            xmlObjectTableAdapter.Fill(xmlStorageDataSet.XmlObject);
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void OpenFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var fileDialog = new OpenFileDialog
            {
                Filter = "Xml Files|*.xml",
                Title = "Select an Xml File"
            };

            if (fileDialog.ShowDialog() != DialogResult.OK) return;

            var fileName = Path.GetFileNameWithoutExtension(fileDialog.FileName);
            if (!_fileController.IsFileNameCorrect(fileName))
            {
                MessageBox.Show("File name is not correct.");
                return;
            }

            var xmlObject = _serializer.Deserialize(fileDialog.OpenFile());
            if (_fileController.SaveFileToDB(xmlObject))
            {
                xmlObjectTableAdapter.Fill(xmlStorageDataSet.XmlObject);
                _logger.Info($"File {xmlObject} has been added.");
            }
            else
            {
                _logger.Info($"Error occured while adding file {xmlObject}.");
            }
        }

        private void saveChangesBtn_Click(object sender, EventArgs e)
        {
            xmlObjectTableAdapter.Update(xmlStorageDataSet);
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (!dataGridView1.Rows[_rowIndex].IsNewRow)
                {
                    dataGridView1.Rows.RemoveAt(_rowIndex);
                    _logger.Info($"Row at {_rowIndex} has been deleted.");
                }
            }
            catch (Exception exception)
            {
                _logger.Error($"Error occured while deleting row at {_rowIndex}. {exception}");
            }
        }

        private void dataGridView1_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right) return;

            dataGridView1.Rows[e.RowIndex].Selected = true;
            _rowIndex = e.RowIndex;
            dataGridView1.CurrentCell = dataGridView1.Rows[e.RowIndex].Cells[1];
            contextMenuStrip1.Show(dataGridView1, e.Location);
            contextMenuStrip1.Show(Cursor.Position);
        }

        private void saveToXmlToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                var cells = dataGridView1.Rows[_rowIndex].Cells;
                var xmlObject = new XmlObject
                {
                    Id = int.Parse(cells[0].Value.ToString()),
                    FileName = cells[1].Value.ToString(),
                    FileVersion = cells[2].Value.ToString(),
                    DateTime = DateTime.Parse(cells[3].Value.ToString())
                };

                var fileDialog = new SaveFileDialog
                {
                    Filter = "Xml Files|*.xml",
                    RestoreDirectory = true
                };

                if (fileDialog.ShowDialog() != DialogResult.OK) return;
                Stream myStream;
                if ((myStream = fileDialog.OpenFile()) != null)
                {
                    _serializer.Serialize(xmlObject, myStream);
                    myStream.Close();
                    _logger.Info($"File: {xmlObject} has been successfully saved to file system.");
                }
            }
            catch (Exception ex)
            {
                _logger.Error("Error occured while saving to xml: " + ex);
            }
        }

        private void CheckFileNameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var fileDialog = new OpenFileDialog
            {
                Filter = "Xml Files|*.xml",
                Title = "Select an Xml File"
            };

            if (fileDialog.ShowDialog() != DialogResult.OK) return;

            var fileName = Path.GetFileNameWithoutExtension(fileDialog.FileName);
            MessageBox.Show(_fileController.IsFileNameCorrect(fileName)
                ? "File name is correct."
                : "File name is not correct.");
        }
    }
}