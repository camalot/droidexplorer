https://github.com/TheCodeKing/ActiveButtons.Net

ActiveButtons is a .Net code library which allows developers to very quickly and easily add additional buttons to the Windows title bar along-side the standard maximise, minimise and close buttons.

Supported Platforms
-------------------
Windows 7 (including Aero)
Windows Vista (including Aero)
Windows XP
Windows 2000

Code Sample
-----------
IActiveMenu menu = ActiveMenu.GetInstance(form);
ActiveButton button = new ActiveButton();
button.Text = "One";
button.Click += button_Click;
menu.Items.Add(button);
