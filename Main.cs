using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace Trash_Sorter
{
    public partial class Main : Form
    {
        private DirectoryInfo directory;
        private Dictionary<string, (string, string)> fileCategories;
        private string targetPath;


        public Main()
        {
            InitializeComponent();
            directory = new DirectoryInfo(Directory.GetCurrentDirectory()); // Получаем текущий рабочий каталог
        }


        private void browseButton_Click(object sender, EventArgs e)
        {
            using (var folderDialog = new FolderBrowserDialog())
            {
                DialogResult result = folderDialog.ShowDialog();
                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(folderDialog.SelectedPath))
                {
                    string folderPath = folderDialog.SelectedPath;
                    SortFiles(folderPath);
                    statusBar.ForeColor = System.Drawing.Color.Green;
                    statusBar.Text = "Файлы успешно отсортированы по категориям!";
                    
                }
            }
        }
        private void SortFiles(string folderPath)
        {
            try
            {
                string[] files = Directory.GetFiles(folderPath);

                foreach (string file in files)
                {
                    string category = GetCategory(file);
                    string destinationPath = Path.Combine(folderPath, category);

                    if (!Directory.Exists(destinationPath))
                    {
                        Directory.CreateDirectory(destinationPath);
                    }

                    string fileName = Path.GetFileName(file);
                    string destinationFile = Path.Combine(destinationPath, fileName);

                    File.Move(file, destinationFile);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при сортировке файлов: " + ex.Message);
            }
        }

        private string GetCategory(string filePath)
        {
            string extension = Path.GetExtension(filePath);
            switch (extension.ToLower())
            {
                case ".txt":
                    return "Текстовые файлы";
                case ".doc":
                case ".docx":
                case ".docm":
                case ".pdf":
                    return "Документы";
                case ".jpg":
                case ".png":
                case ".gif":
                case ".bmp":
                case ".jpeg":
                case ".svg":
                    return "Изображения";
                case ".ico":
                    return "Иконки";
                case ".mp3":
                case ".wav":
                case ".flac":
                    return "Аудио";
                case ".avi":
                case ".mp4":
                case ".mkv":
                case ".mov":
                    return "Видео";
                case ".exe":
                case ".msi":
                    return "Исполняемые файлы";
                case ".zip":
                case ".rar":
                case ".7z":
                case ".tar":
                case ".gz":
                    return "Архивы";
                case ".log":
                    return "Логи";
                case ".ct":
                case ".CT":
                    return "Таблицы CE";
                case ".csv":
                case ".xlsx":
                    return "Таблицы";
                case ".html":
                case ".htm":
                case ".xml":
                    return "Веб-страницы и XML";
                case ".dll":
                    return "Библиотеки";
                case ".cpp":
                    return "С++";
                case ".otf":
                    return "Шрифты";
                case ".safetensors":
                    return "Stable Diffusion Модели";
                default:
                    return "Прочие файлы";
            }
        }
    }
}