using JeutieControl.Properties;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace JeutieControl
{
  public class Form1 : Form
  {
    public const string version = "1.1";
    private IContainer components = null;
    private MenuStrip menuStrip1;
    private ToolStripMenuItem fileToolStripMenuItem;
    private ToolStripMenuItem exitToolStripMenuItem;
    private ToolStripMenuItem optionsToolStripMenuItem;
    private ToolStripMenuItem KeepOnTop;
    private ToolStripMenuItem transparencyToolStripMenuItem;
    private ToolStripMenuItem Opacity100;
    private ToolStripMenuItem Opacity90;
    private ToolStripMenuItem Opacity80;
    private ToolStripMenuItem Opacity70;
    private ToolStripMenuItem Opacity60;
    private ToolStripMenuItem Opacity50;
    private TextBox MySQLPath;
    private TextBox ServerPath;
    private Button startWorld;
    private Button startAuth;
    private PictureBox pictureBox1;
    private PictureBox pictureBox2;
    private Button stopWorld;
    private Button stopAuth;
    private FolderBrowserDialog ServerFolderBrowser;
    private Button ServerPathButton;
    private Button MySQLPathButton;
    private Button WoWPathButton; 
    private FolderBrowserDialog SqlFolderBrowser;
    private FolderBrowserDialog WoWFolderBrowser; 
    private PictureBox pictureBox3;
    private PictureBox pictureBox4;
    private Button startMySQL;
    private Button startWoW; 
    private Button stopMySQL;
    private Button stopWoW; 
    private ToolStripMenuItem aboutToolStripMenuItem;
    private ToolStripMenuItem helpToolStripMenuItem;
    private ToolStripMenuItem openWorldserverConfigToolStripMenuItem;
    private ToolStripMenuItem openAuthserverConfigToolStripMenuItem;
	private ToolStripMenuItem openCoreFolderToolStripMenuItem;
    private RichTextBox log;
    private TextBox WoWPath; 
    private ToolStripMenuItem currentOpacitySetting;
    private bool isServerPathCorrect;
    private bool isMysqlPathCorrect;
    private bool isWoWPathCorrect; 
    private Process authServer;
    private Process worldServer;
    private Process mySQL;
    private Process wowProcess; 
    private ProcessStartInfo authServerInfo;
    private ProcessStartInfo worldServerInfo;
    private ProcessStartInfo mySQLInfo;
	private ProcessStartInfo wowInfo;
	
    private TabControl serverTabs;
    private TabPage tabMySQL;
    private TabPage tabAuth;
    private TabPage tabWorld;

    // НОВЫЕ ЭЛЕМЕНТЫ СТРОКИ КОМАНД СЛЕВА
    private Label lblCommandTitle;
    private TextBox txtCommandInput;
    private Button btnSendCommand;

    [DllImport("user32.dll", SetLastError = true)]
    private static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

    [DllImport("user32.dll", SetLastError = true)]
    private static extern bool MoveWindow(IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);

    [DllImport("user32.dll", SetLastError = true)]
    private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

    [DllImport("user32.dll", SetLastError = true)]
    private static extern int GetWindowLong(IntPtr hWnd, int nIndex);
	
    [DllImport("user32.dll", SetLastError = true)]
    private static extern IntPtr SetFocus(IntPtr hWnd);

    private const int GWL_STYLE = -16;
    private const int WS_CAPTION = 0x00C00000;
    private const int WS_THICKFRAME = 0x00040000;

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }
    private void InitializeComponent()
    {
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (Form1));
      this.menuStrip1 = new MenuStrip();
      this.fileToolStripMenuItem = new ToolStripMenuItem();
      this.openWorldserverConfigToolStripMenuItem = new ToolStripMenuItem();
      this.openAuthserverConfigToolStripMenuItem = new ToolStripMenuItem();
	  this.openCoreFolderToolStripMenuItem = new ToolStripMenuItem();
      this.exitToolStripMenuItem = new ToolStripMenuItem();
      this.optionsToolStripMenuItem = new ToolStripMenuItem();
      this.KeepOnTop = new ToolStripMenuItem();
      this.transparencyToolStripMenuItem = new ToolStripMenuItem();
      this.Opacity100 = new ToolStripMenuItem();
      this.Opacity90 = new ToolStripMenuItem();
      this.Opacity80 = new ToolStripMenuItem();
      this.Opacity70 = new ToolStripMenuItem();
      this.Opacity60 = new ToolStripMenuItem();
      this.Opacity50 = new ToolStripMenuItem();
      this.helpToolStripMenuItem = new ToolStripMenuItem();
      this.aboutToolStripMenuItem = new ToolStripMenuItem();
      this.MySQLPath = new TextBox();
      this.ServerPath = new TextBox();
      this.startWorld = new Button();
      this.startAuth = new Button();
      this.stopWorld = new Button();
      this.stopAuth = new Button();
      this.ServerFolderBrowser = new FolderBrowserDialog();
      this.ServerPathButton = new Button();
      this.MySQLPathButton = new Button();
      this.WoWPathButton = new Button();
      this.SqlFolderBrowser = new FolderBrowserDialog();
      this.WoWFolderBrowser = new FolderBrowserDialog();
      this.startMySQL = new Button();
      this.startWoW = new Button();
      this.stopMySQL = new Button();
      this.stopWoW = new Button();
      this.pictureBox4 = new PictureBox();
      this.pictureBox3 = new PictureBox();
      this.pictureBox2 = new PictureBox();
      this.pictureBox1 = new PictureBox();
      this.log = new RichTextBox();
      this.WoWPath = new TextBox();
      
      this.serverTabs = new TabControl();
      this.tabMySQL = new TabPage();
      this.tabAuth = new TabPage();
      this.tabWorld = new TabPage();
      Panel leftPanelContainer = new Panel();

      // Выделение памяти под строку ввода команд
      this.lblCommandTitle = new Label();
      this.txtCommandInput = new TextBox();
      this.btnSendCommand = new Button();

      this.menuStrip1.SuspendLayout();
      ((ISupportInitialize) this.pictureBox4).BeginInit();
      ((ISupportInitialize) this.pictureBox3).BeginInit();
      ((ISupportInitialize) this.pictureBox2).BeginInit();
      ((ISupportInitialize) this.pictureBox1).BeginInit();
      leftPanelContainer.SuspendLayout();
      this.SuspendLayout();
      this.menuStrip1.BackColor = SystemColors.ControlLight;
      this.menuStrip1.Items.AddRange(new ToolStripItem[] { (ToolStripItem) this.fileToolStripMenuItem, (ToolStripItem) this.optionsToolStripMenuItem, (ToolStripItem) this.helpToolStripMenuItem, (ToolStripItem) this.aboutToolStripMenuItem });
      this.menuStrip1.Location = new Point(0, 0);
      this.menuStrip1.Margin = new Padding(0, 0, 0, 5);
      this.menuStrip1.Name = "menuStrip1";
      this.menuStrip1.Size = new Size(1080, 24);
      this.menuStrip1.TabIndex = 0;
      this.menuStrip1.Text = "menuStrip1";

      this.fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { (ToolStripItem) this.openWorldserverConfigToolStripMenuItem, (ToolStripItem) this.openAuthserverConfigToolStripMenuItem, (ToolStripItem) this.openCoreFolderToolStripMenuItem, (ToolStripItem) this.exitToolStripMenuItem });
      this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
      this.fileToolStripMenuItem.Size = new Size(37, 20);
      this.fileToolStripMenuItem.Text = "Файл";
      this.openWorldserverConfigToolStripMenuItem.Name = "openWorldserverConfigToolStripMenuItem";
      this.openWorldserverConfigToolStripMenuItem.Size = new Size(208, 22);
      this.openWorldserverConfigToolStripMenuItem.Text = "Открыть конфигурацию Worldserver";
      this.openWorldserverConfigToolStripMenuItem.Click += new EventHandler(this.openWorldserverConfigToolStripMenuItem_Click);
      this.openAuthserverConfigToolStripMenuItem.Name = "openAuthserverConfigToolStripMenuItem";
      this.openAuthserverConfigToolStripMenuItem.Size = new Size(208, 22);
      this.openAuthserverConfigToolStripMenuItem.Text = "Открыть конфигурацию Authserver";
      this.openAuthserverConfigToolStripMenuItem.Click += new EventHandler(this.openAuthserverConfigToolStripMenuItem_Click);
	  this.openCoreFolderToolStripMenuItem.Name = "openCoreFolderToolStripMenuItem";
	  this.openCoreFolderToolStripMenuItem.Size = new Size(208, 22);
	  this.openCoreFolderToolStripMenuItem.Text = "Открыть папку Core";
	  this.openCoreFolderToolStripMenuItem.Click += new EventHandler(this.openCoreFolderToolStripMenuItem_Click);
      this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
      this.exitToolStripMenuItem.Size = new Size(208, 22);
      this.exitToolStripMenuItem.Text = "Выход";
      this.exitToolStripMenuItem.Click += new EventHandler(this.exitToolStripMenuItem_Click);
      this.optionsToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { (ToolStripItem) this.KeepOnTop, (ToolStripItem) this.transparencyToolStripMenuItem });
      this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
      this.optionsToolStripMenuItem.Size = new Size(61, 20);
      this.optionsToolStripMenuItem.Text = "Настройки";
      this.KeepOnTop.CheckOnClick = true;
      this.KeepOnTop.Name = "KeepOnTop";
      this.KeepOnTop.Size = new Size(154, 22);
      this.KeepOnTop.Text = "Поверх остальных окон";
      this.KeepOnTop.Click += new EventHandler(this.KeepOnTop_Click);
      this.transparencyToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { (ToolStripItem) this.Opacity100, (ToolStripItem) this.Opacity90, (ToolStripItem) this.Opacity80, (ToolStripItem) this.Opacity70, (ToolStripItem) this.Opacity60, (ToolStripItem) this.Opacity50 });
      this.transparencyToolStripMenuItem.Name = "transparencyToolStripMenuItem";
      this.transparencyToolStripMenuItem.Size = new Size(154, 22);
      this.transparencyToolStripMenuItem.Text = "Прозрачность";
      this.Opacity100.Checked = true;
      this.Opacity100.CheckOnClick = true;
      this.Opacity100.CheckState = CheckState.Checked;
      this.Opacity100.Name = "Opacity100";
      this.Opacity100.Size = new Size(102, 22);
      this.Opacity100.Text = "100%";
      this.Opacity100.Click += new EventHandler(this.Opacity100_Click);
      this.Opacity90.CheckOnClick = true;
      this.Opacity90.Name = "Opacity90";
      this.Opacity90.Size = new Size(102, 22);
      this.Opacity90.Text = "90%";
      this.Opacity90.Click += new EventHandler(this.Opacity90_Click);
      this.Opacity80.CheckOnClick = true;
      this.Opacity80.Name = "Opacity80";
      this.Opacity80.Size = new Size(102, 22);
      this.Opacity80.Text = "80%";
      this.Opacity80.Click += new EventHandler(this.Opacity80_Click);
      this.Opacity70.CheckOnClick = true;
      this.Opacity70.Name = "Opacity70";
      this.Opacity70.Size = new Size(102, 22);
      this.Opacity70.Text = "70%";
      this.Opacity70.Click += new EventHandler(this.Opacity70_Click);
      this.Opacity60.CheckOnClick = true;
      this.Opacity60.Name = "Opacity60";
      this.Opacity60.Size = new Size(102, 22);
      this.Opacity60.Text = "60%";
      this.Opacity60.Click += new EventHandler(this.Opacity60_Click);
      this.Opacity50.CheckOnClick = true;
      this.Opacity50.Name = "Opacity50";
      this.Opacity50.Size = new Size(102, 22);
      this.Opacity50.Text = "50%";
      this.Opacity50.Click += new EventHandler(this.Opacity50_Click);
      this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
      this.helpToolStripMenuItem.Size = new Size(68, 20);
      this.helpToolStripMenuItem.Text = "Помощь";
      this.helpToolStripMenuItem.Click += new EventHandler(this.helpToolStripMenuItem_Click);
      this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
      this.aboutToolStripMenuItem.Size = new Size(94, 20);
      this.aboutToolStripMenuItem.Text = "О программе";
      this.aboutToolStripMenuItem.Click += new EventHandler(this.aboutToolStripMenuItem_Click);

      this.MySQLPath.BorderStyle = BorderStyle.FixedSingle;
      this.MySQLPath.Font = new Font("Microsoft Sans Serif", 7.5f);
      this.MySQLPath.Location = new Point(109, 280);
      this.MySQLPath.Size = new Size(270, 18);
      this.MySQLPath.Text = "Путь еще не задан.";
      this.ServerPath.AllowDrop = true;
      this.ServerPath.BorderStyle = BorderStyle.FixedSingle;
      this.ServerPath.Font = new Font("Microsoft Sans Serif", 7.5f);
      this.ServerPath.Location = new Point(109, 248);
      this.ServerPath.Size = new Size(270, 18);
      this.ServerPath.Text = "Путь еще не задан.";
      this.startWorld.Location = new Point(59, 32);
      this.startWorld.Size = new Size(241, 40);
      this.startWorld.Text = "Запуск Worldserver.";
      this.startWorld.Click += new EventHandler(this.startWorld_Click);
      this.startAuth.Location = new Point(58, 78);
      this.startAuth.Size = new Size(242, 40);
      this.startAuth.Text = "Запуск Authserver.";
      this.startAuth.Click += new EventHandler(this.startAuth_Click);
      this.stopWorld.Location = new Point(306, 32);
      this.stopWorld.Size = new Size(73, 40);
      this.stopWorld.Text = "Стоп";
      this.stopWorld.Click += new EventHandler(this.stopWorld_Click);
      this.stopAuth.Location = new Point(306, 78);
      this.stopAuth.Size = new Size(73, 40);
      this.stopAuth.Text = "Стоп";
      this.stopAuth.Click += new EventHandler(this.stopAuth_Click);
      this.ServerPathButton.Location = new Point(12, 244);
      this.ServerPathButton.Size = new Size(91, 26);
      this.ServerPathButton.Text = "Сервер";
      this.ServerPathButton.Click += new EventHandler(this.ServerPathButton_Click);
      this.MySQLPathButton.Location = new Point(12, 276);
      this.MySQLPathButton.Size = new Size(91, 26);
      this.MySQLPathButton.Text = "MySQL";
      this.MySQLPathButton.Click += new EventHandler(this.MySQLPathButton_Click);
      this.WoWPathButton.Location = new Point(12, 308);
      this.WoWPathButton.Size = new Size(91, 26);
      this.WoWPathButton.Text = "Папка WoW";
      this.WoWPathButton.Click += new EventHandler(this.WoWPathButton_Click);
      this.startMySQL.Location = new Point(58, 152);
      this.startMySQL.Size = new Size(242, 40);
      this.startMySQL.Text = "Запуск MySQL.";
      this.startMySQL.Click += new EventHandler(this.startMySQL_Click);
      this.startWoW.Location = new Point(58, 198);
      this.startWoW.Size = new Size(242, 40);
      this.startWoW.Text = "Запустить WoW.";
      this.startWoW.Click += new EventHandler(this.startWoW_Click);
      this.stopMySQL.Location = new Point(306, 152);
      this.stopMySQL.Size = new Size(73, 40);
      this.stopMySQL.Text = "Стоп";
      this.stopMySQL.Click += new EventHandler(this.stopMySQL_Click);
      this.stopWoW.Location = new Point(306, 198);
      this.stopWoW.Size = new Size(73, 40);
      this.stopWoW.Text = "Стоп";
      this.stopWoW.Click += new EventHandler(this.stopWoW_Click);
      
      // Иконки
      this.pictureBox4.Image = (Image) componentResourceManager.GetObject("pictureBox4.Image");
      this.pictureBox4.Location = new Point(12, 198);
      this.pictureBox4.Size = new Size(40, 40);
      this.pictureBox4.SizeMode = PictureBoxSizeMode.Zoom;
      this.pictureBox3.Image = (Image) componentResourceManager.GetObject("pictureBox3.Image");
      this.pictureBox3.Location = new Point(12, 152);
      this.pictureBox3.Size = new Size(40, 40);
      this.pictureBox3.SizeMode = PictureBoxSizeMode.Zoom;
      this.pictureBox2.Image = (Image) componentResourceManager.GetObject("pictureBox2.Image");
      this.pictureBox2.Location = new Point(12, 78);
      this.pictureBox2.Size = new Size(40, 40);
      this.pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
      this.pictureBox1.Image = (Image) componentResourceManager.GetObject("pictureBox1.Image");
      this.pictureBox1.Location = new Point(12, 32);
      this.pictureBox1.Size = new Size(40, 40);
      this.pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;

      // Главное окно системного лога лаунчера
      this.log.BackColor = SystemColors.Info;
      this.log.BorderStyle = BorderStyle.FixedSingle;
      this.log.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point);
      this.log.Location = new Point(12, 340);
      this.log.ReadOnly = true;
      this.log.ScrollBars = RichTextBoxScrollBars.ForcedVertical;
      this.log.Size = new Size(367, 130); // Стандартное окно логов лаунчера
      this.log.Text = "";

      this.WoWPath.BorderStyle = BorderStyle.FixedSingle;
      this.WoWPath.Font = new Font("Microsoft Sans Serif", 7.5f);
      this.WoWPath.Location = new Point(109, 312);
      this.WoWPath.Size = new Size(270, 18);
      this.WoWPath.Text = "Путь не задан.";

      // НАСТРОЙКА НОВЫХ СВОЙСТВ СТРОКИ ВВОДА КОМАНД (В НИЖНЕЙ ЛЕВОЙ ЧАСТИ)
      this.lblCommandTitle.Location = new Point(12, 483); // Сразу под RichTextBox логов
      this.lblCommandTitle.Size = new Size(367, 16);
      this.lblCommandTitle.Text = "Отправить команду в WorldServer:";
      this.lblCommandTitle.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold);
      this.lblCommandTitle.ForeColor = Color.DarkGoldenrod; // Красивый золотой цвет WoW подложки логов

      this.txtCommandInput.Location = new Point(12, 502);
      this.txtCommandInput.Size = new Size(275, 22);
      this.txtCommandInput.Font = new Font("Consolas", 9.75f);
	  this.txtCommandInput.BackColor = SystemColors.Info; // Такой же оттенок, как у лога выше
	  this.txtCommandInput.ForeColor = Color.Black;       // Черный читаемый текст
      this.txtCommandInput.BorderStyle = BorderStyle.FixedSingle;
      this.txtCommandInput.KeyDown += new KeyEventHandler(this.txtCommandInput_KeyDown);

      this.btnSendCommand.Location = new Point(294, 501);
      this.btnSendCommand.Size = new Size(85, 24);
      this.btnSendCommand.Text = "Отправить";
      this.btnSendCommand.Click += new EventHandler(this.btnSendCommand_Click);
      // Железобетонное прижатие левой части формы
      leftPanelContainer.Dock = DockStyle.Left;
      leftPanelContainer.Width = 395;
      leftPanelContainer.Name = "leftPanelContainer";

      // Наполняем контейнер абсолютно всеми элементами управления левой части
      leftPanelContainer.Controls.Add((Control) this.ServerPathButton);
      leftPanelContainer.Controls.Add((Control) this.WoWPath);
      leftPanelContainer.Controls.Add((Control) this.log);
      leftPanelContainer.Controls.Add((Control) this.MySQLPath);
      leftPanelContainer.Controls.Add((Control) this.stopWoW);
      leftPanelContainer.Controls.Add((Control) this.stopMySQL);
      leftPanelContainer.Controls.Add((Control) this.WoWPathButton);
      leftPanelContainer.Controls.Add((Control) this.ServerPath);
      leftPanelContainer.Controls.Add((Control) this.MySQLPathButton);
      leftPanelContainer.Controls.Add((Control) this.startWoW);
      leftPanelContainer.Controls.Add((Control) this.startMySQL);
      leftPanelContainer.Controls.Add((Control) this.pictureBox4);
      leftPanelContainer.Controls.Add((Control) this.pictureBox3);
      leftPanelContainer.Controls.Add((Control) this.stopAuth);
      leftPanelContainer.Controls.Add((Control) this.stopWorld);
      leftPanelContainer.Controls.Add((Control) this.pictureBox2);
      leftPanelContainer.Controls.Add((Control) this.pictureBox1);
      leftPanelContainer.Controls.Add((Control) this.startAuth);
      leftPanelContainer.Controls.Add((Control) this.startWorld);
      
      // Добавляем новые элементы команд на левую панель!
      leftPanelContainer.Controls.Add((Control) this.lblCommandTitle);
      leftPanelContainer.Controls.Add((Control) this.txtCommandInput);
      leftPanelContainer.Controls.Add((Control) this.btnSendCommand);

      // Вкладки серверов (Занимают всё остальное место)
      this.serverTabs.Controls.Add(this.tabMySQL);
      this.serverTabs.Controls.Add(this.tabAuth);
      this.serverTabs.Controls.Add(this.tabWorld);
      this.serverTabs.Dock = DockStyle.Fill; // Консоли теперь занимают всю высоту справа
      this.serverTabs.Location = new Point(395, 24);
      this.serverTabs.Name = "serverTabs";
      this.serverTabs.SelectedIndex = 0;
      this.serverTabs.TabIndex = 24;
      
      this.tabMySQL.BackColor = Color.Black;
      this.tabMySQL.Text = "MySQL Server";
      this.tabAuth.BackColor = Color.Black;
      this.tabAuth.Text = "Auth Server";
      this.tabWorld.BackColor = Color.Black;
      this.tabWorld.Text = "World Server";

      // Сборка общей формы
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(1080, 545); // Высота окна чуть увеличена до 545, чтобы строка команд поместилась красиво
      this.Controls.Add((Control) this.serverTabs);
      this.Controls.Add((Control) leftPanelContainer);
      this.Controls.Add((Control) this.menuStrip1);
      this.FormBorderStyle = FormBorderStyle.Sizable;
      this.Icon = (Icon) componentResourceManager.GetObject("$this.Icon");
      this.MainMenuStrip = this.menuStrip1;
      this.MaximizeBox = true;
      this.Name = nameof (Form1);
      this.Text = "AzerothCore Control Panel";
      this.Load += new EventHandler(this.Form1_Load);
      this.menuStrip1.ResumeLayout(false);
      this.menuStrip1.PerformLayout();
      ((ISupportInitialize) this.pictureBox4).EndInit();
      ((ISupportInitialize) this.pictureBox3).EndInit();
      ((ISupportInitialize) this.pictureBox2).EndInit();
      ((ISupportInitialize) this.pictureBox1).EndInit();
      leftPanelContainer.ResumeLayout(false);
      leftPanelContainer.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();
    }

    public Form1()
    {
      this.InitializeComponent();
      this.currentOpacitySetting = this.Opacity100;
    }

    private void Form1_Load(object sender, EventArgs e)
    {
      this.log.Text = "Добро пожаловать! Панель управления запущена!";
      this.CheckForNewMachine();
      this.ServerPath.Text = Settings.Default.ServerPath;
      this.WoWPath.Text = Settings.Default.ApachePath; 
      this.MySQLPath.Text = Settings.Default.MySQLPath;
      this.KeepOnTop.Checked = Settings.Default.KeepOnTop;

      this.authServerInfo = new ProcessStartInfo("cmd.exe");
      this.authServerInfo.Arguments = "/c authserver.exe & pause";
      this.authServerInfo.UseShellExecute = false;

      this.worldServerInfo = new ProcessStartInfo("cmd.exe");
      this.worldServerInfo.Arguments = "/c worldserver.exe & pause";
      this.worldServerInfo.UseShellExecute = false;
      this.worldServerInfo.RedirectStandardInput = true; 

      this.mySQLInfo = new ProcessStartInfo("cmd.exe");
      this.mySQLInfo.Arguments = "/c mysqld --defaults-file=my.ini --console & pause";
      this.mySQLInfo.UseShellExecute = false;
      
      this.wowInfo = new ProcessStartInfo("wow.exe");

      this.worldServer = new Process();
      this.authServer = new Process();
      this.mySQL = new Process();
      this.wowProcess = new Process();

      this.worldServer.StartInfo = this.worldServerInfo;
      this.authServer.StartInfo = this.authServerInfo;
      this.mySQL.StartInfo = this.mySQLInfo;
      this.wowProcess.StartInfo = this.wowInfo;

      this.serverTabs.SizeChanged += new EventHandler(this.ServerTabs_SizeChanged);
      this.serverTabs.SelectedIndexChanged += new EventHandler(this.ServerTabs_SelectedIndexChanged);
      this.stopMySQL.Text = "Стоп";
      this.stopWoW.Text = "Стоп";

      this.CheckPaths();
    }
    private void CheckForNewMachine()
    {
      if (!(Settings.Default.MachineName != Environment.MachineName))
        return;
      Settings.Default.ServerPath = "Задайте путь к ядру (Core) сервера!";
      Settings.Default.ApachePath = "Задайте путь к папке с клиентом World of Warcraft!";
      Settings.Default.MySQLPath = @"Задайте путь к MySQL\Bin!";
      Settings.Default.KeepOnTop = false;
      Settings.Default.MachineName = Environment.MachineName;
      Settings.Default.Save();
    }

    private void ServerPathButton_Click(object sender, EventArgs e)
    {
      ServerFolderBrowser.Description = "Укажите путь к папке с ядром сервера, в которой находятся файлы Authserver.exe и Worldserver.exe";
      if (this.ServerFolderBrowser.ShowDialog() == DialogResult.OK)
      {
        this.ServerPath.Text = this.ServerFolderBrowser.SelectedPath;
        Settings.Default.ServerPath = this.ServerFolderBrowser.SelectedPath;
        Settings.Default.Save();
      }
      this.CheckPaths();
    }

    private void MySQLPathButton_Click(object sender, EventArgs e)
    {
      SqlFolderBrowser.Description = "Укажите путь к папке с MySQL.exe, он располагается в папке /MySQL/bin";
      if (this.SqlFolderBrowser.ShowDialog() == DialogResult.OK)
      {
        this.MySQLPath.Text = this.SqlFolderBrowser.SelectedPath;
        Settings.Default.MySQLPath = this.SqlFolderBrowser.SelectedPath;
        Settings.Default.Save();
      }
      this.CheckPaths();
    }

    private void WoWPathButton_Click(object sender, EventArgs e)
    {
      WoWFolderBrowser.Description = "Укажите путь к папке с клиентом World of Warcraft 3.3.5a, в которой находится wow.exe";
      if (this.WoWFolderBrowser.ShowDialog() == DialogResult.OK)
      {
        this.WoWPath.Text = this.WoWFolderBrowser.SelectedPath;
        Settings.Default.ApachePath = this.WoWFolderBrowser.SelectedPath;
        Settings.Default.Save();
      }
      this.CheckPaths();
    }

    private void CheckPaths()
    {
      if (File.Exists(this.ServerPath.Text + @"\authserver.exe") && File.Exists(this.ServerPath.Text + @"\worldserver.exe"))
      {
        this.isServerPathCorrect = true;
        this.ServerPath.BackColor = Color.LightGreen;
        this.startWorld.Enabled = true;
        this.startAuth.Enabled = true;
        this.stopWorld.Enabled = true;
        this.stopAuth.Enabled = true;
        this.worldServerInfo.WorkingDirectory = this.ServerPath.Text;
        this.authServerInfo.WorkingDirectory = this.ServerPath.Text;
      }
      else
      {
        this.isServerPathCorrect = false;
        this.ServerPath.BackColor = Color.LightCoral;
        this.startWorld.Enabled = false;
        this.startAuth.Enabled = false;
        this.stopWorld.Enabled = false;
        this.stopAuth.Enabled = false;
      }
      if (File.Exists(this.MySQLPath.Text + @"\mysqld.exe"))
      {
        this.isMysqlPathCorrect = true;
        this.MySQLPath.BackColor = Color.LightGreen;
        this.startMySQL.Enabled = true;
        this.stopMySQL.Enabled = true;
        this.mySQLInfo.WorkingDirectory = this.MySQLPath.Text;
      }
      else
      {
        this.isMysqlPathCorrect = false;
        this.MySQLPath.BackColor = Color.LightCoral;
        this.startMySQL.Enabled = false;
        this.stopMySQL.Enabled = false;
      }
      if (File.Exists(this.WoWPath.Text + @"\wow.exe"))
      {
        this.isWoWPathCorrect = true;
        this.WoWPath.BackColor = Color.LightGreen;
        this.startWoW.Enabled = true;
        this.stopWoW.Enabled = true;
        this.wowInfo.WorkingDirectory = this.WoWPath.Text;
      }
      else
      {
        this.isWoWPathCorrect = false;
        this.WoWPath.BackColor = Color.LightCoral;
        this.startWoW.Enabled = false;
        this.stopWoW.Enabled = false;
      }
    }
    private void startMySQL_Click(object sender, EventArgs e)
    {
      try
      {
        if (!this.isMysqlPathCorrect) { this.Log("Ошибка! Нет возможности запустить MySQL сервер, некорректный путь."); return; }
        else if (this.IsProcessRunning("mysqld")) { this.Log("Ошибка! Сервер MySQL уже запущен."); return; }
        this.mySQL.Start();
        this.Log("Запуск MySQL server внутри вкладки...");
        int attempts = 0;
        while (this.mySQL.MainWindowHandle == IntPtr.Zero && attempts < 50)
        {
          Thread.Sleep(50); this.mySQL.Refresh(); attempts++;
        }
        if (this.mySQL.MainWindowHandle != IntPtr.Zero)
        {
          IntPtr hWnd = this.mySQL.MainWindowHandle;
          int style = GetWindowLong(hWnd, GWL_STYLE);
          SetWindowLong(hWnd, GWL_STYLE, style & ~WS_CAPTION & ~WS_THICKFRAME);
          SetParent(hWnd, this.tabMySQL.Handle);
          MoveWindow(hWnd, 0, 0, this.tabMySQL.Width, this.tabMySQL.Height, true);
          SetFocus(hWnd); 
        }
      }
      catch (InvalidOperationException) {}
    }

    private void startAuth_Click(object sender, EventArgs e)
    {
      try
      {
        if (!this.isServerPathCorrect) { this.Log("Error! Can't start the Auth server, incorrect path."); return; }
        else if (this.IsProcessRunning("authserver")) { this.Log("Error! Auth server is already started."); return; }
        else if (!this.IsProcessRunning("mysqld")) { this.Log("Error! Please start MySQL server first."); return; }
        this.authServer.Start();
        this.Log("Запуск Auth server внутри вкладки...");
        int attempts = 0;
        while (this.authServer.MainWindowHandle == IntPtr.Zero && attempts < 50)
        {
          Thread.Sleep(50); this.authServer.Refresh(); attempts++;
        }
        if (this.authServer.MainWindowHandle != IntPtr.Zero)
        {
          IntPtr hWnd = this.authServer.MainWindowHandle;
          int style = GetWindowLong(hWnd, GWL_STYLE);
          SetWindowLong(hWnd, GWL_STYLE, style & ~WS_CAPTION & ~WS_THICKFRAME);
          SetParent(hWnd, this.tabAuth.Handle);
          MoveWindow(hWnd, 0, 0, this.tabAuth.Width, this.tabAuth.Height, true);
          SetFocus(hWnd); 
        }
      }
      catch (InvalidOperationException) {}
    }

    private void startWorld_Click(object sender, EventArgs e)
    {
      try
      {
        if (!this.isServerPathCorrect) { this.Log("Error! Can't start the World server, incorrect path."); return; }
        else if (this.IsProcessRunning("worldserver")) { this.Log("Error! World server is already started."); return; }
        else if (!this.IsProcessRunning("mysqld")) { this.Log("Error! Please start MySQL server first."); return; }
        this.worldServer.Start();
        this.Log("Запуск World server внутри вкладки...");
        int attempts = 0;
        while (this.worldServer.MainWindowHandle == IntPtr.Zero && attempts < 50)
        {
          Thread.Sleep(50); this.worldServer.Refresh(); attempts++;
        }
        if (this.worldServer.MainWindowHandle != IntPtr.Zero)
        {
          IntPtr hWnd = this.worldServer.MainWindowHandle;
          int style = GetWindowLong(hWnd, GWL_STYLE);
          SetWindowLong(hWnd, GWL_STYLE, style & ~WS_CAPTION & ~WS_THICKFRAME);
          SetParent(hWnd, this.tabWorld.Handle);
          MoveWindow(hWnd, 0, 0, this.tabWorld.Width, this.tabWorld.Height, true);
          SetFocus(hWnd); 
        }
      }
      catch (InvalidOperationException) {}
    }

    private void stopMySQL_Click(object sender, EventArgs e)
    {
      try { if (!this.KillProcess("mysqld") || !this.IsProcessRunning("mysqld")) return; this.KillProcess("mysqld"); this.Log("Сервер MySQL остановлен."); }
      catch (InvalidOperationException) {}
    }

    private void stopAuth_Click(object sender, EventArgs e)
    {
      try { if (!this.KillProcess("authserver") || !this.IsProcessRunning("authserver")) return; this.Log("Authserver остановлен."); }
      catch (InvalidOperationException) {}
    }

    private void stopWorld_Click(object sender, EventArgs e)
    {
      try { if (this.IsProcessRunning("worldserver")) { this.worldServer.StandardInput.WriteLine("server exit"); this.Log("Отправлена команда 'server exit'."); } }
      catch (InvalidOperationException) {}
    }

    private void startWoW_Click(object sender, EventArgs e)
    {
      try { if (!this.isWoWPathCorrect || this.IsProcessRunning("wow")) return; this.wowProcess.Start(); this.Log("Клиент WoW запущен."); }
      catch (InvalidOperationException) {}
    }

    private void stopWoW_Click(object sender, EventArgs e)
    {
      try { if (!this.KillProcess("wow") || !this.IsProcessRunning("wow")) return; this.Log("Клиент WoW закрыт."); }
      catch (InvalidOperationException) {}
    }

    private void ServerTabs_SizeChanged(object sender, EventArgs e)
    {
      if (this.IsProcessRunning("mysqld") && this.mySQL.MainWindowHandle != IntPtr.Zero) MoveWindow(this.mySQL.MainWindowHandle, 0, 0, this.tabMySQL.Width, this.tabMySQL.Height, true);
      if (this.IsProcessRunning("authserver") && this.authServer.MainWindowHandle != IntPtr.Zero) MoveWindow(this.authServer.MainWindowHandle, 0, 0, this.tabAuth.Width, this.tabAuth.Height, true);
      if (this.IsProcessRunning("worldserver") && this.worldServer.MainWindowHandle != IntPtr.Zero) MoveWindow(this.worldServer.MainWindowHandle, 0, 0, this.tabWorld.Width, this.tabWorld.Height, true);
    }

    private bool IsProcessRunning(string processName) => Process.GetProcessesByName(processName).Length != 0;

    private bool KillProcess(string processName)
    {
      Process[] processesByName = Process.GetProcessesByName(processName); bool flag = false;
      foreach (Process process in processesByName) { try { process.Kill(); flag = true; } catch (Win32Exception) {} }
      return flag;
    }

    private void exitToolStripMenuItem_Click(object sender, EventArgs e) => this.Close();
    private void KeepOnTop_Click(object sender, EventArgs e) { this.TopMost = ((ToolStripMenuItem)sender).Checked; Settings.Default.KeepOnTop = this.TopMost; Settings.Default.Save(); }
    private void Opacity100_Click(object sender, EventArgs e) => this.ChangeOpacity(sender, 1.0);
    private void Opacity90_Click(object sender, EventArgs e) => this.ChangeOpacity(sender, 0.9);
    private void Opacity80_Click(object sender, EventArgs e) => this.ChangeOpacity(sender, 0.8);
    private void Opacity70_Click(object sender, EventArgs e) => this.ChangeOpacity(sender, 0.7);
    private void Opacity60_Click(object sender, EventArgs e) => this.ChangeOpacity(sender, 0.6);
    private void Opacity50_Click(object sender, EventArgs e) => this.ChangeOpacity(sender, 0.5);

    private void ChangeOpacity(object menuSelection, double opacity)
    {
      this.Opacity = opacity; this.currentOpacitySetting.Checked = false; this.currentOpacitySetting = (ToolStripMenuItem)menuSelection; this.currentOpacitySetting.Checked = true;
    }

	private void openWorldserverConfigToolStripMenuItem_Click(object sender, EventArgs e) 
	{ 
		// Добавлен подкаталог \config\ к пути
		if (this.isServerPathCorrect) 
        Process.Start("notepad.exe", this.ServerPath.Text + @"\configs\worldserver.conf"); 
	}
	private void openAuthserverConfigToolStripMenuItem_Click(object sender, EventArgs e) 
	{ 
		// Добавлен подкаталог \config\ к пути
		if (this.isServerPathCorrect) 
			Process.Start("notepad.exe", this.ServerPath.Text + @"\configs\authserver.conf"); 
	}
	// Новый метод для открытия проводника в папке Core
	private void openCoreFolderToolStripMenuItem_Click(object sender, EventArgs e) 
	{ 
		if (this.isServerPathCorrect && Directory.Exists(this.ServerPath.Text)) 
		{
			Process.Start("explorer.exe", this.ServerPath.Text); 
		}
		else 
		{
			this.Log("Ошибка! Путь к ядру сервера не задан или не существует.");
		}
	}

    private void Log(string text)
    {
      if (this.log.InvokeRequired) this.Invoke((Delegate) new Form1.SetLogCallback(this.Log), (object) text);
      else { this.log.AppendText("\n[" + DateTime.Now.ToString("dd-MM-yy HH:mm:ss") + "] " + text); this.log.ScrollToCaret(); }
    }

    private void helpToolStripMenuItem_Click(object sender, EventArgs e) => new Help().ShowDialog();
    private void aboutToolStripMenuItem_Click(object sender, EventArgs e) => new About().ShowDialog();

    protected override void OnFormClosing(FormClosingEventArgs e)
    {
      try { this.KillProcess("worldserver"); this.KillProcess("authserver"); this.KillProcess("mysqld"); this.KillProcess("cmd"); } catch (Exception) {}
      base.OnFormClosing(e);
    }

    private void ServerTabs_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (this.serverTabs.SelectedIndex == 0 && this.IsProcessRunning("mysqld") && this.mySQL.MainWindowHandle != IntPtr.Zero) SetFocus(this.mySQL.MainWindowHandle);
      else if (this.serverTabs.SelectedIndex == 1 && this.IsProcessRunning("authserver") && this.authServer.MainWindowHandle != IntPtr.Zero) SetFocus(this.authServer.MainWindowHandle);
      else if (this.serverTabs.SelectedIndex == 2 && this.IsProcessRunning("worldserver") && this.worldServer.MainWindowHandle != IntPtr.Zero) SetFocus(this.worldServer.MainWindowHandle);
    }

    private void btnSendCommand_Click(object sender, EventArgs e) => this.ExecuteServerCommand();
    private void txtCommandInput_KeyDown(object sender, KeyEventArgs e) { if (e.KeyCode == Keys.Enter) { this.ExecuteServerCommand(); e.SuppressKeyPress = true; } }

    private void ExecuteServerCommand()
    {
      if (string.IsNullOrWhiteSpace(this.txtCommandInput.Text)) return;
      try
      {
        if (this.IsProcessRunning("worldserver")) { this.worldServer.StandardInput.WriteLine(this.txtCommandInput.Text); this.Log("Отправлена команда: " + this.txtCommandInput.Text); this.txtCommandInput.Clear(); this.txtCommandInput.Focus(); }
        else this.Log("Ошибка! Worldserver не запущен.");
      }
      catch (Exception ex) { this.Log("Ошибка ввода: " + ex.Message); }
    }

    private delegate void SetLogCallback(string text);
  }
}
