using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Runtime.InteropServices;
using System.Reflection;
 
namespace Folder_and_Files_Security

   

{

    
    
    
    public partial class Form1 : Form
    {



   
    


        
        /// <summary>
        /// statrting of explore refresh code
        /// </summary>
        public enum SHCNE : uint
        {
            /*SHCNE_RENAMEITEM = 0x00000001,
            SHCNE_CREATE = 0x00000002,
            SHCNE_DELETE = 0x00000004,
            SHCNE_MKDIR = 0x00000008,
            SHCNE_RMDIR = 0x00000010,
            SHCNE_MEDIAINSERTED = 0x00000020,
            SHCNE_MEDIAREMOVED = 0x00000040,
            SHCNE_DRIVEREMOVED = 0x00000080,
            SHCNE_DRIVEADD = 0x00000100,
            SHCNE_NETSHARE = 0x00000200,
            SHCNE_NETUNSHARE = 0x00000400,
            SHCNE_ATTRIBUTES = 0x00000800,*/
           // SHCNE_UPDATEDIR = 0x00001000,
           // SHCNE_UPDATEITEM = 0x00002000,
           /* SHCNE_SERVERDISCONNECT = 0x00004000,
            SHCNE_UPDATEIMAGE = 0x00008000,
            SHCNE_DRIVEADDGUI = 0x00010000,
            SHCNE_RENAMEFOLDER = 0x00020000,
            SHCNE_FREESPACE = 0x00040000,
            SHCNE_EXTENDED_EVENT = 0x04000000, */
            SHCNE_ASSOCCHANGED = 0x08000000,
           /* SHCNE_DISKEVENTS = 0x0002381F,
            SHCNE_GLOBALEVENTS = 0x0C0581E0,
            SHCNE_ALLEVENTS = 0x7FFFFFFF,
            SHCNE_INTERRUPT = 0x80000000*/
        }
        public enum SHCNF : uint
        {
            SHCNF_IDLIST = 0x0000,
          //  SHCNF_PATHA = 0x0001,
          //  SHCNF_PRINTERA = 0x0002,
          //  SHCNF_DWORD = 0x0003,
          /*  SHCNF_PATHW = 0x0005,
            SHCNF_PRINTERW = 0x0006,
            SHCNF_TYPE = 0x00FF,
            SHCNF_FLUSH = 0x1000,
            SHCNF_FLUSHNOWAIT = 0x2000  */
        }
       
        /// fdsaf
        /// </summary>
       // explorer refresh code
        [DllImport("shell32.dll", CharSet = CharSet.Auto)]
        public static extern void SHChangeNotify(UInt32 wEventId, UInt32 uFlags, IntPtr dwItem1, IntPtr dwItem2);
 
        /// <summary>
        /// end of exploer refresh code
        /// </summary>





        string datagridvalue="";
        
        public int i;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
          //  this.MinimizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;
            
            label1.Parent = pictureBox1;
          label3.Parent = pictureBox1;
          label4.Parent = pictureBox1;
           label1.BackColor = Color.Transparent;
          label3.BackColor = Color.Transparent;
          label4.BackColor = Color.Transparent;
          refresh();
        }
        

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            groupBox1.Location = new Point(0, groupBox1.Location.Y);
        }

       

      

        private void pictureBox6_Click(object sender, EventArgs e)
        {
           
            locking();
            locking_of_file();
        }

        public void locking_of_file()
        {
            try
            {

                if (datagridvalue == "")
                {
                    ///
                }
                else
                {
                   
                    FileInfo fi = new FileInfo(datagridvalue);
                    fi.Attributes = FileAttributes.System| FileAttributes.Hidden |FileAttributes.Normal    ;  

                   
                    string adminUserName = WindowsIdentity.GetCurrent().Name.ToString();// getting your adminUserName
                    FileSecurity ds = File.GetAccessControl(datagridvalue  );
                    FileSystemAccessRule fsa = new FileSystemAccessRule(adminUserName, FileSystemRights.FullControl, AccessControlType.Deny);
                    ds.AddAccessRule(fsa);
                    File.SetAccessControl(datagridvalue , ds);
              //      MessageBox.Show("Locked");




                    Microsoft.Win32.RegistryKey key;
                    key = Microsoft.Win32.Registry.CurrentUser.CreateSubKey("harish");
                    key.SetValue(datagridvalue, "Locked");

                    //* testing code
                    //   SendKeys.SendWait("{F5}");

                    SHChangeNotify((uint)SHCNE.SHCNE_ASSOCCHANGED, (uint)SHCNF.SHCNF_IDLIST, IntPtr.Zero, IntPtr.Zero);


                }
            }
            catch (Exception ex)
            {

            }
            datagridvalue = "";
            dataGridView1.Rows.Clear();
            refresh();
            dataGridView1.ClearSelection(); 



        }
        public void unlocking_of_file()
        {
            if (datagridvalue == "")
            {
                ///
            }
            else
            {

                try
                {
                    string adminUserName = WindowsIdentity.GetCurrent().Name.ToString();

                    FileSecurity fs = File.GetAccessControl(datagridvalue);
                    FileSystemAccessRule fsa = new FileSystemAccessRule(adminUserName, FileSystemRights.FullControl, AccessControlType.Deny);
                    fs.RemoveAccessRule(fsa);
                    File.SetAccessControl(datagridvalue, fs);

                    FileInfo fi = new FileInfo(datagridvalue);
                    fi.Attributes = FileAttributes.Normal;  
 

                    Microsoft.Win32.RegistryKey key;
                    key = Microsoft.Win32.Registry.CurrentUser.CreateSubKey("harish");
                    key.SetValue(datagridvalue, "Unlocked");


                    //    SHChangeNotify((uint)SHCNE.SHCNE_ASSOCCHANGED, (uint)SHCNF.SHCNF_IDLIST, IntPtr.Zero, IntPtr.Zero);


                }
                catch (Exception ex)
                {
            //        MessageBox.Show(ex.Message);
                }

            }
            //   datagridvalue = "";
            dataGridView1.Rows.Clear();
            refresh();
            dataGridView1.ClearSelection(); 



        }


        public void locking()
         {
           try
           {

               if (datagridvalue == "")
               {
                   ///
               }
               else
               {
                   
                   DirectoryInfo di = new DirectoryInfo(datagridvalue);
                   di.Attributes = FileAttributes.System | FileAttributes.Directory | FileAttributes.Normal | FileAttributes.Hidden;

                   string adminUserName = WindowsIdentity.GetCurrent().Name.ToString();// getting your adminUserName
                //  WindowsIdentity.GetCurrent().Name.ToString()); 
                   
                   DirectorySecurity ds = Directory.GetAccessControl(datagridvalue);
                   FileSystemAccessRule fsa = new FileSystemAccessRule(adminUserName, FileSystemRights.FullControl, AccessControlType.Deny);
                   ds.AddAccessRule(fsa);
                   Directory.SetAccessControl(datagridvalue, ds);

                   Microsoft.Win32.RegistryKey key;
                   key = Microsoft.Win32.Registry.CurrentUser.CreateSubKey("harish");
                   key.SetValue(datagridvalue, "Locked");
                   
                   //* testing code
                //   SendKeys.SendWait("{F5}");

                   SHChangeNotify((uint)SHCNE.SHCNE_ASSOCCHANGED, (uint)SHCNF.SHCNF_IDLIST, IntPtr.Zero, IntPtr.Zero);
  
                   
               }
           }
           catch (Exception ex)
           {

           }
           datagridvalue = "";
           dataGridView1.Rows.Clear();
           refresh();
           dataGridView1.ClearSelection(); 

        }



        public void unlocking()
        {

           if (datagridvalue == "")
           {
               ///
           }
           else
           {
                        
            try
              {

               string adminUserName = WindowsIdentity.GetCurrent().Name.ToString();// getting your adminUserName
               DirectorySecurity ds = Directory.GetAccessControl(datagridvalue );
               FileSystemAccessRule fsa = new FileSystemAccessRule(adminUserName, FileSystemRights.FullControl, AccessControlType.Deny);
               ds.RemoveAccessRule(fsa);
               Directory.SetAccessControl(datagridvalue , ds);

               DirectoryInfo di = new DirectoryInfo(datagridvalue);
               di.Attributes = FileAttributes.Directory | FileAttributes.Normal ;
          
               Microsoft.Win32.RegistryKey key;
               key = Microsoft.Win32.Registry.CurrentUser.CreateSubKey("harish");
               key.SetValue(datagridvalue , "Unlocked");
              
                
            //    SHChangeNotify((uint)SHCNE.SHCNE_ASSOCCHANGED, (uint)SHCNF.SHCNF_IDLIST, IntPtr.Zero, IntPtr.Zero);
  
          
               }
             catch (Exception ex)
               {
                //MessageBox.Show(ex.Message);
               } 
                              
             }
          //   datagridvalue = "";
             dataGridView1.Rows.Clear();
             refresh();
             dataGridView1.ClearSelection(); 

        }

             

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            DialogResult result = folderBrowserDialog1.ShowDialog();
            
            if (result == DialogResult.OK)
            {
                datagridvalue = folderBrowserDialog1.SelectedPath;
                dataGridView1.ClearSelection(); 
                Microsoft.Win32.RegistryKey key;
                key = Microsoft.Win32.Registry.CurrentUser.CreateSubKey("harish");
                key.SetValue(folderBrowserDialog1.SelectedPath, "Locked");
                datagridvalue = folderBrowserDialog1.SelectedPath; 
    
                locking();
                dataGridView1.Rows.Clear(); 
                refresh();
            }
         }

        public void refresh()
        {
           
             Microsoft.Win32.RegistryKey key;
            key = Microsoft.Win32.Registry.CurrentUser.CreateSubKey("harish");
            int temp = 0; 
            dataGridView1.AllowUserToAddRows = true;
               foreach (string subKey in key.GetValueNames  ())
               {
                    dataGridView1.Rows.Add();
                    string a = (string)key.GetValue(subKey );
                    dataGridView1.Rows[temp].Cells[0].Value = subKey;
                    dataGridView1.Rows[temp].Cells[1].Value = (string)key.GetValue(subKey);
                    temp++;
                }
               dataGridView1.ClearSelection();
               datagridvalue = "";
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            if ( datagridvalue == "" )
            {
                ///
            }
            else
            {
                
                Microsoft.Win32.RegistryKey key;
                key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("harish", true);
                key.DeleteValue(datagridvalue);


                //unlocking before removing
                string adminUserName = WindowsIdentity.GetCurrent().Name.ToString();// getting your adminUserName
                DirectorySecurity ds = Directory.GetAccessControl(datagridvalue);
                FileSystemAccessRule fsa = new FileSystemAccessRule(adminUserName, FileSystemRights.FullControl, AccessControlType.Deny);
                ds.RemoveAccessRule(fsa);
                Directory.SetAccessControl(datagridvalue, ds);

                DirectoryInfo di = new DirectoryInfo(datagridvalue);
                di.Attributes = FileAttributes.Directory | FileAttributes.Normal;
                
        
             
            }
            datagridvalue = "";
            dataGridView1.Rows.Clear();
            refresh();
            dataGridView1.ClearSelection(); 
        }



        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        { 
            if (e.RowIndex > -1)
            {
                   datagridvalue = Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells[0].Value); 
            }

        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            unlocking();
            unlocking_of_file();
        }

      

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            DialogResult result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {

                datagridvalue = openFileDialog1.FileName;
                dataGridView1.ClearSelection();
                Microsoft.Win32.RegistryKey key;
                key = Microsoft.Win32.Registry.CurrentUser.CreateSubKey("harish");
                key.SetValue(openFileDialog1.FileName  , "Locked");
                datagridvalue = openFileDialog1.FileName;
                locking_of_file();
                dataGridView1.Rows.Clear();
                refresh();

            }
           
           /* DialogResult result = folderBrowserDialog1.ShowDialog();

            if (result == DialogResult.OK)
            {
                datagridvalue = folderBrowserDialog1.SelectedPath;
                dataGridView1.ClearSelection();
                Microsoft.Win32.RegistryKey key;
                key = Microsoft.Win32.Registry.CurrentUser.CreateSubKey("harish");
                key.SetValue(folderBrowserDialog1.SelectedPath, "Locked");
                datagridvalue = folderBrowserDialog1.SelectedPath;

                locking();
                dataGridView1.Rows.Clear();
                refresh();
            }*/
        }

        private void pictureBox15_Click(object sender, EventArgs e)
        {
            groupBox1.Location = new Point(-1680, groupBox1.Location.Y);
           
        }

        private void pictureBox12_Click(object sender, EventArgs e)
        {
            groupBox1.Location = new Point(-560, groupBox1.Location.Y);
        }

       

        private void pictureBox14_Click(object sender, EventArgs e)
        {
            groupBox1.Location = new Point(-2240, groupBox1.Location.Y);
        }

        private void pictureBox13_Click(object sender, EventArgs e)
        {
            groupBox1.Location = new Point(-1120, groupBox1.Location.Y);
        }

  
      

      

     
    }
}
