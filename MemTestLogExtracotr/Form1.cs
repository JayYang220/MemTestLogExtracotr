using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using MemTestLogExtracotr.lib;

namespace MemTestLogExtracotr
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        string targetExtension = ".log";
        string lastOutputPath = string.Empty;
        List<string> errTargetList = new List<string>();
        private readonly object errLock = new object();
        // 2025-05-09 23:56:07 - 
        Regex rx_date = new Regex(@"^(?<date>\d{4}-\d{2}-\d{2} \d{2}:\d{2}:\d{2}) - ", RegexOptions.IgnoreCase);
        // 2025-05-09 23:56:07 - MemTest86 V11.5 Pro Build: 1000 (64-bit)
        Regex rx_mVersion = new Regex(@"(?<version>MemTest86.*)", RegexOptions.IgnoreCase);
        // 2025-05-09 23:56:07 - SMBIOS BIOS INFO Vendor: "American Megatrends International, LLC.", Version: "4.02.TC18", Release Date: "02/05/2025"
        Regex rx_biosInfo = new Regex(@"SMBIOS BIOS INFO Vendor: ""(?<vendor>.*)"", Version: ""(?<version>.*)"", Release Date: ""(?<releaseDate>.*)""", RegexOptions.IgnoreCase);
        // 2025-05-09 23:56:19 - mem_size - Total memory size (16838270976 bytes)
        Regex rx_memSize = new Regex(@"mem_size - Total memory size \((?<size>\d+) bytes\)", RegexOptions.IgnoreCase);
        // 2025-05-09 23:56:50 - Current mem timings: 6400 MT/s (52-52-52-103)
        Regex rx_memTiming = new Regex(@"Current mem timings: (?<speed>\d+) MT/s \((?<timings>.*)\)", RegexOptions.IgnoreCase);
        // 2025-05-09 23:56:50 - Current CPU temperature: 54C
        Regex rx_cpuTemp = new Regex(@"Current CPU temperature: (?<temp>\d+)C", RegexOptions.IgnoreCase);
        // 2025-05-09 23:56:50 - Starting pass #1 (of 2)
        Regex rx_passStart = new Regex(@"Starting pass #(?<current>\d+) \(of (?<total>\d+)\)", RegexOptions.IgnoreCase);
        // 2025-05-10 00:18:57 - Finished pass #1 (of 2) (Cumulative error count: 0, buffer full count: 0)
        Regex rx_passEnd = new Regex(@"Finished pass #(?<current>\d+) \(of (?<total>\d+)\) \(Cumulative error count: (?<errorCount>\d+), buffer full count: (?<bufferFullCount>\d+)\)", RegexOptions.IgnoreCase);
        // 2025-05-10 00:59:04 - Test result: PASS (Errors: 0)
        Regex rx_testResult = new Regex(@"Test result: (?<result>\S+) \(Errors: (?<errorCount>\d+)\)", RegexOptions.IgnoreCase);

        private void TextBox_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                try
                {
                    e.Effect = DragDropEffects.Copy;
                }
                catch
                {
                    e.Effect = DragDropEffects.None;
                }
            }
        }

        private void TextBox_DragDropMulti(object sender, DragEventArgs e)
        {
            var files = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (files != null && files.Length > 0)
            {
                // 檢查files陣列中是否全部為null
                if (files.All(f => f == null))
                    return;

                // 讀取目前text內容，加入新路徑且移除重複的路徑
                string[] textLines = GetTextBoxLines(textBoxTarget);
                string[] allLines = RemoveDuplicates(textLines, files);
                if (((TextBox)sender).Multiline)
                {
                    ((TextBox)sender).Text = string.Join("\r\n", allLines);
                }
                else
                {
                    ((TextBox)sender).Text = string.Join(", ", allLines);
                }
            }
        }

        private void buttonAddFolder_Click(object sender, EventArgs e)
        {
            string folder = Helpers.FolderDialog();
            if (!string.IsNullOrEmpty(folder))
            {
                // 讀取目前text內容，加入新路徑且移除重複的路徑
                string[] textLines = GetTextBoxLines(textBoxTarget);
                string[] allLines = RemoveDuplicates(textLines, new[] { folder });
                if (textBoxTarget.Multiline)
                {
                    textBoxTarget.Text = string.Join("\r\n", allLines);
                }
                else
                {
                    textBoxTarget.Text = string.Join(", ", allLines);
                }
            }
        }

        private void buttonAddFiles_Click(object sender, EventArgs e)
        {
            string[] files = Helpers.FileDialog();

            if (files != null && files.Length > 0)
            {
                string[] textLines = GetTextBoxLines(textBoxTarget);
                string[] allLines = RemoveDuplicates(textLines, files);
                if (textBoxTarget.Multiline)
                {
                    textBoxTarget.Text = string.Join("\r\n", allLines);
                }
                else
                {
                    textBoxTarget.Text = string.Join(", ", allLines);
                }
            }
        }

        private string[] GetTextBoxLines(TextBox textBox)
        {
            return textBox.Text.Split(new[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries);
        }

        private string[] RemoveDuplicates(string[] line1, string[] line2)
        {
            return line1.Concat(line2).Distinct(StringComparer.OrdinalIgnoreCase).ToArray();
        }

        private async void buttonStart_Click(object sender, EventArgs e)
        {
            string[] target = GetTextBoxLines(textBoxTarget);
            errTargetList.Clear();
            List<string> targetList = new List<string>();

            buttonStart.Enabled = false;
            try
            {
                // 遍歷target陣列，檢查每個路徑
                foreach (string path in target)
                {
                    if (System.IO.Directory.Exists(path))
                    {
                        // 如果是資料夾, 若檔案符合targetExtension, 則加入到targetList
                        string[] filesInDir = System.IO.Directory.GetFiles(path, "*.*", System.IO.SearchOption.AllDirectories);
                        bool foundTargetInDir = false;
                        if (filesInDir != null && filesInDir.Length > 0)
                        {
                            foreach (string file in filesInDir)
                            {
                                if (file.EndsWith(targetExtension, StringComparison.OrdinalIgnoreCase))
                                {
                                    targetList.Add(file);
                                    foundTargetInDir = true;
                                }
                            }
                        }

                        if (!foundTargetInDir)
                        {
                            errTargetList.Add($"{path}: No Target found in directory.");
                        }
                    }
                    else if (System.IO.File.Exists(path))
                    {
                        // 如果是檔案, 且符合targetExtension, 則加入到targetList
                        if (path.EndsWith(targetExtension, StringComparison.OrdinalIgnoreCase))
                        {
                            targetList.Add(path);
                        }
                        else
                        {
                            errTargetList.Add($"{path}: File does not match target extension.");
                        }
                    }
                    else
                    {
                        errTargetList.Add($"{path}: Path does not exist.");
                    }
                }
                if (errTargetList.Count > 0)
                {
                    MessageBox.Show($"Please check the error list for details.\r\n{string.Join("\r\n", errTargetList)}", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else if (targetList.Count == 0)
                {
                    MessageBox.Show("No valid target files found. Please check the paths and try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            finally
            {
                buttonStart.Enabled = true;
            }

            buttonStart.Enabled = false;
            try
            {
                var result = await Task.Run(() =>
                {
                    List<TestSummary> summaryListLocal = new List<TestSummary>();
                    foreach (string path in targetList)
                    {
                        TestSummary summary = ParseLogFiles(path);
                        if (summary != null)
                            summaryListLocal.Add(summary);
                    }

                    string currentTime = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                    string outputPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"{currentTime}_output.csv");
                    ToCSV(summaryListLocal, outputPath);
                    return (Summaries: summaryListLocal, OutputPath: outputPath);
                });

                lastOutputPath = result.OutputPath;
                buttonOpenOutputFile.Enabled = true;

                if (errTargetList.Count > 0)
                {
                    MessageBox.Show($"Processing completed. {targetList.Count} files processed, {errTargetList.Count} errors found.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                { 
                    MessageBox.Show($"Processing completed. {targetList.Count} files processed successfully.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving CSV file: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                buttonStart.Enabled = true;
            }
        }

        private void buttonOpenOutputFile_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(lastOutputPath) && System.IO.File.Exists(lastOutputPath))
            {
                try
                {
                    Process.Start(new ProcessStartInfo(lastOutputPath) { UseShellExecute = true });
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error opening output file: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("No output file available to open.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void ToCSV(List<TestSummary> summaryList, string outputPath)
        {
            List<string> lines = new List<string>();
            lines.Add(TestSummary.GetTitle());
            foreach (var summary in summaryList)
            {
                lines.Add(summary.ToCsv());
            }
            System.IO.File.WriteAllLines(outputPath, lines);
        }

        private TestSummary ParseLogFiles(string path)
        {
            TestSummary testSummary = new TestSummary(path);

            try
            {
                string[] lines = System.IO.File.ReadAllLines(path);
                for (int i = 0; i < lines.Length; i++)
                {
                    string line = lines[i];
                    if (String.IsNullOrEmpty(testSummary.testDate))
                    {
                        Match m_date = rx_date.Match(line);
                        if (m_date.Success)
                        {
                            testSummary.testDate = m_date.Groups["date"].Value;
                            Debug.WriteLine($"Test Date: {testSummary.testDate}");
                        }
                    }
                    if (String.IsNullOrEmpty(testSummary.mVersion))
                    {
                        Match m_mVersion = rx_mVersion.Match(line);
                        if (m_mVersion.Success)
                        {
                            testSummary.mVersion = m_mVersion.Groups["version"].Value;
                            Debug.WriteLine($"MemTest Version: {testSummary.mVersion}");
                        }
                    }
                    if (String.IsNullOrEmpty(testSummary.biosInfo))
                    {
                        Match m_biosInfo = rx_biosInfo.Match(line);
                        if (m_biosInfo.Success)
                        {
                            string vendor = m_biosInfo.Groups["vendor"].Value;
                            string version = m_biosInfo.Groups["version"].Value;
                            string releaseDate = m_biosInfo.Groups["releaseDate"].Value;
                            testSummary.biosInfo = $"{version}";
                            Debug.WriteLine($"BIOS Vendor: {vendor}, Version: {version}, Release Date: {releaseDate}");
                        }
                    }
                    if (String.IsNullOrEmpty(testSummary.memSize))
                    {
                        Match m_memSize = rx_memSize.Match(line);
                        if (m_memSize.Success)
                        {
                            string size = m_memSize.Groups["size"].Value;
                            testSummary.memSize = size;
                            Debug.WriteLine($"Memory Size: {testSummary.memSize} bytes");
                        }
                    }
                    if (String.IsNullOrEmpty(testSummary.memTiming))
                    {
                        Match m_memTiming = rx_memTiming.Match(line);
                        if (m_memTiming.Success)
                        {
                            string speed = m_memTiming.Groups["speed"].Value;
                            string timings = m_memTiming.Groups["timings"].Value;
                            testSummary.memSpeed = int.Parse(speed);
                            testSummary.memTiming = $"{timings}";
                            Debug.WriteLine($"Memory Speed: {testSummary.memSpeed} MT/s, Timings: {testSummary.memTiming}");
                        }
                    }
                    Match m_cpuTemp = rx_cpuTemp.Match(line);
                    if (m_cpuTemp.Success)
                    {
                        string temp = m_cpuTemp.Groups["temp"].Value;
                        testSummary.cpuTempList.Add(int.Parse(temp));
                        Debug.WriteLine($"CPU Temperature: {temp}C");
                    }
                    if (testSummary.startTime < 0)
                    {
                        Match m_passStart = rx_passStart.Match(line);
                        if (m_passStart.Success)
                        {
                            string current = m_passStart.Groups["current"].Value;
                            string total = m_passStart.Groups["total"].Value;
                            Debug.WriteLine($"Starting Pass: {current} of {total}");
                            Match m_date = rx_date.Match(line);
                            if (m_date.Success)
                            {
                                string date = m_date.Groups["date"].Value;
                                DateTime dt;
                                if (DateTime.TryParse(date, out dt))
                                {
                                    testSummary.startTime = (int)(dt - new DateTime(1970, 1, 1)).TotalSeconds;
                                }
                            }
                        }
                    }

                    Match m_passEnd = rx_passEnd.Match(line);
                    if (m_passEnd.Success) 
                    { 
                        string current = m_passEnd.Groups["current"].Value;
                        string total = m_passEnd.Groups["total"].Value;
                        string errorCount = m_passEnd.Groups["errorCount"].Value;
                        string bufferFullCount = m_passEnd.Groups["bufferFullCount"].Value;
                        Debug.WriteLine($"Finished Pass: {current} of {total}, Cumulative Errors: {errorCount}, Buffer Full Count: {bufferFullCount}");
                    }
                    Match m_testResult = rx_testResult.Match(line);
                    if (m_testResult.Success)
                    {
                        string result = m_testResult.Groups["result"].Value;
                        string errorCount = m_testResult.Groups["errorCount"].Value;
                        testSummary.testResult = result;
                        testSummary.testErrorCount = int.Parse(errorCount);

                        Debug.WriteLine($"Test Result: {result}, Errors: {errorCount}");
                        Match m_date = rx_date.Match(line);
                        if (m_date.Success) 
                        { 
                            string date = m_date.Groups["date"].Value;
                            DateTime dt;
                            if (DateTime.TryParse(date, out dt))
                            {
                                testSummary.endTime = (int)(dt - new DateTime(1970, 1, 1)).TotalSeconds;
                                if (testSummary.startTime > 0 && testSummary.endTime > 0)
                                {
                                    testSummary.totalTestTime = testSummary.endTime - testSummary.startTime;
                                    Debug.WriteLine($"Total Test Time: {testSummary.totalTestTime} seconds");
                                }
                            }
                        }
                        break;
                    }
                }

                if (testSummary.CalculateCpuTempStats())
                {
                    Debug.WriteLine($"CPU Temperature(C), AVG: {testSummary.cpuAvgTemp:F2}, MAX: {testSummary.cpuMaxTemp}, MIN: {testSummary.cpuMinTemp}");
                }

                return testSummary;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                testSummary.errorMessage = $"{ex.Message}";
                lock (errLock)
                {
                    errTargetList.Add($"{path}: Error processing file, {ex.Message}");
                }
                return null;
            }
        }
    }

    internal class TestSummary
    {
        public string fileName { get; set; } = string.Empty;
        public string testDate { get; set; } = string.Empty;
        public string mVersion { get; set; } = string.Empty;
        public string biosInfo { get; set; } = string.Empty;
        public string memSize { get; set; } = string.Empty;
        public int memSpeed { get; set; } = -1;
        public string memTiming { get; set; } = string.Empty;
        public double cpuAvgTemp { get; set; } = -1;
        public double cpuMinTemp { get; set; } = -1;
        public double cpuMaxTemp { get; set; } = -1;
        public int totalTestTime { get; set; } = -1;
        public string testResult { get; set; } = string.Empty;
        public int testErrorCount { get; set; } = -1;

        public int startTime { get; set; } = -1;
        public int endTime { get; set; } = -1;
        public List<int> cpuTempList { get; set; } = new List<int>();

        public string errorMessage { get; set; } = string.Empty;

        public static string GetTitle()
        {
            return "File Name, TestDate, MemTest Ver, BIOS Ver, MemorySize(Bytes), MemorySpeed, MemoryTiming, CPU AvgTemp, CPU MinTemp, CPU MaxTemp, TotalTestTime(s), TestResult, TestErrorCount";
        }

        public string ToCsv()
        {
            if (!string.IsNullOrEmpty(errorMessage))
                return $"{fileName}, ERROR: {errorMessage}";
            else
                return $"{fileName}, {testDate}, {mVersion}, {biosInfo}, {memSize}, {memSpeed}, {memTiming}, {cpuAvgTemp:F2}, {cpuMinTemp}, {cpuMaxTemp}, {totalTestTime}, {testResult}, {testErrorCount}";
        }

        public TestSummary(string path)
        {
            fileName = System.IO.Path.GetFileName(path);
        }

        public bool CalculateCpuTempStats()
        {
            if (cpuTempList.Count > 0)
            {
                cpuAvgTemp = cpuTempList.Average();
                cpuMinTemp = cpuTempList.Min();
                cpuMaxTemp = cpuTempList.Max();
                return true;
            }
            return false;
        }
    }
}
