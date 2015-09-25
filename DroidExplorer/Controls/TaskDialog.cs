using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace DroidExplorer.Controls {
  #region PUBLIC enums
  /// <summary>
  /// 
  /// </summary>
  public enum SysIcons {
    /// <summary>
    /// 
    /// </summary>
    Information,
    /// <summary>
    /// 
    /// </summary>
    Question,
    /// <summary>
    /// 
    /// </summary>
    Warning,
    /// <summary>
    /// 
    /// </summary>
    Error 
  };
  /// <summary>
  /// 
  /// </summary>
  public enum TaskDialogButtons {
    /// <summary>
    /// 
    /// </summary>
    YesNo,
    /// <summary>
    /// 
    /// </summary>
    YesNoCancel,
    /// <summary>
    /// 
    /// </summary>
    OKCancel,
    /// <summary>
    /// 
    /// </summary>
    OK,
    /// <summary>
    /// 
    /// </summary>
    Close,
    /// <summary>
    /// 
    /// </summary>
    Cancel,
    /// <summary>
    /// 
    /// </summary>
    None 
  }
  #endregion
  /// <summary>
  /// 
  /// </summary>
  public static class TaskDialog {
    // PUBLIC static values...
    /// <summary>
    /// 
    /// </summary>
    static public bool VerificationChecked = false;
    /// <summary>
    /// 
    /// </summary>
    static public int RadioButtonResult = -1;
    /// <summary>
    /// 
    /// </summary>
    static public int CommandButtonResult = -1;
    /// <summary>
    /// 
    /// </summary>
    static public int EmulatedFormWidth = 450;
    /// <summary>
    /// 
    /// </summary>
    static public bool ForceEmulationMode = false;
    /// <summary>
    /// 
    /// </summary>
    static public bool UseToolWindowOnXP = true;
    /// <summary>
    /// 
    /// </summary>
    static public bool PlaySystemSounds = true;

    //--------------------------------------------------------------------------------
    #region ShowTaskDialogBox
    //--------------------------------------------------------------------------------
    /// <summary>
    /// Shows the task dialog box.
    /// </summary>
    /// <param name="Title">The title.</param>
    /// <param name="MainInstruction">The main instruction.</param>
    /// <param name="Content">The content.</param>
    /// <param name="ExpandedInfo">The expanded info.</param>
    /// <param name="Footer">The footer.</param>
    /// <param name="VerificationText">The verification text.</param>
    /// <param name="RadioButtons">The radio buttons.</param>
    /// <param name="CommandButtons">The command buttons.</param>
    /// <param name="Buttons">The buttons.</param>
    /// <param name="MainIcon">The main icon.</param>
    /// <param name="FooterIcon">The footer icon.</param>
    /// <returns></returns>
    static public DialogResult ShowTaskDialogBox ( string Title,
                                                 string MainInstruction,
                                                 string Content,
                                                 string ExpandedInfo,
                                                 string Footer,
                                                 string VerificationText,
                                                 string RadioButtons,
                                                 string CommandButtons,
                                                 TaskDialogButtons Buttons,
                                                 SysIcons MainIcon,
                                                 SysIcons FooterIcon ) {
      if ( VistaTaskDialog.IsAvailableOnThisOS && !ForceEmulationMode ) {
        // [OPTION 1] Show Vista TaskDialog
        VistaTaskDialog vtd = new VistaTaskDialog ( );

        vtd.WindowTitle = Title;
        vtd.MainInstruction = MainInstruction;
        vtd.Content = Content;
        vtd.ExpandedInformation = ExpandedInfo;
        vtd.Footer = Footer;

        // Radio Buttons
        if ( RadioButtons != "" ) {
          List<VistaTaskDialogButton> lst = new List<VistaTaskDialogButton> ( );
          string[ ] arr = RadioButtons.Split ( new char[ ] { '|' } );
          for ( int i = 0; i < arr.Length; i++ ) {
            try {
              VistaTaskDialogButton button = new VistaTaskDialogButton ( );
              button.ButtonId = 1000 + i;
              button.ButtonText = arr[ i ];
              lst.Add ( button );
            } catch ( FormatException ) {
            }
          }
          vtd.RadioButtons = lst.ToArray ( );
        }

        // Custom Buttons
        if ( CommandButtons != "" ) {
          List<VistaTaskDialogButton> lst = new List<VistaTaskDialogButton> ( );
          string[ ] arr = CommandButtons.Split ( new char[ ] { '|' } );
          for ( int i = 0; i < arr.Length; i++ ) {
            try {
              VistaTaskDialogButton button = new VistaTaskDialogButton ( );
              button.ButtonId = 2000 + i;
              button.ButtonText = arr[ i ];
              lst.Add ( button );
            } catch ( FormatException ) {
            }
          }
          vtd.Buttons = lst.ToArray ( );
        }

        switch ( Buttons ) {
          case TaskDialogButtons.YesNo:
            vtd.CommonButtons = VistaTaskDialogCommonButtons.Yes | VistaTaskDialogCommonButtons.No;
            break;
          case TaskDialogButtons.YesNoCancel:
            vtd.CommonButtons = VistaTaskDialogCommonButtons.Yes | VistaTaskDialogCommonButtons.No | VistaTaskDialogCommonButtons.Cancel;
            break;
          case TaskDialogButtons.OKCancel:
            vtd.CommonButtons = VistaTaskDialogCommonButtons.Ok | VistaTaskDialogCommonButtons.Cancel;
            break;
          case TaskDialogButtons.OK:
            vtd.CommonButtons = VistaTaskDialogCommonButtons.Ok;
            break;
          case TaskDialogButtons.Close:
            vtd.CommonButtons = VistaTaskDialogCommonButtons.Close;
            break;
          case TaskDialogButtons.Cancel:
            vtd.CommonButtons = VistaTaskDialogCommonButtons.Cancel;
            break;
          default:
            vtd.CommonButtons = 0;
            break;
        }

        switch ( MainIcon ) {
          case SysIcons.Information:
            vtd.MainIcon = VistaTaskDialogIcon.Information;
            break;
          case SysIcons.Question:
            vtd.MainIcon = VistaTaskDialogIcon.Information;
            break;
          case SysIcons.Warning:
            vtd.MainIcon = VistaTaskDialogIcon.Warning;
            break;
          case SysIcons.Error:
            vtd.MainIcon = VistaTaskDialogIcon.Error;
            break;
        }

        switch ( FooterIcon ) {
          case SysIcons.Information:
            vtd.FooterIcon = VistaTaskDialogIcon.Information;
            break;
          case SysIcons.Question:
            vtd.FooterIcon = VistaTaskDialogIcon.Information;
            break;
          case SysIcons.Warning:
            vtd.FooterIcon = VistaTaskDialogIcon.Warning;
            break;
          case SysIcons.Error:
            vtd.FooterIcon = VistaTaskDialogIcon.Error;
            break;
        }

        vtd.EnableHyperlinks = false;
        vtd.ShowProgressBar = false;
        vtd.AllowDialogCancellation = ( Buttons == TaskDialogButtons.Cancel ||
                                       Buttons == TaskDialogButtons.Close ||
                                       Buttons == TaskDialogButtons.OKCancel ||
                                       Buttons == TaskDialogButtons.YesNoCancel );
        vtd.CallbackTimer = false;
        vtd.ExpandedByDefault = false;
        vtd.ExpandFooterArea = false;
        vtd.PositionRelativeToWindow = true;
        vtd.RightToLeftLayout = false;
        vtd.NoDefaultRadioButton = false;
        vtd.CanBeMinimized = false;
        vtd.ShowMarqueeProgressBar = false;
        vtd.UseCommandLinks = ( CommandButtons != "" );
        vtd.UseCommandLinksNoIcon = false;
        vtd.VerificationText = VerificationText;
        vtd.VerificationFlagChecked = false;
        vtd.ExpandedControlText = "Hide details";
        vtd.CollapsedControlText = "Show details";
        vtd.Callback = null;

        // Show the Dialog
        DialogResult result = ( DialogResult ) vtd.Show ( ( vtd.CanBeMinimized ? null : Form.ActiveForm ), out VerificationChecked, out RadioButtonResult );

        // if a command button was clicked, then change return result
        // to "DialogResult.OK" and set the CommandButtonResult
        if ( ( int ) result >= 2000 ) {
          CommandButtonResult = ( ( int ) result - 2000 );
          result = DialogResult.OK;
        }
        if ( RadioButtonResult >= 1000 )
          RadioButtonResult -= 1000;  // deduct the ButtonID start value for radio buttons

        return result;
      } else {
        // [OPTION 2] Show Emulated Form
        TaskDialogForm td = new TaskDialogForm ( );
        td.Title = Title;
        td.MainInstruction = MainInstruction;
        td.Content = Content;
        td.ExpandedInfo = ExpandedInfo;
        td.Footer = Footer;
        td.RadioButtons = RadioButtons;
        td.CommandButtons = CommandButtons;
        td.Buttons = Buttons;
        td.MainIcon = MainIcon;
        td.FooterIcon = FooterIcon;
        td.VerificationText = VerificationText;
        td.Width = EmulatedFormWidth;
        td.BuildForm ( );
				
        DialogResult result = td.ShowDialog ( );

        RadioButtonResult = td.RadioButtonIndex;
        CommandButtonResult = td.CommandButtonClickedIndex;
        VerificationChecked = td.VerificationCheckBoxChecked;
        return result;
      }
    }
    #endregion

    #region MessageBox
    /// <summary>
    /// Messages the box.
    /// </summary>
    /// <param name="Title">The title.</param>
    /// <param name="MainInstruction">The main instruction.</param>
    /// <param name="Content">The content.</param>
    /// <param name="ExpandedInfo">The expanded info.</param>
    /// <param name="Footer">The footer.</param>
    /// <param name="VerificationText">The verification text.</param>
    /// <param name="Buttons">The buttons.</param>
    /// <param name="MainIcon">The main icon.</param>
    /// <param name="FooterIcon">The footer icon.</param>
    /// <returns></returns>
    static public DialogResult MessageBox ( string Title,
                                          string MainInstruction,
                                          string Content,
                                          string ExpandedInfo,
                                          string Footer,
                                          string VerificationText,
                                          TaskDialogButtons Buttons,
                                          SysIcons MainIcon,
                                          SysIcons FooterIcon ) {
      return ShowTaskDialogBox ( Title, MainInstruction, Content, ExpandedInfo, Footer, VerificationText,
                               "", "", Buttons, MainIcon, FooterIcon );
    }

    /// <summary>
    /// Messages the box.
    /// </summary>
    /// <param name="Title">The title.</param>
    /// <param name="MainInstruction">The main instruction.</param>
    /// <param name="Content">The content.</param>
    /// <param name="Buttons">The buttons.</param>
    /// <param name="MainIcon">The main icon.</param>
    /// <returns></returns>
    static public DialogResult MessageBox ( string Title,
                                          string MainInstruction,
                                          string Content,
                                          TaskDialogButtons Buttons,
                                          SysIcons MainIcon ) {
      return MessageBox ( Title, MainInstruction, Content, "", "", "", Buttons, MainIcon, SysIcons.Information );
    }
    #endregion

    #region ShowRadioBox
    /// <summary>
    /// Shows the radio box.
    /// </summary>
    /// <param name="Title">The title.</param>
    /// <param name="MainInstruction">The main instruction.</param>
    /// <param name="Content">The content.</param>
    /// <param name="ExpandedInfo">The expanded info.</param>
    /// <param name="Footer">The footer.</param>
    /// <param name="VerificationText">The verification text.</param>
    /// <param name="RadioButtons">The radio buttons.</param>
    /// <param name="MainIcon">The main icon.</param>
    /// <param name="FooterIcon">The footer icon.</param>
    /// <returns></returns>
    static public int ShowRadioBox ( string Title,
                                   string MainInstruction,
                                   string Content,
                                   string ExpandedInfo,
                                   string Footer,
                                   string VerificationText,
                                   string RadioButtons,
                                   SysIcons MainIcon,
                                   SysIcons FooterIcon ) {
      DialogResult res = ShowTaskDialogBox ( Title, MainInstruction, Content, ExpandedInfo, Footer, VerificationText,
                                           RadioButtons, "", TaskDialogButtons.OKCancel,
                                           MainIcon, FooterIcon );
      if ( res == DialogResult.OK )
        return RadioButtonResult;
      else
        return -1;
    }

    /// <summary>
    /// Shows the radio box.
    /// </summary>
    /// <param name="Title">The title.</param>
    /// <param name="MainInstruction">The main instruction.</param>
    /// <param name="Content">The content.</param>
    /// <param name="RadioButtons">The radio buttons.</param>
    /// <returns></returns>
    static public int ShowRadioBox ( string Title,
                                   string MainInstruction,
                                   string Content,
                                   string RadioButtons ) {
      return ShowRadioBox ( Title, MainInstruction, Content, "", "", "", RadioButtons, SysIcons.Question, SysIcons.Information );
    }
    #endregion

    #region ShowCommandBox
    /// <summary>
    /// Shows the command box.
    /// </summary>
    /// <param name="Title">The title.</param>
    /// <param name="MainInstruction">The main instruction.</param>
    /// <param name="Content">The content.</param>
    /// <param name="ExpandedInfo">The expanded info.</param>
    /// <param name="Footer">The footer.</param>
    /// <param name="VerificationText">The verification text.</param>
    /// <param name="CommandButtons">The command buttons.</param>
    /// <param name="ShowCancelButton">if set to <c>true</c> [show cancel button].</param>
    /// <param name="MainIcon">The main icon.</param>
    /// <param name="FooterIcon">The footer icon.</param>
    /// <returns></returns>
    static public int ShowCommandBox ( string Title,
                                     string MainInstruction,
                                     string Content,
                                     string ExpandedInfo,
                                     string Footer,
                                     string VerificationText,
                                     string CommandButtons,
                                     bool ShowCancelButton,
                                     SysIcons MainIcon,
                                     SysIcons FooterIcon ) {
      DialogResult res = ShowTaskDialogBox ( Title, MainInstruction, Content, ExpandedInfo, Footer, VerificationText,
                                           "", CommandButtons, ( ShowCancelButton ? TaskDialogButtons.Cancel : TaskDialogButtons.None ),
                                           MainIcon, FooterIcon );
      if ( res == DialogResult.OK )
        return CommandButtonResult;
      else
        return -1;
    }

    /// <summary>
    /// Shows the command box.
    /// </summary>
    /// <param name="Title">The title.</param>
    /// <param name="MainInstruction">The main instruction.</param>
    /// <param name="Content">The content.</param>
    /// <param name="CommandButtons">The command buttons.</param>
    /// <param name="ShowCancelButton">if set to <c>true</c> [show cancel button].</param>
    /// <returns></returns>
    static public int ShowCommandBox ( string Title, string MainInstruction, string Content, string CommandButtons, bool ShowCancelButton ) {
      return ShowCommandBox ( Title, MainInstruction, Content, "", "", "", CommandButtons, ShowCancelButton,
                            SysIcons.Question, SysIcons.Information );
    }

    #endregion
  }
}

