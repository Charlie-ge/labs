using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace ооп7
{
    public partial class Form1 : Form
    {
        private string currentFolderPath;
        public Form1()
        {
            InitializeComponent();
        }

        protected void ClearAll()
        {
            listBoxFiles.Items.Clear();
            listBoxFolders.Items.Clear();
            textBoxCreationTime.Text = "";
            textBoxFileName.Text = "";
            textBoxFolders.Text = "";
            textBoxInput.Text = "";
            textBoxLastAccesTime.Text = "";
            textBoxLastModTime.Text = "";
            textBoxSize.Text = "";
        }

        protected void DisableMoves()
        {
            textBoxNewPath.Text = "";
            textBoxNewPath.Enabled = false;
            buttonMoveTo.Enabled = false;
            buttonDelete.Enabled = false;
            buttonCopyTo.Enabled = false;
        }

        protected void DisplayFileInfo(string FileName)
        {
            FileInfo theFile = new FileInfo(FileName);
            if (!theFile.Exists)
            {
                throw new FileNotFoundException("Файл не найден: " + FileName);
            }

            textBoxCreationTime.Text = theFile.CreationTime.ToLongDateString();
            textBoxFileName.Text = theFile.Name;
            textBoxLastAccesTime.Text = theFile.LastAccessTime.ToLongDateString();
            textBoxLastModTime.Text = theFile.LastWriteTime.ToLongDateString();
            textBoxSize.Text = theFile.Length.ToString();

            textBoxNewPath.Text = theFile.FullName;
            textBoxNewPath.Enabled = true;
            buttonCopyTo.Enabled = true;
            buttonDelete.Enabled = true;
            buttonMoveTo.Enabled = true;
        }

        protected void DisplayFolders(string FolderFullName)
        {
            DirectoryInfo theFolder = new DirectoryInfo(FolderFullName);
            if (!theFolder.Exists)
            {
                throw new DirectoryNotFoundException("Папка не найдена" + FolderFullName);
            }

            ClearAll();
            DisableMoves();
            textBoxFolders.Text = theFolder.FullName;
            currentFolderPath = theFolder.FullName;

            foreach (DirectoryInfo nextFolder in theFolder.GetDirectories())
                listBoxFolders.Items.Add(nextFolder.Name);
            foreach (FileInfo nextFile in theFolder.GetFiles())
                listBoxFiles.Items.Add(nextFile.Name);
        }

        private void buttonShow_Click(object sender, EventArgs e)
        {
            try
            {
                string folderPath = textBoxInput.Text;
                DirectoryInfo theFolder = new DirectoryInfo(folderPath);
                FileInfo theFile = new FileInfo(folderPath);
                if (theFolder.Exists)
                {
                    treeView1.Nodes.Add(textBoxInput.Text);
                    PopulateTreeView(textBoxInput.Text, treeView1.Nodes[0]);
                    DisplayFolders(theFolder.FullName);
                    return;
                }

                if (theFile.Exists)
                {
                    DisplayFileInfo(theFile.Directory.FullName);
                    return;
                }
                throw new FileNotFoundException("There is no file or folder with" + "this name: " + textBoxInput.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void listBoxFiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string selectedString = listBoxFiles.SelectedItem.ToString();
                string fullFileName = Path.Combine(currentFolderPath,
                selectedString);
                DisplayFileInfo(fullFileName);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void listBoxFolders_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string selectedString = listBoxFolders.SelectedItem.ToString();
                string fullPathName = Path.Combine(currentFolderPath,
                selectedString);
                DisplayFolders(fullPathName);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void buttonUp_Click(object sender, EventArgs e)
        {
            try
            {
                string folderPath = new
                FileInfo(currentFolderPath).DirectoryName;
                DisplayFolders(folderPath);
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            try
            {
                string filePath = Path.Combine(currentFolderPath,
                textBoxFileName.Text);
                string query = "Really delete the file\n" + filePath + "?";
                if (MessageBox.Show(query,
                  "Delete File?", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    File.Delete(filePath);
                    DisplayFolders(currentFolderPath);
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show("Unable to delete file. The following exception"
                       + "occurred:\n" + ex.Message, "Failed");
            }

        }

        private void buttonCopyTo_Click(object sender, EventArgs e)
        {
            try
            {
                string filePath = Path.Combine(currentFolderPath,
                textBoxFileName.Text);
                string query = "Really copy the file\n" + filePath + "\nto"
                + textBoxNewPath.Text + "?";
                if (MessageBox.Show(query,
                    "Копировать файл?", MessageBoxButtons.YesNo) ==
                      DialogResult.Yes)
                {
                    File.Copy(filePath, textBoxNewPath.Text);
                    DisplayFolders(currentFolderPath);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Невозможно выполнить копирование файла" + "occurred:\n" + ex.Message, "Failed");
            }

        }

        private void buttonMoveTo_Click(object sender, EventArgs e)
        {
            try
            {
                string filePath = Path.Combine(currentFolderPath,
                textBoxFileName.Text);
                string query = "Really move the file\n" + filePath + "\nto "
                + textBoxNewPath.Text + "?";
                if (MessageBox.Show(query,
                     "Переместить файл?", MessageBoxButtons.YesNo) ==
                      DialogResult.Yes)
                {
                    File.Move(filePath, textBoxNewPath.Text);
                    DisplayFolders(currentFolderPath);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Невозможно выполнить перемещение файла" + "occurred:\n" + ex.Message, "Failed");
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string message = "";
            foreach (var drive in DriveInfo.GetDrives() )
            {
                message += "Имя диска: " + drive.Name + "\n";
                message += "Свободное доступное место (в байтах): " + drive.AvailableFreeSpace + "\n";
                message += "Вместимость диска (в байтах): " + drive.TotalSize + "\n";
                message += "Метка тома диска: " + drive.VolumeLabel + "\n";
                message += "Файловая система: " + drive.DriveFormat + "\n";
                message += "\n";
            }
            MessageBox.Show(message, "Информация о дисках");
        }

        public void PopulateTreeView(string directoryValue, TreeNode parentNode)
        {
            string[] directoryArray = Directory.GetDirectories(directoryValue);
            string[] files = Directory.GetFiles(directoryValue);
            try
            {    
                if (directoryArray.Length != 0)
                {
                    foreach (string directory in directoryArray)
                    {
                        TreeNode myNode = new TreeNode(directory);
                        parentNode.Nodes.Add(myNode);
                        PopulateTreeView(directory, myNode);
                    }
                    foreach (string file in files)
                    {
                        TreeNode myNodeF = new TreeNode(file);
                        parentNode.Nodes.Add(myNodeF);
                    }
                }
            }
            catch (UnauthorizedAccessException)
            {
                parentNode.Nodes.Add("Доступ закрыт");
            }
        }


        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            try
            {

                string selectedString = treeView1.SelectedNode.Text;
                FileInfo theFile = new FileInfo(selectedString);
                if (theFile.Exists)
                    DisplayFileInfo(selectedString);
                else DisplayFolders(selectedString);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
    }
}
