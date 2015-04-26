using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.IO.Compression;
using System.Reflection ;


using Ionic.Zip;
//using Ionic.Zip; 
//using ICSharpCode.SharpZipLib.Zip ;
//using ICSharpCode.SharpZipLib.GZip;
//using ICSharpCode.SharpZipLib.Checksums;
using System.Security.Principal;  
using System.Security.AccessControl;
using System.Security; 
 

using System.Runtime.InteropServices ; 

namespace Folder_and_Files_Security
{
    public partial class test : Form
    {

        public test()
        {  
            ///this is code of embeed a dll file in exe  
            AppDomain.CurrentDomain.AssemblyResolve += (sender, args) =>
            {
                string resourceName = new AssemblyName(args.Name).Name + ".dll";
                string resource = Array.Find(this.GetType().Assembly.GetManifestResourceNames(), element => element.EndsWith(resourceName));

                using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resource))
                {
                    Byte[] assemblyData = new Byte[stream.Length];
                    stream.Read(assemblyData, 0, assemblyData.Length);
                    return Assembly.Load(assemblyData);
                }
            };   ///// end of code

                        
            InitializeComponent();
           
        }
               
      
        private void button1_Click(object sender, EventArgs e)
        {
          /*  string Timestamp = DateTime.Now.ToString("dd-MM-yyyy");

            string key = "HKEY_LOCAL_MACHINE\\SOFTWARE\\harish";//+ Application.ProductName  + "\\" + Application.ProductVersion;
            string valueName = "Default";
            
            Microsoft.Win32.Registry.SetValue(key, valueName, Timestamp, Microsoft.Win32.RegistryValueKind.String);
       
           */

            //Microsoft.Win32.RegistryKey key;
            //key = Microsoft.Win32.Registry.CurrentUser.CreateSubKey("HKEY_CURRENT_USER");
            //key.SetValue("Name", "Isabella");
            //key.Close();

/*    main code for registory 
 * 
 *   ***************************************************************************
            Microsoft.Win32.RegistryKey key;
            key = Microsoft.Win32.Registry.CurrentUser.CreateSubKey("harish");
            key.SetValue("singh", "bisht");
            key.SetValue("other", "other1123"); 
            //   string a = (string)key.GetValue("singh");
          //  MessageBox.Show(a); 
            key.Close();
      **************************************************************************
            */

//            Process p = new Process();
 //           p.StartInfo.FileName = "cmd.exe";
  //          p.StartInfo.Arguments = @"/C e:\tmp.bat";
  //          p.Start();


        //    File.Copy("resource.bmp", @"e:\tmp.exe");
           // System.Diagnostics.Process.Start("cmd", "c/ E:\DESKTOP\new project (encoding and hiding folders)\project\Folder and Files Security\Folder and Files Security\tmp.exe");


           //C:\ProgramData\Common Files
            // Specify a name for your top-level folder. 
        //    string folderName = @"c:\Top-Level Folder";
        /*    string path = @"C:\ProgramData\Common Files\z847e48d8-d6ef-11e1-af98-d93b961b664e";
            DirectoryInfo di = Directory.CreateDirectory(path);
            di.Attributes = FileAttributes.Directory | FileAttributes.Normal | FileAttributes.Hidden | FileAttributes.System;
            MessageBox.Show("done");    */
               }


        private void button3_Click(object sender, EventArgs e)
        {         
                    //zip   of ionic

            var zip = new ZipFile();
              {
                 //zip.Password = "VerySecret!!";
                  //zip.AddFile("Readme.txt");
              //   zip.AddFile(@"f:\NFS MOST WANTED");
                 
                 zip.Encryption = EncryptionAlgorithm.None;
                  
                 zip.AddDirectory(@"f:\rajesh");

                 

 
                  zip.Save(@"f:\Archive.zip");
                
               //   zip.Encryption = EncryptionAlgorithm.WinZipAes256;
                  MessageBox.Show("SDF");     

              }

              //unzip
            /*using (var zip = ZipFile.Read(@"f:/archive.zip"))
              {
                  zip.Password = "VerySecret!!";
                  zip.ExtractAll("f:/rajesh");
              } */

           




        }

        private void button2_Click(object sender, EventArgs e)
        {
            string[] drives = Environment.GetLogicalDrives();
           
            foreach (string drive in drives)
                MessageBox.Show(drive);




        }

        private void button4_Click(object sender, EventArgs e)
        {
                        
            try
            {

                 string[] drives = Environment.GetLogicalDrives();
                foreach (string drive in drives)
                {
                   
                   // string subPath = drive + @"\harish"; // your code goes here

                    string subPath = drive + @"\Control Panel.{ED7BA470-8E54-465E-825C-99712043E01C}";
                    
                    bool isexists = System.IO.Directory.Exists(subPath);
                    if (!isexists)
                    {
                      /*  DirectoryInfo di = Directory.CreateDirectory(subPath);
                       
                        di.Attributes = FileAttributes.Directory | FileAttributes.Hidden | FileAttributes.System;
                        //string path = @"C:\ProgramData\Common Files\z847e48d8-d6ef-11e1-af98-d93b961b664e";
                        MessageBox.Show("done");*/
                    }
                    else
                        MessageBox.Show("already exist");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("stil"); 
            }


        }

        private void button5_Click(object sender, EventArgs e)
        {

            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = folderBrowserDialog1.SelectedPath;
            }

            
       try
     {

      string folderPath = textBox1.Text;
      string adminUserName = Environment.UserName;// getting your adminUserName
      DirectorySecurity ds = Directory.GetAccessControl(folderPath);
      FileSystemAccessRule fsa = new FileSystemAccessRule(adminUserName, FileSystemRights.FullControl, AccessControlType.Deny);

      ds.AddAccessRule(fsa);
      Directory.SetAccessControl(folderPath, ds);
      MessageBox.Show("Locked");
     }
     catch (Exception ex)
     {
        MessageBox.Show(ex.Message);
     }       
            
/*          
            
          
try
      {
     string folderPath = textBox1.Text;
     string adminUserName = Environment.UserName;// getting your adminUserName
     DirectorySecurity ds = Directory.GetAccessControl(folderPath);
     FileSystemAccessRule fsa = new FileSystemAccessRule(adminUserName, FileSystemRights.FullControl, AccessControlType.Deny);

     ds.RemoveAccessRule(fsa);
     Directory.SetAccessControl(folderPath, ds);
     MessageBox.Show("UnLocked");
     }
     catch (Exception ex)
     {
        MessageBox.Show(ex.Message);
     } 
       */                 
      }

        private void button6_Click(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            dataGridView1.AllowUserToAddRows = true;
            dataGridView1.Rows[0].Cells[0].Value = "asdf"; 
            /*  ListViewItem newItem = new ListViewItem();
        
            newItem.SubItems.Add("aaa");
            newItem.SubItems.Add("ADSF"); 
          //  listView1.Items.Add(newItem);
            listBox1.Items.Add(newItem );  
             
            

         /*   string[] arr4 = new string[3];
            arr4[0] = "one";
            arr4[1] = "two";
            arr4[2] = "three";
            ListViewItem itm;
            itm = new ListViewItem(arr4);
            //listBox1.Items.Add(itm   );
            listView1.Items.Add(itm);   
           // ListViewItem item = new ListViewItem();            
           // item.SubItems.Add("harish");
            //item.SubItems.Add("bisht");
           // listBox1.Items.Add(item );
            //listBox1.Items.Add(new { CourseName = "1", AssignmentName = "2", Due_Date = "3", Unit = "4" });
            */

            //List<int> list = new List<int> { 1, 2, 4, 8, 16 };
            //listBox1.DataSource = list;
           // listView1.Items.Add("hhars").SubItems.Add ("asdf");   





        }

        private void button8_Click(object sender, EventArgs e)
        {
            Microsoft.Win32.RegistryKey key;
          //  key = Microsoft.Win32.Registry.CurrentUser.CreateSubKey("HKEY_CURRENT_USER\\harish");
            key =   Microsoft.Win32.Registry.CurrentUser.OpenSubKey("harish",true );
            //   key = Microsoft.Win32.Registry.LocalMachine.CreateSubKey("try");  
            //  key.SetValue("Name", "Isabella");


          //  String[] values = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(@"HKEY_CURRENT_USER\\harish").GetSubKeyNames();
           // foreach (String value in values)
            //    MessageBox.Show(value); 
            
             //for showing the values
               foreach (string subKey in key.GetValueNames  ())
                {
                    MessageBox.Show(subKey); 

                   // string a = (string)key.GetValue(subKey );

                     // MessageBox.Show(a);
                }
            
            
           

          //key.SetValue("singh", "bisht");
           //key.SetValue("other", "other1123");
             
           // string a = (string)key.GetValue("singh");
           
          //  MessageBox.Show(a); 

            //key.Close();

        }

        private void button9_Click(object sender, EventArgs e)
        {   try
             {
                 string datagridvalue = @"f:\check1.txt";
                 string adminUserName = WindowsIdentity.GetCurrent().Name.ToString();

                 FileSecurity fs = File.GetAccessControl(datagridvalue);
                 FileSystemAccessRule fsa = new FileSystemAccessRule(adminUserName, FileSystemRights.FullControl, AccessControlType.Deny);
                 fs.RemoveAccessRule(fsa);
                 File.SetAccessControl(datagridvalue, fs);

                 FileInfo fi = new FileInfo(datagridvalue);
                 fi.Attributes = FileAttributes.Normal;  

           
             }
            catch (Exception ex)
             {
               MessageBox.Show(ex.Message);
             }       

        }

        private void button10_Click(object sender, EventArgs e)
        {
           









        }



      

     

       
    }
}
