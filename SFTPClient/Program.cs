using System;
using System.IO;
using Renci.SshNet;
using System.Threading;
using System.Threading.Tasks;

namespace SFTPClient
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string host = "172.22.45.70";
            int port = 22;
            string username = "muwaffaq";
            string password = "PASSword123";
            string remoteFile = "/var/www/html/wp-content/uploads/jobsearch-resumes/675154a621352/2024/12/etho6111993_cv_1976458782_inbound7095374419704858405.pdf";
            string localFile = "C:\\Users\\muwaffaq\\OneDrive - EXPRESSO TELECOM SERVICES DMCC\\Desktop\\cv.pdf";


            // Ensure proper disposal of SftpClient and handle connection errors
            try
            {
                using (SftpClient client = new SftpClient(new PasswordConnectionInfo(host, port, username, password)))
                {
                    client.Connect();

                    try
                    {
                        new Program().DownloadFiles(client, localFile, remoteFile);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"An error occurred while downloading the file: {ex.Message}");
                    }

                    client.Disconnect();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while connecting to the SFTP server: {ex.Message}");
            }

            Console.ReadLine();
        }

        // Download files from remote server
        private void DownloadFiles(SftpClient client, string localFile, string remoteFile)
        {
            // Ensure proper disposal of FileStream and handle file-related errors
            try
            {
                using (FileStream fs = new FileStream(localFile, FileMode.CreateNew, FileAccess.Write, FileShare.None))
                {
                    if (!client.IsConnected)
                    {
                        Console.WriteLine("The client is not connected. Attempting to connect...");
                        client.Connect();
                    }

                    try
                    {
                        client.DownloadFile(remoteFile, fs, bytesDownloaded =>
                        {
                            Console.WriteLine($"Downloaded {bytesDownloaded} bytes");
                        });

                        fs.Flush(); // Ensure all data is written to the file

                        // Check if the file is downloaded successfully
                        if (fs.Length > 0)
                            Console.WriteLine("File downloaded successfully.");
                        else
                            throw new Exception("File download failed: The file is empty.");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"An error occurred during the download: {ex.Message}");
                        throw;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while accessing the local file: {ex.Message}");
                throw;
            }
        }
    }
}