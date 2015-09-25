using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.Windows.Forms;

namespace DroidExplorer.Controls {

  /// <summary>
  /// 
  /// </summary>
  public partial class CommandButton : Button {
    enum ButtonState { Normal, MouseOver, Down }
    ButtonState m_State = ButtonState.Normal;

    Image imgArrow1 = null;
    Image imgArrow2 = null;

    /// <summary>
    /// </summary>
    /// <value></value>
    /// <returns>The text associated with this control.</returns>
    public override string Text { get { return base.Text; } set { base.Text = value; this.Invalidate ( ); } }

    Font _smallFont;
    /// <summary>
    /// Gets or sets the small font.
    /// </summary>
    /// <value>The small font.</value>
    public Font SmallFont { get { return _smallFont; } set { _smallFont = value; } }

    bool _autoHeight = true;
    /// <summary>
    /// Gets or sets a value indicating whether [auto height].
    /// </summary>
    /// <value><c>true</c> if [auto height]; otherwise, <c>false</c>.</value>
    public bool AutoHeight { get { return _autoHeight; } set { _autoHeight = value; if ( _autoHeight ) this.Invalidate ( ); } }

    /// <summary>
    /// Initializes a new instance of the <see cref="CommandButton"/> class.
    /// </summary>
    public CommandButton ( ) {
      InitializeComponent ( );
      base.Font = new Font ( "Arial", 11.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ( byte ) 0 );
      _smallFont = new Font ( "Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ( byte ) 0 );
    }

    /// <summary>
    /// Raises the <see cref="M:System.Windows.Forms.Control.CreateControl"/> method.
    /// </summary>
    protected override void OnCreateControl ( ) {
      base.OnCreateControl ( );
      //imgArrow1 = new Bitmap ( this.GetType ( ), "green_arrow1.png" );
      //imgArrow2 = new Bitmap ( this.GetType ( ), "green_arrow2.png" );
			imgArrow1 = DroidExplorer.Resources.Images.green_arrow1;
			imgArrow2 = DroidExplorer.Resources.Images.green_arrow2;
    }

    const int LEFT_MARGIN = 10;
    const int TOP_MARGIN = 10;
    const int ARROW_WIDTH = 19;

    /// <summary>
    /// Gets the large text.
    /// </summary>
    /// <returns></returns>
    string GetLargeText ( ) {
      string[ ] lines = this.Text.Split ( new char[ ] { '\n' } );
      return lines[ 0 ];
    }

    /// <summary>
    /// Gets the small text.
    /// </summary>
    /// <returns></returns>
    string GetSmallText ( ) {
      if ( this.Text.IndexOf ( '\n' ) < 0 )
        return "";

      string s = this.Text;
      string[ ] lines = s.Split ( new char[ ] { '\n' } );
      s = "";
      for ( int i = 1; i < lines.Length; i++ )
        s += lines[ i ] + "\n";
      return s.Trim ( new char[ ] { '\n' } );
    }

    /// <summary>
    /// Gets the large text size F.
    /// </summary>
    /// <returns></returns>
    SizeF GetLargeTextSizeF ( ) {
      int x = LEFT_MARGIN + ARROW_WIDTH + 5;
      SizeF mzSize = new SizeF ( this.Width - x - LEFT_MARGIN, 5000.0F );  // presume RIGHT_MARGIN = LEFT_MARGIN
      Graphics g = Graphics.FromHwnd ( this.Handle );
      SizeF textSize = g.MeasureString ( GetLargeText ( ), base.Font, mzSize );
      return textSize;
    }

    /// <summary>
    /// Gets the small text size F.
    /// </summary>
    /// <returns></returns>
    SizeF GetSmallTextSizeF ( ) {
      string s = GetSmallText ( );
      if ( s == "" )
        return new SizeF ( 0, 0 );
      int x = LEFT_MARGIN + ARROW_WIDTH + 8; // <- indent small text slightly more
      SizeF mzSize = new SizeF ( this.Width - x - LEFT_MARGIN, 5000.0F );  // presume RIGHT_MARGIN = LEFT_MARGIN
      Graphics g = Graphics.FromHwnd ( this.Handle );
      SizeF textSize = g.MeasureString ( s, _smallFont, mzSize );
      return textSize;
    }

    /// <summary>
    /// Gets the height of the best.
    /// </summary>
    /// <returns></returns>
    public int GetBestHeight ( ) {
      //return 40;
      return ( TOP_MARGIN * 2 ) + ( int ) GetSmallTextSizeF ( ).Height + ( int ) GetLargeTextSizeF ( ).Height;
    }

    /// <summary>
    /// Raises the <see cref="E:System.Windows.Forms.Control.Paint"/> event.
    /// </summary>
    /// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs"/> that contains the event data.</param>
    protected override void OnPaint ( PaintEventArgs e ) {
      e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
      e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

      LinearGradientBrush brush;
      LinearGradientMode mode = LinearGradientMode.Vertical;

      Rectangle newRect = new Rectangle ( ClientRectangle.X, ClientRectangle.Y, ClientRectangle.Width - 1, ClientRectangle.Height - 1 );
      Color text_color = SystemColors.WindowText;

      Image img = imgArrow1;

      if ( Enabled ) {
        switch ( m_State ) {
          case ButtonState.Normal:
            e.Graphics.FillRectangle ( Brushes.White, newRect );
            if ( base.Focused )
              e.Graphics.DrawRectangle ( new Pen ( Color.SkyBlue, 1 ), newRect );
            else
              e.Graphics.DrawRectangle ( new Pen ( Color.White, 1 ), newRect );
            text_color = Color.DarkBlue;
            break;

          case ButtonState.MouseOver:
            brush = new LinearGradientBrush ( newRect, Color.White, Color.WhiteSmoke, mode );
            e.Graphics.FillRectangle ( brush, newRect );
            e.Graphics.DrawRectangle ( new Pen ( Color.Silver, 1 ), newRect );
            img = imgArrow2;
            text_color = Color.Blue;
            break;

          case ButtonState.Down:
            brush = new LinearGradientBrush ( newRect, Color.WhiteSmoke, Color.White, mode );
            e.Graphics.FillRectangle ( brush, newRect );
            e.Graphics.DrawRectangle ( new Pen ( Color.DarkGray, 1 ), newRect );
            text_color = Color.DarkBlue;
            break;
        }
      } else {
        brush = new LinearGradientBrush ( newRect, Color.WhiteSmoke, Color.Gainsboro, mode );
        e.Graphics.FillRectangle ( brush, newRect );
        e.Graphics.DrawRectangle ( new Pen ( Color.DarkGray, 1 ), newRect );
        text_color = Color.DarkBlue;
      }


      string largetext = this.GetLargeText ( );
      string smalltext = this.GetSmallText ( );

      SizeF szL = GetLargeTextSizeF ( );
      e.Graphics.DrawString ( largetext, base.Font, new SolidBrush ( text_color ), new RectangleF ( new PointF ( LEFT_MARGIN + imgArrow1.Width + 5, TOP_MARGIN ), szL ) );

      if ( smalltext != "" ) {
        SizeF szS = GetSmallTextSizeF ( );
        e.Graphics.DrawString ( smalltext, _smallFont, new SolidBrush ( text_color ), new RectangleF ( new PointF ( LEFT_MARGIN + imgArrow1.Width + 8, TOP_MARGIN + ( int ) szL.Height ), szS ) );
      }

      e.Graphics.DrawImage ( img, new Point ( LEFT_MARGIN, TOP_MARGIN + ( int ) ( szL.Height / 2 ) - ( int ) ( img.Height / 2 ) ) );
    }

    /// <summary>
    /// </summary>
    /// <param name="e"></param>
    protected override void OnMouseLeave ( System.EventArgs e ) {
      m_State = ButtonState.Normal;
      this.Invalidate ( );
      base.OnMouseLeave ( e );
    }

    /// <summary>
    /// </summary>
    /// <param name="e"></param>
    protected override void OnMouseEnter ( System.EventArgs e ) {
      m_State = ButtonState.MouseOver;
      this.Invalidate ( );
      base.OnMouseEnter ( e );
    }

    /// <summary>
    /// Raises the <see cref="E:System.Windows.Forms.Control.MouseUp"/> event.
    /// </summary>
    /// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs"/> that contains the event data.</param>
    protected override void OnMouseUp ( System.Windows.Forms.MouseEventArgs e ) {
      m_State = ButtonState.MouseOver;
      this.Invalidate ( );
      base.OnMouseUp ( e );
    }

    /// <summary>
    /// Raises the <see cref="E:System.Windows.Forms.Control.MouseDown"/> event.
    /// </summary>
    /// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs"/> that contains the event data.</param>
    protected override void OnMouseDown ( System.Windows.Forms.MouseEventArgs e ) {
      m_State = ButtonState.Down;
      this.Invalidate ( );
      base.OnMouseDown ( e );
    }

    /// <summary>
    /// Raises the <see cref="E:System.Windows.Forms.Control.SizeChanged"/> event.
    /// </summary>
    /// <param name="e">An <see cref="T:System.EventArgs"/> that contains the event data.</param>
    protected override void OnSizeChanged ( EventArgs e ) {
      if ( _autoHeight ) {
        int h = GetBestHeight ( );
        if ( this.Height != h ) {
          this.Height = h;
          return;
        }
      }
      base.OnSizeChanged ( e );
    }
  }
}
